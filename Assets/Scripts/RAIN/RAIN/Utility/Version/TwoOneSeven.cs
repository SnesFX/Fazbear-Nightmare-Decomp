using System;
using System.Collections.Generic;
using System.IO;
using RAIN.Memory;
using RAIN.Navigation.NavMesh;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Utility.Version
{
	public class TwoOneSeven
	{
		[RAINSerializableClass("Serialize", "Deserialize")]
		public class InterimNavMeshPathGraph
		{
			private string _graphName = "";

			private List<string> _tags = new List<string>();

			private float _minEdgeCost = 0.001f;

			private List<Vector3> _vertices = new List<Vector3>();

			private List<object> _interimNodes = new List<object>();

			public NavMeshPathGraph GetNavMeshPathGraph()
			{
				NavMeshPathGraph navMeshPathGraph = new NavMeshPathGraph();
				navMeshPathGraph.graphName = _graphName;
				navMeshPathGraph.tags = _tags;
				navMeshPathGraph.MinEdgeCost = _minEdgeCost;
				navMeshPathGraph.AddVertices(_vertices.ToArray());
				List<NavMeshNode> list = new List<NavMeshNode>();
				for (int i = 0; i < _interimNodes.Count; i++)
				{
					if (_interimNodes[i] is InterimNavMeshPoly)
					{
						list.Add(((InterimNavMeshPoly)_interimNodes[i]).GetPoly(navMeshPathGraph));
					}
					else
					{
						list.Add(((InterimNavMeshEdge)_interimNodes[i]).GetEdge(navMeshPathGraph));
					}
				}
				for (int j = 0; j < _interimNodes.Count; j++)
				{
					if (_interimNodes[j] is InterimNavMeshPoly)
					{
						NavMeshPoly navMeshPoly = (NavMeshPoly)list[j];
						List<NavMeshEdge> edges = ((InterimNavMeshPoly)_interimNodes[j]).GetEdges(list);
						for (int k = 0; k < edges.Count; k++)
						{
							navMeshPoly.AddEdgeNode(edges[k]);
							edges[k].AddPolyNode(navMeshPoly);
						}
					}
				}
				for (int l = 0; l < list.Count; l++)
				{
					navMeshPathGraph.AddNode(list[l]);
				}
				return navMeshPathGraph;
			}

			public byte[] Serialize()
			{
				throw new NotImplementedException();
			}

			private void Deserialize(byte[] aData)
			{
				MemoryStream input = new MemoryStream(aData);
				BinaryReader binaryReader = new BinaryReader(input);
				binaryReader.ReadInt32();
				int num = binaryReader.ReadInt32();
				_vertices = new List<Vector3>(num);
				for (int i = 0; i < num; i++)
				{
					_vertices.Add(new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()));
				}
				binaryReader.ReadSingle();
				binaryReader.ReadSingle();
				_graphName = binaryReader.ReadString();
				num = binaryReader.ReadInt32();
				_tags = new List<string>(num);
				for (int j = 0; j < num; j++)
				{
					_tags.Add(binaryReader.ReadString());
				}
				_minEdgeCost = binaryReader.ReadSingle();
				num = binaryReader.ReadInt32();
				for (int k = 0; k < num; k++)
				{
					int num2 = binaryReader.ReadInt32();
					byte[] array = new byte[binaryReader.ReadInt32()];
					binaryReader.Read(array, 0, array.Length);
					if (num2 == 0)
					{
						InterimNavMeshPoly interimNavMeshPoly = new InterimNavMeshPoly();
						interimNavMeshPoly.Deserialize(array);
						_interimNodes.Add(interimNavMeshPoly);
					}
					else
					{
						InterimNavMeshEdge interimNavMeshEdge = new InterimNavMeshEdge();
						interimNavMeshEdge.Deserialize(array);
						_interimNodes.Add(interimNavMeshEdge);
					}
				}
			}
		}

		private class InterimNavMeshPoly
		{
			private int[] _contour = new int[0];

			private int[] _triangles = new int[0];

			private List<int> _edges = new List<int>();

			public void Deserialize(byte[] aData)
			{
				MemoryStream input = new MemoryStream(aData);
				BinaryReader binaryReader = new BinaryReader(input);
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
				_edges = new List<int>(num);
				for (int k = 0; k < num; k++)
				{
					_edges.Add(binaryReader.ReadInt32());
				}
				binaryReader.ReadBoolean();
				binaryReader.ReadSingle();
				binaryReader.ReadSingle();
				binaryReader.ReadSingle();
				binaryReader.ReadInt32();
			}

			public NavMeshPoly GetPoly(NavMeshPathGraph aGraph)
			{
				return new NavMeshPoly(aGraph, _contour, _triangles);
			}

			public List<NavMeshEdge> GetEdges(List<NavMeshNode> aNodes)
			{
				List<NavMeshEdge> list = new List<NavMeshEdge>();
				for (int i = 0; i < _edges.Count; i++)
				{
					list.Add((NavMeshEdge)aNodes[_edges[i]]);
				}
				return list;
			}
		}

		private class InterimNavMeshEdge
		{
			private int[] _indices = new int[2];

			public void Deserialize(byte[] aData)
			{
				MemoryStream input = new MemoryStream(aData);
				BinaryReader binaryReader = new BinaryReader(input);
				_indices = new int[binaryReader.ReadInt32()];
				for (int i = 0; i < _indices.Length; i++)
				{
					_indices[i] = binaryReader.ReadInt32();
				}
				int num = binaryReader.ReadInt32();
				for (int j = 0; j < num; j++)
				{
					binaryReader.ReadInt32();
				}
				binaryReader.ReadInt32();
			}

			public NavMeshEdge GetEdge(NavMeshPathGraph aGraph)
			{
				return new NavMeshEdge(aGraph, _indices[0], _indices[1]);
			}
		}

		public void UpgradeNavMeshPathGraph(FieldSerializer aRigSerializer)
		{
			aRigSerializer.ChangeAllTypes("RAIN.Navigation.NavMesh.NavMeshPathGraph", typeof(InterimNavMeshPathGraph).ToString());
			List<ObjectElement> fieldElementsForType = aRigSerializer.GetFieldElementsForType(typeof(InterimNavMeshPathGraph).ToString());
			for (int i = 0; i < fieldElementsForType.Count; i++)
			{
				aRigSerializer.HoldReferences = true;
				object aResult;
				if (aRigSerializer.DeserializeObjectFromElement(fieldElementsForType[i], typeof(InterimNavMeshPathGraph), null, out aResult))
				{
					aRigSerializer.SerializeObjectToElement(fieldElementsForType[i], typeof(BasicMemory), ((InterimNavMeshPathGraph)aResult).GetNavMeshPathGraph(), true);
				}
				aRigSerializer.HoldReferences = false;
			}
		}
	}
}
