using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using RAIN.Navigation.Graph;
using RAIN.Navigation.NavMesh.RecastNodes;
using RAIN.Serialization;
using RAIN.Utility;
using UnityEngine;

namespace RAIN.Navigation.NavMesh
{
	[RAINSerializableClass("Serialize", "Deserialize")]
	public class NavMeshPathGraph : RAINNavigationGraph
	{
		private const int _serialVersion = 4;

		private List<Vector3> _vertices = new List<Vector3>();

		private Bounds _vertexBounds = default(Bounds);

		private float _cellSize;

		private float _stepHeight;

		private float _walkableHeight;

		private PolyBuckets _polyBuckets;

		private SimpleProfiler _profiler = SimpleProfiler.GetProfiler("NavMeshPathGraph");

		public IList<Vector3> Vertices
		{
			get
			{
				return _vertices.AsReadOnly();
			}
		}

		public Bounds VertexBounds
		{
			get
			{
				return _vertexBounds;
			}
		}

		public float CellSize
		{
			get
			{
				return _cellSize;
			}
		}

		public float StepHeight
		{
			get
			{
				return _stepHeight;
			}
		}

		public float WalkableHeight
		{
			get
			{
				return _walkableHeight;
			}
		}

		public void SetParams(float aCellSize, float aStepHeight, float aWalkableHeight)
		{
			_cellSize = aCellSize;
			_stepHeight = aStepHeight;
			_walkableHeight = aWalkableHeight;
		}

		public void InitPolyBuckets()
		{
			_polyBuckets = new PolyBuckets(_vertexBounds, _vertexBounds.size / 10f);
			for (int i = 0; i < Size; i++)
			{
				NavMeshPoly navMeshPoly = GetNode(i) as NavMeshPoly;
				if (navMeshPoly != null)
				{
					navMeshPoly.ConnectAllEdges();
					_polyBuckets.AddPoly(navMeshPoly);
				}
			}
		}

		public void AddVertices(Vector3[] aVertices)
		{
			if (aVertices.Length != 0)
			{
				if (_vertices.Count == 0)
				{
					_vertexBounds = new Bounds(aVertices[0], Vector3.zero);
				}
				for (int i = 0; i < aVertices.Length; i++)
				{
					_vertexBounds.Encapsulate(aVertices[i]);
				}
				_vertices.AddRange(aVertices);
			}
		}

		public void AddContourVertices(ContourMeshData aData)
		{
			if (aData.Vertices.Length == 0)
			{
				return;
			}
			int count = _vertices.Count;
			AddVertices(aData.Vertices);
			for (int i = 0; i < aData.Contours.Length; i++)
			{
				int[] array = new int[aData.Contours[i].Length];
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = count + aData.Contours[i][j];
				}
				int[] array2 = new int[aData.Contours[i].Length * 2];
				for (int k = 0; k < array2.Length; k += 2)
				{
					array2[k] = count + aData.Contours[i][k / 2];
					array2[k + 1] = count + aData.Contours[i][(k / 2 + 1) % aData.Contours[i].Length];
				}
				int[] array3 = new int[aData.Triangles[i].Length];
				for (int l = 0; l < array3.Length; l++)
				{
					array3[l] = count + aData.Triangles[i][l];
				}
				NavMeshPoly navMeshPoly = new NavMeshPoly(this, array, array3);
				AddNode(navMeshPoly);
				for (int m = 0; m < array2.Length; m += 2)
				{
					NavMeshEdge navMeshEdge = new NavMeshEdge(this, array2[m], array2[m + 1]);
					AddNode(navMeshEdge);
					navMeshPoly.AddEdgeNode(navMeshEdge);
					navMeshEdge.AddPolyNode(navMeshPoly);
				}
			}
		}

		public void CompactNavMesh()
		{
			if (_vertices.Count == 0)
			{
				return;
			}
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			int[] array = new int[_vertices.Count];
			List<Vector3> list = new List<Vector3>();
			new List<Point3>();
			for (int i = 0; i < _vertices.Count; i++)
			{
				string key = _vertices[i].x + " " + _vertices[i].y + " " + _vertices[i].z;
				int value;
				if (dictionary.TryGetValue(key, out value))
				{
					array[i] = value;
					continue;
				}
				dictionary[key] = list.Count;
				array[i] = list.Count;
				list.Add(_vertices[i]);
			}
			Bounds vertexBounds = new Bounds(list[0], Vector3.zero);
			for (int j = 1; j < list.Count; j++)
			{
				vertexBounds.Encapsulate(list[j]);
			}
			for (int k = 0; k < Size; k++)
			{
				((NavMeshNode)GetNode(k)).RemapVertices(array);
			}
			Dictionary<string, NavMeshEdge> dictionary2 = new Dictionary<string, NavMeshEdge>();
			List<NavMeshNode> list2 = new List<NavMeshNode>();
			for (int l = 0; l < Size; l++)
			{
				NavMeshNode navMeshNode = (NavMeshNode)GetNode(l);
				if (navMeshNode is NavMeshPoly)
				{
					list2.Add(navMeshNode);
					continue;
				}
				NavMeshEdge navMeshEdge = (NavMeshEdge)navMeshNode;
				string key2 = navMeshEdge.IndexOne + " " + navMeshEdge.IndexTwo;
				string key3 = navMeshEdge.IndexTwo + " " + navMeshEdge.IndexOne;
				NavMeshEdge value2;
				if (dictionary2.TryGetValue(key2, out value2) || dictionary2.TryGetValue(key3, out value2))
				{
					value2.AddPolyNode(navMeshEdge.GetPolyNode(0));
					navMeshEdge.GetPolyNode(0).ReplaceEdgeNode(navMeshEdge, value2);
				}
				else
				{
					dictionary2[key2] = navMeshEdge;
					dictionary2[key3] = navMeshEdge;
					list2.Add(navMeshNode);
				}
			}
			ClearNodes();
			_vertices = list;
			_vertexBounds = vertexBounds;
			for (int m = 0; m < list2.Count; m++)
			{
				AddNode(list2[m]);
				list2[m].UpdateChangedNode();
			}
		}

