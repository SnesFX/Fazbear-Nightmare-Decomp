using System;
using System.Collections.Generic;
using System.Threading;
using RAIN.Navigation.NavMesh.Collectors;
using RAIN.Navigation.NavMesh.RecastNodes;
using RAIN.Utility;
using UnityEngine;

namespace RAIN.Navigation.NavMesh.RecastProcess
{
	public class ContourCreator
	{
		private LayerMask _includedLayers;

		private List<string> _ignoredTags;

		private List<string> _unwalkableTags;

		private float _maxSlope;

		private float _cellSize;

		private int _sectionSize;

		private int _borderSize;

		private int _walkableHeight;

		private int _walkableRadius;

		private int _unwalkableRadius;

		private int _stepHeight;

		private int _minRegionArea;

		private int _mergeRegionArea;

		private float _maxVertexError;

		private float _maxSegmentLength;

		private PointBounds _gridBounds;

		private int _gridWidth;

		private int _gridLength;

		private List<ColliderCollector> _collectors;

		private ContourPoly _poly;

		private Queue<Point2> _colliderRecasting;

		private Queue<Point2> _waitingForData;

		private List<SpanMesh>[,] _spanMeshes;

		private bool[,] _colliderRecasted;

		private List<Point2> _contourTriangulating;

		private List<Point2> _contourMeshesReady;

		private float[,] _gridProgress;

		private int _gridCompleted;

		private bool _canceled;

		private List<Thread> _contourThreads;

		public float Progress
		{
			get
			{
				float num = 0f;
				for (int i = 0; i < _gridProgress.GetLength(0); i++)
				{
					for (int j = 0; j < _gridProgress.GetLength(1); j++)
					{
						num += _gridProgress[i, j];
					}
				}
				float num2 = _gridWidth * _gridLength;
				return num / num2;
			}
		}

		public bool Complete
		{
			get
			{
				return _gridCompleted == _gridWidth * _gridLength;
			}
		}

		public bool Asynchronous
		{
			get
			{
				return _contourThreads.Count > 0;
			}
		}

		public ContourCreator(Bounds aBounds, LayerMask aIncludedLayers, List<string> aIgnoredTags, List<string> aUnwalkableTags, float aMaxSlope, float aCellSize, int aGridSize, float aBorderSize, float aWalkableHeight, float aWalkableRadius, float aUnwalkableRadius, float aStepHeight, float aMinRegionArea, float aMergeRegionArea, float aMaxVertexError, float aMaxSegmentLength, int aThreadCount)
		{
			if (aBounds.extents.x <= 0f || aBounds.extents.z <= 0f)
			{
				throw new Exception("aBounds x and z extents must be positive");
			}
			_includedLayers = aIncludedLayers;
			_ignoredTags = aIgnoredTags;
			_unwalkableTags = aUnwalkableTags;
			_maxSlope = aMaxSlope;
			_cellSize = aCellSize;
			_sectionSize = Mathf.Max(Mathf.CeilToInt(aBounds.size.x / ((float)aGridSize * _cellSize)), Mathf.CeilToInt(aBounds.size.z / ((float)aGridSize * _cellSize)));
			_borderSize = Mathf.Max(2, Mathf.RoundToInt(aBorderSize / _cellSize));
			_walkableHeight = Mathf.RoundToInt(aWalkableHeight / _cellSize);
			_walkableRadius = Mathf.RoundToInt(aWalkableRadius / _cellSize);
			_unwalkableRadius = Mathf.RoundToInt(aUnwalkableRadius / _cellSize);
			_stepHeight = Mathf.RoundToInt(aStepHeight / _cellSize);
			_minRegionArea = Mathf.RoundToInt(aMinRegionArea / _cellSize);
			_mergeRegionArea = Mathf.RoundToInt(aMergeRegionArea / _cellSize);
			_maxVertexError = aMaxVertexError;
			_maxSegmentLength = aMaxSegmentLength;
			_gridBounds = new PointBounds(Point3.RoundToPoint(aBounds.center / _cellSize), Point3.RoundToPoint(aBounds.extents / _cellSize));
			_gridWidth = Mathf.CeilToInt((float)_gridBounds.Extents.X * 2f / (float)_sectionSize);
			if (_gridWidth == 0)
			{
				_gridWidth++;
			}
			_gridLength = Mathf.CeilToInt((float)_gridBounds.Extents.Z * 2f / (float)_sectionSize);
			if (_gridLength == 0)
			{
				_gridLength++;
			}
			_collectors = new List<ColliderCollector>();
			List<Type> allClassSubclassTypes = ReflectionUtils.GetAllClassSubclassTypes(typeof(ColliderCollector));
			for (int i = 0; i < allClassSubclassTypes.Count; i++)
			{
				ColliderCollector colliderCollector = (ColliderCollector)Activator.CreateInstance(allClassSubclassTypes[i]);
				colliderCollector.InitCollector(_cellSize, _maxSlope, _unwalkableTags, _ignoredTags, _includedLayers);
				_collectors.Add(colliderCollector);
			}
			_poly = new ContourPoly(_cellSize, _gridWidth, _gridLength);
			_spanMeshes = new List<SpanMesh>[_gridWidth, _gridLength];
			_colliderRecasting = new Queue<Point2>(_gridWidth * _gridLength);
			_waitingForData = new Queue<Point2>(_gridWidth * _gridLength);
			_colliderRecasted = new bool[_gridWidth, _gridLength];
			_contourTriangulating = new List<Point2>(_gridWidth * _gridLength);
			_contourMeshesReady = new List<Point2>();
			_gridProgress = new float[_gridWidth, _gridLength];
			_canceled = false;
			_contourThreads = new List<Thread>(aThreadCount);
			for (int j = 0; j < _gridLength; j++)
			{
				for (int k = 0; k < _gridWidth; k++)
				{
					_colliderRecasting.Enqueue(new Point2(k, j));
				}
			}
			for (int l = 0; l < aThreadCount; l++)
			{
				_contourThreads.Add(new Thread(ProcessWorker));
				_contourThreads[l].Start();
			}
		}

