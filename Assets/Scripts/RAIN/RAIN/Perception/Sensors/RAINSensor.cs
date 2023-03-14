using System.Collections.Generic;
using RAIN.Core;
using RAIN.Entities.Aspects;
using RAIN.Perception.Sensors.Filters;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Perception.Sensors
{
	[RAINSerializableClass]
	public abstract class RAINSensor : RAINAIElement
	{
		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Show visualization")]
		private bool _showVisual = true;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "The sensor name")]
		private string _sensorName = "Sensor";

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Whether the Sensor is currently detecting Aspects")]
		private bool _isActive = true;

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Visualization color")]
		private Color _sensorColor = Color.green;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Transform to which the sensor is mounted")]
		private Transform _mountPoint;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Positional offset from the mount point")]
		private Vector3 _positionOffset = Vector3.zero;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Rotational offset from the mount point")]
		private Vector3 _angleOffset = Vector3.zero;

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Optional filters to apply when detecting Aspects")]
		private List<RAINSensorFilter> _filters = new List<RAINSensorFilter>();

		public virtual bool IsActive
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

		public virtual bool ShowVisual
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

		public virtual Color SensorColor
		{
			get
			{
				return _sensorColor;
			}
			set
			{
				_sensorColor = value;
			}
		}

		public virtual string SensorName
		{
			get
			{
				return _sensorName;
			}
			set
			{
				_sensorName = value;
			}
		}

		public virtual Transform MountPoint
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

		public virtual Vector3 PositionOffset
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

		public virtual Vector3 AngleOffset
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
					return AI.Body.transform.TransformPoint(_positionOffset);
				}
				return _mountPoint.TransformPoint(_positionOffset);
			}
			set
			{
				if (_mountPoint == null)
				{
					_positionOffset = AI.Body.transform.InverseTransformPoint(value);
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
					return AI.Body.transform.rotation.eulerAngles + _angleOffset;
				}
				return _mountPoint.rotation.eulerAngles + _angleOffset;
			}
			set
			{
				if (_mountPoint == null)
				{
					_angleOffset = value - AI.Body.transform.rotation.eulerAngles;
				}
				else
				{
					_angleOffset = value - _mountPoint.rotation.eulerAngles;
				}
			}
		}

		public abstract IList<RAINAspect> Matches { get; }

		public RAINSensor()
		{
		}

		public virtual void Filter(List<RAINAspect> aspects)
		{
			foreach (RAINSensorFilter filter in _filters)
			{
				filter.Filter(this, aspects);
			}
		}

		public abstract void MatchAspectName(string aAspectName);

		public abstract void MatchAspect(RAINAspect aAspect);

		public abstract void MatchAll();
	}
}
