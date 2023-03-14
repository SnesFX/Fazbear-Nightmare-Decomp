using System.Xml;

namespace RAIN.Serialization.Xml
{
	public class XmlAttribute
	{
		private string _name = "";

		private string _value = "";

		public string Name
		{
			get
			{
				return _name;
			}
		}

		public string Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
			}
		}

		public XmlAttribute(string aName, string aValue)
		{
			_name = aName;
			_value = aValue;
		}

		public void WriteXml(XmlWriter aWriter)
		{
			aWriter.WriteAttributeString(_name, _value);
		}
	}
}
