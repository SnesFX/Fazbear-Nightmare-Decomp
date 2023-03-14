using System;
using RAIN.Core;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Entities
{
	[AddComponentMenu("Rival Theory/RAIN/Entity Rig")]
	public class EntityRig : RAINComponent
	{
		[RAINSerializableField(Visibility = FieldVisibility.Show)]
		private VisualModeEnum _visualMode = VisualModeEnum.OnWhenSelected;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Determines whether Entity methods are called manually or by Unity")]
		private bool _useUnityMessages = true;

		[RAINSerializableField(Visibility = FieldVisibility.Show)]
		private Entity _entity = new Entity();

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

		public Entity Entity
		{
			get
			{
				return _entity;
			}
		}

		public static EntityRig AddRig(GameObject aEntityForm)
		{
			if (aEntityForm == null)
			{
				return null;
			}
			GameObject gameObject = new GameObject("Entity");
			gameObject.transform.position = aEntityForm.transform.position;
			gameObject.transform.rotation = aEntityForm.transform.rotation;
			gameObject.transform.parent = aEntityForm.transform;
			EntityRig entityRig = gameObject.AddComponent<EntityRig>();
			entityRig.Entity.EntityName = aEntityForm.name;
			entityRig.Entity.Form = aEntityForm;
			entityRig.Serialize();
			return entityRig;
		}

		public override void Awake()
		{
			base.Awake();
			if (_useUnityMessages)
			{
				EntityAwake();
			}
		}

		public void EntityAwake()
		{
			_entity.EntityName = base.name;
			_entity.EntityInit();
			_entity.FormInit();
		}

		public override void OnDestroy()
		{
			_entity.DeactivateEntity();
			base.OnDestroy();
		}

		protected override void Reset()
		{
			_entity.DeactivateEntity();
			_entity = new Entity();
			base.Reset();
		}

		protected override void OnValidate()
		{
			base.OnValidate();
			bool flag = false;
			for (int i = 0; i < Entity.Aspects.Count; i++)
			{
				if (Entity.Aspects[i] == null)
				{
					flag = true;
					Entity.RemoveAspect(i--);
				}
			}
			if (flag)
			{
				Debug.LogWarning("EntityRig: Reserializing due to a missing aspect type on the Entity", base.gameObject);
				Serialize();
			}
		}
	}
}
