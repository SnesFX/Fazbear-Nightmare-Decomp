using RAIN.Core;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Entities.Aspects
{
	[RAINSerializableClass]
	[RAINElement("Audio Aspect")]
	public class AudioAspect : RAINAspect
	{
		public const string cnstAspectType = "audio";

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "The Audio Source for this Aspect")]
		private AudioSource _audioSource;

		public override string AspectType
		{
			get
			{
				return "audio";
			}
		}

		public AudioSource UnityAudioSource
		{
			get
			{
				return _audioSource;
			}
			set
			{
				_audioSource = value;
			}
		}

		public AudioAspect()
		{
		}

		public AudioAspect(string aAspectName)
			: base(aAspectName)
		{
		}

		public AudioAspect(string aAspectName, AudioSource aAudioSource)
			: base(aAspectName)
		{
			_audioSource = aAudioSource;
		}
	}
}
