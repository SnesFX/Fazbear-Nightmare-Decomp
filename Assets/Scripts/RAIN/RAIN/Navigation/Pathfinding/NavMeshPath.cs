using System.Collections.Generic;
using RAIN.Navigation.Graph;
using RAIN.Navigation.NavMesh;
using RAIN.Serialization;
using RAIN.Utility;
using UnityEngine;

namespace RAIN.Navigation.Pathfinding
{
	[RAINSerializableClass]
	public class NavMeshPath : BasicPath
	{
		private NavigationGraphNode[] _smoothNodes = new NavigationGraphNode[0];

		private Vector3[] _smoothPoints = new Vector3[0];

		public override int WaypointCount
		{
			get
			{
				return _smoothPoints.Length;
			}
		}

		public NavigationGraphNode[] SmoothNodes
		{
			get
			{
				return _smoothNodes;
			}
		}

		public Vector3[] SmoothPoints
		{
			get
			{
				return _smoothPoints;
			}
		}

		public NavMeshPath(RAINNavigationGraph aGraph, float aCost, NavigationGraphNode[] aNodes, Vector3[] aPoints)
			: base(aGraph, aCost, aNodes, aPoints)
		{
		}

		public override void Init(RAINNavigationGraph aGraph, float aCost, NavigationGraphNode[] aNodes, Vector3[] aPoints = null)
		{
			base.Init(aGraph, aCost, aNodes, aPoints);
			Smooth();
		}

		public override Vector3 GetWaypointPosition(int aWaypointIndex)
		{
			return _smoothPoints[aWaypointIndex];
		}

		public override NavigationGraphNode GetGraphNodeOfWaypoint(int aWaypointIndex)
		{
			return _smoothNodes[aWaypointIndex];
		}

		public override int GetWaypointOfGraphNode(NavigationGraphNode aNode)
		{
			return base.GetWaypointOfGraphNode(aNode);
		}

