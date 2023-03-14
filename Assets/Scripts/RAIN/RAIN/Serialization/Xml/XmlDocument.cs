using System.IO;
using System.Xml;

namespace RAIN.Serialization.Xml
{
	public class XmlDocument
	{
		private XmlElement _documentElement;

		public XmlElement DocumentElement
		{
			get
			{
				return _documentElement;
			}
			set
			{
				_documentElement = value;
			}
		}

		public void LoadXml(string aDocument)
		{
			using (XmlReader xmlReader = XmlReader.Create(new StringReader(aDocument)))
			{
				while (xmlReader.Read() && xmlReader.NodeType != XmlNodeType.Element)
				{
				}
				_documentElement = new XmlElement(xmlReader.Name);
				while (xmlReader.MoveToNextAttribute())
				{
					_documentElement.SetAttribute(xmlReader.Name, xmlReader.Value);
				}
				XmlElement xmlElement = _documentElement;
				while (xmlReader.Read())
				{
					switch (xmlReader.NodeType)
					{
					case XmlNodeType.Element:
					{
						XmlElement xmlElement2 = new XmlElement(xmlReader.Name);
						xmlElement.AppendChild(xmlElement2);
						while (xmlReader.MoveToNextAttribute())
						{
							xmlElement2.SetAttribute(xmlReader.Name, xmlReader.Value);
						}
						xmlReader.MoveToElement();
						if (!xmlReader.IsEmptyElement)
						{
							xmlElement = xmlElement2;
						}
						break;
					}
					case XmlNodeType.EndElement:
						xmlElement = xmlElement.Parent;
						break;
					}
				}
			}
		}
	}
}
