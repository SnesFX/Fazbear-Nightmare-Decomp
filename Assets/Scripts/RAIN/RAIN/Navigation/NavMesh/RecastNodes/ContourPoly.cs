using System;
using System.Collections.Generic;
using UnityEngine;

namespace RAIN.Navigation.NavMesh.RecastNodes
{
	public class ContourPoly
	{
		private float _cellSize;

		private List<SpanPoint> _vertices;

		private Point2 _quadrant;

		private List<LinkedList<int>>[,] _contours;

		private List<bool>[,] _contourBorders;

		private Point3[,] _quadrantMin;

		private Point3[,] _quadrantMax;

		private ushort _regionID;

		public ContourPoly(float aCellSize, int aWidth, int aLength)
		{
			_cellSize = aCellSize;
			_vertices = new List<SpanPoint>();
			_quadrant = null;
			_contours = new List<LinkedList<int>>[aWidth, aLength];
			_contourBorders = new List<bool>[aWidth, aLength];
			_quadrantMin = new Point3[aWidth, aLength];
			_quadrantMax = new Point3[aWidth, aLength];
			_regionID = 1;
		}

		public void AddRawContours(List<List<SpanPoint>> aVertices, List<bool> aBorderContours, Point2 aQuadrant, Point3 aMinPoint, Point3 aMaxPoint)
		{
			_quadrant = aQuadrant;
			_contours[0, 0] = new List<LinkedList<int>>();
			_contourBorders[0, 0] = new List<bool>();
			_quadrantMin[0, 0] = aMinPoint;
			_quadrantMax[0, 0] = aMaxPoint;
			for (int i = 0; i < aVertices.Count; i++)
			{
				int count = _vertices.Count;
				_vertices.AddRange(aVertices[i]);
				LinkedList<int> linkedList = new LinkedList<int>();
				for (int j = 0; j < aVertices[i].Count; j++)
				{
					linkedList.AddLast(count + j);
				}
				_contours[0, 0].Add(linkedList);
				_contourBorders[0, 0].Add(aBorderContours[i]);
			}
		}

		public void SimplifyContours(float aMaxVertexError, float aMaxSegmentLength)
		{
			List<LinkedList<int>> list = new List<LinkedList<int>>();
			List<bool> list2 = new List<bool>();
			for (int i = 0; i < _contours[0, 0].Count; i++)
			{
				if (_contourBorders[0, 0][i])
				{
					list.Add(_contours[0, 0][i]);
					list2.Add(_contourBorders[0, 0][i]);
					continue;
				}
				LinkedList<int> linkedList = SimplifyContour(_contours[0, 0][i], aMaxVertexError, aMaxSegmentLength);
				if (linkedList.Count != 0)
				{
					list.Add(linkedList);
					list2.Add(_contourBorders[0, 0][i]);
				}
			}
			_contours[0, 0] = list;
			_contourBorders[0, 0] = list2;
			RemapAllContours(CollapseVertices());
			Dictionary<ushort, ushort> aRemapDictionary = new Dictionary<ushort, ushort>();
			for (int j = 0; j < _vertices.Count; j++)
			{
				_vertices[j].RemapRegion(aRemapDictionary, ref _regionID);
			}
		}

