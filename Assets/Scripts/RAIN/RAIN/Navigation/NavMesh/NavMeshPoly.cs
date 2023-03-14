using System;
using System.Collections.Generic;
using System.IO;
using RAIN.Navigation.Graph;
using RAIN.Utility;
using UnityEngine;

namespace RAIN.Navigation.NavMesh
{
	public class NavMeshPoly : NavMeshNode
	{
		private NavMeshPathGraph _graph;

		private int[] _contour = new int[0];

		private int[] _contourPairing = new int[0];

		private int[] _triangles = new int[0];

		private List<NavMeshEdge> _edges = new List<NavMeshEdge>();

		private Vector3 _center = Vector3.zero;

		private Bounds _bounds = default(Bounds);

		private bool _unwalkable;

		private SimpleProfiler _profiler = SimpleProfiler.GetProfiler("NavMeshPathGraph");

		public int ContourCount
		{
			get
			{
				return _contour.Length;
			}
		}

		public int TriangleCount
		{
			get
			{
				return _triangles.Length / 3;
			}
		}

		public int EdgeCount
		{
			get
			{
				return _edges.Count;
			}
		}

		public Vector3 Center
		{
			get
			{
				return _center;
			}
		}

		public Bounds PolyBounds
		{
			get
			{
				return _bounds;
			}
		}

		public bool Unwalkable
		{
			get
			{
				return _unwalkable;
			}
		}

		public NavMeshPoly(NavMeshPathGraph aGraph)
			: base(aGraph)
		{
			_graph = aGraph;
		}

		public NavMeshPoly(NavMeshPathGraph aGraph, int[] aContour, int[] aTriangles)
			: base(aGraph)
		{
			_graph = aGraph;
			_contour = aContour;
			_contourPairing = new int[_contour.Length];
			for (int i = 0; i < _contour.Length; i++)
			{
				_contourPairing[i] = MathUtils.CommutativeCantorPairing(_contour[i], _contour[(i + 1) % _contour.Length]);
			}
			_triangles = aTriangles;
			_center = CalculateCenter();
			_bounds = new Bounds(_center, Vector3.zero);
			for (int j = 0; j < _contour.Length; j++)
			{
				_bounds.Encapsulate(aGraph.Vertices[_contour[j]]);
			}
		}

		public int GetContour(int aIndex)
		{
			return _contour[aIndex];
		}

		public int GetContourPairing(int aIndex)
		{
			return _contourPairing[aIndex];
		}

		public int GetTriangle(int aIndex)
		{
			return _triangles[aIndex];
		}

		[Obsolete("Use AddEdgeNode(NavMeshEdge aEdge) instead")]
		public void AddEdge(int aEdge)
		{
			AddEdgeNode((NavMeshEdge)_graph.GetNode(aEdge));
		}

		public void AddEdgeNode(NavMeshEdge aEdge)
		{
			if (aEdge.Pairing != _contourPairing[_edges.Count])
			{
				throw new Exception("Edge doesn't match contour");
			}
			_edges.Add(aEdge);
		}

		public bool ReplaceEdgeNode(NavMeshEdge aOldEdge, NavMeshEdge aNewEdge)
		{
			if (aOldEdge.Pairing != aNewEdge.Pairing)
			{
				throw new Exception("Edges don't match each other");
			}
			for (int i = 0; i < _edges.Count; i++)
			{
				if (_edges[i] == aOldEdge)
				{
					_edges[i] = aNewEdge;
					return true;
				}
			}
			return false;
		}

		[Obsolete("Use ContainsEdgeNode(NavMeshEdge aEdge) instead")]
		public bool ContainsEdge(int aEdge)
		{
			return ContainsEdgeNode((NavMeshEdge)_graph.GetNode(aEdge));
		}

		public bool ContainsEdgeNode(NavMeshEdge aEdge)
		{
			return _edges.Contains(aEdge);
		}

		[Obsolete("Use GetEdgeNode(int aIndex) instead")]
		public int GetEdge(int aIndex)
		{
			return GetEdgeNode(aIndex).NodeIndex;
		}

		public NavMeshEdge GetEdgeNode(int aIndex)
		{
			return _edges[aIndex];
		}

