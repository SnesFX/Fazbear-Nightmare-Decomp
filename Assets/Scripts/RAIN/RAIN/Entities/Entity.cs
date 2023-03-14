using System.Collections.Generic;
using RAIN.Core;
using RAIN.Entities.Aspects;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Entities
{
	[RAINSerializableClass]
	public class Entity
	{
		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Name of the entity")]
		private string _entityName = "";

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Toggle Entity activity on/off")]
		private bool _isActive = true;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Object described by the Entity")]
		private GameObject _form;

		[RAINSerializableField(Visibility = FieldVisibility.Hide)]
		private List<RAINAspect> _aspects = new List<RAINAspect>();

		private bool _initialized;

		public Entity ParentEntity
		{
			get
			{
				if (_form != null)
				{
					Transform parent = _form.transform.parent;
					while (parent != null)
					{
						EntityRig component = parent.gameObject.GetComponent<EntityRig>();
						if (component != null)
						{
							return component.Entity;
						}
						parent = parent.transform.parent;
					}
				}
				return this;
			}
		}

		public Entity RootEntity
		{
			get
			{
				Entity result = this;
				if (_form != null)
				{
					Transform parent = _form.transform.parent;
					while (parent != null)
					{
						EntityRig component = parent.gameObject.GetComponent<EntityRig>();
						if (component != null)
						{
							result = component.Entity;
						}
						parent = parent.transform.parent;
					}
				}
				return result;
			}
		}

		public string EntityName
		{
			get
			{
				return _entityName;
			}
			set
			{
				_entityName = value;
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
				if (value)
				{
					ActivateEntity();
				}
				else
				{
					DeactivateEntity();
				}
			}
		}

		public GameObject Form
		{
			get
			{
				return _form;
			}
			set
			{
				if (_form != value)
				{
					_form = value;
					FormInit();
				}
			}
		}

		public IList<RAINAspect> Aspects
		{
			get
			{
				return _aspects.AsReadOnly();
			}
		}

		public bool Initialized
		{
			get
			{
				return _initialized;
			}
		}

		public virtual void EntityInit()
		{
			for (int i = 0; i < _aspects.Count; i++)
			{
				_aspects[i].EntityInit(this);
			}
			if (_isActive)
			{
				foreach (RAINAspect aspect in Aspects)
				{
					aspect.RegisterWithSensorManager();
				}
			}
			_initialized = true;
		}

		public virtual void FormInit()
		{
			for (int i = 0; i < _aspects.Count; i++)
			{
				_aspects[i].FormInit();
			}
		}

		public static Entity FindNearestEntityAtOrAbove(GameObject aGameObject)
		{
			Transform parent = aGameObject.transform.parent;
			while (parent != null)
			{
				EntityRig component = parent.gameObject.GetComponent<EntityRig>();
				if (component != null)
				{
					return component.Entity;
				}
				parent = parent.transform.parent;
			}
			return null;
		}

		public RAINAspect GetAspect(string aAspectName)
		{
			for (int i = 0; i < _aspects.Count; i++)
			{
				if (_aspects[i].AspectName == aAspectName)
				{
					return _aspects[i];
				}
			}
			return null;
		}

		public void AddAspect(RAINAspect aAspect)
		{
			if (aAspect == null || _aspects.Contains(aAspect))
			{
				return;
			}
			_aspects.Add(aAspect);
			if (Initialized)
			{
				aAspect.EntityInit(this);
				aAspect.FormInit();
				if (_isActive)
				{
					aAspect.RegisterWithSensorManager();
				}
			}
		}

		public void RemoveAspect(RAINAspect aAspect)
		{
			if (aAspect != null)
			{
				_aspects.Remove(aAspect);
				if (aAspect.Entity == this)
				{
					aAspect.UnregisterWithSensorManager();
				}
			}
		}

		public void RemoveAspect(int aIndex)
		{
			if (_aspects[aIndex] != null && _aspects[aIndex].Entity == this)
			{
				_aspects[aIndex].UnregisterWithSensorManager();
			}
			_aspects.RemoveAt(aIndex);
		}

		public void ActivateEntity()
		{
			if (!Initialized)
			{
				return;
			}
			if (!_isActive)
			{
				foreach (RAINAspect aspect in Aspects)
				{
					aspect.RegisterWithSensorManager();
				}
			}
			_isActive = true;
		}

		public void DeactivateEntity()
		{
			if (!Initialized)
			{
				return;
			}
			if (_isActive)
			{
				foreach (RAINAspect aspect in Aspects)
				{
					aspect.UnregisterWithSensorManager();
				}
			}
			_isActive = false;
		}

		public virtual void Reset(RAINComponent aComponent)
		{
			foreach (RAINAspect aspect in _aspects)
			{
				if (aspect != null)
				{
					aspect.Reset(aComponent);
				}
			}
		}
	}
}
