using UnityEngine;

namespace RAIN.Navigation.Graph
{
	public class VectorPathNode : NavigationGraphNode
	{
		private int _index = -1;

		private Vector3 _position = Vector3.zero;

		public new int NodeIndex
		{
			get
			{
				return _index;
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

		public VectorPathNode(VectorPathGraph aGraph, int index, Vector3 pos)
			: base(aGraph)
		{
			_index = index;
			_position = pos;
		}

		public override Vector3 Localize()
		{
			return _position;
		}

		public override Vector3 NodeIntersection(Vector3 aPosition)
		{
			return _position;
		}
	}
}
