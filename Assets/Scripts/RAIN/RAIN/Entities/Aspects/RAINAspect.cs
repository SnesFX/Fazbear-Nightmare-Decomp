using RAIN.Entities.Elements;
using RAIN.Perception.Sensors;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Entities.Aspects
{
	[RAINSerializableClass]
	public abstract class RAINAspect : RAINEntityElement
	{
		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Toggle visualization on/off")]
		private bool _showVisual = true;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "The Aspect name, used when detecting")]
		private string _aspectName = "";

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Toggle active on/off")]
		private bool _isActive = true;

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Aspect visualization color")]
		private Color _aspectColor = new Color(0f, 0.6f, 0f);

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Aspect visualization outline color offset")]
		private float _outlineColorOffset = -0.5f;

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Aspect visualization selected color offset")]
		private float _selectionColorOffset = 0.4f;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "The size of the visualization", OldFieldNames = new string[] { "_size" })]
		private float _visualSize = 0.1f;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Physical location of the Aspect")]
		private Transform _mountPoint;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Offset from mount point")]
		private Vector3 _positionOffset = Vector3.zero;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Angle from mount point")]
		private Vector3 _angleOffset = Vector3.zero;

		public abstract string AspectType { get; }

		public bool ShowVisual
		{
			get
			{
				return _showVisual;
			}
			set
			{
				_showVisual = value;
			}
		}

		public string AspectName
		{
			get
			{
				return _aspectName;
			}
			set
			{
				_aspectName = value;
			}
		}

		public bool IsActive
		{
			get
			{
				return _isActive;
			}
			set
			{
				_isActive = value;
			}
		}

		public Color AspectColor
		{
			get
			{
				return _aspectColor;
			}
			set
			{
				_aspectColor = value;
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

		public float VisualSize
		{
			get
			{
				return _visualSize;
			}
			set
			{
				_visualSize = Mathf.Max(0f, value);
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

		public virtual Vector3 Position
		{
			get
			{
				if (_mountPoint == null)
				{
					return base.Entity.Form.transform.TransformPoint(_positionOffset);
				}
				return _mountPoint.TransformPoint(_positionOffset);
			}
			set
			{
				if (_mountPoint == null)
				{
					_positionOffset = base.Entity.Form.transform.InverseTransformPoint(value);
				}
				else
				{
					_positionOffset = _mountPoint.InverseTransformPoint(value);
				}
			}
		}

		public virtual Vector3 Orientation
		{
			get
			{
				if (_mountPoint == null)
				{
					return base.Entity.Form.transform.rotation.eulerAngles + _angleOffset;
				}
				return _mountPoint.rotation.eulerAngles + _angleOffset;
			}
			set
			{
				if (_mountPoint == null)
				{
					_angleOffset = value - base.Entity.Form.transform.rotation.eulerAngles;
				}
				else
				{
					_angleOffset = value - _mountPoint.rotation.eulerAngles;
				}
			}
		}

		public RAINAspect()
		{
		}

		public RAINAspect(string aAspectName)
		{
			_aspectName = aAspectName;
		}

		public virtual void RegisterWithSensorManager()
		{
			SensorManager.Instance.Register(this, AspectType);
		}

		public virtual void UnregisterWithSensorManager()
		{
			SensorManager.Instance.Unregister(this, AspectType);
		}
	}
}
