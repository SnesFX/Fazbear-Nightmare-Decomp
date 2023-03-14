using System;
using System.Collections.Generic;
using RAIN.Navigation.Graph;
using UnityEngine;

namespace RAIN.Navigation.Pathfinding
{
	public class HeuristicPathFinder
	{
		public class HeuristicPathFinderNodeHelper : PathNodeHelper
		{
			private HeuristicPathFinderNodeHelper previousNode;

			private float heuristicCostToGoal;

			private float actualCostFromStart;

			private float offMeshRating;

			private Vector3 offMeshIntersection = Vector3.zero;

			private Vector3 position;

			public Vector3 Position
			{
				get
				{
					return position;
				}
				set
				{
					position = value;
				}
			}

			public HeuristicPathFinderNodeHelper PreviousNode
			{
				get
				{
					return previousNode;
				}
				set
				{
					previousNode = value;
				}
			}

			public virtual float HeuristicCostToGoal
			{
				get
				{
					return heuristicCostToGoal;
				}
				set
				{
					heuristicCostToGoal = value;
				}
			}

			public virtual float ActualCostFromStart
			{
				get
				{
					return actualCostFromStart;
				}
				set
				{
					actualCostFromStart = value;
				}
			}

			public virtual Vector3 OffMeshIntersection
			{
				get
				{
					return offMeshIntersection;
				}
				set
				{
					offMeshIntersection = value;
				}
			}

			public virtual float OffMeshRating
			{
				get
				{
					return offMeshRating;
				}
				set
				{
					offMeshRating = value;
				}
			}

			public virtual float TotalHeuristicCost()
			{
				float num = heuristicCostToGoal + actualCostFromStart;
				if (num < 0f)
				{
					num = 0f;
				}
				return num;
			}

			public void CalculateOffMeshRating(Vector3 goalPosition)
			{
				OffMeshIntersection = base.Node.NodeIntersection(goalPosition);
				OffMeshRating = Vector3.Distance(OffMeshIntersection, goalPosition);
			}

			public override int CompareTo(PathNodeHelper aCompareTo)
			{
				HeuristicPathFinderNodeHelper heuristicPathFinderNodeHelper = aCompareTo as HeuristicPathFinderNodeHelper;
				if (heuristicPathFinderNodeHelper == null)
				{
					throw new Exception("Invalid search, attempted to compare HeuristicSearchNodeHelper to an invalid type");
				}
				float num = TotalHeuristicCost();
				float num2 = heuristicPathFinderNodeHelper.TotalHeuristicCost();
				if (num < num2)
				{
					return -1;
				}
				if (num > num2)
				{
					return 1;
				}
				return 0;
			}

			public HeuristicPathFinderNodeHelper(NavigationGraphNode aGraphNode)
				: base(aGraphNode)
			{
			}
		}

		private Dictionary<NavigationGraphNode, HeuristicPathFinderNodeHelper> _nodes = new Dictionary<NavigationGraphNode, HeuristicPathFinderNodeHelper>();

		private PathGraphPriorityHeap _openNodes = new PathGraphPriorityHeap();

		private Vector3 _startPosition;

		private Vector3 _goalPosition;

		private HeuristicPathFinderNodeHelper _bestNode;

		private NavigationGraphNode _startNode;

		private NavigationGraphNode _goalNode;

		private bool _inProgress;

		private float _maxPathLength;

		private bool _lastPathSuccessful;

		private bool _reversePath;

		public bool LastPathSuccessful
		{
			get
			{
				return _lastPathSuccessful;
			}
		}

		public bool ReversePath
		{
			get
			{
				return _reversePath;
			}
		}

		public NavigationGraphNode StartNode
		{
			get
			{
				return _startNode;
			}
		}

		public NavigationGraphNode GoalNode
		{
			get
			{
				return _goalNode;
			}
		}

		public Vector3 StartPosition
		{
			get
			{
				return _startPosition;
			}
		}

		public Vector3 GoalPosition
		{
			get
			{
				return _goalPosition;
			}
		}

		public bool InProgress
		{
			get
			{
				return _inProgress;
			}
		}

		[Obsolete("Use InitializePathfinding(RAINNavigationGraph, NavigationGraphNode, NavigationGraphNode, Vector3, Vector3, float, bool) instead]")]
		public void InitializePathfinding(RAINNavigationGraph graph, NavigationGraphNode startNode, NavigationGraphNode goalNode, Vector3 goalPosition, float maxPathLength, bool reversePath)
		{
			Vector3 startPosition = Vector3.zero;
			if (_startNode != null)
			{
				startPosition = _startNode.Localize();
			}
			InitializePathfinding(graph, startNode, goalNode, startPosition, goalPosition, maxPathLength, reversePath);
		}

		public void InitializePathfinding(RAINNavigationGraph graph, NavigationGraphNode startNode, NavigationGraphNode goalNode, Vector3 startPosition, Vector3 goalPosition, float maxPathLength, bool reversePath)
		{
			_lastPathSuccessful = false;
			_bestNode = null;
			_inProgress = true;
			_startPosition = startPosition;
			_goalPosition = goalPosition;
			_maxPathLength = maxPathLength;
			_startNode = startNode;
			_goalNode = goalNode;
			_reversePath = reversePath;
			if (_startNode != null)
			{
				_openNodes.Reset();
				_nodes.Clear();
				HeuristicPathFinderNodeHelper node = GetNode(_startNode);
				node.ActualCostFromStart = 0f;
				if (_goalNode == null)
				{
					node.HeuristicCostToGoal = (_goalPosition - _startPosition).magnitude;
				}
				else
				{
					node.HeuristicCostToGoal = _startNode.Graph.HeuristicCost(_startNode, _goalNode);
				}
				node.State = PathNodeHelper.PathNodeHelperState.OPEN;
				_openNodes.Add(node);
			}
		}