		public void FixZeroOrNegativeContours()
		{
			int num = 0;
			while (num < _contours[0, 0].Count)
			{
				int areaForContour = GetAreaForContour(_contours[0, 0][num]);
				if (areaForContour >= 0)
				{
					num++;
					continue;
				}
				bool flag = false;
				for (int i = 0; i < _contours[0, 0].Count; i++)
				{
					if (i != num && _contours[0, 0][i].Count != 0 && _vertices[_contours[0, 0][i].First.Value].RegionID == _vertices[_contours[0, 0][num].First.Value].RegionID && GetAreaForContour(_contours[0, 0][i]) != 0)
					{
						LinkedList<int> linkedList = MergeDisconnectedContours(_contours[0, 0][num], _contours[0, 0][i]);
						if (linkedList.Count > 0)
						{
							_contours[0, 0][num] = linkedList;
							_contours[0, 0][i] = new LinkedList<int>();
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					num++;
				}
			}
			num = 0;
			while (num < _contours[0, 0].Count)
			{
				if (_contours[0, 0][num].Count == 0)
				{
					_contours[0, 0].RemoveAt(num);
					_contourBorders[0, 0].RemoveAt(num);
				}
				else
				{
					num++;
				}
			}
		}

		public void TriangulatePoly(Point2 aQuadrant, float aMaxVertexError, float aMaxSegmentLength)
		{
			for (int i = 0; i < _contours[aQuadrant.X, aQuadrant.Y].Count; i++)
			{
				if (_contourBorders[aQuadrant.X, aQuadrant.Y][i])
				{
					_contours[aQuadrant.X, aQuadrant.Y][i] = SimplifyContour(_contours[aQuadrant.X, aQuadrant.Y][i], aMaxVertexError, aMaxSegmentLength);
				}
			}
			List<LinkedList<int>> list = new List<LinkedList<int>>();
			List<bool> list2 = new List<bool>();
			for (int j = 0; j < _contours[aQuadrant.X, aQuadrant.Y].Count; j++)
			{
				List<LinkedList<int>> list3 = TriangulateContour(_contours[aQuadrant.X, aQuadrant.Y][j]);
				MergeContours(list3);
				for (int k = 0; k < list3.Count; k++)
				{
					list.Add(list3[k]);
					bool flag = false;
					for (LinkedListNode<int> linkedListNode = list3[k].First; linkedListNode != null; linkedListNode = linkedListNode.Next)
					{
						flag |= _vertices[linkedListNode.Value].ConnectBorderRegion;
					}
					list2.Add(_contourBorders[aQuadrant.X, aQuadrant.Y][j]);
				}
			}
			_contours[aQuadrant.X, aQuadrant.Y] = list;
			_contourBorders[aQuadrant.X, aQuadrant.Y] = list2;
		}

		public void MergeContourPoly(ContourPoly aContourPoly)
		{
			if (aContourPoly._contours.GetLength(0) > 1 || aContourPoly._contours.GetLength(1) > 1)
			{
				throw new Exception("MergeContourPoly only works with single quadrants.");
			}
			int count = _vertices.Count;
			_vertices.AddRange(aContourPoly._vertices);
			Dictionary<ushort, ushort> aRemapDictionary = new Dictionary<ushort, ushort>();
			for (int i = count; i < _vertices.Count; i++)
			{
				_vertices[i].RemapRegion(aRemapDictionary, ref _regionID);
			}
			List<LinkedList<int>> list = new List<LinkedList<int>>();
			for (int j = 0; j < aContourPoly._contours[0, 0].Count; j++)
			{
				LinkedList<int> linkedList = new LinkedList<int>();
				for (LinkedListNode<int> linkedListNode = aContourPoly._contours[0, 0][j].First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					linkedList.AddLast(count + linkedListNode.Value);
				}
				list.Add(linkedList);
			}
			Point2 quadrant = aContourPoly._quadrant;
			_contours[quadrant.X, quadrant.Y] = list;
			_contourBorders[quadrant.X, quadrant.Y] = aContourPoly._contourBorders[0, 0];
			_quadrantMin[quadrant.X, quadrant.Y] = aContourPoly._quadrantMin[0, 0];
			_quadrantMax[quadrant.X, quadrant.Y] = aContourPoly._quadrantMax[0, 0];
			if (_contours[quadrant.X, quadrant.Y].Count != 0)
			{
				Point2 point = quadrant - new Point2(1, 0);
				if (point.X >= 0 && _contours[point.X, point.Y] != null)
				{
					LinkContours(new Point2(_quadrantMin[quadrant.X, quadrant.Y].X, int.MaxValue), quadrant, point);
				}
				point = quadrant + new Point2(0, 1);
				if (point.Y < _contours.GetLength(1) && _contours[point.X, point.Y] != null)
				{
					LinkContours(new Point2(int.MaxValue, _quadrantMax[quadrant.X, quadrant.Y].Z), quadrant, point);
				}
				point = quadrant + new Point2(1, 0);
				if (point.X < _contours.GetLength(0) && _contours[point.X, point.Y] != null)
				{
					LinkContours(new Point2(_quadrantMax[quadrant.X, quadrant.Y].X, int.MaxValue), point, quadrant);
				}
				point = quadrant - new Point2(0, 1);
				if (point.Y >= 0 && _contours[point.X, point.Y] != null)
				{
					LinkContours(new Point2(int.MaxValue, _quadrantMin[quadrant.X, quadrant.Y].Z), point, quadrant);
				}
				if (quadrant.X == 0 || quadrant.X == _contours.GetLength(0) - 1 || quadrant.Y == 0 || quadrant.Y == _contours.GetLength(1) - 1)
				{
					SimplifyBorderEdge(quadrant);
				}
			}
		}

		public ContourMeshData GetContourMesh(Point2 aQuadrant)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			List<Vector3> list = new List<Vector3>();
			List<int[]> list2 = new List<int[]>();
			List<int[]> list3 = new List<int[]>();
			List<int> list4 = new List<int>();
			List<int> list5 = new List<int>();
			for (int i = 0; i < _contours[aQuadrant.X, aQuadrant.Y].Count; i++)
			{
				list4.Clear();
				list5.Clear();
				for (LinkedListNode<int> linkedListNode = _contours[aQuadrant.X, aQuadrant.Y][i].First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					dictionary[linkedListNode.Value] = list.Count;
					list4.Add(list.Count);
					list.Add((Vector3)_vertices[linkedListNode.Value].Point * _cellSize);
				}
				List<LinkedList<int>> list6 = TriangulateContour(_contours[aQuadrant.X, aQuadrant.Y][i]);
				for (int j = 0; j < list6.Count; j++)
				{
					for (LinkedListNode<int> linkedListNode2 = list6[j].First; linkedListNode2 != null; linkedListNode2 = linkedListNode2.Next)
					{
						list5.Add(dictionary[linkedListNode2.Value]);
					}
				}
				list2.Add(list4.ToArray());
				list3.Add(list5.ToArray());
			}
			ContourMeshData contourMeshData = new ContourMeshData();
			contourMeshData.Vertices = list.ToArray();
			contourMeshData.Contours = list2.ToArray();
			contourMeshData.Triangles = list3.ToArray();
			return contourMeshData;
		}

		private LinkedList<int> SimplifyContour(LinkedList<int> aPoints, float aMaxVertexError, float aMaxSegmentLength)
		{
			LinkedList<int> linkedList = new LinkedList<int>();
			for (LinkedListNode<int> linkedListNode = aPoints.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				int connectRegionID = _vertices[linkedListNode.Value].ConnectRegionID;
				int connectRegionID2 = _vertices[(linkedListNode.Next ?? linkedListNode.List.First).Value].ConnectRegionID;
				bool connectBorderRegion = _vertices[linkedListNode.Value].ConnectBorderRegion;
				if (connectRegionID != connectRegionID2 || connectBorderRegion)
				{
					linkedList.AddLast(linkedListNode.Value);
				}
			}
			if (linkedList.Count == 0)
			{
				Point3 point = _vertices[aPoints.First.Value].Point;
				int value = aPoints.First.Value;
				Point3 point2 = _vertices[aPoints.First.Value].Point;
				int value2 = aPoints.First.Value;
				for (LinkedListNode<int> linkedListNode2 = aPoints.First; linkedListNode2 != null; linkedListNode2 = linkedListNode2.Next)
				{
					Point3 point3 = _vertices[linkedListNode2.Value].Point;
					if (point3.X < point.X || (point3.X == point.X && point3.Z < point.Z))
					{
						point = point3;
						value = linkedListNode2.Value;
					}
					if (point3.X > point2.X || (point3.X == point2.X && point3.Z > point2.Z))
					{
						point2 = point3;
						value2 = linkedListNode2.Value;
					}
				}
				linkedList.AddLast(value);
				linkedList.AddLast(value2);
			}
			while (aPoints.First.Value != linkedList.First.Value)
			{
				LinkedListNode<int> first = aPoints.First;
				aPoints.RemoveFirst();
				aPoints.AddLast(first);
			}
			LinkedListNode<int> linkedListNode3 = linkedList.First;
			LinkedListNode<int> linkedListNode4 = aPoints.First;
			float num = aMaxVertexError / _cellSize;
			num *= num;
			while (linkedListNode3 != null)
			{
				int num2 = linkedListNode3.Value;
				int num3 = (linkedListNode3.Next ?? linkedListNode3.List.First).Value;
				bool flag = _vertices[num2].Point.X > _vertices[num3].Point.X || (_vertices[num2].Point.X == _vertices[num3].Point.X && _vertices[num2].Point.Z > _vertices[num3].Point.Z);
				if (flag)
				{
					int num4 = num2;
					num2 = num3;
					num3 = num4;
				}
				LinkedListNode<int> linkedListNode5 = linkedListNode4.Next ?? linkedListNode4.List.First;
				if (flag)
				{
					while (linkedListNode5.Value != num2)
					{
						linkedListNode5 = linkedListNode5.Next ?? linkedListNode5.List.First;
					}
					linkedListNode5 = linkedListNode5.Previous ?? linkedListNode5.List.Last;
				}
				if (_vertices[linkedListNode5.Value].IsExternalPoint())
				{
					int num5 = -1;
					float num6 = 0f;
					while (linkedListNode5.Value != num3)
					{
						float num7 = DistanceSqrFromPointToLine(_vertices[linkedListNode5.Value].Point, _vertices[num2].Point, _vertices[num3].Point);
						if (num7 > num6)
						{
							num5 = linkedListNode5.Value;
							num6 = num7;
						}
						linkedListNode5 = ((!flag) ? (linkedListNode5.Next ?? linkedListNode5.List.First) : (linkedListNode5.Previous ?? linkedListNode5.List.Last));
					}
					if (num5 != -1 && num6 > num)
					{
						linkedList.AddAfter(linkedListNode3, num5);
						continue;
					}
					linkedListNode3 = linkedListNode3.Next;
					if (linkedListNode3 != null)
					{
						while (linkedListNode4.Value != linkedListNode3.Value)
						{
							linkedListNode4 = linkedListNode4.Next ?? linkedListNode4.List.First;
						}
					}
					continue;
				}
				linkedListNode3 = linkedListNode3.Next;
				if (linkedListNode3 != null)
				{
					while (linkedListNode4.Value != linkedListNode3.Value)
					{
						linkedListNode4 = linkedListNode4.Next ?? linkedListNode4.List.First;
					}
				}
			}
			float num8 = aMaxSegmentLength / _cellSize;
			num8 *= num8;
			if (num8 > 0f)
			{
				linkedListNode3 = linkedList.First;
				linkedListNode4 = aPoints.First;
				while (linkedListNode3 != null)
				{
					int value3 = linkedListNode3.Value;
					int value4 = (linkedListNode3.Next ?? linkedListNode3.List.First).Value;
					LinkedListNode<int> linkedListNode6 = linkedListNode4.Next ?? linkedListNode4.List.First;
					if (_vertices[linkedListNode6.Value].IsExternalPoint())
					{
						Point3 point4 = _vertices[value3].Point;
						Point3 point5 = _vertices[value4].Point;
						if ((float)(point5 - point4).GetLengthSquared() > num8)
						{
							int num9 = -1;
							float num10 = 0f;
							while (linkedListNode6.Value != value4)
							{
								if (_vertices[linkedListNode6.Value].IsExternalPoint())
								{
									float num11 = DistanceSqrFromPointToLine(_vertices[linkedListNode6.Value].Point, point4, point5);
									if (num11 > num10)
									{
										num9 = linkedListNode6.Value;
										num10 = num11;
									}
								}
								linkedListNode6 = linkedListNode6.Next ?? aPoints.First;
							}
							if (num9 != -1)
							{
								linkedList.AddAfter(linkedListNode3, num9);
								continue;
							}
							linkedListNode3 = linkedListNode3.Next;
							linkedListNode4 = linkedListNode6;
						}
						else
						{
							linkedListNode3 = linkedListNode3.Next;
							while (linkedListNode4.Value != value4)
							{
								linkedListNode4 = linkedListNode4.Next ?? linkedListNode4.List.First;
							}
						}
					}
					else
					{
						linkedListNode3 = linkedListNode3.Next;
						while (linkedListNode4.Value != value4)
						{
							linkedListNode4 = linkedListNode4.Next ?? linkedListNode4.List.First;
						}
					}
				}
			}
			linkedListNode3 = linkedList.First;
			while (linkedListNode3 != null)
			{
				int value5 = linkedListNode3.Value;
				int value6 = (linkedListNode3.Next ?? linkedListNode3.List.First).Value;
				if (_vertices[value5].IsExternalPoint())
				{
					Point3 point6 = _vertices[value5].Point;
					Point3 point7 = _vertices[value6].Point;
					if (point6.X == point7.X && point6.Z == point7.Z)
					{
						LinkedListNode<int> linkedListNode7 = linkedListNode3;
						linkedListNode3 = linkedListNode3.Next;
						linkedListNode7.List.Remove(linkedListNode7);
					}
					else
					{
						linkedListNode3 = linkedListNode3.Next;
					}
				}
				else
				{
					linkedListNode3 = linkedListNode3.Next;
				}
			}
			if (linkedList.Count < 3)
			{
				for (linkedListNode4 = aPoints.First; linkedListNode4 != null; linkedListNode4 = linkedListNode4.Next)
				{
					_vertices[linkedListNode4.Value] = null;
				}
				linkedList.Clear();
			}
			else
			{
				linkedListNode3 = linkedList.First;
				linkedListNode4 = aPoints.First;
				do
				{
					linkedListNode3 = linkedListNode3.Next ?? linkedListNode3.List.First;
					linkedListNode4 = linkedListNode4.Next ?? linkedListNode4.List.First;
					while (linkedListNode4.Value != linkedListNode3.Value)
					{
						_vertices[linkedListNode4.Value] = null;
						linkedListNode4 = linkedListNode4.Next ?? linkedListNode4.List.First;
					}
				}
				while (linkedListNode3.Value != linkedList.First.Value);
			}
			return linkedList;
		}

		private Dictionary<int, int> CollapseVertices(int aStartIndex = 0)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			while (_vertices.Count > 0 && _vertices[_vertices.Count - 1] == null)
			{
				_vertices.RemoveAt(_vertices.Count - 1);
			}
			for (int i = aStartIndex; i < _vertices.Count; i++)
			{
				if (_vertices[i] == null)
				{
					dictionary.Add(_vertices.Count - 1, i);
					_vertices[i] = _vertices[_vertices.Count - 1];
					_vertices.RemoveAt(_vertices.Count - 1);
					while (_vertices.Count > 0 && _vertices[_vertices.Count - 1] == null)
					{
						_vertices.RemoveAt(_vertices.Count - 1);
					}
				}
			}
			return dictionary;
		}

