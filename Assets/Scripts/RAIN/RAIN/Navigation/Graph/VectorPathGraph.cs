using System;
using UnityEngine;

namespace RAIN.Navigation.Graph
{
	public class VectorPathGraph : RAINNavigationGraph
	{
		public VectorPathGraph()
		{
		}

		public VectorPathGraph(Vector3[] points)
		{
			if (points != null)
			{
				for (int i = 0; i < points.Length; i++)
				{
					AddNode(new VectorPathNode(this, i, points[i]));
				}
			}
		}

		[Obsolete("Use AddTwoWayEdge that accepts NavigationGraphNodes instead")]
		public void AddTwoWayEdge(int index1, int index2)
		{
			AddTwoWayEdge(GetNode(index1), GetNode(index2));
		}

		[Obsolete("Use AddEdge that accepts NavigationGraphNodes instead")]
		public void AddEdge(int fromIndex, int toIndex)
		{
			AddEdge(GetNode(fromIndex), GetNode(toIndex));
		}

		public void AddTwoWayEdge(NavigationGraphNode aNodeOne, NavigationGraphNode aNodeTwo)
		{
			float magnitude = (aNodeOne.Localize() - aNodeTwo.Localize()).magnitude;
			AddEdge(aNodeOne, aNodeTwo, magnitude);
			AddEdge(aNodeTwo, aNodeOne, magnitude);
		}

		public void AddEdge(NavigationGraphNode aFromNode, NavigationGraphNode aToNode)
		{
			float magnitude = (aFromNode.Localize() - aToNode.Localize()).magnitude;
			AddEdge(aFromNode, aToNode, magnitude);
		}

		public override float HeuristicCost(NavigationGraphNode aFromNode, NavigationGraphNode aToNode)
		{
			return (aFromNode.Localize() - aToNode.Localize()).magnitude;
		}

		public override NavigationGraphNode QuantizeToNode(Vector3 location, float aMaxYOffset)
		{
			VectorPathNode result = null;
			float num = float.MaxValue;
			for (int i = 0; i < Size; i++)
			{
				VectorPathNode vectorPathNode = (VectorPathNode)GetNode(i);
				float magnitude = (location - vectorPathNode.Localize()).magnitude;
				if (magnitude < num)
				{
					result = vectorPathNode;
					num = magnitude;
				}
			}
			return result;
		}
	}
}
