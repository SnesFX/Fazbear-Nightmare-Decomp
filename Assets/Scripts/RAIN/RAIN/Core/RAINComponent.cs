using System;
using System.Collections.Generic;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Core
{
	public abstract class RAINComponent : MonoBehaviour
	{
		[SerializeField]
		[HideInInspector]
		private FieldSerializer _dataSerializer = new FieldSerializer();

		private static bool _useValidation = true;

		private static List<RAINComponent> _dirtyAssets = new List<RAINComponent>();

		public FieldSerializer DataSerializer
		{
			get
			{
				return _dataSerializer;
			}
		}

		public static bool UseValidation
		{
			get
			{
				return _useValidation;
			}
			set
			{
				_useValidation = value;
			}
		}

		public static List<RAINComponent> DirtyAssets
		{
			get
			{
				return _dirtyAssets;
			}
		}

		public static RAINComponent CreateRAINComponent(GameObject aObject, string aXML, List<UnityEngine.Object> aSerializedObjects, List<FieldSerializer.CustomSerializedData> aSerializedCustomData)
		{
			FieldSerializer fieldSerializer = new FieldSerializer(aXML, aSerializedObjects, aSerializedCustomData);
			Type componentType = fieldSerializer.GetComponentType();
			if (componentType == null)
			{
				return null;
			}
			RAINComponent rAINComponent = aObject.AddComponent(componentType) as RAINComponent;
			if (rAINComponent == null)
			{
				return null;
			}
			rAINComponent._dataSerializer = fieldSerializer;
			rAINComponent.UpdateSerialization();
			rAINComponent.Serialize();
			return rAINComponent;
		}

		public bool DeserializeInPlace(string aXML, List<UnityEngine.Object> aLinkedObjects = null, List<FieldSerializer.CustomSerializedData> aCustomData = null)
		{
			if (aLinkedObjects == null)
			{
				aLinkedObjects = new List<UnityEngine.Object>(_dataSerializer.SerializedGameObjects);
			}
			if (aCustomData == null)
			{
				aCustomData = new List<FieldSerializer.CustomSerializedData>(_dataSerializer.SerializedCustomData);
			}
			FieldSerializer fieldSerializer = new FieldSerializer(aXML, aLinkedObjects, aCustomData);
			Type componentType = fieldSerializer.GetComponentType();
			if (componentType != _dataSerializer.GetComponentType())
			{
				return false;
			}
			_dataSerializer = fieldSerializer;
			UpdateSerialization();
			Serialize();
			return true;
		}

		public virtual void OnEnable()
		{
		}

		public virtual void Awake()
		{
			UpdateSerialization();
		}

		public virtual void OnDestroy()
		{
		}

		public virtual void Start()
		{
		}

		public virtual void Update()
		{
		}

		public virtual void FixedUpdate()
		{
		}

		public virtual void LateUpdate()
		{
		}

		public virtual void OnAnimatorMove()
		{
		}

		public virtual void OnAnimatorIK(int aLayerIndex)
		{
		}

		public void Serialize()
		{
			_dataSerializer.SerializeRAINObject(this);
			if (Application.isEditor)
			{
				_dirtyAssets.Add(this);
			}
		}

		public void UpdateSerialization()
		{
			if (_dataSerializer.NeedsSerialization)
			{
				_dataSerializer.SerializeRAINObject(this);
				if (Application.isEditor)
				{
					_dirtyAssets.Add(this);
				}
			}
			else
			{
				if (!_dataSerializer.NeedsDeserialization)
				{
					return;
				}
				_dataSerializer.DeserializeRAINObjectHeader(true);
				if (OnVersion(_dataSerializer.GetComponentVersion()))
				{
					Debug.LogWarning("RAINComponent: Reserializing due to a change in version", base.gameObject);
					_dataSerializer.DeserializeRAINObject(this);
					_dataSerializer.SerializeRAINObject(this);
					if (Application.isEditor)
					{
						_dirtyAssets.Add(this);
					}
				}
				else if (!_dataSerializer.DeserializeRAINObject(this))
				{
					Debug.LogWarning("RAINComponent: Reserializing due to a change in serialization", base.gameObject);
					_dataSerializer.SerializeRAINObject(this);
					if (Application.isEditor)
					{
						_dirtyAssets.Add(this);
					}
				}
			}
		}

		public void ApplySerializationChanges()
		{
			_dataSerializer.ApplySerializationChanges();
			if (Application.isEditor)
			{
				_dirtyAssets.Add(this);
			}
		}

		protected virtual bool OnVersion(string aVersion)
		{
			return false;
		}

		protected virtual void Reset()
		{
			_dataSerializer = new FieldSerializer();
			Serialize();
		}

		protected virtual void OnValidate()
		{
			if (!UseValidation || Application.isPlaying)
			{
				return;
			}
			if (_dataSerializer.NeedsSerialization)
			{
				_dataSerializer.SerializeRAINObject(this);
				if (Application.isEditor)
				{
					_dirtyAssets.Add(this);
				}
				return;
			}
			_dataSerializer.DeserializeRAINObjectHeader(true);
			if (OnVersion(_dataSerializer.GetComponentVersion()))
			{
				Debug.LogWarning("RAINComponent: Reserializing due to a change in version", base.gameObject);
				_dataSerializer.DeserializeRAINObject(this);
				_dataSerializer.SerializeRAINObject(this);
				if (Application.isEditor)
				{
					_dirtyAssets.Add(this);
				}
			}
			else if (!_dataSerializer.DeserializeRAINObject(this))
			{
				Debug.LogWarning("RAINComponent: Reserializing due to a change in serialization", base.gameObject);
				_dataSerializer.SerializeRAINObject(this);
				if (Application.isEditor)
				{
					_dirtyAssets.Add(this);
				}
			}
		}
	}
}