		private void RemapContour(LinkedList<int> aPoints, Dictionary<int, int> aIndexRemap)
		{
			for (LinkedListNode<int> linkedListNode = aPoints.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				if (aIndexRemap.ContainsKey(linkedListNode.Value))
				{
					linkedListNode.Value = aIndexRemap[linkedListNode.Value];
				}
			}
		}

		private void RemapAllContours(Dictionary<int, int> aIndexRemap)
		{
			for (int i = 0; i < _contours[0, 0].Count; i++)
			{
				RemapContour(_contours[0, 0][i], aIndexRemap);
			}
		}

		private LinkedList<int> MergeDisconnectedContours(LinkedList<int> aContourOne, LinkedList<int> aContourTwo)
		{
			LinkedListNode<int> linkedListNode = aContourOne.First;
			LinkedListNode<int> linkedListNode2 = aContourTwo.First;
			int num = -1;
			for (LinkedListNode<int> linkedListNode3 = aContourOne.First; linkedListNode3 != null; linkedListNode3 = linkedListNode3.Next)
			{
				int value = (linkedListNode3.Previous ?? linkedListNode3.List.Last).Value;
				int value2 = linkedListNode3.Value;
				int value3 = (linkedListNode3.Next ?? linkedListNode3.List.First).Value;
				for (LinkedListNode<int> linkedListNode4 = aContourTwo.First; linkedListNode4 != null; linkedListNode4 = linkedListNode4.Next)
				{
					int value4 = linkedListNode4.Value;
					if (Area2(value, value2, value4) <= 0 && Area2(value2, value3, value4) <= 0)
					{
						int lengthSquared = (_vertices[linkedListNode4.Value].Point - _vertices[linkedListNode3.Value].Point).GetLengthSquared();
						if (lengthSquared < num)
						{
							linkedListNode = linkedListNode3;
							linkedListNode2 = linkedListNode4;
							num = lengthSquared;
						}
					}
				}
			}
			if (num < 0)
			{
				return new LinkedList<int>();
			}
			LinkedList<int> linkedList = new LinkedList<int>();
			for (int i = 0; i < aContourOne.Count; i++)
			{
				linkedList.AddLast(linkedListNode.Value);
				linkedListNode = linkedListNode.Next ?? linkedListNode.List.First;
			}
			for (int j = 0; j < aContourTwo.Count; j++)
			{
				linkedList.AddLast(linkedListNode2.Value);
				linkedListNode2 = linkedListNode2.Next ?? linkedListNode2.List.First;
			}
			return linkedList;
		}

