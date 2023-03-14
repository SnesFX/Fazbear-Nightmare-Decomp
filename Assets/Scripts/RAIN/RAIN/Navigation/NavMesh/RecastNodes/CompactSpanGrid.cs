using System;
using System.Collections.Generic;
using UnityEngine;

namespace RAIN.Navigation.NavMesh.RecastNodes
{
	public class CompactSpanGrid
	{
		private Point3 _minPoint;

		private Point3 _maxPoint;

		private int _width;

		private int _length;

		private int[] _spanIndex;

		private CompactSpan[] _spans;

		private ushort _regionID;

		private ushort _contourID;

		private List<int> _expandedList;

		private List<int> _expandedSpans;

		private List<ushort> _expandedRegions;

		private List<ushort> _expandedPriorities;

		private Stack<int> _floodStack;

		public CompactSpanGrid(SpanGrid aGrid)
		{
			_minPoint = aGrid.MinPoint;
			_maxPoint = aGrid.MaxPoint;
			_width = aGrid.Width;
			_length = aGrid.Length;
			_spanIndex = new int[_width * _length * 2];
			_spans = new CompactSpan[aGrid.SpanCount];
			int num = 0;
			for (int i = 0; i < _width; i++)
			{
				for (int j = 0; j < _length; j++)
				{
					List<Span> allSpans = aGrid.Grid[i, j].GetAllSpans();
					_spanIndex[(i * _length + j) * 2] = num;
					_spanIndex[(i * _length + j) * 2 + 1] = allSpans.Count;
					for (int k = 0; k < allSpans.Count; k++)
					{
						_spans[num++] = new CompactSpan(allSpans[k]);
					}
				}
			}
			_regionID = 1;
			_contourID = 1;
			_expandedList = new List<int>();
			_expandedSpans = new List<int>();
			_expandedRegions = new List<ushort>();
			_expandedPriorities = new List<ushort>();
			_floodStack = new Stack<int>();
		}

		public void FindNeighbors(int aWalkableHeight, int aStepHeight)
		{
			for (int i = 0; i < _width; i++)
			{
				for (int j = 0; j < _length; j++)
				{
					int num = _spanIndex[(i * _length + j) * 2];
					int num2 = _spanIndex[(i * _length + j) * 2 + 1];
					for (int k = 0; k < num2; k++)
					{
						int num3 = num + k;
						int val = _spans[num3].MaxHeight + aWalkableHeight + aStepHeight;
						if (k + 1 < num2)
						{
							val = _spans[num + k + 1].MinHeight;
						}
						for (byte b = 0; b < 4; b = (byte)(b + 1))
						{
							int num4 = i + CompactSpan.GetXOffset(b);
							int num5 = j + CompactSpan.GetZOffset(b);
							if (WithinGrid(num4, num5))
							{
								int num6 = _spanIndex[(num4 * _length + num5) * 2];
								int num7 = _spanIndex[(num4 * _length + num5) * 2 + 1];
								for (int l = 0; l < num7; l++)
								{
									int num8 = num6 + l;
									int val2 = _spans[num8].MaxHeight + aWalkableHeight + aStepHeight;
									if (l + 1 < num7)
									{
										val2 = _spans[num6 + l + 1].MinHeight;
									}
									int num9 = Math.Max(_spans[num3].MaxHeight, _spans[num8].MaxHeight);
									int num10 = Math.Min(val, val2);
									if (num10 - num9 >= aWalkableHeight && Math.Abs(_spans[num8].MaxHeight - _spans[num3].MaxHeight) <= aStepHeight)
									{
										_spans[num + k].SetNeighbor(num6 + l, b);
										break;
									}
								}
							}
						}
					}
				}
			}
		}

