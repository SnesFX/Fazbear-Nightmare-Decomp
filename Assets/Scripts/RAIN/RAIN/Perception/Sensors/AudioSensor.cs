using System.Collections.Generic;
using RAIN.Core;
using RAIN.Entities.Aspects;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Perception.Sensors
{
	[RAINElement("Audio Sensor")]
	[RAINSerializableClass]
	public class AudioSensor : RAINSensor
	{
		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Sensor clipping volume")]
		private float _volumeThreshold = 0.1f;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Sensor clipping distance", OldFieldNames = new string[] { "_size" })]
		private float _range = 10f;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Whether to include the AI Body when matching Aspects")]
		private bool _canDetectSelf;

		private List<RAINAspect> _matches = new List<RAINAspect>();

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

		public virtual float VolumeThreshold
		{
			get
			{
				return _volumeThreshold;
			}
			set
			{
				_volumeThreshold = Mathf.Clamp01(value);
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
			if (aAspect != null && aAspect.IsActive && aAspect.Entity != null && !(aAspect.Entity.Form == null) && aAspect.Entity.Form.activeInHierarchy)
			{
				IList<RAINAspect> aspects = SensorManager.Instance.GetAspects("audio");
				float aSqrRange = _range * _range;
				if (aspects.Contains(aAspect) && TestVisibility(aAspect, aSqrRange))
				{
					_matches.Add(aAspect);
				}
				Filter(_matches);
			}
		}

		public override void MatchAspectName(string aAspectName)
		{
			_matches.Clear();
			IList<RAINAspect> aspects = SensorManager.Instance.GetAspects("audio");
			float aSqrRange = _range * _range;
			foreach (RAINAspect item in aspects)
			{
				if (item != null && item.IsActive && item.Entity != null && !(item.Entity.Form == null) && item.Entity.Form.activeInHierarchy && item.AspectName == aAspectName && TestVisibility(item, aSqrRange))
				{
					_matches.Add(item);
				}
			}
			Filter(_matches);
		}

		public override void MatchAll()
		{
			_matches.Clear();
			IList<RAINAspect> aspects = SensorManager.Instance.GetAspects("audio");
			float aSqrRange = Range * Range;
			foreach (RAINAspect item in aspects)
			{
				if (item != null && item.IsActive && item.Entity != null && !(item.Entity.Form == null) && item.Entity.Form.activeInHierarchy && TestVisibility(item, aSqrRange))
				{
					_matches.Add(item);
				}
			}
			Filter(_matches);
		}

		protected virtual bool TestVisibility(RAINAspect aAspect, float aSqrRange)
		{
			if (aAspect.Entity.Form == null)
			{
				return false;
			}
			Transform transform = aAspect.Entity.Form.transform;
			if (aAspect.MountPoint != null)
			{
				transform = aAspect.MountPoint.transform;
			}
			if (!CanDetectSelf && transform != null && (transform == AI.Body.transform || transform.IsChildOf(AI.Body.transform)))
			{
				return false;
			}
			if ((aAspect.Position - Position).sqrMagnitude > aSqrRange)
			{
				return false;
			}
			AudioAspect audioAspect = aAspect as AudioAspect;
			if (audioAspect == null || audioAspect.UnityAudioSource == null)
			{
				return false;
			}
			if (!audioAspect.UnityAudioSource.isPlaying)
			{
				return false;
			}
			Vector3 vector = audioAspect.Position - Position;
			if (audioAspect.UnityAudioSource.maxDistance < vector.magnitude)
			{
				return false;
			}
			if (GetVolumeLevel(audioAspect.UnityAudioSource, vector.magnitude) < _volumeThreshold)
			{
				return false;
			}
			return true;
		}

		public virtual float GetVolumeLevel(AudioSource aSource, float aDistance)
		{
			if (aSource == null)
			{
				return 0f;
			}
			if (aDistance <= Mathf.Max(aSource.minDistance, 0f))
			{
				return 1f;
			}
			if (aDistance >= aSource.maxDistance)
			{
				return 0f;
			}
			if (aSource.rolloffMode == AudioRolloffMode.Linear)
			{
				return (aSource.maxDistance - aDistance) / (aSource.maxDistance - aSource.minDistance);
			}
			if (aSource.rolloffMode == AudioRolloffMode.Logarithmic)
			{
				return 1f / (1f + (aDistance - aSource.minDistance));
			}
			return 1f;
		}
	}
}