		private List<LinkedList<int>> TriangulateContour(LinkedList<int> aPoints)
		{
			LinkedList<int> linkedList = new LinkedList<int>();
			LinkedList<bool> linkedList2 = new LinkedList<bool>();
			for (LinkedListNode<int> linkedListNode = aPoints.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedList.AddLast(linkedListNode.Value);
			}
			for (LinkedListNode<int> linkedListNode2 = linkedList.First; linkedListNode2 != null; linkedListNode2 = linkedListNode2.Next)
			{
				linkedList2.AddLast(IsInternalDiagonal(linkedListNode2));
			}
			List<LinkedList<int>> list = new List<LinkedList<int>>();
			while (linkedList.Count > 3)
			{
				int num = int.MaxValue;
				LinkedListNode<int> linkedListNode3 = null;
				LinkedListNode<bool> linkedListNode4 = null;
				LinkedListNode<int> linkedListNode5 = linkedList.First;
				LinkedListNode<bool> linkedListNode6 = linkedList2.First;
				do
				{
					if (linkedListNode6.Value)
					{
						Point3 point = _vertices[(linkedListNode5.Previous ?? linkedListNode5.List.Last).Value].Point;
						Point3 point2 = _vertices[(linkedListNode5.Next ?? linkedListNode5.List.First).Value].Point;
						int lengthSquared = (point - point2).GetLengthSquared();
						if (lengthSquared < num)
						{
							num = lengthSquared;
							linkedListNode3 = linkedListNode5;
							linkedListNode4 = linkedListNode6;
						}
					}
					linkedListNode5 = linkedListNode5.Next ?? linkedListNode5.List.First;
					linkedListNode6 = linkedListNode6.Next ?? linkedListNode6.List.First;
				}
				while (linkedListNode5 != linkedList.First);
				if (linkedListNode3 == null || linkedList.Count < 3)
				{
					return list;
				}
				LinkedList<int> linkedList3 = new LinkedList<int>();
				linkedList3.AddLast((linkedListNode3.Previous ?? linkedListNode3.List.Last).Value);
				linkedList3.AddLast(linkedListNode3.Value);
				linkedList3.AddLast((linkedListNode3.Next ?? linkedListNode3.List.First).Value);
				list.Add(linkedList3);
				linkedListNode3 = linkedListNode3.Previous ?? linkedListNode3.List.Last;
				linkedListNode4 = linkedListNode4.Previous ?? linkedListNode4.List.Last;
				linkedListNode3.List.Remove(linkedListNode3.Next ?? linkedListNode3.List.First);
				linkedListNode4.List.Remove(linkedListNode4.Next ?? linkedListNode4.List.First);
				for (int i = 0; i < 2; i++)
				{
					linkedListNode4.Value = IsInternalDiagonal(linkedListNode3);
					linkedListNode3 = linkedListNode3.Next ?? linkedListNode3.List.First;
					linkedListNode4 = linkedListNode4.Next ?? linkedListNode4.List.First;
				}
			}
			if (linkedList.Count == 3)
			{
				LinkedList<int> linkedList4 = new LinkedList<int>();
				linkedList4.AddLast(linkedList.First.Value);
				linkedList4.AddLast(linkedList.First.Next.Value);
				linkedList4.AddLast(linkedList.First.Next.Next.Value);
				list.Add(linkedList4);
			}
			return list;
		}

		private void MergeContours(List<LinkedList<int>> aContours)
		{
			while (true)
			{
				int num = -1;
				int index = -1;
				int index2 = -1;
				LinkedListNode<int> aEdgeOne = null;
				LinkedListNode<int> aEdgeTwo = null;
				for (int i = 0; i < aContours.Count; i++)
				{
					for (int j = i + 1; j < aContours.Count; j++)
					{
						LinkedListNode<int> aEdgeOne2;
						LinkedListNode<int> aEdgeTwo2;
						int edgesForMerge = GetEdgesForMerge(aContours[i], aContours[j], out aEdgeOne2, out aEdgeTwo2);
						if (edgesForMerge > num)
						{
							num = edgesForMerge;
							index = i;
							index2 = j;
							aEdgeOne = aEdgeOne2;
							aEdgeTwo = aEdgeTwo2;
						}
					}
				}
				if (num < 0)
				{
					break;
				}
				aContours[index] = MergePolysAlongEdge(aEdgeOne, aEdgeTwo);
				aContours.RemoveAt(index2);
			}
		}

		private bool ContainsIndex(LinkedList<int> aPoints, int aIndex)
		{
			for (LinkedListNode<int> linkedListNode = aPoints.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				if (linkedListNode.Value == aIndex)
				{
					return true;
				}
			}
			return false;
		}