		public bool CreateContours(out List<ContourMeshData> aMeshes)
		{
			SimpleProfiler profiler = SimpleProfiler.GetProfiler("RAINNavMeshThreads");
			aMeshes = new List<ContourMeshData>();
			if (_contourThreads.Count == 0)
			{
				if (_colliderRecasting.Count > 0)
				{
					Point2 point = _colliderRecasting.Dequeue();
					RecastGrid(point, GetGridMeshes(point), profiler);
					_colliderRecasted[point.X, point.Y] = true;
					_contourTriangulating.Add(point);
				}
				for (int i = 0; i < _contourTriangulating.Count; i++)
				{
					if (CanTriangulateGrid(_contourTriangulating[i]))
					{
						TriangulateGrid(_contourTriangulating[i], profiler);
						aMeshes.Add(_poly.GetContourMesh(_contourTriangulating[i]));
						_gridCompleted++;
						_contourTriangulating.RemoveAt(i--);
					}
				}
				if (_colliderRecasting.Count <= 0)
				{
					return _contourTriangulating.Count > 0;
				}
				return true;
			}
			while (true)
			{
				Point2 point2 = null;
				lock (_waitingForData)
				{
					if (_waitingForData.Count > 0)
					{
						point2 = _waitingForData.Dequeue();
					}
				}
				if (point2 == null)
				{
					break;
				}
				List<SpanMesh> gridMeshes = GetGridMeshes(point2);
				lock (_spanMeshes)
				{
					_spanMeshes[point2.X, point2.Y] = gridMeshes;
				}
			}
			while (true)
			{
				List<Point2> list = new List<Point2>();
				lock (_contourMeshesReady)
				{
					list.AddRange(_contourMeshesReady);
					_contourMeshesReady.Clear();
				}
				if (list.Count == 0)
				{
					break;
				}
				for (int j = 0; j < list.Count; j++)
				{
					lock (_poly)
					{
						aMeshes.Add(_poly.GetContourMesh(list[j]));
						_gridCompleted++;
					}
				}
			}
			return !Complete;
		}

		public void CancelCreatingContours()
		{
			_canceled = true;
			for (int i = 0; i < _contourThreads.Count; i++)
			{
				_contourThreads[i].Join();
			}
			_contourThreads.Clear();
			GC.Collect();
		}

		public void FinishCreation()
		{
		}

