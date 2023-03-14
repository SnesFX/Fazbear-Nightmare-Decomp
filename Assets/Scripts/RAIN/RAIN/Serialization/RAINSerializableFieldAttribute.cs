using System;

namespace RAIN.Serialization
{
	[AttributeUsage(AttributeTargets.Field)]
	public class RAINSerializableFieldAttribute : Attribute
	{
		private string _serializeFieldCallback;

		private string _deserializeFieldCallback;

		private FieldVisibility _visibility;

		private string _tooltip = "";

		private string[] _oldFieldNames = new string[0];

		public bool OverrideSerialization
		{
			get
			{
				if (_serializeFieldCallback != null)
				{
					return _deserializeFieldCallback != null;
				}
				return false;
			}
		}

		public string SerializeFieldCallback
		{
			get
			{
				return _serializeFieldCallback;
			}
		}

		public string DeserializeFieldCallback
		{
			get
			{
				return _deserializeFieldCallback;
			}
		}

		public FieldVisibility Visibility
		{
			get
			{
				return _visibility;
			}
			set
			{
				_visibility = value;
			}
		}

		public string ToolTip
		{
			get
			{
				return _tooltip;
			}
			set
			{
				_tooltip = value;
			}
		}

		public string[] OldFieldNames
		{
			get
			{
				return _oldFieldNames;
			}
			set
			{
				_oldFieldNames = value;
			}
		}

		public RAINSerializableFieldAttribute()
		{
			_serializeFieldCallback = null;
			_deserializeFieldCallback = null;
		}

		public RAINSerializableFieldAttribute(string aSerializeFieldCallback, string aDeserializeFieldCallback)
		{
			_serializeFieldCallback = aSerializeFieldCallback;
			_deserializeFieldCallback = aDeserializeFieldCallback;
			_visibility = FieldVisibility.Hide;
		}
	}
}
