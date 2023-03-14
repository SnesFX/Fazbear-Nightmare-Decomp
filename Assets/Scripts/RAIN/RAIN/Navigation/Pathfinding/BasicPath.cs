using System;
using RAIN.Navigation.Graph;
using RAIN.Serialization;
using RAIN.Utility;
using UnityEngine;

namespace RAIN.Navigation.Pathfinding
{
	[RAINSerializableClass]
	public class BasicPath : RAINPath
	{
		private RAINNavigationGraph _pathGraph;

		private float _cost;

		private NavigationGraphNode[] _nodes;

		private Vector3[] _points;

		private float[] _lengths;

		private bool _allow3DMovement;

		private bool _isPartial;

		public override RAINNavigationGraph Graph
		{
			get
			{
				return _pathGraph;
			}
		}

		public override NavigationGraphNode[] Nodes
		{
			get
			{
				return _nodes;
			}
		}

		public override Vector3[] Points
		{
			get
			{
				return _points;
			}
		}

		public override int WaypointCount
		{
			get
			{
				return _points.Length;
			}
		}

		public override bool Allow3DMovement
		{
			get
			{
				return _allow3DMovement;
			}
			set
			{
				_allow3DMovement = value;
			}
		}

		public override bool IsValid
		{
			get
			{
				return _points.Length > 1;
			}
		}

		public override bool IsPartial
		{
			get
			{
				return _isPartial;
			}
			set
			{
				_isPartial = value;
			}
		}

		public BasicPath()
		{
		}

		public BasicPath(RAINNavigationGraph aGraph, float aCost, NavigationGraphNode[] aNodes)
		{
			Init(aGraph, aCost, aNodes);
		}

		public BasicPath(RAINNavigationGraph aGraph, float aCost, NavigationGraphNode[] aNodes, Vector3[] aPoints)
		{
			Init(aGraph, aCost, aNodes, aPoints);
		}

		public override void Init(RAINNavigationGraph aGraph, float aCost, NavigationGraphNode[] aNodes, Vector3[] aPoints = null)
		{
			_pathGraph = aGraph;
			_cost = aCost;
			_nodes = new NavigationGraphNode[aNodes.Length];
			Array.Copy(aNodes, _nodes, _nodes.Length);
			if (aPoints == null)
			{
				_points = new Vector3[_nodes.Length];
				for (int i = 0; i < _nodes.Length; i++)
				{
					_points[i] = _nodes[i].Localize();
				}
			}
			else
			{
				if (_nodes.Length != aPoints.Length)
				{
					throw new ArgumentException("aPoints must be the same length as aNodes if not null");
				}
				_points = new Vector3[aPoints.Length];
				Array.Copy(aPoints, _points, _points.Length);
			}
			_lengths = new float[_points.Length];
			for (int j = 0; j < _points.Length; j++)
			{
				if (j == 0)
				{
					_lengths[j] = 0f;
				}
				else
				{
					_lengths[j] = _lengths[j - 1] + (_points[j] - _points[j - 1]).magnitude;
				}
			}
		}

		public override bool IsGraphNodeInPath(NavigationGraphNode aNode)
		{
			for (int i = 0; i < _nodes.Length; i++)
			{
				if (_nodes[i] == aNode)
				{
					return true;
				}
			}
			return false;
		}

		public override float GetTotalCost()
		{
			return _cost;
		}

		public override float RecomputeTotalCost()
		{
			_cost = 0f;
			if (_nodes == null || _nodes.Length == 0)
			{
				return 0f;
			}
			NavigationGraphNode navigationGraphNode = _nodes[0];
			for (int i = 1; i < _nodes.Length; i++)
			{
				if (navigationGraphNode != null)
				{
					NavigationGraphEdge navigationGraphEdge = navigationGraphNode.EdgeTo(_nodes[i]);
					if (navigationGraphEdge != null)
					{
						_cost += navigationGraphEdge.Cost;
					}
				}
				navigationGraphNode = _nodes[i];
			}
			return _cost;
		}

