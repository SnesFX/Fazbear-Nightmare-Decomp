using System;
using System.Collections.Generic;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Animation
{
	[RAINSerializableClass]
	public class BasicAnimator : RAINAnimator
	{
		[RAINSerializableClass]
		public class BasicAnimatorState
		{
			[RAINSerializableClass]
			public class MixingTransform
			{
				[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Transform to apply")]
				public Transform transform;

				[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Is the mixing transform recursive?")]
				public bool recursive;
			}

			[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Name of this Basic Animation State")]
			public string stateName = "";

			[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Animation clip to play")]
			public AnimationClip animationClip;

			[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Layer on which to play this clip")]
			public int layer;

			[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Max blend weight when playing this clip")]
			public float weight = 1f;

			[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Default playback speed of the clip")]
			public float speed = 1f;

			[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Fade in time when playback starts")]
			public float fadeInTime;

			[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Fade out time when playback ends")]
			public float fadeOutTime;

			[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Looping Wrap Mode")]
			public WrapMode wrapMode;

			[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Mixing transforms to apply")]
			public List<MixingTransform> mixingTransforms = new List<MixingTransform>();

			[RAINNonSerializableField]
			public string runtimeAnimationName;
		}

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Available Basic Animation States")]
		private List<BasicAnimatorState> _animationStates = new List<BasicAnimatorState>();

		private UnityEngine.Animation _unityAnimation;

		private Dictionary<string, BasicAnimatorState> _animationStateDictionary = new Dictionary<string, BasicAnimatorState>();

		public IList<BasicAnimatorState> AnimationStates
		{
			get
			{
				return _animationStates.AsReadOnly();
			}
		}

		public virtual UnityEngine.Animation UnityAnimation
		{
			get
			{
				return _unityAnimation;
			}
			set
			{
				if (_unityAnimation == value)
				{
					return;
				}
				_unityAnimation = value;
				_animationStateDictionary.Clear();
				if (_unityAnimation != null)
				{
					for (int i = 0; i < _animationStates.Count; i++)
					{
						InitAnimationState(_animationStates[i], RuntimeBaseName + i);
					}
				}
			}
		}

		public virtual string RuntimeBaseName
		{
			get
			{
				return "__rain_basicanimator";
			}
		}

		public override void BodyInit()
		{
			base.BodyInit();
			if (AI.Body == null)
			{
				UnityAnimation = null;
				return;
			}
			UnityAnimation = AI.Body.GetComponent<UnityEngine.Animation>();
			if (UnityAnimation == null)
			{
				if (_animationStates.Count > 0)
				{
					Debug.LogWarning("BasicAnimator: No Legacy Animation on AI Body (adding a temporary one)", AI.Body);
				}
				UnityAnimation = AI.Body.AddComponent<UnityEngine.Animation>();
			}
		}

		public void ClearAnimationStates()
		{
			_animationStates.Clear();
			if (Initialized)
			{
				_animationStateDictionary.Clear();
			}
		}

		[Obsolete("Use AddAllExistingAnimationStates instead")]
		public void AddExistingAnimationStates(UnityEngine.Animation aAnimationComponent)
		{
			AddAllExistingAnimationStates(aAnimationComponent);
		}

		public void AddAllExistingAnimationStates(UnityEngine.Animation aAnimationComponent)
		{
			foreach (AnimationState item in aAnimationComponent)
			{
				AddExistingAnimationState(aAnimationComponent, item.name);
			}
		}

		public void AddExistingAnimationState(UnityEngine.Animation aAnimationComponent, string aStateName)
		{
			if (aAnimationComponent == null)
			{
				return;
			}
			AnimationState animationState = aAnimationComponent[aStateName];
			if (animationState == null)
			{
				return;
			}
			for (int i = 0; i < _animationStates.Count; i++)
			{
				if (_animationStates[i].stateName == animationState.name)
				{
					return;
				}
			}
			BasicAnimatorState basicAnimatorState = new BasicAnimatorState();
			basicAnimatorState.stateName = animationState.name;
			basicAnimatorState.animationClip = animationState.clip;
			basicAnimatorState.layer = animationState.layer;
			basicAnimatorState.weight = 1f;
			basicAnimatorState.speed = animationState.speed;
			basicAnimatorState.wrapMode = animationState.wrapMode;
			_animationStates.Add(basicAnimatorState);
			if (Initialized)
			{
				InitAnimationState(basicAnimatorState, RuntimeBaseName + (_animationStates.Count - 1));
			}
		}

		public void AddAnimationState(BasicAnimatorState aState)
		{
			_animationStates.Add(aState);
			if (Initialized)
			{
				InitAnimationState(aState, RuntimeBaseName + (_animationStates.Count - 1));
			}
		}

		public override bool StartState(string aStateName)
		{
			BasicAnimatorState animationState = GetAnimationState(aStateName);
			if (animationState == null)
			{
				return false;
			}
			if (animationState.weight < 0f || Mathf.Approximately(animationState.weight, 0f))
			{
				return true;
			}
			float num = 0f;
			if (!UnityAnimation.IsPlaying(animationState.runtimeAnimationName))
			{
				_unityAnimation[animationState.runtimeAnimationName].weight = 1E-05f;
				num = 0f;
			}
			else
			{
				num = _unityAnimation[animationState.runtimeAnimationName].weight / animationState.weight;
			}
			UnityAnimation.Blend(animationState.runtimeAnimationName, animationState.weight, Mathf.Max(0f, (1f - num) * animationState.fadeInTime));
			return true;
		}

		public override bool IsStatePlaying(string aStateName)
		{
			BasicAnimatorState animationState = GetAnimationState(aStateName);
			if (animationState == null)
			{
				return false;
			}
			if (UnityAnimation.IsPlaying(animationState.runtimeAnimationName))
			{
				return _unityAnimation[animationState.runtimeAnimationName].weight > 0f;
			}
			return false;
		}

		public override bool StopState(string aStateName)
		{
			BasicAnimatorState animationState = GetAnimationState(aStateName);
			if (animationState == null)
			{
				return false;
			}
			float num = 0f;
			num = ((!(animationState.weight < 0f) && !Mathf.Approximately(animationState.weight, 0f)) ? (_unityAnimation[animationState.runtimeAnimationName].weight / animationState.weight) : 0f);
			if (UnityAnimation.IsPlaying(animationState.runtimeAnimationName))
			{
				UnityAnimation.Blend(animationState.runtimeAnimationName, 0f, Mathf.Max(0f, num * animationState.fadeOutTime));
			}
			return true;
		}

		public override void UpdateAnimation()
		{
		}

		public override List<string> GetStates()
		{
			List<string> list = new List<string>();
			for (int i = 0; i < _animationStates.Count; i++)
			{
				list.Add(_animationStates[i].stateName);
			}
			return list;
		}

		public BasicAnimatorState GetAnimationState(string aStateName)
		{
			if (aStateName == null)
			{
				return null;
			}
			BasicAnimatorState value;
			if (_animationStateDictionary.TryGetValue(aStateName, out value))
			{
				return value;
			}
			return null;
		}

		public override void UpdateIK(int aLayerIndex)
		{
		}

		public virtual bool ScaleSpeed(string aStateName, float aSpeedScale)
		{
			BasicAnimatorState animationState = GetAnimationState(aStateName);
			if (animationState == null)
			{
				return false;
			}
			_unityAnimation[animationState.runtimeAnimationName].speed = animationState.speed * aSpeedScale;
			return true;
		}

		private void InitAnimationState(BasicAnimatorState aState, string aRuntimeAnimationName)
		{
			if (aState != null && !(aState.animationClip == null))
			{
				aState.runtimeAnimationName = aRuntimeAnimationName;
				if (UnityAnimation.GetClip(aState.runtimeAnimationName) != null)
				{
					UnityAnimation.RemoveClip(aState.runtimeAnimationName);
				}
				UnityAnimation.AddClip(aState.animationClip, aState.runtimeAnimationName);
				AnimationState animationState = _unityAnimation[aState.runtimeAnimationName];
				animationState.weight = 0f;
				animationState.layer = aState.layer;
				animationState.speed = aState.speed;
				animationState.wrapMode = aState.wrapMode;
				for (int i = 0; i < aState.mixingTransforms.Count; i++)
				{
					animationState.AddMixingTransform(aState.mixingTransforms[i].transform, aState.mixingTransforms[i].recursive);
				}
				_animationStateDictionary[aState.stateName] = aState;
			}
		}
	}
}