		public int CalculateDistanceField()
		{
			for (int i = 0; i < _spans.Length; i++)
			{
				if (_spans[i].HasExternalEdge())
				{
					_spans[i].SetDistance(0);
				}
				else
				{
					_spans[i].SetDistance(ushort.MaxValue);
				}
			}
			for (int j = 0; j < _spans.Length; j++)
			{
				int aDirection = 2;
				for (int k = 0; k < 2; k++)
				{
					if (_spans[j].HasNeighbor(aDirection))
					{
						int neighbor = _spans[j].GetNeighbor(aDirection);
						if (_spans[neighbor].Distance + 2 < _spans[j].Distance)
						{
							_spans[j].SetDistance((ushort)(_spans[neighbor].Distance + 2));
						}
						if (_spans[neighbor].HasNeighbor(CompactSpan.RotateClockwise(aDirection)))
						{
							int neighbor2 = _spans[neighbor].GetNeighbor(CompactSpan.RotateClockwise(aDirection));
							if (_spans[neighbor2].Distance + 3 < _spans[j].Distance)
							{
								_spans[j].SetDistance((ushort)(_spans[neighbor2].Distance + 3));
							}
						}
					}
					aDirection = CompactSpan.RotateClockwise(aDirection);
				}
			}
			for (int num = _spans.Length - 1; num >= 0; num--)
			{
				int aDirection2 = 0;
				for (int l = 0; l < 2; l++)
				{
					if (_spans[num].HasNeighbor(aDirection2))
					{
						int neighbor3 = _spans[num].GetNeighbor(aDirection2);
						if (_spans[neighbor3].Distance + 2 < _spans[num].Distance)
						{
							_spans[num].SetDistance((ushort)(_spans[neighbor3].Distance + 2));
						}
						if (_spans[neighbor3].HasNeighbor(CompactSpan.RotateClockwise(aDirection2)))
						{
							int neighbor4 = _spans[neighbor3].GetNeighbor(CompactSpan.RotateClockwise(aDirection2));
							if (_spans[neighbor4].Distance + 3 < _spans[num].Distance)
							{
								_spans[num].SetDistance((ushort)(_spans[neighbor4].Distance + 3));
							}
						}
					}
					aDirection2 = CompactSpan.RotateClockwise(0);
				}
			}
			int num2 = 0;
			for (int m = 0; m < _spans.Length; m++)
			{
				num2 = Math.Max(num2, _spans[m].Distance);
			}
			for (int n = 0; n < _spans.Length; n++)
			{
				ushort num3 = _spans[n].Distance;
				for (int num4 = 0; num4 < 4; num4++)
				{
					if (_spans[n].HasNeighbor(num4))
					{
						int neighbor5 = _spans[n].GetNeighbor(num4);
						num3 = (ushort)(num3 + _spans[neighbor5].Distance);
						if (_spans[neighbor5].HasNeighbor(CompactSpan.RotateClockwise(num4)))
						{
							int neighbor6 = _spans[neighbor5].GetNeighbor(CompactSpan.RotateClockwise(num4));
							num3 = (ushort)(num3 + _spans[neighbor6].Distance);
						}
						else
						{
							num3 = (ushort)(num3 + _spans[n].Distance);
						}
					}
					else
					{
						num3 = (ushort)(num3 + (ushort)(2 * _spans[n].Distance));
					}
				}
				_spans[n].SetDistance((ushort)((num3 + 5) / 9));
			}
			return num2;
		}

		public void MarkBorderRegions(int aBorderSize)
		{
			_regionID = 1;
			for (int i = 0; i < _width - aBorderSize; i++)
			{
				for (int j = 0; j < aBorderSize; j++)
				{
					int num = _spanIndex[(i * _length + j) * 2];
					int num2 = _spanIndex[(i * _length + j) * 2 + 1];
					for (int k = 0; k < num2; k++)
					{
						_spans[num + k].SetRegion(_regionID);
					}
				}
			}
			_regionID++;
			for (int l = _width - aBorderSize; l < _width; l++)
			{
				for (int m = 0; m < _length - aBorderSize; m++)
				{
					int num3 = _spanIndex[(l * _length + m) * 2];
					int num4 = _spanIndex[(l * _length + m) * 2 + 1];
					for (int n = 0; n < num4; n++)
					{
						_spans[num3 + n].SetRegion(_regionID);
					}
				}
			}
			_regionID++;
			for (int num5 = aBorderSize; num5 < _width; num5++)
			{
				for (int num6 = _length - aBorderSize; num6 < _length; num6++)
				{
					int num7 = _spanIndex[(num5 * _length + num6) * 2];
					int num8 = _spanIndex[(num5 * _length + num6) * 2 + 1];
					for (int num9 = 0; num9 < num8; num9++)
					{
						_spans[num7 + num9].SetRegion(_regionID);
					}
				}
			}
			_regionID++;
			for (int num10 = 0; num10 < aBorderSize; num10++)
			{
				for (int num11 = aBorderSize; num11 < _length; num11++)
				{
					int num12 = _spanIndex[(num10 * _length + num11) * 2];
					int num13 = _spanIndex[(num10 * _length + num11) * 2 + 1];
					for (int num14 = 0; num14 < num13; num14++)
					{
						_spans[num12 + num14].SetRegion(_regionID);
					}
				}
			}
			_regionID++;
		}

