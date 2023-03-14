using System;
using RAIN.Navigation.Graph;

namespace RAIN.Navigation.Pathfinding
{
	public class PathNodeHelper
	{
		public enum PathNodeHelperState
		{
			OPEN = 0,
			CLOSED = 1,
			UNVISITED = 2
		}

		protected NavigationGraphNode _node;

		private float _staticCost;

		private PathNodeHelperState _state;

		private int _heappos;

		public NavigationGraphNode Node
		{
			get
			{
				return _node;
			}
		}

		public PathNodeHelperState State
		{
			get
			{
				return _state;
			}
			set
			{
				_state = value;
			}
		}

		public int HeapPosition
		{
			get
			{
				return _heappos;
			}
			set
			{
				_heappos = value;
			}
		}

		public float StaticCost
		{
			get
			{
				return _staticCost;
			}
			set
			{
				_staticCost = value;
			}
		}

		public PathNodeHelper(NavigationGraphNode aNode)
		{
			_heappos = -1;
			_node = aNode;
			_state = PathNodeHelperState.UNVISITED;
		}

		[Obsolete("Use CompareTo(PathNodeHelper aHelper) instead")]
		public virtual int CompareTo(RAINNavigationGraph aGraph, PathNodeHelper aHelper)
		{
			return CompareTo(aHelper);
		}

		public virtual int CompareTo(PathNodeHelper aHelper)
		{
			if (StaticCost < aHelper.StaticCost)
			{
				return -1;
			}
			if (StaticCost > aHelper.StaticCost)
			{
				return 1;
			}
			return 0;
		}

		public virtual float HeuristicCostFrom(NavigationGraphNode aFromNode)
		{
			return aFromNode.Graph.HeuristicCost(aFromNode, _node);
		}

		public virtual float HeuristicCostTo(NavigationGraphNode aToNode)
		{
			return aToNode.Graph.HeuristicCost(_node, aToNode);
		}
	}
}
