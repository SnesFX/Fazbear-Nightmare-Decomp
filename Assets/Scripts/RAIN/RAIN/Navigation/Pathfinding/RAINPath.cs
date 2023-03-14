using System;
using RAIN.Navigation.Graph;
using UnityEngine;

namespace RAIN.Navigation.Pathfinding
{
	public abstract class RAINPath
	{
		public abstract RAINNavigationGraph Graph { get; }

		public abstract NavigationGraphNode[] Nodes { get; }

		public abstract Vector3[] Points { get; }

		public abstract int WaypointCount { get; }

		public abstract bool Allow3DMovement { get; set; }

		public abstract bool IsValid { get; }

		public abstract bool IsPartial { get; set; }

		public abstract void Init(RAINNavigationGraph aGraph, float aCost, NavigationGraphNode[] aNodes, Vector3[] aPoints = null);

		[Obsolete("Check IsValid and use Nodes[0] instead")]
		public virtual int GetStartGraphNodeIndex()
		{
			return Nodes[0].NodeIndex;
		}

		[Obsolete("Check IsValid and use Nodes[Nodes.Length - 1] instead")]
		public virtual int GetEndGraphNodeIndex()
		{
			return Nodes[Nodes.Length - 1].NodeIndex;
		}

		[Obsolete("Use IsGraphNodeInPath(NavigationGraphNode aGraphNode)")]
		public virtual bool IsGraphNodeInPath(int graphNodeIndex)
		{
			return IsGraphNodeInPath(Graph.GetNode(graphNodeIndex));
		}

		public abstract bool IsGraphNodeInPath(NavigationGraphNode aGraphNode);

		public abstract float GetTotalCost();

		public abstract float RecomputeTotalCost();

		public abstract Vector3 GetWaypointPosition(int aWaypoint);

		[Obsolete("Use GetGraphNodeOfWaypoint(int aWaypoint) instead")]
		public virtual int GetGraphNodeIndexOfWaypoint(int waypointIndex)
		{
			return GetGraphNodeOfWaypoint(waypointIndex).NodeIndex;
		}

		public abstract NavigationGraphNode GetGraphNodeOfWaypoint(int aWaypoint);

		public abstract int GetWaypointOfGraphNode(NavigationGraphNode aNode);

		[Obsolete("Use GetGraphNodeOfWaypoint instead")]
		public virtual int GetClosestWaypointIndex(Vector3 position, int startIndex = -1)
		{
			return GetClosestWaypoint(position, startIndex);
		}

		public abstract int GetClosestWaypoint(Vector3 aPosition, int aStartWaypoint = 0);

		[Obsolete("Use GetNextWaypoint(Vector3 aCurrentPosition, float aMinForwardDistance, int aLastWaypoint) instead")]
		public virtual int GetNextWaypointIndex(Vector3 aCurrentPosition, float aMinForwardDistance, int aLastWaypointIndex)
		{
			return GetNextWaypoint(aCurrentPosition, aMinForwardDistance, aLastWaypointIndex);
		}

		public abstract int GetNextWaypoint(Vector3 aCurrentPosition, float aMinForwardDistance, int aLastWaypoint);

		public abstract int GetLastWaypoint();

		public abstract Vector3 GetPositionOnPath(float distance);

		public abstract float GetDistanceRemaining(Vector3 aStartPosition, int aNextWaypoint);

		public abstract float GetLength();

		[Obsolete("Smoothing is handled internally by NavMeshPath now")]
		public virtual void Smooth()
		{
		}
	}
}
