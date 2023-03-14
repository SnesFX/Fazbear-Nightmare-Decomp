using System.Collections.Generic;
using RAIN.Core;
using RAIN.Entities.Aspects;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Perception.Sensors
{
	[RAINSerializableClass]
	[RAINElement("Visual Sensor")]
	public class VisualSensor : RAINSensor
	{
		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Horizontal clipping angle")]
		private float _horizontalAngle = 360f;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Vertical clipping angle")]
		private float _verticalAngle = 360f;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Sensor clipping distance")]
		private float _range = 10f;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Whether to include the AI Body when matching Aspects")]
		private bool _canDetectSelf;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Require line of sight", OldFieldNames = new string[] { "_requireLineOfSight" })]
		private bool _lineOfSight;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Line of sight layer mask", OldFieldNames = new string[] { "_lineOfSightMask" })]
		private LayerMask _lineOfSightMask = -5;

		private List<RAINAspect> _matches = new List<RAINAspect>();

		public virtual float HorizontalAngle
		{
			get
			{
				return _horizontalAngle;
			}
			set
			{
				_horizontalAngle = value;
			}
		}

		public virtual float VerticalAngle
		{
			get
			{
				return _verticalAngle;
			}
			set
			{
				_verticalAngle = value;
			}
		}

		public virtual float Range
		{
			get
			{
				return _range;
			}
			set
			{
				_range = value;
			}
		}

		public virtual bool CanDetectSelf
		{
			get
			{
				return _canDetectSelf;
			}
			set
			{
				_canDetectSelf = value;
			}
		}

		public virtual bool RequireLineOfSight
		{
			get
			{
				return _lineOfSight;
			}
			set
			{
				_lineOfSight = value;
			}
		}

		public virtual LayerMask LineOfSightMask
		{
			get
			{
				return _lineOfSightMask;
			}
			set
			{
				_lineOfSightMask = value;
			}
		}

		public override IList<RAINAspect> Matches
		{
			get
			{
				return _matches.AsReadOnly();
			}
		}

		public override void MatchAspect(RAINAspect aAspect)
		{
			_matches.Clear();
			if (aAspect == null || !aAspect.IsActive || aAspect.Entity == null || aAspect.Entity.Form == null || !aAspect.Entity.Form.activeInHierarchy)
			{
				return;
			}
			IList<RAINAspect> aspects = SensorManager.Instance.GetAspects("visual");
			if (aspects.Contains(aAspect))
			{
				float aSqrRange = Range * Range;
				Matrix4x4 inverse = Matrix4x4.TRS(PositionOffset, Quaternion.Euler(AngleOffset), Vector3.one).inverse;
				if (MountPoint != null)
				{
					inverse *= MountPoint.worldToLocalMatrix;
				}
				else
				{
					inverse *= AI.Body.transform.worldToLocalMatrix;
				}
				if (TestVisibility(aAspect, aSqrRange, inverse))
				{
					_matches.Add(aAspect);
				}
			}
			Filter(_matches);
		}

		public override void MatchAspectName(string aAspectName)
		{
			_matches.Clear();
			IList<RAINAspect> aspects = SensorManager.Instance.GetAspects("visual");
			float aSqrRange = Range * Range;
			Matrix4x4 inverse = Matrix4x4.TRS(PositionOffset, Quaternion.Euler(AngleOffset), Vector3.one).inverse;
			if (MountPoint != null)
			{
				inverse *= MountPoint.worldToLocalMatrix;
			}
			else
			{
				inverse *= AI.Body.transform.worldToLocalMatrix;
			}
			foreach (RAINAspect item in aspects)
			{
				if (item != null && item.IsActive && item.Entity != null && !(item.Entity.Form == null) && item.Entity.Form.activeInHierarchy && item.AspectName == aAspectName && TestVisibility(item, aSqrRange, inverse))
				{
					_matches.Add(item);
				}
			}
			Filter(_matches);
		}

		public override void MatchAll()
		{
			_matches.Clear();
			IList<RAINAspect> aspects = SensorManager.Instance.GetAspects("visual");
			float aSqrRange = Range * Range;
			Matrix4x4 inverse = Matrix4x4.TRS(PositionOffset, Quaternion.Euler(AngleOffset), Vector3.one).inverse;
			if (MountPoint != null)
			{
				inverse *= MountPoint.worldToLocalMatrix;
			}
			else
			{
				inverse *= AI.Body.transform.worldToLocalMatrix;
			}
			foreach (RAINAspect item in aspects)
			{
				if (item.IsActive && item.Entity != null && !(item.Entity.Form == null) && item.Entity.Form.activeInHierarchy && TestVisibility(item, aSqrRange, inverse))
				{
					_matches.Add(item);
				}
			}
			Filter(_matches);
		}

		protected virtual bool TestVisibility(RAINAspect aAspect, float aSqrRange)
		{
			Matrix4x4 inverse = Matrix4x4.TRS(PositionOffset, Quaternion.Euler(AngleOffset), Vector3.one).inverse;
			if (MountPoint != null)
			{
				inverse *= MountPoint.worldToLocalMatrix;
			}
			else
			{
				inverse *= AI.Body.transform.worldToLocalMatrix;
			}
			return TestVisibility(aAspect, aSqrRange, inverse);
		}

		protected virtual bool TestVisibility(RAINAspect aAspect, float aSqrRange, Matrix4x4 aSensorSpace)
		{
			Vector3 position = aAspect.Position;
			if ((position - Position).sqrMagnitude > aSqrRange)
			{
				return false;
			}
			Transform transform = aAspect.Entity.Form.transform;
			if (aAspect.MountPoint != null)
			{
				transform = aAspect.MountPoint.transform;
			}
			if (!CanDetectSelf && (transform == AI.Body.transform || transform.IsChildOf(AI.Body.transform)))
			{
				return false;
			}
			bool flag = true;
			if (HorizontalAngle == 360f && VerticalAngle == 360f)
			{
				flag = true;
			}
			else
			{
				position = aSensorSpace.MultiplyPoint(position);
				Vector3 normalized = new Vector3(position.x, 0f, position.z).normalized;
				float num = Mathf.Acos(Mathf.Clamp(normalized.z, -1f, 1f)) * 57.29578f;
				if (num > 180f)
				{
					num = 360f - num;
				}
				if (!Mathf.Approximately(normalized.sqrMagnitude, 0f) && num > HorizontalAngle / 2f)
				{
					flag = false;
				}
				else
				{
					normalized = new Vector3(0f, position.y, position.z).normalized;
					float num2 = Mathf.Acos(Mathf.Clamp(normalized.z, -1f, 1f)) * 57.29578f;
					if (num2 > 180f)
					{
						num2 = 360f - num2;
					}
					if (num2 > 90f)
					{
						num2 = 180f - num2;
					}
					if (!Mathf.Approximately(normalized.sqrMagnitude, 0f) && num2 > VerticalAngle / 2f)
					{
						flag = false;
					}
				}
			}
			if (!flag)
			{
				return false;
			}
			if (_lineOfSight)
			{
				Vector3 direction = aAspect.Position - Position;
				RaycastHit[] array = Physics.RaycastAll(Position, direction, direction.magnitude, _lineOfSightMask);
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].collider.isTrigger || array[i].transform == AI.Body.transform || array[i].transform.IsChildOf(AI.Body.transform))
					{
						continue;
					}
					if (aAspect.MountPoint != null)
					{
						if (array[i].transform == aAspect.MountPoint.transform || array[i].transform.IsChildOf(aAspect.MountPoint.transform))
						{
							continue;
						}
					}
					else if (aAspect.Entity.Form != null && (array[i].transform == aAspect.Entity.Form.transform || array[i].transform.IsChildOf(aAspect.Entity.Form.transform)))
					{
						continue;
					}
					return false;
				}
			}
			return true;
		}
	}
}