		public Vector3[] GetVertices()
		{
			Vector3[] array = new Vector3[_contour.Length];
			for (int i = 0; i < _contour.Length; i++)
			{
				array[i] = _graph.Vertices[_contour[i]];
			}
			return array;
		}

		public void ConnectAllEdges()
		{
			for (int i = 0; i < _edges.Count; i++)
			{
				for (int j = i + 1; j < _edges.Count; j++)
				{
					NavigationGraphEdge aEdge = new NavigationGraphEdge(_edges[i], _edges[j], (_edges[i].Center - _edges[j].Center).magnitude);
					_edges[i].AddEdgeOut(aEdge);
					_edges[j].AddEdgeIn(aEdge);
					NavigationGraphEdge aEdge2 = new NavigationGraphEdge(_edges[j], _edges[i], (_edges[j].Center - _edges[i].Center).magnitude);
					_edges[j].AddEdgeOut(aEdge2);
					_edges[i].AddEdgeIn(aEdge2);
				}
			}
		}

		public void DisconnectAllEdges()
		{
			for (int i = 0; i < _edges.Count; i++)
			{
				for (int j = i + 1; j < _edges.Count; j++)
				{
					_edges[i].RemoveAllEdgesTo(_edges[j]);
					_edges[i].RemoveAllEdgesFrom(_edges[j]);
					_edges[j].RemoveAllEdgesTo(_edges[i]);
					_edges[j].RemoveAllEdgesFrom(_edges[i]);
				}
			}
		}

		public void AddConnectedPoly()
		{
			AddConnectedPoly(Center);
		}

		public void AddConnectedPoly(Vector3 aEdgeConnectionPoint)
		{
			for (int i = 0; i < _edges.Count; i++)
			{
				NavigationGraphEdge aEdge = new NavigationGraphEdge(_edges[i], this, (_edges[i].Center - aEdgeConnectionPoint).magnitude);
				AddEdgeIn(aEdge);
				_edges[i].AddEdgeOut(aEdge);
				NavigationGraphEdge aEdge2 = new NavigationGraphEdge(this, _edges[i], (aEdgeConnectionPoint - _edges[i].Center).magnitude);
				AddEdgeOut(aEdge2);
				_edges[i].AddEdgeIn(aEdge2);
			}
		}

		public void RemoveConnectedPoly()
		{
			for (int i = 0; i < _edges.Count; i++)
			{
				RemoveAllEdgesFrom(_edges[i]);
				_edges[i].RemoveAllEdgesTo(this);
				RemoveAllEdgesTo(_edges[i]);
				_edges[i].RemoveAllEdgesFrom(this);
			}
		}

		public void MarkUnwalkable()
		{
			_unwalkable = true;
			for (int i = 0; i < _edges.Count; i++)
			{
				for (int j = 0; j < _edges[i].InEdgeCount; j++)
				{
					NavigationGraphEdge navigationGraphEdge = _edges[i].EdgeIn(j);
					if (navigationGraphEdge.FromNode is NavMeshEdge && ContainsEdgeNode((NavMeshEdge)navigationGraphEdge.FromNode))
					{
						navigationGraphEdge.OverrideCost = float.MaxValue;
					}
				}
				for (int k = 0; k < _edges[i].OutEdgeCount; k++)
				{
					NavigationGraphEdge navigationGraphEdge2 = _edges[i].EdgeOut(k);
					if (navigationGraphEdge2.ToNode is NavMeshEdge && ContainsEdgeNode((NavMeshEdge)navigationGraphEdge2.ToNode))
					{
						navigationGraphEdge2.OverrideCost = float.MaxValue;
					}
				}
			}
		}

