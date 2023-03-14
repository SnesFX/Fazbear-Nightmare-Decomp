using System;
using System.Collections.Generic;
using RAIN.Navigation.NavMesh.Collectors;
using RAIN.Utility;
using UnityEngine;

namespace RAIN.Navigation.NavMesh.RecastNodes
{
	public class SpanGrid
	{
		private Point3 _minPoint;

		private Point3 _maxPoint;

		private int _width;

		private int _length;

		private float _cellSize;

		private SpanColumn[,] _grid;

		private int _spanCount;

		private SimpleProfiler _profiler;

		public int Width
		{
			get
			{
				return _width;
			}
		}

		public int Length
		{
			get
			{
				return _length;
			}
		}

		public int Height
		{
			get
			{
				return _maxPoint.Y - _minPoint.Y;
			}
		}

		public Point3 MinPoint
		{
			get
			{
				return _minPoint;
			}
		}

		public Point3 MaxPoint
		{
			get
			{
				return _maxPoint;
			}
		}

		public SpanColumn[,] Grid
		{
			get
			{
				return _grid;
			}
		}

		public int SpanCount
		{
			get
			{
				return _spanCount;
			}
		}

		public SpanGrid(Point3 aMin, Point3 aMax, float aCellSize)
		{
			_minPoint = aMin;
			_maxPoint = aMax;
			_cellSize = aCellSize;
			if (_maxPoint.X - _minPoint.X <= 0)
			{
				throw new Exception("aBounds must have an X length larger than 0");
			}
			if (_maxPoint.Z - _minPoint.Z == 0)
			{
				throw new Exception("aBounds must have a Z length larger than 0");
			}
			Point3 point = _maxPoint - _minPoint;
			_width = point.X;
			_length = point.Z;
			_grid = new SpanColumn[_width, _length];
			for (int i = 0; i < _width; i++)
			{
				for (int j = 0; j < _length; j++)
				{
					_grid[i, j] = new SpanColumn();
				}
			}
			_spanCount = 0;
		}

		public void AddSpanMesh(SpanMesh aSpanMesh, SimpleProfiler aProfiler)
		{
			_profiler = aProfiler;
			aSpanMesh.Profiler = aProfiler;
			int num = Mathf.Max(aSpanMesh.Origin.X, _minPoint.X);
			int num2 = Mathf.Max(aSpanMesh.Origin.Z, _minPoint.Z);
			int num3 = Mathf.Min(_maxPoint.X, aSpanMesh.Origin.X + aSpanMesh.Width);
			int num4 = Mathf.Min(_maxPoint.Z, aSpanMesh.Origin.Z + aSpanMesh.Length);
			List<Span>[] allSpans = aSpanMesh.GetAllSpans(num - aSpanMesh.Origin.X, num2 - aSpanMesh.Origin.Z, num3 - aSpanMesh.Origin.X, num4 - aSpanMesh.Origin.Z);
			for (int i = num; i < num3; i++)
			{
				for (int j = num2; j < num4; j++)
				{
					int num5 = (j - num2) * (num3 - num) + (i - num);
					if (allSpans[num5] == null)
					{
						continue;
					}
					for (int k = 0; k < allSpans[num5].Count; k++)
					{
						if (allSpans[num5][k].VolumeID > 0)
						{
							AddSpan(i - _minPoint.X, j - _minPoint.Z, allSpans[num5][k]);
						}
						else if (allSpans[num5][k].MinHeight <= _maxPoint.Y && allSpans[num5][k].MaxHeight >= _minPoint.Y)
						{
							AddSpan(i - _minPoint.X, j - _minPoint.Z, allSpans[num5][k]);
						}
					}
				}
			}
		}

		public void MergeSpans(int aWalkableHeight)
		{
			for (int i = 0; i < _width; i++)
			{
				for (int j = 0; j < _length; j++)
				{
					_spanCount -= _grid[i, j].MergeVolumes();
				}
			}
			for (int k = 0; k < _width; k++)
			{
				for (int l = 0; l < _length; l++)
				{
					_spanCount -= _grid[k, l].Merge();
				}
			}
			for (int m = 0; m < _width; m++)
			{
				for (int n = 0; n < _length; n++)
				{
					_grid[m, n].MarkUnwalkableByHeight(aWalkableHeight);
				}
			}
		}

		public int RemoveUnwalkables()
		{
			int num = 0;
			for (int i = 0; i < _width; i++)
			{
				for (int j = 0; j < _length; j++)
				{
					for (int num2 = GetSpanCount(i, j) - 1; num2 >= 0; num2--)
					{
						Span span = GetSpan(i, j, num2);
						if (!span.Walkable)
						{
							_grid[i, j].RemoveSpan(num2);
							num++;
						}
					}
				}
			}
			_spanCount -= num;
			return num;
		}

		private void AddSpan(int aX, int aZ, Span aSpan)
		{
			_grid[aX, aZ].Add(aSpan);
			_spanCount++;
		}

		private Span GetSpan(int aX, int aZ, int aDepth)
		{
			return _grid[aX, aZ].GetSpan(aDepth);
		}

		private int GetSpanCount(int aX, int aZ)
		{
			return _grid[aX, aZ].GetSpanCount();
		}

		private bool WithinGrid(int aX, int aZ)
		{
			if (aX >= 0 && aZ >= 0 && aX < _width)
			{
				return aZ < _length;
			}
			return false;
		}
	}
}