		public void ExpandRegions(ushort aLevel, int aMaxIter)
		{
			_expandedList.Clear();
			for (int i = 0; i < _spans.Length; i++)
			{
				if (_spans[i].Distance >= aLevel && _spans[i].RegionID == 0)
				{
					_expandedList.Add(i);
				}
			}
			int num = aMaxIter;
			while (true)
			{
				bool flag = true;
				_expandedSpans.Clear();
				_expandedRegions.Clear();
				_expandedPriorities.Clear();
				for (int j = 0; j < _expandedList.Count; j++)
				{
					if (_spans[_expandedList[j]].RegionID > 0)
					{
						continue;
					}
					ushort num2 = 0;
					ushort num3 = ushort.MaxValue;
					for (int k = 0; k < 4; k++)
					{
						if (_spans[_expandedList[j]].HasNeighbor(k))
						{
							int neighbor = _spans[_expandedList[j]].GetNeighbor(k);
							if (_spans[neighbor].RegionID > 0 && !_spans[neighbor].BorderRegion && _spans[neighbor].FloodPriority + 2 < num3)
							{
								num2 = _spans[neighbor].RegionID;
								num3 = (ushort)(_spans[neighbor].FloodPriority + 2);
							}
						}
					}
					if (num2 > 0)
					{
						_expandedSpans.Add(_expandedList[j]);
						_expandedRegions.Add(num2);
						_expandedPriorities.Add(num3);
						flag = false;
					}
				}
				if (flag)
				{
					break;
				}
				for (int l = 0; l < _expandedSpans.Count; l++)
				{
					_spans[_expandedSpans[l]].SetRegion(_expandedRegions[l]);
					_spans[_expandedSpans[l]].SetFloodPriority(_expandedPriorities[l]);
				}
				if (aMaxIter >= 0)
				{
					num--;
					if (num == 0)
					{
						break;
					}
				}
			}
		}

		public void FloodGrid(ushort aLevel)
		{
			for (int i = 0; i < _spans.Length; i++)
			{
				if (_spans[i].Distance >= aLevel && _spans[i].RegionID == 0 && FloodSpan(i, aLevel))
				{
					_regionID++;
				}
			}
		}

		public int RemoveSmallRegions(int aMinRegionArea)
		{
			int num = 0;
			int num2 = 0;
			while (true)
			{
				if (num < _spans.Length && (_spans[num].RegionID <= 0 || _spans[num].Visited || _spans[num].BorderRegion))
				{
					num++;
					continue;
				}
				if (num == _spans.Length)
				{
					break;
				}
				List<int> list = new List<int>();
				Stack<int> stack = new Stack<int>();
				stack.Push(num);
				while (stack.Count > 0)
				{
					int num3 = stack.Pop();
					if (_spans[num3].Visited)
					{
						continue;
					}
					_spans[num3].SetVisited();
					list.Add(num3);
					for (int i = 0; i < 4; i++)
					{
						if (_spans[num3].HasNeighbor(i))
						{
							int neighbor = _spans[num3].GetNeighbor(i);
							if (!_spans[neighbor].Visited && _spans[num3].RegionID == _spans[neighbor].RegionID)
							{
								stack.Push(neighbor);
							}
						}
					}
				}
				if (list.Count >= aMinRegionArea)
				{
					continue;
				}
				for (int j = 0; j < list.Count; j++)
				{
					for (int k = 0; k < 4; k++)
					{
						if (_spans[list[j]].HasNeighbor(k))
						{
							_spans[_spans[list[j]].GetNeighbor(k)].RemoveNeighbor(CompactSpan.GetOppositeDirection(k));
							_spans[list[j]].RemoveNeighbor(k);
						}
					}
				}
				num2++;
			}
			return num2;
		}

