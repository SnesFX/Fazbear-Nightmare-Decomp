using System.Collections.Generic;
using RAIN.Navigation.Graph;

namespace RAIN.Navigation.Pathfinding
{
	public class LPAPathFinder
	{
		private LPAPathNodeHelper _targetNodeHelper;

		private Dictionary<NavigationGraphNode, LPAPathNodeHelper> _nodes = new Dictionary<NavigationGraphNode, LPAPathNodeHelper>();

		private NavigationGraphNode _startNode;

		private NavigationGraphNode _goalNode;

		private PathGraphPriorityHeap _openNodes;

		private void InitializeSearch()
		{
			if (_openNodes == null)
			{
				_openNodes = new PathGraphPriorityHeap();
			}
			else
			{
				_openNodes.Reset();
			}
			_nodes.Clear();
			_targetNodeHelper = GetNode(_startNode);
			LPAPathNodeHelper node = GetNode(_goalNode);
			node.rhs = 0f;
			UpdateState(_goalNode);
		}

		public List<NavigationGraphNode> ConstructPath(NavigationGraphNode aStartNode, NavigationGraphNode aGoalNode, out float aCost)
		{
			aCost = 0f;
			if (aStartNode == null || aGoalNode == null)
			{
				return null;
			}
			_startNode = aStartNode;
			_goalNode = aGoalNode;
			InitializeSearch();
			ComputePath(-1);
			return PathNodes(out aCost);
		}

		public List<NavigationGraphNode> ConstructPath(NavigationGraphNode aStartNode, NavigationGraphNode aGoalNode, int aMaxIterations, bool aNewSearch, out float aCost, out bool aDone)
		{
			aCost = 0f;
			aDone = true;
			if (aStartNode == null || aGoalNode == null)
			{
				return null;
			}
			if (_startNode != aStartNode || _goalNode != aGoalNode)
			{
				aNewSearch = true;
			}
			if (aNewSearch)
			{
				_startNode = aStartNode;
				_goalNode = aGoalNode;
				InitializeSearch();
			}
			aDone = ComputePath(aMaxIterations);
			if (!aDone)
			{
				return null;
			}
			return PathNodes(out aCost);
		}

		private LPAPathNodeHelper GetNode(NavigationGraphNode aNode)
		{
			try
			{
				LPAPathNodeHelper value;
				if (!_nodes.TryGetValue(aNode, out value))
				{
					value = new LPAPathNodeHelper(aNode, _startNode);
					_nodes.Add(aNode, value);
				}
				return value;
			}
			catch
			{
			}
			return null;
		}

		public void UpdateState(NavigationGraphNode aPathNode)
		{
			LPAPathNodeHelper node = GetNode(aPathNode);
			if (aPathNode.NodeChanged)
			{
				aPathNode.UpdateChangedNode();
			}
			if (aPathNode != _goalNode)
			{
				float num = float.PositiveInfinity;
				for (int i = 0; i < aPathNode.OutEdgeCount; i++)
				{
					NavigationGraphNode toNode = aPathNode.EdgeOut(i).ToNode;
					LPAPathNodeHelper node2 = GetNode(toNode);
					float num2 = aPathNode.EdgeOut(i).Cost + node2.g;
					if (num2 < num)
					{
						num = num2;
					}
				}
				node.rhs = num;
			}
			if (node.State == PathNodeHelper.PathNodeHelperState.OPEN)
			{
				if (node.g != node.rhs)
				{
					_openNodes.Fix(node.HeapPosition);
					return;
				}
				_openNodes.Remove(node.HeapPosition);
				node.State = PathNodeHelper.PathNodeHelperState.CLOSED;
			}
			else if (node.g != node.rhs)
			{
				_openNodes.Add(node);
				node.State = PathNodeHelper.PathNodeHelperState.OPEN;
			}
		}

		public bool ComputePath(int maxIterations)
		{
			if (maxIterations == 0)
			{
				return true;
			}
			LPAPathNodeHelper lPAPathNodeHelper = (LPAPathNodeHelper)_openNodes.GetItem(0);
			while (lPAPathNodeHelper != null && (lPAPathNodeHelper.CompareTo(_targetNodeHelper) < 0 || _targetNodeHelper.g != _targetNodeHelper.rhs))
			{
				lPAPathNodeHelper = (LPAPathNodeHelper)_openNodes.Remove(0);
				lPAPathNodeHelper.State = PathNodeHelper.PathNodeHelperState.CLOSED;
				NavigationGraphNode node = lPAPathNodeHelper.Node;
				if (lPAPathNodeHelper.g > lPAPathNodeHelper.rhs)
				{
					lPAPathNodeHelper.g = lPAPathNodeHelper.rhs;
					for (int i = 0; i < node.InEdgeCount; i++)
					{
						UpdateState(node.EdgeIn(i).FromNode);
					}
				}
				else
				{
					lPAPathNodeHelper.g = float.PositiveInfinity;
					for (int j = 0; j < node.InEdgeCount; j++)
					{
						UpdateState(node.EdgeIn(j).FromNode);
					}
					UpdateState(lPAPathNodeHelper.Node);
				}
				lPAPathNodeHelper = (LPAPathNodeHelper)_openNodes.GetItem(0);
				maxIterations--;
				if (maxIterations == 0)
				{
					return false;
				}
			}
			return true;
		}

		public string PathString()
		{
			string text = "";
			float cost;
			List<NavigationGraphNode> list = PathNodes(out cost);
			for (int i = 0; i < list.Count; i++)
			{
				if (i > 0)
				{
					text += ",";
				}
				text += list[i];
			}
			return text;
		}

		public List<NavigationGraphNode> PathNodes(out float cost)
		{
			cost = 0f;
			List<NavigationGraphNode> list = new List<NavigationGraphNode>();
			if (_startNode == _goalNode)
			{
				list.Add(_startNode);
				list.Add(_startNode);
				return list;
			}
			LPAPathNodeHelper lPAPathNodeHelper = GetNode(_startNode);
			float g = lPAPathNodeHelper.g;
			int num = _startNode.Graph.Size;
			if (g == float.PositiveInfinity)
			{
				return list;
			}
			while (lPAPathNodeHelper.Node != _goalNode)
			{
				list.Add(lPAPathNodeHelper.Node);
				g = float.PositiveInfinity;
				NavigationGraphNode node = lPAPathNodeHelper.Node;
				for (int i = 0; i < node.OutEdgeCount; i++)
				{
					LPAPathNodeHelper node2 = GetNode(node.EdgeOut(i).ToNode);
					if (node2.g + node.EdgeOut(i).Cost < g)
					{
						g = node2.g + node.EdgeOut(i).Cost;
						lPAPathNodeHelper = node2;
					}
				}
				if (g == float.PositiveInfinity || --num <= 0)
				{
					lPAPathNodeHelper = null;
					break;
				}
				cost += g;
			}
			if (lPAPathNodeHelper != null)
			{
				list.Add(lPAPathNodeHelper.Node);
			}
			return list;
		}
	}
}
