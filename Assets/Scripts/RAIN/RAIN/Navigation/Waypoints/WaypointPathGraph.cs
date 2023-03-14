using RAIN.Navigation.Graph;
using UnityEngine;

namespace RAIN.Navigation.Waypoints
{
	public class WaypointPathGraph : VectorPathGraph
	{
		public WaypointPathGraph(WaypointSet aWaypointSet)
		{
			if (aWaypointSet == null || aWaypointSet.Waypoints.Count == 0)
			{
				return;
			}
			Vector3[] array = new Vector3[aWaypointSet.Waypoints.Count];
			for (int i = 0; i < array.Length; i++)
			{
				AddNode(new VectorPathNode(this, i, aWaypointSet.Waypoints[i].position));
			}
			for (int j = 0; j < aWaypointSet.Connections.Count; j++)
			{
				WaypointSet.WaypointConnection waypointConnection = aWaypointSet.Connections[j];
				AddEdge(GetNode(waypointConnection.wpOne), GetNode(waypointConnection.wpTwo));
				if (waypointConnection.twoWay)
				{
					AddEdge(GetNode(waypointConnection.wpTwo), GetNode(waypointConnection.wpOne));
				}
			}
		}
	}
}