		public int MergeSmallRegions(float aMergeRegionArea)
		{
			Debug.LogWarning("CompactSpanGrid: Not merging regions, not implemented");
			return 0;
		}

		public ContourPoly WalkContours(Point2 aQuadrant, float aCellSize)
		{
			ContourPoly contourPoly = new ContourPoly(aCellSize, 1, 1);
			List<List<SpanPoint>> list = new List<List<SpanPoint>>();
			List<bool> list2 = new List<bool>();
			Point3 point = new Point3(int.MaxValue, int.MaxValue, int.MaxValue);
			Point3 point2 = new Point3(int.MinValue, int.MinValue, int.MinValue);
			_contourID = 1;
			int num = 0;
			while (true)
			{
				if (num < _spans.Length && (_spans[num].RegionID <= 0 || _spans[num].ContourID != 0 || _spans[num].BorderRegion || !SpanHasEdge(num)))
				{
					num++;
					continue;
				}
				if (num == _spans.Length)
				{
					break;
				}
				int num2 = 0;
				while (_spans[num].HasNeighbor(num2) && _spans[num].RegionID == _spans[_spans[num].GetNeighbor(num2)].RegionID)
				{
					num2 = CompactSpan.RotateClockwise(num2);
				}
				int num3 = num;
				int num4 = num2;
				List<SpanPoint> list3 = new List<SpanPoint>();
				bool flag = false;
				do
				{
					if (!_spans[num3].HasNeighbor(num4) || _spans[num3].RegionID != _spans[_spans[num3].GetNeighbor(num4)].RegionID)
					{
						if (_spans[num3].ContourID == 0)
						{
							_spans[num3].SetContourID(_contourID);
						}
						SpanPoint cornerPoint = GetCornerPoint(num3, num4);
						list3.Add(cornerPoint);
						flag |= cornerPoint.ConnectBorderRegion;
						point = Point3.Min(cornerPoint.Point, point);
						point2 = Point3.Max(cornerPoint.Point, point2);
						num4 = CompactSpan.RotateClockwise(num4);
					}
					else
					{
						num3 = _spans[num3].GetNeighbor(num4);
						num4 = CompactSpan.RotateCounterClockwise(num4);
					}
				}
				while (num3 != num || num4 != num2);
				if (list3.Count > 0)
				{
					list.Add(list3);
					list2.Add(flag);
				}
			}
			contourPoly.AddRawContours(list, list2, aQuadrant, point, point2);
			return contourPoly;
		}

		private void ClearVisited()
		{
			for (int i = 0; i < _width; i++)
			{
				for (int j = 0; j < _length; j++)
				{
					int num = _spanIndex[(i * _length + j) * 2];
					int num2 = _spanIndex[(i * _length + j) * 2 + 1];
					for (int k = 0; k < num2; k++)
					{
						_spans[num + k].ClearVisited();
					}
				}
			}
		}

		private bool WithinGrid(int aX, int aZ)
		{
			if (aX >= 0 && aZ >= 0 && aX < _width)
			{
				return aZ < _length;
			}
			return false;
		}

