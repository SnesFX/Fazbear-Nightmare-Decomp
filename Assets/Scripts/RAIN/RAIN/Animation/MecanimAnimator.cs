using System;
using System.Collections.Generic;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Animation
{
	[RAINSerializableClass]
	public class MecanimAnimator : RAINAnimator
	{
		public enum SupportedMecanimIKType
		{
			RightFoot = 0,
			LeftFoot = 1,
			RightHand = 2,
			LeftHand = 3,
			LookAt = 4
		}

		[RAINSerializableClass]
		public class IKLookAt
		{
			public bool isActive;

			public float lookAtWeight;

			public float lookAtBodyWeight;

			public float lookAtHeadWeight;

			public float lookAtEyesWeight;

			public float lookAtClampWeight;

			public float maxTurnRate = 90f;

			public Vector3 positionVector = Vector3.zero;

			public Vector3 lastPositionVector = Vector3.zero;

			public void Copy(IKLookAt aLookAt, bool aCopyLastPosition = false)
			{
				if (aLookAt != null)
				{
					isActive = aLookAt.isActive;
					positionVector = aLookAt.positionVector;
					lookAtWeight = aLookAt.lookAtWeight;
					lookAtBodyWeight = aLookAt.lookAtBodyWeight;
					lookAtHeadWeight = aLookAt.lookAtHeadWeight;
					lookAtEyesWeight = aLookAt.lookAtEyesWeight;
					lookAtClampWeight = aLookAt.lookAtClampWeight;
					maxTurnRate = aLookAt.maxTurnRate;
					if (aCopyLastPosition)
					{
						lastPositionVector = aLookAt.lastPositionVector;
					}
				}
			}
		}

		[RAINSerializableClass]
		public class IKTarget
		{
			public bool isActive;

			public float positionWeight;

			public float rotationWeight;

			public Vector3 positionVector = Vector3.zero;

			public Vector3 rotationVector = Vector3.zero;

			public void Copy(IKTarget aTarget)
			{
				if (aTarget != null)
				{
					isActive = aTarget.isActive;
					positionWeight = aTarget.positionWeight;
					rotationWeight = aTarget.rotationWeight;
					positionVector = aTarget.positionVector;
					rotationVector = aTarget.rotationVector;
				}
			}
		}

		[RAINSerializableClass]
		public class MecanimAnimatorState
		{
			[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "State machine state name", OldFieldNames = new string[] { "mecanimStateName" })]
			public string mecanimState = "";

			[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Mecanim layer for state")]
			public int layer;

			[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Mecanim parameter set when playback starts")]
			public MecanimParameterValue startParameter = new MecanimParameterValue();

			[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Mecanim parameter set when playback ends")]
			public MecanimParameterValue stopParameter = new MecanimParameterValue();
		}

		public const string STATE_IKRIGHTFOOT = "IKRightFoot";

		public const string STATE_IKLEFTFOOT = "IKLeftFoot";

		public const string STATE_IKRIGHTHAND = "IKRightHand";

		public const string STATE_IKLEFTHAND = "IKLeftHand";

		public const string STATE_IKLOOKAT = "IKLookAt";

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Right Foot IK")]
		private IKTarget _rightFoot = new IKTarget();

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Left Foot IK")]
		private IKTarget _leftFoot = new IKTarget();

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Right Hand IK")]
		private IKTarget _rightHand = new IKTarget();

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Left Hand IK")]
		private IKTarget _leftHand = new IKTarget();

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Look At IK")]
		private IKLookAt _lookAt = new IKLookAt();

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "A list of states managed by this Animator")]
		private List<MecanimAnimatorState> _animationStates = new List<MecanimAnimatorState>();

		private Animator _mecanimAnimator;

		private IKProxy _ikProxy;

		private Dictionary<string, MecanimAnimatorState> _animationStateDictionary = new Dictionary<string, MecanimAnimatorState>();

		private MecanimAnimatorState _currentState;

		private Transform _lastHeadTransform;

		public virtual IKTarget RightFoot
		{
			get
			{
				return _rightFoot;
			}
			set
			{
				_rightFoot = value;
			}
		}

		public virtual IKTarget LeftFoot
		{
			get
			{
				return _leftFoot;
			}
			set
			{
				_leftFoot = value;
			}
		}

		public virtual IKTarget RightHand
		{
			get
			{
				return _rightHand;
			}
			set
			{
				_rightHand = value;
			}
		}

		public virtual IKTarget LeftHand
		{
			get
			{
				return _leftHand;
			}
			set
			{
				_leftHand = value;
			}
		}

		public virtual IKLookAt LookAt
		{
			get
			{
				return _lookAt;
			}
			set
			{
				_lookAt = value;
			}
		}

		public IList<MecanimAnimatorState> AnimationStates
		{
			get
			{
				return _animationStates.AsReadOnly();
			}
		}

		public virtual Animator UnityAnimator
		{
			get
			{
				return _mecanimAnimator;
			}
			set
			{
				_mecanimAnimator = value;
			}
		}

		public IKProxy IKProxy
		{
			get
			{
				return _ikProxy;
			}
			set
			{
				_ikProxy = value;
			}
		}

		public override void BodyInit()
		{
			base.BodyInit();
			if (AI.Body == null)
			{
				UnityAnimator = null;
				if (IKProxy != null)
				{
					IKProxy.Cleanup();
				}
				IKProxy = null;
				_animationStateDictionary.Clear();
			}
			else
			{
				UnityAnimator = AI.Body.GetComponent<Animator>();
				if (UnityAnimator == null)
				{
					if (_animationStates.Count > 0)
					{
						Debug.LogWarning("MecanimAnimator: No Animator present on AI Body (adding a temporary one)", AI.Body);
					}
					UnityAnimator = AI.Body.AddComponent<Animator>();
				}
				IKProxy = AI.Body.GetComponent<IKProxy>();
				if (IKProxy == null)
				{
					IKProxy = AI.Body.AddComponent<IKProxy>();
				}
				_animationStateDictionary.Clear();
				for (int i = 0; i < _animationStates.Count; i++)
				{
					if (_animationStates[i] != null && !(_animationStates[i].mecanimState == ""))
					{
						_animationStateDictionary[_animationStates[i].mecanimState] = _animationStates[i];
					}
				}
			}
			_currentState = null;
			_lastHeadTransform = null;
		}

		public void ClearAnimationStates()
		{
			_animationStates.Clear();
			if (Initialized)
			{
				_animationStateDictionary.Clear();
			}
			_currentState = null;
		}

		[Obsolete("Use AddAnimationState instead")]
		public void AddNewAnimationState()
		{
			AddAnimationState(new MecanimAnimatorState());
		}

		public void AddAnimationState(MecanimAnimatorState aState)
		{
			if (aState != null)
			{
				_animationStates.Add(aState);
				if (Initialized && aState.mecanimState != "")
				{
					_animationStateDictionary[aState.mecanimState] = aState;
				}
			}
		}

		public bool IsInTransition(string aStateName)
		{
			if (!string.IsNullOrEmpty(aStateName) && _animationStateDictionary.ContainsKey(aStateName))
			{
				return UnityAnimator.IsInTransition(_animationStateDictionary[aStateName].layer);
			}
			return false;
		}

		public bool IsInTransition(int aLayer)
		{
			return UnityAnimator.IsInTransition(aLayer);
		}

		public override bool StartState(string aStateName)
		{
			if (string.IsNullOrEmpty(aStateName))
			{
				return false;
			}
			switch (aStateName)
			{
			case "IKRightFoot":
				RightFoot.isActive = true;
				break;
			case "IKLeftFoot":
				LeftFoot.isActive = true;
				break;
			case "IKRightHand":
				RightHand.isActive = true;
				break;
			case "IKLeftHand":
				LeftHand.isActive = true;
				break;
			case "IKLookAt":
				if (!LookAt.isActive)
				{
					_lastHeadTransform = null;
				}
				LookAt.isActive = true;
				break;
			default:
				if (_animationStateDictionary.ContainsKey(aStateName))
				{
					MecanimAnimatorState mecanimAnimatorState = _animationStateDictionary[aStateName];
					if (UnityAnimator.IsInTransition(mecanimAnimatorState.layer))
					{
						return false;
					}
					if (_currentState != null && _currentState.mecanimState != aStateName)
					{
						StopState(_currentState.mecanimState);
					}
					_currentState = mecanimAnimatorState;
					mecanimAnimatorState.startParameter.SetParameter(UnityAnimator, AI);
				}
				return false;
			}
			return true;
		}

		public override bool IsStatePlaying(string aStateName)
		{
			if (string.IsNullOrEmpty(aStateName))
			{
				return false;
			}
			switch (aStateName)
			{
			case "IKRightFoot":
				return RightFoot.isActive;
			case "IKLeftFoot":
				return LeftFoot.isActive;
			case "IKRightHand":
				return RightHand.isActive;
			case "IKLeftHand":
				return LeftHand.isActive;
			case "IKLookAt":
				return LookAt.isActive;
			default:
				if (_animationStateDictionary.ContainsKey(aStateName))
				{
					int num = Animator.StringToHash(aStateName);
					MecanimAnimatorState mecanimAnimatorState = _animationStateDictionary[aStateName];
					AnimatorStateInfo currentAnimatorStateInfo = UnityAnimator.GetCurrentAnimatorStateInfo(mecanimAnimatorState.layer);
					if (currentAnimatorStateInfo.nameHash == num || currentAnimatorStateInfo.tagHash == num)
					{
						return true;
					}
					if (UnityAnimator.IsInTransition(mecanimAnimatorState.layer))
					{
						currentAnimatorStateInfo = UnityAnimator.GetNextAnimatorStateInfo(mecanimAnimatorState.layer);
						if (currentAnimatorStateInfo.nameHash == num || currentAnimatorStateInfo.tagHash == num)
						{
							return true;
						}
					}
				}
				else
				{
					int num2 = Animator.StringToHash(aStateName);
					if (UnityAnimator.GetCurrentAnimatorStateInfo(0).nameHash == num2)
					{
						return true;
					}
					if (UnityAnimator.IsInTransition(0))
					{
						AnimatorStateInfo nextAnimatorStateInfo = UnityAnimator.GetNextAnimatorStateInfo(0);
						if (nextAnimatorStateInfo.nameHash == num2 || nextAnimatorStateInfo.tagHash == num2)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		public override bool StopState(string aStateName)
		{
			if (string.IsNullOrEmpty(aStateName))
			{
				return false;
			}
			switch (aStateName)
			{
			case "IKRightFoot":
				RightFoot.isActive = false;
				break;
			case "IKLeftFoot":
				LeftFoot.isActive = false;
				break;
			case "IKRightHand":
				RightHand.isActive = false;
				break;
			case "IKLeftHand":
				LeftHand.isActive = false;
				break;
			case "IKLookAt":
				LookAt.isActive = false;
				break;
			default:
				if (_currentState != null && _currentState.mecanimState == aStateName)
				{
					_currentState = null;
				}
				if (_animationStateDictionary.ContainsKey(aStateName))
				{
					MecanimAnimatorState mecanimAnimatorState = _animationStateDictionary[aStateName];
					_currentState = null;
					mecanimAnimatorState.stopParameter.SetParameter(UnityAnimator, AI);
					return true;
				}
				return false;
			}
			return true;
		}

		public override void UpdateAnimation()
		{
			bool flag = _lastHeadTransform == null;
			_lastHeadTransform = UnityAnimator.GetBoneTransform(HumanBodyBones.Head);
			if (flag && _lastHeadTransform != null)
			{
				LookAt.lastPositionVector = _lastHeadTransform.TransformPoint(Vector3.forward);
			}
		}

		public virtual bool SetState(string aStateName, IKLookAt aLookAt)
		{
			if (aStateName == "IKLookAt")
			{
				return SetState(SupportedMecanimIKType.LookAt, aLookAt);
			}
			return false;
		}

		public virtual bool SetState(SupportedMecanimIKType aIKType, IKLookAt aLookAt)
		{
			if (aLookAt == null)
			{
				return false;
			}
			if (aIKType == SupportedMecanimIKType.LookAt)
			{
				LookAt.Copy(aLookAt);
				return true;
			}
			return false;
		}

		public virtual bool SetState(string aStateName, IKTarget aTarget)
		{
			switch (aStateName)
			{
			case "IKRightFoot":
				return SetState(SupportedMecanimIKType.RightFoot, aTarget);
			case "IKLeftFoot":
				return SetState(SupportedMecanimIKType.LeftFoot, aTarget);
			case "IKRightHand":
				return SetState(SupportedMecanimIKType.RightHand, aTarget);
			case "IKLeftHand":
				return SetState(SupportedMecanimIKType.LeftHand, aTarget);
			default:
				return false;
			}
		}

		public virtual bool SetState(SupportedMecanimIKType aIKType, IKTarget aTarget)
		{
			if (aTarget == null)
			{
				return false;
			}
			IKTarget iKTarget = null;
			switch (aIKType)
			{
			case SupportedMecanimIKType.RightFoot:
				iKTarget = RightFoot;
				break;
			case SupportedMecanimIKType.LeftFoot:
				iKTarget = LeftFoot;
				break;
			case SupportedMecanimIKType.RightHand:
				iKTarget = RightHand;
				break;
			case SupportedMecanimIKType.LeftHand:
				iKTarget = LeftHand;
				break;
			}
			if (iKTarget == null)
			{
				return false;
			}
			iKTarget.Copy(aTarget);
			return true;
		}

		public override List<string> GetStates()
		{
			List<string> list = new List<string>();
			list.Add("IKRightFoot");
			list.Add("IKLeftFoot");
			list.Add("IKRightHand");
			list.Add("IKLeftHand");
			list.Add("IKLookAt");
			for (int i = 0; i < _animationStates.Count; i++)
			{
				if (_animationStates[i] != null && !string.IsNullOrEmpty(_animationStates[i].mecanimState))
				{
					list.Add(_animationStates[i].mecanimState);
				}
			}
			return list;
		}

		public MecanimAnimatorState GetAnimationState(string aStateName)
		{
			if (string.IsNullOrEmpty(aStateName))
			{
				return null;
			}
			MecanimAnimatorState value;
			if (_animationStateDictionary.TryGetValue(aStateName, out value))
			{
				return value;
			}
			return null;
		}

		public override void UpdateIK(int aLayerIndex)
		{
			if (LeftFoot.isActive)
			{
				SetIKForGoal(AvatarIKGoal.LeftFoot, LeftFoot);
			}
			if (RightFoot.isActive)
			{
				SetIKForGoal(AvatarIKGoal.RightFoot, RightFoot);
			}
			if (LeftHand.isActive)
			{
				SetIKForGoal(AvatarIKGoal.LeftHand, LeftHand);
			}
			if (RightHand.isActive)
			{
				SetIKForGoal(AvatarIKGoal.RightHand, RightHand);
			}
			if (LookAt.isActive)
			{
				SetLookAt(LookAt);
			}
		}

		private void SetIKForGoal(AvatarIKGoal goal, IKTarget entry)
		{
			UnityAnimator.SetIKPosition(goal, entry.positionVector);
			UnityAnimator.SetIKPositionWeight(goal, entry.positionWeight);
			UnityAnimator.SetIKRotation(goal, Quaternion.Euler(entry.rotationVector));
			UnityAnimator.SetIKRotationWeight(goal, entry.rotationWeight);
		}

		private void SetLookAt(IKLookAt entry)
		{
			if (!(_lastHeadTransform == null))
			{
				Vector3 normalized = (entry.lastPositionVector - _lastHeadTransform.position).normalized;
				Vector3 normalized2 = (entry.positionVector - _lastHeadTransform.position).normalized;
				float num = Vector3.Angle(normalized, normalized2);
				float t = 1f;
				if (!Mathf.Approximately(num, 0f))
				{
					t = Mathf.Clamp01(entry.maxTurnRate * AI.DeltaTime / num);
				}
				entry.lastPositionVector = Vector3.Lerp(entry.lastPositionVector, entry.positionVector, t);
				UnityAnimator.SetLookAtPosition(entry.lastPositionVector);
				UnityAnimator.SetLookAtWeight(entry.lookAtWeight, entry.lookAtBodyWeight, entry.lookAtHeadWeight, entry.lookAtEyesWeight, entry.lookAtClampWeight);
			}
		}
	}
}
