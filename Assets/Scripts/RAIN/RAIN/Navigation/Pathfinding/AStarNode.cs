using RAIN.Navigation.Graph;
using UnityEngine;

namespace RAIN.Navigation.Pathfinding
{
	public class AStarNode : PathNodeHelper
	{
		private AStarNode _parent;

		private Vector3 _position = Vector3.zero;

		private float _g;

		private float _h;

		public float F
		{
			get
			{
				return _g + _h;
			}
		}

		public float G
		{
			get
			{
				return _g;
			}
			set
			{
				_g = value;
			}
		}

		public float H
		{
			get
			{
				return _h;
			}
			set
			{
				_h = value;
			}
		}

		public AStarNode Parent
		{
			get
			{
				return _parent;
			}
			set
			{
				_parent = value;
			}
		}

		public Vector3 Position
		{
			get
			{
				return _position;
			}
			set
			{
				_position = value;
			}
		}

		public AStarNode(NavigationGraphNode aNode)
			: base(aNode)
		{
		}

		public override int CompareTo(PathNodeHelper aHelper)
		{
			AStarNode aStarNode = (AStarNode)aHelper;
			int result = 0;
			if (aStarNode.F > F)
			{
				result = -1;
			}
			else if (aStarNode.F < F)
			{
				result = 1;
			}
			return result;
		}
	}
}