		private void CheckProperContours(LinkedList<LinkedList<int>> aContours)
		{
			for (LinkedListNode<LinkedList<int>> linkedListNode = aContours.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				bool flag = false;
				for (LinkedListNode<int> linkedListNode2 = linkedListNode.Value.First; linkedListNode2 != null; linkedListNode2 = linkedListNode2.Next)
				{
					for (LinkedListNode<int> next = linkedListNode2.Next; next != null; next = next.Next)
					{
						if (linkedListNode2.Value == next.Value)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						Debug.LogWarning("ContourPoly: Improper contour (same index in the contour twice)");
						break;
					}
				}
			}
		}

		private void CheckOverlappingContours(LinkedList<LinkedList<int>> aContours)
		{
			for (LinkedListNode<LinkedList<int>> linkedListNode = aContours.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				for (LinkedListNode<LinkedList<int>> next = linkedListNode.Next; next != null; next = next.Next)
				{
					LinkedListNode<int> first = linkedListNode.Value.First;
					LinkedListNode<int> linkedListNode2 = next.Value.First;
					while (linkedListNode2 != null && linkedListNode2.Value != first.Value)
					{
						linkedListNode2 = linkedListNode2.Next;
					}
					if (linkedListNode2 != null)
					{
						bool flag = true;
						LinkedListNode<int> linkedListNode3 = first;
						LinkedListNode<int> linkedListNode4 = linkedListNode2;
						while (linkedListNode3 != null)
						{
							if (linkedListNode3.Value != linkedListNode4.Value)
							{
								flag = false;
								break;
							}
							linkedListNode3 = linkedListNode3.Next;
							linkedListNode4 = linkedListNode4.Next ?? linkedListNode4.List.First;
						}
						if (flag)
						{
							Debug.LogWarning("ContourPoly: Contours overlap directly");
						}
						flag = true;
						linkedListNode3 = first;
						linkedListNode4 = linkedListNode2;
						while (linkedListNode3 != null)
						{
							if (linkedListNode3.Value != linkedListNode4.Value)
							{
								flag = false;
								break;
							}
							linkedListNode3 = linkedListNode3.Next;
							linkedListNode4 = linkedListNode4.Previous ?? linkedListNode4.List.Last;
						}
						if (flag)
						{
							Debug.LogWarning("ContourPoly: Contours overlap in opposite directions");
						}
					}
				}
			}
		}

		private void CheckTriangleContours(LinkedList<LinkedList<int>> aContours)
		{
			for (LinkedListNode<LinkedList<int>> linkedListNode = aContours.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				if (linkedListNode.Value.Count != 3)
				{
					Debug.LogWarning("ContourPoly: Expected 3 points in contour");
				}
				else if (GetAreaForTriangle(linkedListNode.Value.First) == 0)
				{
					Debug.LogWarning("ContourPoly: Contour is vertically coplanar");
				}
			}
		}

		private LinkedList<int> MergeBrokenContours(List<LinkedList<int>> aContours, int aIndex)
		{
			LinkedList<int[]> linkedList = new LinkedList<int[]>();
			for (int i = 0; i < aContours.Count; i++)
			{
				for (LinkedListNode<int> linkedListNode = aContours[i].First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					int value = linkedListNode.Value;
					int value2 = (linkedListNode.Next ?? linkedListNode.List.First).Value;
					if (value != aIndex && value2 != aIndex)
					{
						linkedList.AddLast(new int[2] { value, value2 });
					}
				}
			}
			if (linkedList.Count == 0)
			{
				return null;
			}
			LinkedList<int> linkedList2 = new LinkedList<int>();
			linkedList2.AddLast(linkedList.First.Value[0]);
			bool flag;
			do
			{
				flag = false;
				LinkedListNode<int[]> linkedListNode2 = linkedList.First;
				while (linkedListNode2 != null)
				{
					if (linkedList2.Last.Value == linkedListNode2.Value[0])
					{
						flag = true;
						linkedList2.AddLast(linkedListNode2.Value[1]);
						if (linkedListNode2.List.Count == 1)
						{
							linkedListNode2.List.Remove(linkedListNode2);
							linkedListNode2 = null;
						}
						else
						{
							linkedListNode2 = linkedListNode2.Previous ?? linkedListNode2.List.Last;
							linkedListNode2.List.Remove(linkedListNode2.Next ?? linkedListNode2.List.First);
						}
					}
					else if (linkedList2.First.Value == linkedListNode2.Value[1])
					{
						flag = true;
						linkedList2.AddFirst(linkedListNode2.Value[0]);
						if (linkedListNode2.List.Count == 1)
						{
							linkedListNode2.List.Remove(linkedListNode2);
							linkedListNode2 = null;
						}
						else
						{
							linkedListNode2 = linkedListNode2.Previous ?? linkedListNode2.List.Last;
							linkedListNode2.List.Remove(linkedListNode2.Next ?? linkedListNode2.List.First);
						}
					}
					else
					{
						linkedListNode2 = linkedListNode2.Next;
					}
				}
			}
			while (flag);
			return linkedList2;
		}

		private float DistanceSqrFromPointToLine(Point3 aPoint1, Point3 aLinePointA, Point3 aLinePointB)
		{
			Point3 point = aLinePointB - aLinePointA;
			Point3 aPointB = aPoint1 - aLinePointA;
			int lengthSquared = point.GetLengthSquared();
			if (lengthSquared == 0)
			{
				return (aLinePointB - aPoint1).GetLengthSquared();
			}
			float num = Point3.Dot(point, aPointB) / (float)lengthSquared;
			if (num < 0f)
			{
				return (aLinePointA - aPoint1).GetLengthSquared();
			}
			if (num > 1f)
			{
				return (aLinePointB - aPoint1).GetLengthSquared();
			}
			Vector3 vector = (Vector3)aLinePointA + (Vector3)point * num;
			return (vector - (Vector3)aPoint1).sqrMagnitude;
		}

		private int GetAreaForContour(LinkedList<int> aContour)
		{
			int num = 0;
			for (LinkedListNode<int> linkedListNode = aContour.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				Point3 point = _vertices[linkedListNode.Value].Point;
				Point3 point2 = _vertices[(linkedListNode.Next ?? linkedListNode.List.First).Value].Point;
				num += point.Z * point2.X - point.X * point2.Z;
			}
			return (num + 1) / 2;
		}

		private int Area2(int aIndexA, int aIndexB, int aIndexC)
		{
			Point3 point = _vertices[aIndexA].Point;
			Point3 point2 = _vertices[aIndexB].Point;
			Point3 point3 = _vertices[aIndexC].Point;
			return (point2.X - point.X) * (point3.Z - point.Z) - (point3.X - point.X) * (point2.Z - point.Z);
		}

