using RAIN.Core;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Navigation.Waypoints
{
	[RAINSerializableClass]
	public class Waypoint : RAINElement
	{
		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Waypoint name")]
		public string waypointName;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Waypoint position")]
		public Vector3 position;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Waypoint size")]
		public float range = 0.5f;

		public Waypoint()
		{
		}

		public Waypoint(Vector3 aPosition, string aName = null)
		{
			position = aPosition;
			waypointName = aName;
		}
	}
}