		public void MarkWalkable()
		{
			_unwalkable = false;
			for (int i = 0; i < _edges.Count; i++)
			{
				for (int j = 0; j < _edges[i].InEdgeCount; j++)
				{
					NavigationGraphEdge navigationGraphEdge = _edges[i].EdgeIn(j);
					if (navigationGraphEdge.FromNode is NavMeshEdge && ContainsEdgeNode((NavMeshEdge)navigationGraphEdge.FromNode))
					{
						navigationGraphEdge.OverrideCost = -1f;
					}
				}
				for (int k = 0; k < _edges[i].OutEdgeCount; k++)
				{
					NavigationGraphEdge navigationGraphEdge2 = _edges[i].EdgeOut(k);
					if (navigationGraphEdge2.ToNode is NavMeshEdge && ContainsEdgeNode((NavMeshEdge)navigationGraphEdge2.ToNode))
					{
						navigationGraphEdge2.OverrideCost = -1f;
					}
				}
			}
		}

		public bool ContainsPoint(Vector3 point)
		{
			return ContainsPoint(_contour, point);
		}

		public bool GetYInterceptPoint(Vector3 point, out Vector3 intercept)
		{
			intercept = point;
			int i = 0;
			int[] array = new int[3];
			for (; i < _triangles.Length; i += 3)
			{
				Array.Copy(_triangles, i, array, 0, 3);
				if (ContainsPoint(array, point))
				{
					Plane plane = new Plane(_graph.Vertices[_triangles[i]], _graph.Vertices[_triangles[i + 1]], _graph.Vertices[_triangles[i + 2]]);
					Ray ray = new Ray(point, new Vector3(0f, -1f, 0f));
					Ray ray2 = new Ray(point, new Vector3(0f, 1f, 0f));
					float enter;
					if (plane.Raycast(ray, out enter))
					{
						intercept = ray.GetPoint(enter);
					}
					else if (plane.Raycast(ray2, out enter))
					{
						intercept = ray2.GetPoint(enter);
					}
					return true;
				}
			}
			return false;
		}

		public bool OverlapsPoly(Vector3[] testVerts)
		{
			for (int i = 0; i < _contour.Length; i++)
			{
				bool flag = true;
				int num = (i + 1) % _contour.Length;
				Vector3 line = _graph.Vertices[_contour[num]] - _graph.Vertices[_contour[i]];
				for (int j = 0; j < _contour.Length; j++)
				{
					if (i != j && num != j && !MathUtils.IsLineColinearXZ(line, _graph.Vertices[_contour[j]] - _graph.Vertices[_contour[i]]))
					{
						flag = MathUtils.IsLineLeftXZ(line, _graph.Vertices[_contour[j]] - _graph.Vertices[_contour[i]]);
						break;
					}
				}
				bool flag2 = false;
				for (int k = 0; k < testVerts.Length; k++)
				{
					bool flag3 = MathUtils.IsLineLeftXZ(line, testVerts[k] - _graph.Vertices[_contour[i]]);
					if (flag == flag3)
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					return false;
				}
			}
			for (int l = 0; l < testVerts.Length; l++)
			{
				bool flag4 = true;
				int num2 = (l + 1) % testVerts.Length;
				Vector3 line2 = testVerts[num2] - testVerts[l];
				for (int m = 0; m < testVerts.Length; m++)
				{
					if (m != l && m != num2 && !MathUtils.IsLineColinearXZ(line2, testVerts[m] - testVerts[l]))
					{
						flag4 = MathUtils.IsLineLeftXZ(line2, testVerts[m] - testVerts[l]);
						break;
					}
				}
				bool flag5 = false;
				for (int n = 0; n < _contour.Length; n++)
				{
					bool flag6 = MathUtils.IsLineLeftXZ(line2, _graph.Vertices[_contour[n]] - testVerts[l]);
					if (flag4 == flag6)
					{
						flag5 = true;
						break;
					}
				}
				if (!flag5)
				{
					return false;
				}
			}
			return true;
		}

		public override void SetNodeChanged()
		{
			base.SetNodeChanged();
			for (int i = 0; i < _edges.Count; i++)
			{
				_edges[i].SetNodeChanged();
			}
		}

		public override void UpdateChangedNode()
		{
			for (int i = 0; i < _edges.Count; i++)
			{
				if (_edges[i].PolyCount <= 1 || ((_edges[i].GetPolyNode(0) != this || !_edges[i].GetPolyNode(1).NodeChanged) && !_edges[i].GetPolyNode(0).NodeChanged))
				{
					_edges[i].UpdateChangedNode();
				}
			}
			base.UpdateChangedNode();
		}