		private void ProcessWorker()
		{
			SimpleProfiler profiler = SimpleProfiler.GetProfiler("RAINNavMeshThreads");
			while (!_canceled)
			{
				Point2 point = null;
				lock (_contourTriangulating)
				{
					for (int i = 0; i < _contourTriangulating.Count; i++)
					{
						if (CanTriangulateGrid(_contourTriangulating[i]))
						{
							point = _contourTriangulating[i];
							_contourTriangulating.RemoveAt(i);
							break;
						}
					}
				}
				if (point != null)
				{
					try
					{
						TriangulateGrid(point, profiler);
						lock (_contourMeshesReady)
						{
							_contourMeshesReady.Add(point);
						}
					}
					catch (Exception ex)
					{
						Debug.LogWarning(ex.Message + "\n" + ex.StackTrace);
					}
					continue;
				}
				if (_canceled)
				{
					break;
				}
				lock (_colliderRecasting)
				{
					if (_colliderRecasting.Count > 0)
					{
						point = _colliderRecasting.Dequeue();
					}
				}
				if (point != null)
				{
					try
					{
						lock (_waitingForData)
						{
							_waitingForData.Enqueue(point);
						}
						List<SpanMesh> list;
						while (true)
						{
							if (_canceled)
							{
								return;
							}
							lock (_spanMeshes)
							{
								list = _spanMeshes[point.X, point.Y];
								_spanMeshes[point.X, point.Y] = null;
							}
							if (list != null)
							{
								break;
							}
							Thread.Sleep(10);
						}
						RecastGrid(point, list, profiler);
						lock (_contourTriangulating)
						{
							_colliderRecasted[point.X, point.Y] = true;
							_contourTriangulating.Add(point);
						}
					}
					catch (Exception ex2)
					{
						Debug.LogWarning(ex2.Message + "\n" + ex2.StackTrace);
					}
				}
				else
				{
					if (_colliderRecasting.Count == 0 && _contourTriangulating.Count == 0)
					{
						break;
					}
					Thread.Sleep(10);
				}
			}
		}

		private void RecastGrid(Point2 aGridSquare, List<SpanMesh> aSpanMeshes, SimpleProfiler aProfiler)
		{
			if (_canceled)
			{
				return;
			}
			Point3 point = new Point3(_gridBounds.Center);
			point.X -= _sectionSize * _gridWidth / 2;
			point.Y -= _gridBounds.Extents.Y;
			point.Z -= _sectionSize * _gridLength / 2;
			int num = _walkableRadius + _borderSize;
			Point3 point2 = point + new Point3(_sectionSize * aGridSquare.X - num, 0, _sectionSize * aGridSquare.Y - num);
			Point3 point3 = point2 + new Point3(_sectionSize + num * 2, _gridBounds.Extents.Y * 2, _sectionSize + num * 2);
			point2.X = Mathf.Max(point2.X, _gridBounds.Min.X - num);
			point2.Z = Mathf.Max(point2.Z, _gridBounds.Min.Z - num);
			point3.X = Mathf.Min(point3.X, _gridBounds.Max.X + num);
			point3.Z = Mathf.Min(point3.Z, _gridBounds.Max.Z + num);
			SpanGrid spanGrid = new SpanGrid(point2, point3, _cellSize);
			for (int i = 0; i < aSpanMeshes.Count; i++)
			{
				spanGrid.AddSpanMesh(aSpanMeshes[i], aProfiler);
				_gridProgress[aGridSquare.X, aGridSquare.Y] = 0.25f * (float)(i + 1) / (float)aSpanMeshes.Count;
			}
			_gridProgress[aGridSquare.X, aGridSquare.Y] = 0.25f;
			if (_canceled)
			{
				return;
			}
			spanGrid.MergeSpans(_walkableHeight);
			spanGrid.RemoveUnwalkables();
			if (_canceled)
			{
				return;
			}
			CompactSpanGrid compactSpanGrid = new CompactSpanGrid(spanGrid);
			spanGrid = null;
			if (_canceled)
			{
				return;
			}
			compactSpanGrid.FindNeighbors(_walkableHeight, _stepHeight);
			if (_canceled)
			{
				return;
			}
			int num2 = compactSpanGrid.CalculateDistanceField();
			num2 += 2;
			if (_canceled)
			{
				return;
			}
			compactSpanGrid.MarkBorderRegions(_walkableRadius + _borderSize);
			num2 += 2;
			int num6 = num2 / 2;
			int num3 = 0;
			int num4 = _walkableRadius * 2;
			int num5 = num2 - num4;
			while (num2 > num4)
			{
				if (_canceled)
				{
					return;
				}
				num2 = Math.Max(num4, num2 - 2);
				num3 = Math.Min(num5, num3 + 2);
				compactSpanGrid.ExpandRegions((ushort)num2, 8);
				if (_canceled)
				{
					return;
				}
				compactSpanGrid.FloodGrid((ushort)num2);
				_gridProgress[aGridSquare.X, aGridSquare.Y] = 0.25f + 0.7f * (float)num3 / (float)num5;
			}
			if (_canceled)
			{
				return;
			}
			compactSpanGrid.ExpandRegions((ushort)num2, -1);
			_gridProgress[aGridSquare.X, aGridSquare.Y] = 0.95f;
			if (_canceled)
			{
				return;
			}
			ContourPoly contourPoly = compactSpanGrid.WalkContours(aGridSquare, _cellSize);
			compactSpanGrid = null;
			contourPoly.SimplifyContours(_maxVertexError, _maxSegmentLength);
			contourPoly.FixZeroOrNegativeContours();
			if (!_canceled)
			{
				lock (_poly)
				{
					_poly.MergeContourPoly(contourPoly);
				}
				_gridProgress[aGridSquare.X, aGridSquare.Y] = 0.99f;
			}
		}

