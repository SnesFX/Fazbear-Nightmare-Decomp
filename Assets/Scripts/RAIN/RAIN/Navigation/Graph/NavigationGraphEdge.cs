using System;
using UnityEngine;

namespace RAIN.Navigation.Graph
{
	public class NavigationGraphEdge
	{
		private NavigationGraphNode _fromNode;

		private NavigationGraphNode _toNode;

		private float _staticCost;

		private float _overrideCost = -1f;

		private float _minimumCost = 0.001f;

		[Obsolete("Use FromNode instead")]
		public int From
		{
			get
			{
				return _fromNode.NodeIndex;
			}
			set
			{
			}
		}

		public NavigationGraphNode FromNode
		{
			get
			{
				return _fromNode;
			}
			set
			{
				_fromNode = value;
			}
		}

		[Obsolete("Use ToNode instead")]
		public int To
		{
			get
			{
				return _toNode.NodeIndex;
			}
			set
			{
			}
		}

		public NavigationGraphNode ToNode
		{
			get
			{
				return _toNode;
			}
			set
			{
				_toNode = value;
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

		public virtual float OverrideCost
		{
			get
			{
				return _overrideCost;
			}
			set
			{
				_overrideCost = value;
			}
		}

		public virtual float MinimumCost
		{
			get
			{
				return _minimumCost;
			}
			set
			{
				_minimumCost = value;
			}
		}

		public virtual float Cost
		{
			get
			{
				if (OverrideCost > 0f)
				{
					return OverrideCost;
				}
				return _staticCost;
			}
		}

		public NavigationGraphEdge(NavigationGraphNode aFrom, NavigationGraphNode aTo, float aCost)
		{
			_fromNode = aFrom;
			_toNode = aTo;
			_staticCost = Mathf.Max(aCost, MinimumCost);
		}
	}
}