		public void Reset()
		{
			_startNode = null;
			_goalNode = null;
			_inProgress = false;
			_nodes.Clear();
			_openNodes.Reset();
		}

		public bool ComputePath(int aMaxIterations, out List<NavigationGraphNode> nodes, out List<Vector3> positions, out float cost)
		{
			bool flag = false;
			nodes = null;
			positions = null;
			cost = 0f;
			_lastPathSuccessful = false;
			do
			{
				aMaxIterations--;
				HeuristicPathFinderNodeHelper heuristicPathFinderNodeHelper = _openNodes.Remove(0) as HeuristicPathFinderNodeHelper;
				if (heuristicPathFinderNodeHelper == null)
				{
					flag = true;
					break;
				}
				heuristicPathFinderNodeHelper.State = PathNodeHelper.PathNodeHelperState.CLOSED;
				if (heuristicPathFinderNodeHelper.Node == _goalNode)
				{
					flag = true;
					_bestNode = heuristicPathFinderNodeHelper;
					_lastPathSuccessful = true;
					break;
				}
				NavigationGraphNode node = heuristicPathFinderNodeHelper.Node;
				if (node == null)
				{
					continue;
				}
				if (node.NodeChanged)
				{
					node.UpdateChangedNode();
				}
				float actualCostFromStart = heuristicPathFinderNodeHelper.ActualCostFromStart;
				for (int i = 0; i < node.OutEdgeCount; i++)
				{
					NavigationGraphEdge navigationGraphEdge = node.EdgeOut(i);
					HeuristicPathFinderNodeHelper node2 = GetNode(navigationGraphEdge.ToNode);
					if (navigationGraphEdge == null)
					{
						continue;
					}
					float num = navigationGraphEdge.Cost;
					if (num <= 0f)
					{
						num = 1E-05f;
					}
					if (node2.State != PathNodeHelper.PathNodeHelperState.UNVISITED && !(node2.ActualCostFromStart > actualCostFromStart + num))
					{
						continue;
					}
					node2.PreviousNode = heuristicPathFinderNodeHelper;
					node2.ActualCostFromStart = actualCostFromStart + num;
					if (_maxPathLength <= 0f || node2.ActualCostFromStart <= _maxPathLength)
					{
						if (node2.State == PathNodeHelper.PathNodeHelperState.OPEN)
						{
							_openNodes.Fix(node2.HeapPosition);
							continue;
						}
						node2.State = PathNodeHelper.PathNodeHelperState.OPEN;
						_openNodes.Add(node2);
					}
				}
			}
			while (aMaxIterations > 0);
			_inProgress = !flag;
			if (flag)
			{
				GetPathResult(out nodes, out positions, out cost);
			}
			return flag;
		}

		public void GetPathResult(out List<NavigationGraphNode> nodes, out List<Vector3> positions, out float cost)
		{
			nodes = new List<NavigationGraphNode>();
			positions = new List<Vector3>();
			HeuristicPathFinderNodeHelper heuristicPathFinderNodeHelper = _bestNode;
			if (heuristicPathFinderNodeHelper != null)
			{
				if (_goalNode == null)
				{
					cost = heuristicPathFinderNodeHelper.ActualCostFromStart + heuristicPathFinderNodeHelper.OffMeshRating;
					nodes.Add(heuristicPathFinderNodeHelper.Node);
					positions.Add(heuristicPathFinderNodeHelper.OffMeshIntersection);
					heuristicPathFinderNodeHelper = heuristicPathFinderNodeHelper.PreviousNode;
				}
				else
				{
					cost = heuristicPathFinderNodeHelper.TotalHeuristicCost();
				}
			}
			else
			{
				cost = 0f;
			}
			while (heuristicPathFinderNodeHelper != null && nodes.Count < _nodes.Count)
			{
				nodes.Add(heuristicPathFinderNodeHelper.Node);
				positions.Add(heuristicPathFinderNodeHelper.Position);
				heuristicPathFinderNodeHelper = heuristicPathFinderNodeHelper.PreviousNode;
			}
			if (nodes.Count == 1)
			{
				nodes.Add(nodes[0]);
				positions.Add(positions[0]);
			}
			if (!_reversePath)
			{
				nodes.Reverse();
				positions.Reverse();
			}
		}

		private HeuristicPathFinderNodeHelper GetNode(NavigationGraphNode aGraphNode)
		{
			HeuristicPathFinderNodeHelper value = null;
			_nodes.TryGetValue(aGraphNode, out value);
			if (value == null)
			{
				value = new HeuristicPathFinderNodeHelper(aGraphNode);
				value.Position = aGraphNode.Localize();
				if (_goalNode == null)
				{
					value.HeuristicCostToGoal = Vector3.Distance(value.Position, _goalPosition);
				}
				else
				{
					value.HeuristicCostToGoal = aGraphNode.Graph.HeuristicCost(aGraphNode, _goalNode);
				}
				value.CalculateOffMeshRating(_goalPosition);
				if (_bestNode == null || value.OffMeshRating < _bestNode.OffMeshRating)
				{
					_bestNode = value;
				}
				_nodes.Add(aGraphNode, value);
			}
			return value;
		}
	}
}