		public override Vector3 GetWaypointPosition(int aWaypointIndex)
		{
			return _points[aWaypointIndex];
		}

		public override NavigationGraphNode GetGraphNodeOfWaypoint(int aWaypointIndex)
		{
			return _nodes[aWaypointIndex];
		}

		public override int GetWaypointOfGraphNode(NavigationGraphNode aNode)
		{
			for (int i = 0; i < _nodes.Length; i++)
			{
				if (_nodes[i] == aNode)
				{
					return i;
				}
			}
			return -1;
		}

		public override int GetClosestWaypoint(Vector3 aPosition, int aStartIndex = 0)
		{
			int result = aStartIndex;
			float num = float.MaxValue;
			float num2 = 0f;
			for (int i = aStartIndex; i < _points.Length; i++)
			{
				num2 = ((!Allow3DMovement) ? MathUtils.DistanceXZ(aPosition, _points[i]) : (aPosition - _points[i]).magnitude);
				if (num2 < num)
				{
					num = num2;
					result = i;
				}
			}
			return result;
		}

		public override int GetNextWaypoint(Vector3 aCurrentPosition, float aMinForwardDistance, int aLastWaypointIndex)
		{
			if (_points.Length == 0)
			{
				return 0;
			}
			if (aLastWaypointIndex >= _points.Length - 1)
			{
				return _points.Length - 1;
			}
			int num = aLastWaypointIndex + 1;
			float num2;
			if (Allow3DMovement)
			{
				Vector3 a = MathUtils.FindNearestPointOnLine(aCurrentPosition, _points[num - 1], _points[num]);
				num2 = Vector3.Distance(a, _points[num]);
			}
			else
			{
				Vector3 a = MathUtils.FindNearestPointOnLineXZ(aCurrentPosition, _points[num - 1], _points[num]);
				num2 = MathUtils.DistanceXZ(a, _points[num]);
			}
			while (num2 < aMinForwardDistance && num < _points.Length - 1)
			{
				num++;
				num2 = ((!Allow3DMovement) ? (num2 + MathUtils.DistanceXZ(_points[num - 1], _points[num])) : (num2 + Vector3.Distance(_points[num - 1], _points[num])));
			}
			return num;
		}

		public override int GetLastWaypoint()
		{
			return _points.Length - 1;
		}

		public override float GetDistanceRemaining(Vector3 aStartPosition, int aNextWaypointIndex)
		{
			float num = 0f;
			if (aNextWaypointIndex >= 0 && aNextWaypointIndex < _points.Length)
			{
				if (Allow3DMovement)
				{
					num += Vector3.Distance(aStartPosition, _points[aNextWaypointIndex]);
					for (int i = aNextWaypointIndex + 1; i < _points.Length; i++)
					{
						num += Vector3.Distance(_points[i - 1], _points[i]);
					}
				}
				else
				{
					num += MathUtils.DistanceXZ(aStartPosition, _points[aNextWaypointIndex]);
					for (int j = aNextWaypointIndex + 1; j < _points.Length; j++)
					{
						num += MathUtils.DistanceXZ(_points[j - 1], _points[j]);
					}
				}
			}
			return num;
		}

		public override Vector3 GetPositionOnPath(float distance)
		{
			if (_points.Length == 0)
			{
				return Vector3.zero;
			}
			for (int i = 1; i < _points.Length; i++)
			{
				if (_lengths[i] > distance)
				{
					Vector3 vector = _points[i] - _points[i - 1];
					vector.Normalize();
					vector *= distance - _lengths[i - 1];
					return _points[i - 1] + vector;
				}
			}
			return _points[_points.Length - 1];
		}

		public override float GetLength()
		{
			if (_lengths.Length == 0)
			{
				return 0f;
			}
			return _lengths[_lengths.Length - 1];
		}
	}
}
