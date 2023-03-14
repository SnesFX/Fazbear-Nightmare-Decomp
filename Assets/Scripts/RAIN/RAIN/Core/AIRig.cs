using System;
using RAIN.Serialization;
using RAIN.Utility.Version;
using UnityEngine;

namespace RAIN.Core
{
	[AddComponentMenu("Rival Theory/RAIN/AI Rig")]
	public class AIRig : RAINComponent
	{
		[RAINSerializableField(Visibility = FieldVisibility.Show)]
		private VisualModeEnum _visualMode = VisualModeEnum.OnWhenSelected;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Determines whether AI methods are called manually or by Unity")]
		private bool _useUnityMessages = true;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Determines whether AI responds to Unity Update or FixedUpdate messages")]
		private bool _useFixedUpdate;

		[RAINSerializableField(Visibility = FieldVisibility.Show)]
		private AI _ai = new AI();

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

		public bool UseUnityMessages
		{
			get
			{
				return _useUnityMessages;
			}
			set
			{
				_useUnityMessages = value;
			}
		}

		public bool UseFixedUpdate
		{
			get
			{
				return _useFixedUpdate;
			}
			set
			{
				_useFixedUpdate = value;
			}
		}

		public AI AI
		{
			get
			{
				return _ai;
			}
		}

		public static AIRig FindRig(GameObject aObject)
		{
			AIRig componentInChildren = aObject.GetComponentInChildren<AIRig>();
			if (componentInChildren == null)
			{
				return null;
			}
			if (componentInChildren.gameObject == aObject || (componentInChildren.transform.parent != null && componentInChildren.transform.parent.gameObject == aObject))
			{
				return componentInChildren;
			}
			return null;
		}

		public static AIRig AddRig(GameObject aObject)
		{
			if (aObject == null)
			{
				return null;
			}
			AIRig aIRig = FindRig(aObject);
			if (aIRig != null)
			{
				Debug.Log("AddRig called on an already rigged object.");
				return aIRig;
			}
			GameObject gameObject = new GameObject("AI");
			gameObject.transform.position = aObject.transform.position;
			gameObject.transform.rotation = aObject.transform.rotation;
			gameObject.transform.parent = aObject.transform;
			aIRig = gameObject.AddComponent<AIRig>();
			aIRig.AI.Body = aObject;
			aIRig.Serialize();
			return aIRig;
		}

		public override void Awake()
		{
			base.Awake();
			if (_useUnityMessages)
			{
				AIAwake();
			}
		}

		public void AIAwake()
		{
			_ai.AIInit();
			_ai.BodyInit();
		}

		public override void Start()
		{
			base.Start();
			if (_useUnityMessages)
			{
				AIStart();
			}
		}

		public void AIStart()
		{
			_ai.Start();
		}

		public override void Update()
		{
			base.Update();
			if (_useUnityMessages && !_useFixedUpdate)
			{
				AIUpdate();
			}
		}

		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (_useUnityMessages && _useFixedUpdate)
			{
				AIUpdate();
			}
		}

		public void AIUpdate()
		{
			_ai.UpdateTime();
			_ai.Pre();
			_ai.Sense();
			_ai.Think();
			_ai.Act();
		}

		public override void LateUpdate()
		{
			base.LateUpdate();
			if (_useUnityMessages)
			{
				AILateUpdate();
			}
		}

		public void AILateUpdate()
		{
			_ai.Post();
		}

		public override void OnAnimatorMove()
		{
			base.OnAnimatorMove();
			if (_useUnityMessages)
			{
				AIRootMotion();
			}
		}

		public void AIRootMotion()
		{
			_ai.RootMotion();
		}

		public override void OnAnimatorIK(int aLayerIndex)
		{
			base.OnAnimatorIK(aLayerIndex);
			if (_useUnityMessages)
			{
				AIIK(aLayerIndex);
			}
		}

		public void AIIK(int aLayerIndex)
		{
			_ai.IK(aLayerIndex);
		}

		public override void OnDestroy()
		{
			_ai.Destroy();
			base.OnDestroy();
		}

		protected override bool OnVersion(string aVersion)
		{
			bool result = base.OnVersion(aVersion);
			if (aVersion == "1.0")
			{
				TwoOneZero twoOneZero = new TwoOneZero();
				twoOneZero.UpgradeBasicMemory(base.DataSerializer);
				twoOneZero.UpgradeMecanimMotor(base.DataSerializer);
				result = true;
			}
			return result;
		}

		protected override void Reset()
		{
			_ai.Reset(this);
			base.Reset();
		}

		protected override void OnValidate()
		{
			base.OnValidate();
			bool flag = false;
			for (int i = 0; i < AI.Senses.Sensors.Count; i++)
			{
				if (AI.Senses.Sensors[i] == null)
				{
					flag = true;
					AI.Senses.RemoveSensor(i--);
				}
			}
			for (int j = 0; j < AI.CustomElements.Count; j++)
			{
				if (AI.CustomElements[j] == null)
				{
					flag = true;
					AI.RemoveCustomElement(j--);
				}
			}
			if (flag)
			{
				Debug.LogWarning("AIRig: Reserializing due to a missing sensor or custom element type on the AI", base.gameObject);
				Serialize();
			}
		}
	}
}
