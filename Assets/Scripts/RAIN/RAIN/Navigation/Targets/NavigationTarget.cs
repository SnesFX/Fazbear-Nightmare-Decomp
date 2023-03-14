using RAIN.Core;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Navigation.Targets
{
	[RAINSerializableClass]
	[RAINElement("Navigation Target")]
	public class NavigationTarget : RAINElement
	{
		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Target name")]
		private string _targetName;

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Visualization color")]
		private Color _targetColor = Color.green;

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Visualization outline color offset")]
		private float _outlineColorOffset = -0.5f;

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Visualization selected color offset")]
		private float _selectionColorOffset = 0.4f;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Physical location of the Target")]
		private Transform _mountPoint;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Offset from mount point")]
		private Vector3 _positionOffset = Vector3.zero;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Angle from mount point")]
		private Vector3 _angleOffset = Vector3.zero;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Size of visualization")]
		private float _range = 0.5f;

		public string TargetName
		{
			get
			{
				return _targetName;
			}
			set
			{
				_targetName = value;
			}
		}

		public Color TargetColor
		{
			get
			{
				return _targetColor;
			}
			set
			{
				_targetColor = value;
			}
		}

		public float OutlineColorOffset
		{
			get
			{
				return _outlineColorOffset;
			}
			set
			{
				_outlineColorOffset = value;
			}
		}

		public float SelectionColorOffset
		{
			get
			{
				return _selectionColorOffset;
			}
			set
			{
				_selectionColorOffset = value;
			}
		}

		public Transform MountPoint
		{
			get
			{
				return _mountPoint;
			}
			set
			{
				_mountPoint = value;
			}
		}

		public Vector3 PositionOffset
		{
			get
			{
				return _positionOffset;
			}
			set
			{
				_positionOffset = value;
			}
		}

		public Vector3 AngleOffset
		{
			get
			{
				return _angleOffset;
			}
			set
			{
				_angleOffset = value;
			}
		}

		public float Range
		{
			get
			{
				return _range;
			}
			set
			{
				_range = Mathf.Max(0f, value);
			}
		}

		public virtual Vector3 Position
		{
			get
			{
				return _mountPoint.TransformPoint(_positionOffset);
			}
			set
			{
				_positionOffset = _mountPoint.InverseTransformPoint(value);
			}
		}

		public virtual Vector3 Orientation
		{
			get
			{
				return _mountPoint.rotation.eulerAngles + _angleOffset;
			}
			set
			{
				_angleOffset = value - _mountPoint.rotation.eulerAngles;
			}
		}
	}
}
