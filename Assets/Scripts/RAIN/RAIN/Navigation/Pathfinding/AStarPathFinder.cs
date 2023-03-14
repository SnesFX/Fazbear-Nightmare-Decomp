using System.Collections.Generic;
using RAIN.Navigation.Graph;
using UnityEngine;

namespace RAIN.Navigation.Pathfinding
{
	public class AStarPathFinder
	{
		private NavigationGraphNode _fromNode;

		private NavigationGraphNode _toNode;

		private int _maxIterations = -1;

		private bool _inProgress;

		private Dictionary<NavigationGraphNode, AStarNode> _nodes = new Dictionary<NavigationGraphNode, AStarNode>();

		private PathGraphPriorityHeap _openHeap = new PathGraphPriorityHeap();

		public bool InProgress
		{
			get
			{
				return _inProgress;
			}
		}

		public bool FindPath(NavigationGraphNode aFromNode, NavigationGraphNode aToNode, int aMaxIterations, out List<NavigationGraphNode> aNodes, out List<Vector3> aPositions, out float aCost)
		{
			aCost = 0f;
			aNodes = new List<NavigationGraphNode>();
			aPositions = new List<Vector3>();
			if (_fromNode != aFromNode || _toNode != aToNode)
			{
				Reset();
				_fromNode = aFromNode;
				_toNode = aToNode;
				_maxIterations = aMaxIterations;
			}
			if (!_inProgress)
			{
				AStarNode node = GetNode(_fromNode);
				node.G = 0f;
				node.H = _fromNode.Graph.HeuristicCost(_fromNode, _toNode);
				node.State = PathNodeHelper.PathNodeHelperState.OPEN;
				_openHeap.Add(node);
			}
			int num = 0;
			AStarNode aStarNode;
			do
			{
				aStarNode = (AStarNode)_openHeap.GetItem(0);
				if (aStarNode == null || aStarNode.Node == _toNode)
				{
					break;
				}
				aStarNode.State = PathNodeHelper.PathNodeHelperState.CLOSED;
				_openHeap.Remove(aStarNode.HeapPosition);
				List<float> costs;
				List<AStarNode> neighborNodes = GetNeighborNodes(aStarNode.Node, out costs);
				for (int i = 0; i < neighborNodes.Count; i++)
				{
					AStarNode aStarNode2 = neighborNodes[i];
					if (1 == 0 || aStarNode2.State == PathNodeHelper.PathNodeHelperState.CLOSED)
					{
						continue;
					}
					if (aStarNode2.State == PathNodeHelper.PathNodeHelperState.OPEN)
					{
						float num2 = aStarNode.G + costs[i];
						if (num2 < aStarNode2.G)
						{
							aStarNode2.Parent = aStarNode;
							aStarNode2.G = num2;
							_openHeap.Fix(aStarNode2.HeapPosition);
						}
					}
					else
					{
						aStarNode2.Parent = aStarNode;
						aStarNode2.G = aStarNode.G + costs[i];
						aStarNode2.H = aStarNode2.Node.Graph.HeuristicCost(aStarNode2.Node, _toNode);
						aStarNode2.State = PathNodeHelper.PathNodeHelperState.OPEN;
						_openHeap.Add(aStarNode2);
					}
				}
				num++;
				if (aMaxIterations > 0 && num >= aMaxIterations)
				{
					_inProgress = true;
					return false;
				}
			}
			while (aStarNode != null);
			if (aStarNode != null)
			{
				aCost = aStarNode.G;
				aNodes.Add(aStarNode.Node);
				aPositions.Add(aStarNode.Position);
				AStarNode parent = aStarNode.Parent;
				if (parent == null)
				{
					aNodes.Add(aStarNode.Node);
					aPositions.Add(aStarNode.Position);
				}
				while (parent != null)
				{
					aNodes.Insert(0, parent.Node);
					aPositions.Insert(0, parent.Position);
					parent = parent.Parent;
				}
			}
			Reset();
			return true;
		}

		public bool ContinuePath(out List<NavigationGraphNode> aNodes, out List<Vector3> aPositions, out float aCost)
		{
			return FindPath(_fromNode, _toNode, _maxIterations, out aNodes, out aPositions, out aCost);
		}

		public void Reset()
		{
			_fromNode = null;
			_toNode = null;
			_maxIterations = -1;
			_inProgress = false;
			_nodes.Clear();
			_openHeap.Reset();
		}

		private List<AStarNode> GetNeighborNodes(NavigationGraphNode aNode, out List<float> costs)
		{
			List<AStarNode> list = new List<AStarNode>();
			costs = new List<float>();
			for (int i = 0; i < aNode.OutEdgeCount; i++)
			{
				NavigationGraphEdge navigationGraphEdge = aNode.EdgeOut(i);
				AStarNode node = GetNode(navigationGraphEdge.ToNode);
				list.Add(node);
				costs.Add(navigationGraphEdge.Cost);
			}
			return list;
		}

		private AStarNode GetNode(NavigationGraphNode aNode)
		{
			AStarNode value;
			if (!_nodes.TryGetValue(aNode, out value))
			{
				value = new AStarNode(aNode);
				value.Position = aNode.Localize();
				_nodes.Add(aNode, value);
			}
			return value;
		}
	}
}
