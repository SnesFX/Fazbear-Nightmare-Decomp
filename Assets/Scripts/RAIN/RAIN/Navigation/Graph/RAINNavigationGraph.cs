using System;
using System.Collections.Generic;
using UnityEngine;

namespace RAIN.Navigation.Graph
{
	public abstract class RAINNavigationGraph
	{
		public string graphName = "";

		public List<string> tags = new List<string>();

		private float _minEdgeCost = 0.001f;

		private List<NavigationGraphNode> _pathNodes = new List<NavigationGraphNode>();

		public virtual float MinEdgeCost
		{
			get
			{
				return _minEdgeCost;
			}
			set
			{
				if (value > 0f)
				{
					_minEdgeCost = value;
				}
			}
		}

		public virtual int Size
		{
			get
			{
				return _pathNodes.Count;
			}
		}

		public RAINNavigationGraph()
		{
		}

		public RAINNavigationGraph(float minEdgeCost)
		{
			MinEdgeCost = minEdgeCost;
		}

		[Obsolete("Use QuantizeToNode instead")]
		public virtual int Quantize(Vector3 location, float aMaxYOffset)
		{
			return QuantizeToNode(location, aMaxYOffset).NodeIndex;
		}

		public abstract NavigationGraphNode QuantizeToNode(Vector3 location, float aMaxYOffset);

		[Obsolete("Use NavigationGraphNode Localize instead")]
		public virtual Vector3 Localize(int aNodeIndex)
		{
			return GetNode(aNodeIndex).Localize();
		}

		[Obsolete("Use NavigationGraphNode NodeIntersection instead")]
		public virtual Vector3 NodeIntersection(int nodeIndex, Vector3 position)
		{
			return GetNode(nodeIndex).NodeIntersection(position);
		}

		public virtual void AddNode(NavigationGraphNode aNode)
		{
			_pathNodes.Add(aNode);
		}

		public virtual NavigationGraphNode GetNode(int index)
		{
			return _pathNodes[index];
		}

		public virtual void ClearNodes()
		{
			for (int i = 0; i < _pathNodes.Count; i++)
			{
				_pathNodes[i].SetNodeChanged();
			}
			_pathNodes.Clear();
		}

		[Obsolete("Use AddEdge that accepts NavigationGraphNodes instead")]
		public void AddEdge(int fromIndex, int toIndex, float staticCost)
		{
			NavigationGraphEdge aEdge = new NavigationGraphEdge(GetNode(fromIndex), GetNode(toIndex), Mathf.Max(staticCost, MinEdgeCost));
			_pathNodes[fromIndex].AddEdgeOut(aEdge);
			_pathNodes[toIndex].AddEdgeIn(aEdge);
		}

		public void AddEdge(NavigationGraphNode aFromNode, NavigationGraphNode aToNode, float aStaticCost)
		{
			NavigationGraphEdge aEdge = new NavigationGraphEdge(aFromNode, aToNode, Mathf.Max(aStaticCost, MinEdgeCost));
			aFromNode.AddEdgeOut(aEdge);
			aToNode.AddEdgeIn(aEdge);
		}

		[Obsolete("Use HeuristicCost that accepts NavigationGraphNodes instead")]
		public virtual float HeuristicCost(int fromNodeIndex, int toNodeIndex)
		{
			return HeuristicCost(GetNode(fromNodeIndex), GetNode(toNodeIndex));
		}

		public virtual float HeuristicCost(NavigationGraphNode aFromNode, NavigationGraphNode aToNode)
		{
			return 0f;
		}

		public virtual Vector3 ClosestPointOnGraph(Vector3 aPosition, float aMaxYOffset = 0f)
		{
			return aPosition;
		}

		public virtual bool IsPointOnGraph(Vector3 aPoint, float aMaxYOffset = 0f)
		{
			return QuantizeToNode(aPoint, aMaxYOffset) != null;
		}
	}
}
