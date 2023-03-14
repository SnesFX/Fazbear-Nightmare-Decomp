using System;
using System.Collections.Generic;
using RAIN.Animation;
using RAIN.Memory;
using RAIN.Minds;
using RAIN.Motion;
using RAIN.Navigation;
using RAIN.Perception;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Core
{
	[RAINSerializableClass]
	public class AI
	{
		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Toggle AI activity on/off")]
		private bool _isActive = true;

		[RAINSerializableField(ToolTip = "The root body node the AI will move and animate")]
		private GameObject _body;

		[RAINSerializableField]
		private RAINMemory _workingMemory = new BasicMemory();

		[RAINSerializableField]
		private RAINMind _mind = new BasicMind();

		[RAINSerializableField]
		private RAINMotor _motor = new BasicMotor();

		[RAINSerializableField]
		private RAINAnimator _animator = new BasicAnimator();

		[RAINSerializableField]
		private RAINNavigator _navigator = new BasicNavigator();

		[RAINSerializableField]
		private RAINSenses _senses = new BasicSenses();

		[RAINSerializableField]
		private List<CustomAIElement> _customElements = new List<CustomAIElement>();

		private Kinematic _kinematic = new Kinematic();

		private bool _initialized;

		private bool _started;

		public float Time { get; set; }

		public float DeltaTime { get; set; }

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

		public virtual GameObject Body
		{
			get
			{
				return _body;
			}
			set
			{
				if (!(_body == value))
				{
					_body = value;
					BodyInit();
				}
			}
		}

		public virtual RAINMemory WorkingMemory
		{
			get
			{
				return _workingMemory;
			}
			set
			{
				_workingMemory = value;
			}
		}

		public virtual RAINMind Mind
		{
			get
			{
				return _mind;
			}
			set
			{
				_mind = value;
			}
		}

		public virtual RAINMotor Motor
		{
			get
			{
				return _motor;
			}
			set
			{
				_motor = value;
			}
		}

		public virtual RAINAnimator Animator
		{
			get
			{
				return _animator;
			}
			set
			{
				_animator = value;
			}
		}

		public virtual RAINNavigator Navigator
		{
			get
			{
				return _navigator;
			}
			set
			{
				_navigator = value;
			}
		}

		public virtual RAINSenses Senses
		{
			get
			{
				return _senses;
			}
			set
			{
				_senses = value;
			}
		}

		public IList<CustomAIElement> CustomElements
		{
			get
			{
				return _customElements.AsReadOnly();
			}
		}

		public virtual Kinematic Kinematic
		{
			get
			{
				return _kinematic;
			}
			set
			{
				_kinematic = value;
			}
		}

		public bool Initialized
		{
			get
			{
				return _initialized;
			}
		}

		public bool Started
		{
			get
			{
				return _started;
			}
		}

		public virtual void AIInit()
		{
			WorkingMemory.AIInit(this);
			Mind.AIInit(this);
			Motor.AIInit(this);
			Animator.AIInit(this);
			Navigator.AIInit(this);
			Senses.AIInit(this);
			for (int i = 0; i < _customElements.Count; i++)
			{
				_customElements[i].AIInit(this);
			}
			_initialized = true;
		}

		public virtual void BodyInit()
		{
			if (Initialized)
			{
				WorkingMemory.BodyInit();
				Mind.BodyInit();
				Motor.BodyInit();
				Animator.BodyInit();
				Navigator.BodyInit();
				Senses.BodyInit();
				for (int i = 0; i < _customElements.Count; i++)
				{
					_customElements[i].BodyInit();
				}
			}
		}

		public virtual void UpdateTime()
		{
			if (IsActive && !(Body == null))
			{
				Time = UnityEngine.Time.time;
				DeltaTime = UnityEngine.Time.deltaTime;
			}
		}

		public virtual void Start()
		{
			WorkingMemory.Start();
			Mind.Start();
			Motor.Start();
			Animator.Start();
			Navigator.Start();
			Senses.Start();
			foreach (CustomAIElement customElement in _customElements)
			{
				customElement.Start();
			}
			_started = true;
		}

		public virtual void Pre()
		{
			if (!IsActive || Body == null)
			{
				return;
			}
			Motor.UpdateMotionTransforms();
			foreach (CustomAIElement customElement in _customElements)
			{
				customElement.Pre();
			}
		}

		public virtual void Sense()
		{
			if (!IsActive || Body == null)
			{
				return;
			}
			Senses.UpdateSenses();
			foreach (CustomAIElement customElement in _customElements)
			{
				if (customElement != null)
				{
					customElement.Sense();
				}
			}
		}

		public virtual void Think()
		{
			if (!IsActive || Body == null)
			{
				return;
			}
			Mind.Think();
			foreach (CustomAIElement customElement in _customElements)
			{
				customElement.Think();
			}
		}

		public virtual void Act()
		{
			if (!IsActive || Body == null)
			{
				return;
			}
			Motor.ApplyMotionTransforms();
			foreach (CustomAIElement customElement in _customElements)
			{
				customElement.Act();
			}
		}

		public virtual void Post()
		{
			if (!IsActive || Body == null)
			{
				return;
			}
			Motor.UpdateAnimation();
			Animator.UpdateAnimation();
			foreach (CustomAIElement customElement in _customElements)
			{
				customElement.Post();
			}
		}

		public virtual void RootMotion()
		{
			if (!IsActive || Body == null)
			{
				return;
			}
			Motor.UpdateRootMotion();
			foreach (CustomAIElement customElement in _customElements)
			{
				customElement.RootMotion();
			}
		}

		public virtual void IK(int aLayerIndex)
		{
			if (!IsActive || Body == null)
			{
				return;
			}
			Motor.UpdateIK(aLayerIndex);
			Animator.UpdateIK(aLayerIndex);
			foreach (CustomAIElement customElement in _customElements)
			{
				customElement.IK(aLayerIndex);
			}
		}

		public virtual void Destroy()
		{
			foreach (CustomAIElement customElement in _customElements)
			{
				customElement.Destroy();
			}
		}

		public virtual void Reset(RAINComponent aComponent)
		{
			WorkingMemory = (RAINMemory)Activator.CreateInstance(WorkingMemory.GetType());
			WorkingMemory.Reset(aComponent);
			Senses = (RAINSenses)Activator.CreateInstance(Senses.GetType());
			Senses.Reset(aComponent);
			Mind = (RAINMind)Activator.CreateInstance(Mind.GetType());
			Mind.Reset(aComponent);
			Navigator = (RAINNavigator)Activator.CreateInstance(Navigator.GetType());
			Navigator.Reset(aComponent);
			Motor = (RAINMotor)Activator.CreateInstance(Motor.GetType());
			Motor.Reset(aComponent);
			Animator = (RAINAnimator)Activator.CreateInstance(Animator.GetType());
			Animator.Reset(aComponent);
			foreach (CustomAIElement customElement in _customElements)
			{
				if (customElement != null)
				{
					customElement.Reset(aComponent);
				}
			}
		}

		public void AddCustomElement(CustomAIElement aCustomElement)
		{
			if (aCustomElement != null && !_customElements.Contains(aCustomElement))
			{
				_customElements.Add(aCustomElement);
				if (Initialized)
				{
					aCustomElement.AIInit(this);
					aCustomElement.BodyInit();
				}
				if (Started)
				{
					aCustomElement.Start();
				}
			}
		}

		public void RemoveCustomElement(CustomAIElement aCustomElement)
		{
			_customElements.Remove(aCustomElement);
		}

		public void RemoveCustomElement(int aIndex)
		{
			_customElements.RemoveAt(aIndex);
		}

		public CustomAIElement GetCustomElement(string aElementName)
		{
			for (int i = 0; i < _customElements.Count; i++)
			{
				if (_customElements[i] != null && _customElements[i].Name == aElementName)
				{
					return _customElements[i];
				}
			}
			return null;
		}

		public T GetCustomElement<T>() where T : CustomAIElement
		{
			for (int i = 0; i < _customElements.Count; i++)
			{
				if (_customElements[i] != null && _customElements[i] is T)
				{
					return _customElements[i] as T;
				}
			}
			return null;
		}

		public T GetCustomElement<T>(string aElementName) where T : CustomAIElement
		{
			for (int i = 0; i < _customElements.Count; i++)
			{
				if (_customElements[i] != null && _customElements[i].Name == aElementName && _customElements[i] is T)
				{
					return _customElements[i] as T;
				}
			}
			return null;
		}
	}
}