		private bool IntersectProper(int aIndexA, int aIndexB, int aIndexC, int aIndexD)
		{
			int num = Area2(aIndexA, aIndexB, aIndexC);
			int num2 = Area2(aIndexA, aIndexB, aIndexD);
			int num3 = Area2(aIndexC, aIndexD, aIndexA);
			int num4 = Area2(aIndexC, aIndexD, aIndexB);
			if (num == 0 || num2 == 0 || num3 == 0 || num4 == 0)
			{
				return false;
			}
			if ((num < 0) ^ (num2 < 0))
			{
				return (num3 < 0) ^ (num4 < 0);
			}
			return false;
		}

		private bool PointBetween(int aIndexA, int aIndexB, int aIndexC)
		{
			if (Area2(aIndexA, aIndexB, aIndexC) != 0)
			{
				return false;
			}
			Point3 point = _vertices[aIndexA].Point;
			Point3 point2 = _vertices[aIndexB].Point;
			Point3 point3 = _vertices[aIndexC].Point;
			if (point.X != point2.X)
			{
				if (point3.X < point.X || point3.X > point2.X)
				{
					if (point3.X <= point.X)
					{
						return point3.X >= point2.X;
					}
					return false;
				}
				return true;
			}
			if (point3.Z < point.Z || point3.Z > point2.Z)
			{
				if (point3.Z <= point.Z)
				{
					return point3.Z >= point2.Z;
				}
				return false;
			}
			return true;
		}

		private bool Intersect(int aIndexA, int aIndexB, int aIndexC, int aIndexD)
		{
			if (IntersectProper(aIndexA, aIndexB, aIndexC, aIndexD))
			{
				return true;
			}
			if (!PointBetween(aIndexA, aIndexB, aIndexC) && !PointBetween(aIndexA, aIndexB, aIndexD) && !PointBetween(aIndexC, aIndexD, aIndexA))
			{
				return PointBetween(aIndexC, aIndexD, aIndexB);
			}
			return true;
		}

		private int GetAreaForTriangle(LinkedListNode<int> aIndex)
		{
			Point3 point = _vertices[(aIndex.Previous ?? aIndex.List.Last).Value].Point;
			Point3 point2 = _vertices[aIndex.Value].Point;
			Point3 point3 = _vertices[(aIndex.Next ?? aIndex.List.First).Value].Point;
			int num = (point2.X - point.X) * (point3.Z - point.Z) - (point3.X - point.X) * (point2.Z - point.Z);
			return -num;
		}

		private bool IsInternalDiagonal(LinkedListNode<int> aIndex)
		{
			int value = ((aIndex.Previous ?? aIndex.List.Last).Previous ?? aIndex.List.Last).Value;
			int value2 = (aIndex.Previous ?? aIndex.List.Last).Value;
			int value3 = aIndex.Value;
			int value4 = (aIndex.Next ?? aIndex.List.First).Value;
			if (Area2(value, value2, value3) <= 0)
			{
				if (Area2(value2, value4, value) < 0 && Area2(value4, value2, value3) < 0)
				{
					return IsDiagonalie(aIndex);
				}
			}
			else if (Area2(value2, value4, value3) > 0 || Area2(value4, value2, value) > 0)
			{
				return IsDiagonalie(aIndex);
			}
			return false;
		}

		private bool IsDiagonalie(LinkedListNode<int> aIndex)
		{
			int value = (aIndex.Previous ?? aIndex.List.Last).Value;
			int value2 = (aIndex.Next ?? aIndex.List.First).Value;
			Point3 point = _vertices[value].Point;
			Point3 point2 = _vertices[value2].Point;
			for (LinkedListNode<int> linkedListNode = aIndex.List.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				int value3 = linkedListNode.Value;
				int value4 = (linkedListNode.Next ?? linkedListNode.List.First).Value;
				if (value3 != value && value4 != value && value3 != value2 && value4 != value2)
				{
					Point3 point3 = _vertices[value3].Point;
					Point3 point4 = _vertices[value4].Point;
					if ((point3.X != point.X || point3.Z != point.Z) && (point4.X != point.X || point4.Z != point.Z) && (point3.X != point2.X || point3.Z != point2.Z) && (point4.X != point2.X || point4.Z != point2.Z) && Intersect(value, value2, value3, value4))
					{
						return false;
					}
				}
			}
			return true;
		}

		private int GetEdgesForMerge(LinkedList<int> aPolygonOne, LinkedList<int> aPolygonTwo, out LinkedListNode<int> aEdgeOne, out LinkedListNode<int> aEdgeTwo)
		{
			if (!FindCommonEdge(aPolygonOne, aPolygonTwo, out aEdgeOne, out aEdgeTwo))
			{
				return -1;
			}
			Point3 point = _vertices[(aEdgeOne.Previous ?? aPolygonOne.Last).Value].Point;
			Point3 point2 = _vertices[aEdgeOne.Value].Point;
			Point3 point3 = _vertices[(aEdgeTwo.Next ?? aPolygonTwo.First).Value].Point;
			int num = -(-point2.X * point.Z + point3.X * point.Z + point.X * point2.Z - point3.X * point2.Z - point.X * point3.Z + point2.X * point3.Z) / 2;
			if (num <= 0)
			{
				return -1;
			}
			aEdgeOne = aEdgeOne.Next ?? aPolygonOne.First;
			aEdgeTwo = aEdgeTwo.Previous ?? aPolygonTwo.Last;
			point = _vertices[(aEdgeTwo.Previous ?? aPolygonTwo.Last).Value].Point;
			point2 = _vertices[aEdgeTwo.Value].Point;
			point3 = _vertices[(aEdgeOne.Next ?? aPolygonOne.First).Value].Point;
			num = -(-point2.X * point.Z + point3.X * point.Z + point.X * point2.Z - point3.X * point2.Z - point.X * point3.Z + point2.X * point3.Z) / 2;
			if (num <= 0)
			{
				return -1;
			}
			aEdgeOne = aEdgeOne.Previous ?? aPolygonOne.Last;
			aEdgeTwo = aEdgeTwo.Next ?? aPolygonTwo.First;
			return (_vertices[aEdgeOne.Value].Point - _vertices[(aEdgeOne.Next ?? aPolygonOne.First).Value].Point).GetLengthSquared();
		}

