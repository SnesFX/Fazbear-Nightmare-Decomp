using System;
using System.Collections.Generic;
using System.IO;
using RAIN.Utility;
using UnityEngine;

namespace RAIN.Navigation.NavMesh
{
	public class NavMeshEdge : NavMeshNode
	{
		private NavMeshPathGraph _graph;

		private int[] _indices = new int[2];

		private int _pairing = -1;

		private List<NavMeshPoly> _polys = new List<NavMeshPoly>();

		public Vector3 PointOne
		{
			get
			{
				return _graph.Vertices[_indices[0]];
			}
		}

		public Vector3 PointTwo
		{
			get
			{
				return _graph.Vertices[_indices[1]];
			}
		}

		public Vector3 Center
		{
			get
			{
				return PointOne + (PointTwo - PointOne) / 2f;
			}
		}

		public int IndexOne
		{
			get
			{
				return _indices[0];
			}
		}

		public int IndexTwo
		{
			get
			{
				return _indices[1];
			}
		}

		public int Pairing
		{
			get
			{
				return _pairing;
			}
		}

		public int PolyCount
		{
			get
			{
				return _polys.Count;
			}
		}

		public NavMeshEdge(NavMeshPathGraph aGraph)
			: base(aGraph)
		{
			_graph = aGraph;
		}

		[Obsolete("Use NavMeshEdge(NavMeshPathGraph aGraph, int aIndexOne, int aIndexTwo) instead")]
		public NavMeshEdge(NavMeshPathGraph aGraph, int[] aIndices, int aNodeIndex)
			: this(aGraph, aIndices[0], aIndices[1])
		{
			_graph = aGraph;
			_indices = aIndices;
		}

		public NavMeshEdge(NavMeshPathGraph aGraph, int aIndexOne, int aIndexTwo)
			: base(aGraph)
		{
			_graph = aGraph;
			_indices[0] = aIndexOne;
			_indices[1] = aIndexTwo;
			_pairing = MathUtils.CommutativeCantorPairing(_indices[0], _indices[1]);
		}

		[Obsolete("Use AddPolyNode(NavMeshPoly aPoly) instead")]
		public void AddPoly(int aPoly)
		{
			AddPolyNode((NavMeshPoly)_graph.GetNode(aPoly));
		}

		public void AddPolyNode(NavMeshPoly aPoly)
		{
			_polys.Add(aPoly);
		}

		[Obsolete("Use GetPolyNode(int aIndex) instead")]
		public int GetPoly(int aIndex)
		{
			return GetPolyNode(aIndex).NodeIndex;
		}

		public NavMeshPoly GetPolyNode(int aIndex)
		{
			return _polys[aIndex];
		}

		[Obsolete("Use ContainsPolyNode(NavMeshPoly aPoly) instead")]
		public bool ContainsPoly(int aPoly)
		{
			return ContainsPolyNode((NavMeshPoly)_graph.GetNode(aPoly));
		}

		public bool ContainsPolyNode(NavMeshPoly aPoly)
		{
			return _polys.Contains(aPoly);
		}

		public override Vector3 Localize()
		{
			return Center;
		}

		public override Vector3 NodeIntersection(Vector3 aPosition)
		{
			return MathUtils.FindNearestPointOnLine(aPosition, PointOne, PointTwo);
		}

		public override void RemapVertices(int[] aVertexMap)
		{
			base.RemapVertices(aVertexMap);
			for (int i = 0; i < _indices.Length; i++)
			{
				_indices[i] = aVertexMap[_indices[i]];
			}
			_pairing = MathUtils.CommutativeCantorPairing(_indices[0], _indices[1]);
		}

		[Obsolete("No longer necessary")]
		public void RemapPolys(int[] aPolyMap)
		{
		}

		public override void Serialize(Dictionary<NavMeshNode, int> aNodeLookup, Stream aStream)
		{
			base.Serialize(aNodeLookup, aStream);
			BinaryWriter binaryWriter = new BinaryWriter(aStream);
			binaryWriter.Write(_indices.Length);
			for (int i = 0; i < _indices.Length; i++)
			{
				binaryWriter.Write(_indices[i]);
			}
			binaryWriter.Write(_polys.Count);
			for (int j = 0; j < _polys.Count; j++)
			{
				binaryWriter.Write(aNodeLookup[_polys[j]]);
			}
			binaryWriter.Write(_pairing);
			binaryWriter.Flush();
		}

		public override void Deserialize(NavMeshNode[] aIndexLookup, Stream aStream)
		{
			base.Deserialize(aIndexLookup, aStream);
			BinaryReader binaryReader = new BinaryReader(aStream);
			_indices = new int[binaryReader.ReadInt32()];
			for (int i = 0; i < _indices.Length; i++)
			{
				_indices[i] = binaryReader.ReadInt32();
			}
			int num = binaryReader.ReadInt32();
			_polys = new List<NavMeshPoly>(num);
			for (int j = 0; j < num; j++)
			{
				_polys.Add((NavMeshPoly)aIndexLookup[binaryReader.ReadInt32()]);
			}
			_pairing = binaryReader.ReadInt32();
		}
	}
}
