using System;

namespace RAIN.Serialization
{
	[AttributeUsage(AttributeTargets.Class)]
	public class RAINSerializableClassAttribute : Attribute
	{
		private string _serializeClassCallback;

		private string _deserializeClassCallback;

		public bool OverrideSerialization
		{
			get
			{
				if (_serializeClassCallback != null)
				{
					return _deserializeClassCallback != null;
				}
				return false;
			}
		}

		public string SerializeClassCallback
		{
			get
			{
				return _serializeClassCallback;
			}
		}

		public string DeserializeClassCallback
		{
			get
			{
				return _deserializeClassCallback;
			}
		}

		public RAINSerializableClassAttribute()
		{
			_serializeClassCallback = null;
			_deserializeClassCallback = null;
		}

		public RAINSerializableClassAttribute(string aSerializeClassCallback, string aDeserializeClassCallback)
		{
			_serializeClassCallback = aSerializeClassCallback;
			_deserializeClassCallback = aDeserializeClassCallback;
		}
	}
}