		private bool FindCommonEdge(LinkedList<int> aPolygonOne, LinkedList<int> aPolygonTwo, out LinkedListNode<int> aEdgeOne, out LinkedListNode<int> aEdgeTwo)
		{
			aEdgeOne = null;
			aEdgeTwo = null;
			for (LinkedListNode<int> linkedListNode = aPolygonOne.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				for (LinkedListNode<int> linkedListNode2 = aPolygonTwo.First; linkedListNode2 != null; linkedListNode2 = linkedListNode2.Next)
				{
					if (linkedListNode.Value == linkedListNode2.Value)
					{
						LinkedListNode<int> linkedListNode3 = linkedListNode.Next ?? linkedListNode.List.First;
						LinkedListNode<int> linkedListNode4 = linkedListNode2.Previous ?? linkedListNode2.List.Last;
						if (linkedListNode3.Value != linkedListNode4.Value)
						{
							break;
						}
						aEdgeOne = linkedListNode;
						aEdgeTwo = linkedListNode2;
						return true;
					}
				}
			}
			return false;
		}

		private LinkedList<int> MergePolysAlongEdge(LinkedListNode<int> aEdgeOne, LinkedListNode<int> aEdgeTwo)
		{
			LinkedList<int> linkedList = new LinkedList<int>();
			LinkedListNode<int> linkedListNode = aEdgeOne.Next ?? aEdgeOne.List.First;
			LinkedListNode<int> linkedListNode2 = aEdgeOne;
			while (linkedListNode.Value != linkedListNode2.Value)
			{
				linkedList.AddLast(linkedListNode.Value);
				linkedListNode = linkedListNode.Next ?? linkedListNode.List.First;
			}
			linkedListNode = aEdgeTwo;
			linkedListNode2 = aEdgeTwo.Previous ?? aEdgeTwo.List.Last;
			while (linkedListNode.Value != linkedListNode2.Value)
			{
				linkedList.AddLast(linkedListNode.Value);
				linkedListNode = linkedListNode.Next ?? linkedListNode.List.First;
			}
			return linkedList;
		}

		private void LinkContours(Point2 aLine, Point2 aOneQuadrant, Point2 aTwoQuadrant)
		{
			List<LinkedListNode<int>> list = new List<LinkedListNode<int>>();
			for (int i = 0; i < _contours[aOneQuadrant.X, aOneQuadrant.Y].Count; i++)
			{
				if (_contourBorders[aOneQuadrant.X, aOneQuadrant.Y][i])
				{
					GetLinePoints(aLine, true, _contours[aOneQuadrant.X, aOneQuadrant.Y][i], list);
				}
			}
			List<LinkedListNode<int>> list2 = new List<LinkedListNode<int>>();
			for (int j = 0; j < _contours[aTwoQuadrant.X, aTwoQuadrant.Y].Count; j++)
			{
				if (_contourBorders[aTwoQuadrant.X, aTwoQuadrant.Y][j])
				{
					GetLinePoints(aLine, false, _contours[aTwoQuadrant.X, aTwoQuadrant.Y][j], list2);
				}
			}
			for (int k = 0; k < list.Count; k += 2)
			{
				Point3 point = _vertices[list[k].Value].Point;
				Point3 point2 = _vertices[list[k + 1].Value].Point;
				for (int l = 0; l < list2.Count; l += 2)
				{
					Point3 point3 = _vertices[list2[l].Value].Point;
					Point3 point4 = _vertices[list2[l + 1].Value].Point;
					if ((aLine.X != int.MaxValue && point4.Z <= point.Z) || (aLine.Y != int.MaxValue && point4.X <= point.X))
					{
						continue;
					}
					if ((aLine.X != int.MaxValue && point3.Z >= point2.Z) || (aLine.Y != int.MaxValue && point3.X >= point2.X))
					{
						break;
					}
					LinkedListNode<int> aMatchOne = list[k];
					LinkedListNode<int> aMatchTwo = list2[l];
					if (LineupBorderContours(aLine, ref aMatchOne, ref aMatchTwo))
					{
						ZipBorderContours(aLine, ref aMatchOne, ref aMatchTwo);
						list[k] = aMatchOne;
						list2[l] = aMatchTwo;
						if (list[k].Value != list[k + 1].Value && list2[l].Value != list2[l + 1].Value)
						{
							list[k] = list[k].Next ?? list[k].List.First;
							list2[l] = list2[l].Previous ?? list2[l].List.Last;
						}
						if (list[k].Value == list[k + 1].Value)
						{
							list.RemoveAt(k);
							list.RemoveAt(k);
						}
						if (list2[l].Value == list2[l + 1].Value)
						{
							list2.RemoveAt(l);
							list2.RemoveAt(l);
						}
						k -= 2;
						break;
					}
				}
			}
			for (int m = 0; m < list.Count; m += 2)
			{
				LinkedListNode<int> linkedListNode = list[m].Next ?? list[m].List.First;
				while (linkedListNode.Value != list[m + 1].Value)
				{
					linkedListNode = linkedListNode.Next ?? linkedListNode.List.First;
					linkedListNode.List.Remove(linkedListNode.Previous ?? linkedListNode.List.Last);
				}
			}
			for (int n = 0; n < list2.Count; n += 2)
			{
				LinkedListNode<int> linkedListNode2 = list2[n].Previous ?? list2[n].List.Last;
				while (linkedListNode2.Value != list2[n + 1].Value)
				{
					linkedListNode2 = linkedListNode2.Previous ?? linkedListNode2.List.Last;
					linkedListNode2.List.Remove(linkedListNode2.Next ?? linkedListNode2.List.First);
				}
			}
		}

		private void SimplifyBorderEdge(Point2 aQuadrant)
		{
			List<LinkedListNode<int>> list = new List<LinkedListNode<int>>();
			List<LinkedListNode<int>> list2 = new List<LinkedListNode<int>>();
			for (int i = 0; i < _contours[aQuadrant.X, aQuadrant.Y].Count; i++)
			{
				if (_contourBorders[aQuadrant.X, aQuadrant.Y][i])
				{
					if (aQuadrant.X == 0)
					{
						GetLinePoints(new Point2(_quadrantMin[aQuadrant.X, aQuadrant.Y].X, int.MaxValue), true, _contours[aQuadrant.X, aQuadrant.Y][i], list);
					}
					if (aQuadrant.X == _contours.GetLength(0) - 1)
					{
						GetLinePoints(new Point2(_quadrantMax[aQuadrant.X, aQuadrant.Y].X, int.MaxValue), false, _contours[aQuadrant.X, aQuadrant.Y][i], list2);
					}
					if (aQuadrant.Y == 0)
					{
						GetLinePoints(new Point2(int.MaxValue, _quadrantMin[aQuadrant.X, aQuadrant.Y].Z), false, _contours[aQuadrant.X, aQuadrant.Y][i], list2);
					}
					if (aQuadrant.Y == _contours.GetLength(1) - 1)
					{
						GetLinePoints(new Point2(int.MaxValue, _quadrantMax[aQuadrant.X, aQuadrant.Y].Z), true, _contours[aQuadrant.X, aQuadrant.Y][i], list);
					}
				}
			}
			for (int j = 0; j < list.Count; j += 2)
			{
				LinkedListNode<int> linkedListNode = list[j].Next ?? list[j].List.First;
				while (linkedListNode.Value != list[j + 1].Value)
				{
					linkedListNode = linkedListNode.Next ?? linkedListNode.List.First;
					linkedListNode.List.Remove(linkedListNode.Previous ?? linkedListNode.List.Last);
				}
			}
			for (int k = 0; k < list2.Count; k += 2)
			{
				LinkedListNode<int> linkedListNode2 = list2[k].Previous ?? list2[k].List.Last;
				while (linkedListNode2.Value != list2[k + 1].Value)
				{
					linkedListNode2 = linkedListNode2.Previous ?? linkedListNode2.List.Last;
					linkedListNode2.List.Remove(linkedListNode2.Next ?? linkedListNode2.List.First);
				}
			}
		}