		private bool FloodSpan(int aSpan, ushort aLevel)
		{
			_spans[aSpan].SetFloodPriority(0);
			_spans[aSpan].SetRegion(_regionID);
			_floodStack.Clear();
			_floodStack.Push(aSpan);
			int num = 0;
			while (_floodStack.Count > 0)
			{
				int num2 = _floodStack.Pop();
				ushort num3 = 0;
				for (int i = 0; i < 4; i++)
				{
					if (!_spans[num2].HasNeighbor(i))
					{
						continue;
					}
					int neighbor = _spans[num2].GetNeighbor(i);
					if (_spans[neighbor].RegionID > 0 && _spans[neighbor].RegionID != _regionID && !_spans[neighbor].BorderRegion)
					{
						num3 = _spans[neighbor].RegionID;
					}
					if (_spans[neighbor].HasNeighbor(CompactSpan.RotateClockwise(i)))
					{
						int neighbor2 = _spans[neighbor].GetNeighbor(CompactSpan.RotateClockwise(i));
						if (_spans[neighbor2].RegionID > 0 && _spans[neighbor2].RegionID != _regionID && !_spans[neighbor2].BorderRegion)
						{
							num3 = _spans[neighbor2].RegionID;
						}
					}
				}
				if (num3 > 0)
				{
					_spans[num2].ClearRegion();
					continue;
				}
				num++;
				for (int j = 0; j < 4; j++)
				{
					if (_spans[num2].HasNeighbor(j))
					{
						int neighbor3 = _spans[num2].GetNeighbor(j);
						if (_spans[neighbor3].RegionID == 0 && !_spans[neighbor3].BorderRegion && _spans[neighbor3].Distance >= aLevel)
						{
							_spans[neighbor3].SetFloodPriority(0);
							_spans[neighbor3].SetRegion(_regionID);
							_floodStack.Push(neighbor3);
						}
					}
				}
			}
			return num > 0;
		}

		private bool SpanHasEdge(int aSpan)
		{
			if (_spans[aSpan].HasExternalEdge())
			{
				return true;
			}
			for (int i = 0; i < 4; i++)
			{
				if (_spans[aSpan].HasNeighbor(i) && _spans[aSpan].RegionID != _spans[_spans[aSpan].GetNeighbor(i)].RegionID)
				{
					return true;
				}
			}
			return false;
		}

		private SpanPoint GetCornerPoint(int aSpan, int aDirection)
		{
			int aDirection2 = CompactSpan.RotateClockwise(aDirection);
			int num = _spans[aSpan].MaxHeight;
			ushort[] array = new ushort[4]
			{
				_spans[aSpan].RegionID,
				0,
				0,
				0
			};
			bool[] array2 = new bool[4]
			{
				_spans[aSpan].BorderRegion,
				false,
				false,
				false
			};
			if (_spans[aSpan].HasNeighbor(aDirection))
			{
				int neighbor = _spans[aSpan].GetNeighbor(aDirection);
				num = Math.Max(num, _spans[neighbor].MaxHeight);
				array[1] = _spans[neighbor].RegionID;
				array2[1] = _spans[neighbor].BorderRegion;
				if (_spans[neighbor].HasNeighbor(aDirection2))
				{
					int neighbor2 = _spans[neighbor].GetNeighbor(aDirection2);
					num = Math.Max(num, _spans[neighbor2].MaxHeight);
					array[2] = _spans[neighbor2].RegionID;
					array2[2] = _spans[neighbor2].BorderRegion;
				}
			}
			if (_spans[aSpan].HasNeighbor(aDirection2))
			{
				int neighbor3 = _spans[aSpan].GetNeighbor(aDirection2);
				num = Math.Max(num, _spans[neighbor3].MaxHeight);
				array[3] = _spans[neighbor3].RegionID;
				array2[3] = _spans[neighbor3].BorderRegion;
				if (_spans[neighbor3].HasNeighbor(aDirection))
				{
					int neighbor4 = _spans[neighbor3].GetNeighbor(aDirection);
					num = Math.Max(num, _spans[neighbor4].MaxHeight);
					array[2] = _spans[neighbor4].RegionID;
					array2[2] = _spans[neighbor4].BorderRegion;
				}
			}
			Point3 point = new Point3(_spans[aSpan].X, num, _spans[aSpan].Z);
			switch (aDirection)
			{
			case 0:
				point.X++;
				point.Z++;
				break;
			case 1:
				point.X++;
				break;
			case 3:
				point.Z++;
				break;
			}
			if (_spans[aSpan].HasNeighbor(aDirection))
			{
				return new SpanPoint(point, _spans[_spans[aSpan].GetNeighbor(aDirection)], _spans[aSpan].RegionID);
			}
			return new SpanPoint(point, _spans[aSpan].RegionID);
		}
	}
}
