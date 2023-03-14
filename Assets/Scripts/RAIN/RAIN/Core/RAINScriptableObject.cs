using System.Collections.Generic;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Core
{
	public abstract class RAINScriptableObject : ScriptableObject
	{
		[SerializeField]
		private FieldSerializer _dataSerializer = new FieldSerializer();

		private static List<RAINScriptableObject> _dirtyAssets = new List<RAINScriptableObject>();

		public FieldSerializer DataSerializer
		{
			get
			{
				return _dataSerializer;
			}
		}

		public static List<RAINScriptableObject> DirtyAssets
		{
			get
			{
				return _dirtyAssets;
			}
		}

		public virtual void OnEnable()
		{
			UpdateSerialization();
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
					Debug.LogWarning("RAINScriptableObject: Reserializing due to a change in version");
					_dataSerializer.DeserializeRAINObject(this);
					_dataSerializer.SerializeRAINObject(this);
					if (Application.isEditor)
					{
						_dirtyAssets.Add(this);
					}
				}
				else if (!_dataSerializer.DeserializeRAINObject(this))
				{
					Debug.LogWarning("RAINScriptableObject: Reserializing due to a change in serialization");
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
	}
}
