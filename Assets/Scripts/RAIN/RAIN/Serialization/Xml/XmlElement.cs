using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace RAIN.Serialization.Xml
{
	public class XmlElement
	{
		private string _name = "";

		private string _id = "";

		private List<XmlAttribute> _attributes = new List<XmlAttribute>();

		private XmlElement _parent;

		private XmlElement _previousSibling;

		private XmlElement _nextSibling;

		private XmlElementList _children;

		public string Name
		{
			get
			{
				return _name;
			}
		}

		public string ID
		{
			get
			{
				return _id;
			}
		}

		public XmlElement Parent
		{
			get
			{
				return _parent;
			}
			set
			{
				_parent = value;
			}
		}

		public XmlElement PreviousSibling
		{
			get
			{
				return _previousSibling;
			}
			set
			{
				_previousSibling = value;
			}
		}

		public XmlElement NextSibling
		{
			get
			{
				return _nextSibling;
			}
			set
			{
				_nextSibling = value;
			}
		}

		public XmlElementList ChildNodes
		{
			get
			{
				return _children;
			}
		}

		public XmlElement(string aName)
		{
			_name = aName;
			_children = new XmlElementList(this);
		}

		public void WriteXml(XmlWriter aWriter)
		{
			aWriter.WriteStartElement(_name);
			for (int i = 0; i < _attributes.Count; i++)
			{
				_attributes[i].WriteXml(aWriter);
			}
			for (int j = 0; j < _children.Count; j++)
			{
				_children[j].WriteXml(aWriter);
			}
			aWriter.WriteEndElement();
		}

		public bool HasAttribute(string aAttribute)
		{
			for (int i = 0; i < _attributes.Count; i++)
			{
				int num = string.CompareOrdinal(aAttribute, _attributes[i].Name);
				if (num > 0)
				{
					return false;
				}
				if (num == 0)
				{
					return true;
				}
			}
			return false;
		}

		public void SetAttribute(string aAttribute, string aValue)
		{
			if (aAttribute == "id")
			{
				_id = aValue;
			}
			for (int i = 0; i < _attributes.Count; i++)
			{
				int num = string.CompareOrdinal(aAttribute, _attributes[i].Name);
				if (num > 0)
				{
					_attributes.Insert(i, new XmlAttribute(aAttribute, aValue));
					return;
				}
				if (num == 0)
				{
					_attributes[i].Value = aValue;
					return;
				}
			}
			_attributes.Add(new XmlAttribute(aAttribute, aValue));
		}

		public string GetAttribute(string aAttribute)
		{
			for (int i = 0; i < _attributes.Count; i++)
			{
				int num = string.CompareOrdinal(aAttribute, _attributes[i].Name);
				if (num > 0)
				{
					return "";
				}
				if (num == 0)
				{
					return _attributes[i].Value;
				}
			}
			return "";
		}

		public void InsertBefore(XmlElement aNewElement, XmlElement aReference)
		{
			if (aNewElement.Parent != null)
			{
				throw new Exception("New element already has a parent");
			}
			if (aReference.Parent != this)
			{
				throw new Exception("Reference doesn't belong to this element");
			}
			for (int i = 0; i < _children.Count; i++)
			{
				if (_children[i] == aReference)
				{
					_children.Insert(i, aNewElement);
					break;
				}
			}
		}

		public void ReplaceChild(XmlElement aNewElement, XmlElement aReference)
		{
			if (aNewElement.Parent != null)
			{
				throw new Exception("New element already has a parent");
			}
			if (aReference.Parent != this)
			{
				throw new Exception("Reference doesn't belong to this element");
			}
			for (int i = 0; i < _children.Count; i++)
			{
				if (_children[i] == aReference)
				{
					_children[i] = aNewElement;
					break;
				}
			}
		}

		public void RemoveAllChildren()
		{
			_children.Clear();
		}

		public void AppendChild(XmlElement aNewElement)
		{
			if (aNewElement.Parent != null)
			{
				throw new Exception("New element already has a parent");
			}
			_children.Add(aNewElement);
		}

		public void RemoveChild(XmlElement aElement)
		{
			if (aElement.Parent != this)
			{
				throw new Exception("Child doesn't belong to this element");
			}
			_children.Remove(aElement);
		}

		public XmlElement GetChildByName(string aName)
		{
			for (int i = 0; i < _children.Count; i++)
			{
				if (_children[i].Name == aName)
				{
					return _children[i];
				}
			}
			return null;
		}

		public XmlElement GetChildByID(string aID)
		{
			for (int i = 0; i < _children.Count; i++)
			{
				if (_children[i].ID == aID)
				{
					return _children[i];
				}
			}
			return null;
		}

		public string GetXml()
		{
			using (StringWriter stringWriter = new StringWriter())
			{
				XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
				xmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;
				xmlWriterSettings.OmitXmlDeclaration = true;
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
				{
					WriteXml(xmlWriter);
					xmlWriter.Close();
					return stringWriter.ToString();
				}
			}
		}
	}
}