		[Obsolete("No longer necessary")]
		public void AddConnectedPoly(int nodeIndex)
		{
		}

		[Obsolete("No longer necessary")]
		public void RemoveConnectedPoly(int nodeIndex)
		{
		}

		public override void ClearNodes()
		{
			_vertices.Clear();
			_vertexBounds = default(Bounds);
			if (_polyBuckets != null)
			{
				_polyBuckets.Clear();
			}
			base.ClearNodes();
		}

		public override float HeuristicCost(NavigationGraphNode aFromNode, NavigationGraphNode aToNode)
		{
			return (aFromNode.Localize() - aToNode.Localize()).magnitude;
		}

		public override NavigationGraphNode QuantizeToNode(Vector3 aLocation, float aMaxYOffset)
		{
			if (_polyBuckets == null)
			{
				InitPolyBuckets();
			}
			float aMaxYOffset2 = Mathf.Max(aMaxYOffset, _walkableHeight);
			NavMeshPoly navMeshPoly = _polyBuckets.PolyForPoint(aLocation, aMaxYOffset2);
			if (navMeshPoly == null)
			{
				return null;
			}
			return navMeshPoly;
		}

		public List<NavigationGraphNode> QuantizeToNode(Vector3 aLocation, float aMaxYOffset, float aSize)
		{
			if (_polyBuckets == null)
			{
				InitPolyBuckets();
			}
			List<NavMeshPoly> list = _polyBuckets.PolysForPoint(aLocation, aSize);
			List<NavigationGraphNode> list2 = new List<NavigationGraphNode>(list.Count);
			float num = aLocation.y + Mathf.Max(aMaxYOffset, _walkableHeight);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Center.y < num)
				{
					list2.Add(list[i]);
				}
			}
			return list2;
		}

		public override Vector3 ClosestPointOnGraph(Vector3 aPosition, float aMaxYOffset = 0f)
		{
			NavMeshPoly navMeshPoly = QuantizeToNode(aPosition, aMaxYOffset) as NavMeshPoly;
			if (navMeshPoly == null)
			{
				return aPosition;
			}
			Vector3 intercept;
			navMeshPoly.GetYInterceptPoint(aPosition, out intercept);
			return intercept;
		}

		public byte[] Serialize()
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(4);
			binaryWriter.Write(_vertices.Count);
			for (int i = 0; i < _vertices.Count; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					binaryWriter.Write(_vertices[i][j]);
				}
			}
			for (int k = 0; k < 3; k++)
			{
				binaryWriter.Write(_vertexBounds.center[k]);
			}
			for (int l = 0; l < 3; l++)
			{
				binaryWriter.Write(_vertexBounds.size[l]);
			}
			binaryWriter.Write(graphName);
			binaryWriter.Write(tags.Count);
			for (int m = 0; m < tags.Count; m++)
			{
				binaryWriter.Write(tags[m]);
			}
			binaryWriter.Write(MinEdgeCost);
			binaryWriter.Write(Size);
			Dictionary<NavMeshNode, int> dictionary = new Dictionary<NavMeshNode, int>();
			for (int n = 0; n < Size; n++)
			{
				NavMeshNode navMeshNode = (NavMeshNode)GetNode(n);
				if (navMeshNode is NavMeshPoly)
				{
					binaryWriter.Write(0);
				}
				else
				{
					binaryWriter.Write(1);
				}
				dictionary[navMeshNode] = n;
			}
			for (int num = 0; num < Size; num++)
			{
				((NavMeshNode)GetNode(num)).Serialize(dictionary, binaryWriter.BaseStream);
			}
			binaryWriter.Flush();
			return memoryStream.ToArray();
		}

		public void Deserialize(byte[] aData)
		{
			ClearNodes();
			MemoryStream input = new MemoryStream(aData);
			BinaryReader binaryReader = new BinaryReader(input);
			int num = binaryReader.ReadInt32();
			if (4 != num)
			{
				UnityEngine.Debug.LogWarning("NavMeshPathGraph: versions don't match, need to regenerate Navigation Mesh");
				return;
			}
			int num2 = binaryReader.ReadInt32();
			_vertices = new List<Vector3>(num2);
			for (int i = 0; i < num2; i++)
			{
				_vertices.Add(new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()));
			}
			_vertexBounds = new Bounds(new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()), new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle()));
			graphName = binaryReader.ReadString();
			num2 = binaryReader.ReadInt32();
			tags = new List<string>(num2);
			for (int j = 0; j < num2; j++)
			{
				tags.Add(binaryReader.ReadString());
			}
			MinEdgeCost = binaryReader.ReadSingle();
			num2 = binaryReader.ReadInt32();
			NavMeshNode[] array = new NavMeshNode[num2];
			for (int k = 0; k < num2; k++)
			{
				NavMeshNode navMeshNode = ((binaryReader.ReadInt32() != 0) ? ((NavMeshNode)new NavMeshEdge(this)) : ((NavMeshNode)new NavMeshPoly(this)));
				AddNode(navMeshNode);
				array[k] = navMeshNode;
			}
			for (int l = 0; l < num2; l++)
			{
				((NavMeshNode)GetNode(l)).Deserialize(array, binaryReader.BaseStream);
			}
		}

		[Conditional("DEBUG")]
		internal void Verify()
		{
			UnityEngine.Debug.Log("Verifying NavMesh");
			for (int i = 0; i < Size; i++)
			{
				NavigationGraphNode node = GetNode(i);
				if (node is NavMeshPoly)
				{
					NavMeshPoly navMeshPoly = (NavMeshPoly)node;
					for (int j = 0; j < navMeshPoly.EdgeCount; j++)
					{
						NavMeshEdge edgeNode = navMeshPoly.GetEdgeNode(j);
						if (!edgeNode.ContainsPolyNode(navMeshPoly))
						{
							throw new Exception("Poly has Edge but Edge doesn't have Poly");
						}
						for (int k = j + 1; k < navMeshPoly.EdgeCount; k++)
						{
							if (navMeshPoly.GetEdgeNode(j) == navMeshPoly.GetEdgeNode(k))
							{
								throw new Exception("Duplicate edge in poly");
							}
						}
						bool flag = false;
						for (int l = 0; l < navMeshPoly.ContourCount; l++)
						{
							if ((navMeshPoly.GetContour(l) == edgeNode.IndexOne && navMeshPoly.GetContour((l + 1) % navMeshPoly.ContourCount) == edgeNode.IndexTwo) || (navMeshPoly.GetContour(l) == edgeNode.IndexTwo && navMeshPoly.GetContour((l + 1) % navMeshPoly.ContourCount) == edgeNode.IndexOne))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							throw new Exception("Edge and Contour don't line up");
						}
					}
					for (int m = 0; m < navMeshPoly.ContourCount; m++)
					{
						for (int n = m + 1; n < navMeshPoly.ContourCount; n++)
						{
							if (navMeshPoly.GetContour(m) == navMeshPoly.GetContour(n))
							{
								throw new Exception("Contour has duplicate points");
							}
						}
					}
					continue;
				}
				NavMeshEdge navMeshEdge = (NavMeshEdge)node;
				if (navMeshEdge.PolyCount == 0 || navMeshEdge.PolyCount > 2)
				{
					throw new Exception("Bad edge");
				}
				for (int num = 0; num < navMeshEdge.PolyCount; num++)
				{
					NavMeshPoly polyNode = navMeshEdge.GetPolyNode(0);
					if (!polyNode.ContainsEdgeNode(navMeshEdge))
					{
						throw new Exception("Edge has Poly but Poly doesn't have Edge");
					}
					bool flag2 = false;
					for (int num2 = 0; num2 < polyNode.ContourCount; num2++)
					{
						if ((polyNode.GetContour(num2) == navMeshEdge.IndexOne && polyNode.GetContour((num2 + 1) % polyNode.ContourCount) == navMeshEdge.IndexTwo) || (polyNode.GetContour(num2) == navMeshEdge.IndexTwo && polyNode.GetContour((num2 + 1) % polyNode.ContourCount) == navMeshEdge.IndexOne))
						{
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						throw new Exception("Edge and Contour don't line up");
					}
				}
				for (int num3 = i + 1; num3 < Size; num3++)
				{
					NavMeshEdge navMeshEdge2 = GetNode(num3) as NavMeshEdge;
					if (navMeshEdge2 != null && ((navMeshEdge.IndexOne == navMeshEdge2.IndexOne && navMeshEdge.IndexTwo == navMeshEdge2.IndexTwo) || (navMeshEdge.IndexOne == navMeshEdge2.IndexTwo && navMeshEdge.IndexTwo == navMeshEdge2.IndexOne)))
					{
						throw new Exception("Duplicate edge");
					}
				}
			}
		}
	}
}