		public override Vector3 Localize()
		{
			return Center;
		}

		public override Vector3 NodeIntersection(Vector3 aPosition)
		{
			return Center;
		}

		public override void RemapVertices(int[] aVertexMap)
		{
			base.RemapVertices(aVertexMap);
			for (int i = 0; i < _contour.Length; i++)
			{
				_contour[i] = aVertexMap[_contour[i]];
			}
			for (int j = 0; j < _triangles.Length; j++)
			{
				_triangles[j] = aVertexMap[_triangles[j]];
			}
		}

		[Obsolete("No longer necessary")]
		public void RemapEdges(int[] aEdgeMap)
		{
		}

		public override void Serialize(Dictionary<NavMeshNode, int> aNodeLookup, Stream aStream)
		{
			base.Serialize(aNodeLookup, aStream);
			BinaryWriter binaryWriter = new BinaryWriter(aStream);
			binaryWriter.Write(_contour.Length);
			for (int i = 0; i < _contour.Length; i++)
			{
				binaryWriter.Write(_contour[i]);
			}
			binaryWriter.Write(_triangles.Length);
			for (int j = 0; j < _triangles.Length; j++)
			{
				binaryWriter.Write(_triangles[j]);
			}
			binaryWriter.Write(_edges.Count);
			for (int k = 0; k < _edges.Count; k++)
			{
				binaryWriter.Write(aNodeLookup[_edges[k]]);
			}
			for (int l = 0; l < 3; l++)
			{
				binaryWriter.Write(_center[l]);
			}
			for (int m = 0; m < 3; m++)
			{
				binaryWriter.Write(_bounds.center[m]);
			}
			for (int n = 0; n < 3; n++)
			{
				binaryWriter.Write(_bounds.size[n]);
			}
			binaryWriter.Write(_unwalkable);
			binaryWriter.Flush();
		}

		public override void Deserialize(NavMeshNode[] aIndexLookup, Stream aStream)
		{
			base.Deserialize(aIndexLookup, aStream);
			BinaryReader binaryReader = new BinaryReader(aStream);
			_contour = new int[binaryReader.ReadInt32()];
			for (int i = 0; i < _contour.Length; i++)
			{
				_contour[i] = binaryReader.ReadInt32();
			}
			_triangles = new int[binaryReader.ReadInt32()];
			for (int j = 0; j < _triangles.Length; j++)
			{
				_triangles[j] = binaryReader.ReadInt32();
			}
			int num = binaryReader.ReadInt32();
			_edges = new List<NavMeshEdge>(num);
			for (int k = 0; k < num; k++)
			{
				_edges.Add((NavMeshEdge)_graph.GetNode(binaryReader.ReadInt32()));
			}
			_center = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
			_bounds = new Bounds(new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()), new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()));
			_unwalkable = binaryReader.ReadBoolean();
		}

		private bool ContainsPoint(int[] aContour, Vector3 point)
		{
			for (int i = 0; i < aContour.Length; i++)
			{
				if (MathUtils.IsLeftXZ(_graph.Vertices[aContour[i]], _graph.Vertices[aContour[(i + 1) % aContour.Length]], point))
				{
					return false;
				}
			}
			return true;
		}

		private Vector3 CalculateCenter()
		{
			float num = 0f;
			float num2 = 0f;
			Vector3 zero = Vector3.zero;
			for (int i = 0; i < _contour.Length; i++)
			{
				int num3 = i + 1;
				if (num3 == _contour.Length)
				{
					num3 = 0;
				}
				float x = _graph.Vertices[_contour[i]].x;
				float z = _graph.Vertices[_contour[i]].z;
				float x2 = _graph.Vertices[_contour[num3]].x;
				float z2 = _graph.Vertices[_contour[num3]].z;
				float num4 = x * z2 - x2 * z;
				zero.x += (x + x2) * num4;
				zero.z += (z + z2) * num4;
				num += num4;
				num2 += _graph.Vertices[i].y;
			}
			zero /= num * 3f;
			Vector3 intercept;
			GetYInterceptPoint(zero, out intercept);
			return intercept;
		}
	}
}