		public override int GetClosestWaypoint(Vector3 aPosition, int aStartIndex = 0)
		{
			float num = float.MaxValue;
			int result = -1;
			for (int i = aStartIndex; i < _smoothPoints.Length; i++)
			{
				float num2 = ((!Allow3DMovement) ? MathUtils.SqrDistanceXZ(aPosition, _smoothPoints[i]) : (aPosition - _smoothPoints[i]).sqrMagnitude);
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
			if (!IsValid)
			{
				return -1;
			}
			if (aLastWaypointIndex >= _smoothPoints.Length - 1)
			{
				return _smoothPoints.Length - 1;
			}
			int num = aLastWaypointIndex + 1;
			float num2;
			if (Allow3DMovement)
			{
				Vector3 a = MathUtils.FindNearestPointOnLine(aCurrentPosition, _smoothPoints[num - 1], _smoothPoints[num]);
				num2 = Vector3.Distance(a, _smoothPoints[num]);
			}
			else
			{
				Vector3 a = MathUtils.FindNearestPointOnLineXZ(aCurrentPosition, _smoothPoints[num - 1], _smoothPoints[num]);
				num2 = MathUtils.DistanceXZ(a, _smoothPoints[num]);
			}
			while (num2 < aMinForwardDistance && num < _smoothPoints.Length - 1)
			{
				num++;
				num2 = ((!Allow3DMovement) ? (num2 + MathUtils.DistanceXZ(_smoothPoints[num - 1], _smoothPoints[num])) : (num2 + Vector3.Distance(_smoothPoints[num - 1], _smoothPoints[num])));
			}
			return num;
		}

		public override int GetLastWaypoint()
		{
			return _smoothPoints.Length - 1;
		}

		public override float GetDistanceRemaining(Vector3 aStartPosition, int aNextWaypointIndex)
		{
			float num = 0f;
			if (aNextWaypointIndex >= 0 && aNextWaypointIndex < _smoothPoints.Length)
			{
				if (Allow3DMovement)
				{
					num += Vector3.Distance(aStartPosition, _smoothPoints[aNextWaypointIndex]);
					for (int i = aNextWaypointIndex + 1; i < _smoothPoints.Length; i++)
					{
						num += Vector3.Distance(_smoothPoints[i - 1], _smoothPoints[i]);
					}
				}
				else
				{
					num += MathUtils.DistanceXZ(aStartPosition, _smoothPoints[aNextWaypointIndex]);
					for (int j = aNextWaypointIndex + 1; j < _smoothPoints.Length; j++)
					{
						num += MathUtils.DistanceXZ(_smoothPoints[j - 1], _smoothPoints[j]);
					}
				}
			}
			return num;
		}

		private new void Smooth()
		{
			if (!IsValid)
			{
				return;
			}
			bool flag = true;
			int num = -1;
			Vector3 vector = Vector3.zero;
			NavigationGraphNode item = null;
			int num2 = -1;
			Vector3 vector2 = Vector3.zero;
			NavigationGraphNode item2 = null;
			List<Vector3> list = new List<Vector3>(Points.Length);
			List<NavigationGraphNode> list2 = new List<NavigationGraphNode>(Nodes.Length);
			list.Add(Points[0]);
			list2.Add(Nodes[0]);
			if (Nodes[0] == null)
			{
				list.Add(Points[1]);
				list2.Add(Nodes[1]);
			}
			Vector3 vector3 = list[list.Count - 1];
			for (int i = 0; i < Nodes.Length; i++)
			{
				if (Nodes[i] == null)
				{
					continue;
				}
				NavigationGraphNode navigationGraphNode = Nodes[i];
				if (navigationGraphNode is NavMeshEdge)
				{
					NavMeshEdge navMeshEdge = navigationGraphNode as NavMeshEdge;
					if (MathUtils.IsOnXZ(navMeshEdge.PointOne, navMeshEdge.PointTwo, vector3))
					{
						continue;
					}
					Vector3 vector4;
					Vector3 vector5;
					if (MathUtils.IsLeftXZ(vector3, navMeshEdge.PointOne, navMeshEdge.PointTwo))
					{
						vector4 = navMeshEdge.PointTwo;
						vector5 = navMeshEdge.PointOne;
					}
					else
					{
						vector4 = navMeshEdge.PointOne;
						vector5 = navMeshEdge.PointTwo;
					}
					if (flag)
					{
						flag = false;
						num = i;
						vector = vector4;
						item = navMeshEdge;
						num2 = i;
						vector2 = vector5;
						item2 = navMeshEdge;
					}
					else
					{
						if (MathUtils.IsRightXZ(vector3, vector, vector4))
						{
							if (MathUtils.IsRightOrOnXZ(vector3, vector2, vector4))
							{
								vector3 = vector2;
								list.Add(vector2);
								list2.Add(item2);
								i = num2;
								flag = true;
							}
							else
							{
								num = i;
								vector = vector4;
								item = navMeshEdge;
							}
						}
						if (MathUtils.IsLeftXZ(vector3, vector2, vector5))
						{
							if (MathUtils.IsLeftOrOnXZ(vector3, vector, vector5))
							{
								vector3 = vector;
								list.Add(vector);
								list2.Add(item);
								i = num;
								flag = true;
							}
							else
							{
								num2 = i;
								vector2 = vector5;
								item2 = navMeshEdge;
							}
						}
					}
				}
				if (!flag && (i == Nodes.Length - 1 || Nodes[i + 1] == null))
				{
					if (MathUtils.IsRightXZ(vector3, vector2, Points[i]))
					{
						vector3 = vector2;
						list.Add(vector2);
						list2.Add(item2);
						i = num2;
						flag = true;
					}
					else if (MathUtils.IsLeftXZ(vector3, vector, Points[i]))
					{
						vector3 = vector;
						list.Add(vector);
						list2.Add(item);
						i = num;
						flag = true;
					}
				}
			}
			if (Nodes[Nodes.Length - 1] == null)
			{
				list.Add(Points[Points.Length - 2]);
				list2.Add(Nodes[Nodes.Length - 2]);
			}
			list.Add(Points[Points.Length - 1]);
			list2.Add(Nodes[Nodes.Length - 1]);
			_smoothPoints = list.ToArray();
			_smoothNodes = list2.ToArray();
		}
	}
}