		private bool CanTriangulateGrid(Point2 aGridSquare)
		{
			if ((aGridSquare.X == 0 || _colliderRecasted[aGridSquare.X - 1, aGridSquare.Y]) && (aGridSquare.X == _gridWidth - 1 || _colliderRecasted[aGridSquare.X + 1, aGridSquare.Y]) && (aGridSquare.Y == 0 || _colliderRecasted[aGridSquare.X, aGridSquare.Y - 1]))
			{
				if (aGridSquare.Y != _gridLength - 1)
				{
					return _colliderRecasted[aGridSquare.X, aGridSquare.Y + 1];
				}
				return true;
			}
			return false;
		}

		private void TriangulateGrid(Point2 aGridSquare, SimpleProfiler aProfiler)
		{
			if (!_canceled)
			{
				lock (_poly)
				{
					_poly.TriangulatePoly(aGridSquare, _maxVertexError, _maxSegmentLength);
				}
				_gridProgress[aGridSquare.X, aGridSquare.Y] = 1f;
			}
		}

		private List<SpanMesh> GetGridMeshes(Point2 aGridSquare)
		{
			List<SpanMesh> list = new List<SpanMesh>();
			Bounds gridBounds = GetGridBounds(aGridSquare);
			for (int i = 0; i < _collectors.Count; i++)
			{
				List<SpanMesh> aSpanMeshes;
				_collectors[i].CollectColliders(gridBounds, 0, _collectors[i].ColliderCount, out aSpanMeshes);
				list.AddRange(aSpanMeshes);
			}
			return list;
		}

		private Bounds GetGridBounds(Point2 aGridSquare)
		{
			Point3 point = new Point3(_gridBounds.Center);
			point.X -= _sectionSize * _gridWidth / 2;
			point.Y -= _gridBounds.Extents.Y;
			point.Z -= _sectionSize * _gridLength / 2;
			int num = _walkableRadius + _borderSize;
			Point3 point2 = point + new Point3(_sectionSize * aGridSquare.X - num, 0, _sectionSize * aGridSquare.Y - num);
			Point3 point3 = point2 + new Point3(_sectionSize + num * 2, _gridBounds.Extents.Y * 2, _sectionSize + num * 2);
			point2.X = Mathf.Max(point2.X, _gridBounds.Min.X - num);
			point2.Z = Mathf.Max(point2.Z, _gridBounds.Min.Z - num);
			point3.X = Mathf.Min(point3.X, _gridBounds.Max.X + num);
			point3.Z = Mathf.Min(point3.Z, _gridBounds.Max.Z + num);
			return new Bounds((Vector3)(point2 + point3) * 0.5f * _cellSize, (Vector3)(point3 - point2) * _cellSize);
		}
	}
}
