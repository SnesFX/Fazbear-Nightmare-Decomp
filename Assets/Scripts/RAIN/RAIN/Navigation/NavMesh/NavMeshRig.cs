using System;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Serialization;
using RAIN.Utility.Version;
using UnityEngine;

namespace RAIN.Navigation.NavMesh
{
	[AddComponentMenu("Rival Theory/RAIN/Navigation Mesh Rig")]
	public class NavMeshRig : RAINComponent
	{
		[RAINSerializableField(Visibility = FieldVisibility.Show)]
		private VisualModeEnum _visualMode = VisualModeEnum.OnWhenSelected;

		[RAINSerializableField(Visibility = FieldVisibility.Show)]
		private NavMesh _navMesh = new NavMesh();

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

		public NavMesh NavMesh
		{
			get
			{
				return _navMesh;
			}
		}

		[Obsolete("No longer used")]
		public bool ContourVisualsNeedGenerating
		{
			get
			{
				return false;
			}
		}

		[Obsolete("No longer used")]
		public IList<Mesh> ContourVisualMeshes
		{
			get
			{
				return new List<Mesh>();
			}
		}

		public override void Awake()
		{
			base.Awake();
			_navMesh.GraphName = base.name;
			_navMesh.RegisterNavigationGraph();
		}

		public override void OnDestroy()
		{
			if (_navMesh.Creating)
			{
				_navMesh.CancelCreatingContours();
			}
			_navMesh.ClearNavigationGraph();
			base.OnDestroy();
		}

		protected override bool OnVersion(string aVersion)
		{
			bool result = base.OnVersion(aVersion);
			if (aVersion == "1.0" || aVersion == "1.1")
			{
				try
				{
					TwoOneSeven twoOneSeven = new TwoOneSeven();
					twoOneSeven.UpgradeNavMeshPathGraph(base.DataSerializer);
				}
				catch
				{
					Debug.Log("NavMeshRig: Problem versioning");
				}
				result = true;
			}
			return result;
		}

		[Obsolete("Meshes no longer need to be generated")]
		public void GenerateContourVisuals()
		{
		}

		[Obsolete("Meshes no longer need to be generated")]
		public void GenerateAllContourVisuals()
		{
		}

		[Obsolete("Meshes no longer need to be generated")]
		public void ClearContourVisuals()
		{
		}

		protected override void Reset()
		{
			_navMesh = new NavMesh();
			base.Reset();
		}
	}
}
