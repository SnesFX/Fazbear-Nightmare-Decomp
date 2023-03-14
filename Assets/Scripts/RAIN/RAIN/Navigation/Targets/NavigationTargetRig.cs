using System;
using RAIN.Core;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Navigation.Targets
{
	[AddComponentMenu("Rival Theory/RAIN/Navigation Target Rig")]
	public class NavigationTargetRig : RAINComponent
	{
		[RAINSerializableField(Visibility = FieldVisibility.Show)]
		private VisualModeEnum _visualMode = VisualModeEnum.AlwaysOn;

		[RAINSerializableField(Visibility = FieldVisibility.Show)]
		private NavigationTarget _target = new NavigationTarget();

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
					_visualMode = VisualModeEnum.AlwaysOn;
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

		public NavigationTarget Target
		{
			get
			{
				return _target;
			}
			protected set
			{
				_target = value;
			}
		}

		public static NavigationTargetRig CreateRig(Vector3 aLocation)
		{
			GameObject gameObject = new GameObject("Navigation Target");
			gameObject.transform.position = Vector3.zero;
			NavigationTargetRig navigationTargetRig = gameObject.AddComponent<NavigationTargetRig>();
			navigationTargetRig.Target.TargetName = gameObject.name;
			navigationTargetRig.Target.MountPoint = gameObject.transform;
			navigationTargetRig.Target.Position = aLocation;
			navigationTargetRig.Serialize();
			return navigationTargetRig;
		}

		public override void Awake()
		{
			base.Awake();
			if (_target.MountPoint == null)
			{
				Debug.LogWarning("NavigationTargetRig: Missing mount point, not registered", base.gameObject);
				return;
			}
			_target.TargetName = base.name;
			NavigationManager.Instance.Register(_target);
		}

		public override void OnDestroy()
		{
			NavigationManager.Instance.Unregister(_target);
			base.OnDestroy();
		}
	}
}