		private void GetLinePoints(Point2 aLine, bool aForward, LinkedList<int> aContour, List<LinkedListNode<int>> aPoints)
		{
			LinkedListNode<int> linkedListNode = null;
			for (LinkedListNode<int> linkedListNode2 = (aForward ? aContour.First : aContour.Last); linkedListNode2 != null; linkedListNode2 = (aForward ? linkedListNode2.Next : linkedListNode2.Previous))
			{
				Point3 point = _vertices[linkedListNode2.Value].Point;
				if ((aLine.X == int.MaxValue || point.X == aLine.X) && (aLine.Y == int.MaxValue || point.Z == aLine.Y))
				{
					while (true)
					{
						point = _vertices[linkedListNode2.Value].Point;
						Point3 point2 = ((!aForward) ? _vertices[(linkedListNode2.Next ?? linkedListNode2.List.First).Value].Point : _vertices[(linkedListNode2.Previous ?? linkedListNode2.List.Last).Value].Point);
						if ((aLine.X != int.MaxValue && (point2.X != aLine.X || point2.Z > point.Z)) || (aLine.Y != int.MaxValue && (point2.Z != aLine.Y || point2.X > point.X)))
						{
							break;
						}
						linkedListNode2 = ((!aForward) ? (linkedListNode2.Next ?? linkedListNode2.List.First) : (linkedListNode2.Previous ?? linkedListNode2.List.Last));
					}
					if (linkedListNode == null)
					{
						linkedListNode = linkedListNode2;
					}
					else if (linkedListNode2 == linkedListNode)
					{
						break;
					}
					LinkedListNode<int> linkedListNode3 = linkedListNode2;
					while (true)
					{
						point = _vertices[linkedListNode3.Value].Point;
						Point3 point3 = ((!aForward) ? _vertices[(linkedListNode3.Previous ?? linkedListNode3.List.Last).Value].Point : _vertices[(linkedListNode3.Next ?? linkedListNode3.List.First).Value].Point);
						if ((aLine.X != int.MaxValue && (point3.X != aLine.X || point3.Z < point.Z)) || (aLine.Y != int.MaxValue && (point3.Z != aLine.Y || point3.X < point.X)))
						{
							break;
						}
						linkedListNode3 = ((!aForward) ? (linkedListNode3.Previous ?? linkedListNode3.List.Last) : (linkedListNode3.Next ?? linkedListNode3.List.First));
					}
					if (linkedListNode2.Value != linkedListNode3.Value)
					{
						point = _vertices[linkedListNode2.Value].Point;
						int i;
						for (i = 0; i < aPoints.Count && (aLine.X == int.MaxValue || point.Z > _vertices[aPoints[i].Value].Point.Z) && (aLine.Y == int.MaxValue || point.X > _vertices[aPoints[i].Value].Point.X); i += 2)
						{
						}
						aPoints.Insert(i, linkedListNode3);
						aPoints.Insert(i, linkedListNode2);
						linkedListNode2 = linkedListNode3;
					}
				}
			}
		}

		private bool LineupBorderContours(Point2 aLine, ref LinkedListNode<int> aMatchOne, ref LinkedListNode<int> aMatchTwo)
		{
			int value = aMatchOne.Value;
			int value2 = aMatchTwo.Value;
			while (true)
			{
				Point3 point = _vertices[aMatchOne.Value].Point;
				Point3 point2 = _vertices[aMatchTwo.Value].Point;
				if ((aLine.X != int.MaxValue && point.X != aLine.X) || (aLine.Y != int.MaxValue && point.Z != aLine.Y) || (aLine.X != int.MaxValue && point2.X != aLine.X) || (aLine.Y != int.MaxValue && point2.Z != aLine.Y))
				{
					return false;
				}
				if ((aLine.X != int.MaxValue && point.Z < point2.Z) || (aLine.Y != int.MaxValue && point.X < point2.X))
				{
					aMatchOne = aMatchOne.Next ?? aMatchOne.List.First;
					if (aMatchOne.Value == value)
					{
						return false;
					}
					continue;
				}
				if ((aLine.X != int.MaxValue && point2.Z < point.Z) || (aLine.Y != int.MaxValue && point2.X < point.X))
				{
					aMatchTwo = aMatchTwo.Previous ?? aMatchTwo.List.Last;
					if (aMatchTwo.Value == value2)
					{
						return false;
					}
					continue;
				}
				if (point.Equals(point2))
				{
					break;
				}
				aMatchOne = aMatchOne.Next ?? aMatchOne.List.First;
				aMatchTwo = aMatchTwo.Previous ?? aMatchTwo.List.Last;
				if (aMatchOne.Value == value || aMatchTwo.Value == value2)
				{
					return false;
				}
			}
			return true;
		}

		private void ZipBorderContours(Point2 aLine, ref LinkedListNode<int> aMatchOne, ref LinkedListNode<int> aMatchTwo)
		{
			bool flag = true;
			while (true)
			{
				Point3 point = _vertices[(aMatchOne.Next ?? aMatchOne.List.First).Value].Point;
				Point3 point2 = _vertices[(aMatchTwo.Previous ?? aMatchTwo.List.Last).Value].Point;
				if (!point.Equals(point2) || aMatchOne.List.Count == 1 || aMatchTwo.List.Count == 1)
				{
					break;
				}
				aMatchOne = aMatchOne.Next ?? aMatchOne.List.First;
				aMatchTwo = aMatchTwo.Previous ?? aMatchTwo.List.Last;
				if (flag)
				{
					flag = false;
					continue;
				}
				aMatchOne.List.Remove(aMatchOne.Previous ?? aMatchOne.List.Last);
				aMatchTwo.List.Remove(aMatchTwo.Next ?? aMatchTwo.List.First);
			}
		}
	}
}
