using System;
using RAIN.Core;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Navigation.Waypoints
{
	[AddComponentMenu("Rival Theory/RAIN/Waypoint Rig")]
	public class WaypointRig : RAINComponent
	{
		[RAINSerializableField(Visibility = FieldVisibility.Show)]
		private VisualModeEnum _visualMode = VisualModeEnum.AlwaysOn;

		[RAINSerializableField(Visibility = FieldVisibility.Show)]
		private WaypointSet _waypointSet = new WaypointSet();

		[Obsolete("Use VisualMode instead")]
		public bool ShowVisual
		{
			get
			{
				if (_visualMode != VisualModeEnum.OnWhenSelected)
				{
					return _visualMode == VisualModeEnum.AlwaysOn;
				}
				return true;
			}
			set
			{
				if (value)
				{
					_visualMode = VisualModeEnum.OnWhenSelected;
				}
				else
				{
					_visualMode = VisualModeEnum.Hidden;
				}
			}
		}

		public VisualModeEnum VisualMode
		{
			get
			{
				return _visualMode;
			}
			set
			{
				_visualMode = value;
			}
		}

		public WaypointSet WaypointSet
		{
			get
			{
				return _waypointSet;
			}
		}

		public override void Awake()
		{
			base.Awake();
			_waypointSet.Name = base.name;
			NavigationManager.Instance.Register(_waypointSet);
		}

		public override void OnDestroy()
		{
			NavigationManager.Instance.Unregister(_waypointSet);
			base.OnDestroy();
		}

		protected override void Reset()
		{
			WaypointSet.WaypointSetType setType = WaypointSet.WaypointSetType.Route;
			if (_waypointSet != null)
			{
				setType = _waypointSet.SetType;
			}
			_waypointSet = new WaypointSet();
			_waypointSet.SetType = setType;
			base.Reset();
		}
	}
}
