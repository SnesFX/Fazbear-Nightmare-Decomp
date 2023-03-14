namespace RAIN.Serialization.Xml
{
	public class XmlObjectElement : ObjectElement
	{
		private XmlElement _element;

		public override string Name
		{
			get
			{
				return _element.Name;
			}
		}

		public override ObjectElement NextSibling
		{
			get
			{
				if (_element.NextSibling == null)
				{
					return null;
				}
				return new XmlObjectElement(_element.NextSibling);
			}
		}

		public override ObjectElement Parent
		{
			get
			{
				if (_element.Parent == null)
				{
					return null;
				}
				return new XmlObjectElement(_element.Parent);
			}
		}

		public override int ChildCount
		{
			get
			{
				return _element.ChildNodes.Count;
			}
		}

		public override object WrappedElement
		{
			get
			{
				return _element;
			}
		}

		public XmlObjectElement(XmlElement aElement)
		{
			_element = aElement;
		}

		public override string GetElementAsString()
		{
			return _element.GetXml();
		}

		public override bool HasAttribute(string aAttribute)
		{
			return _element.HasAttribute(aAttribute);
		}

		public override void SetAttribute(string aAttribute, string aValue)
		{
			_element.SetAttribute(aAttribute, aValue);
		}

		public override string GetAttribute(string aAttribute)
		{
			return _element.GetAttribute(aAttribute);
		}

		public override void InsertChild(int aIndex, ObjectElement aElement)
		{
			_element.InsertBefore((XmlElement)aElement.WrappedElement, _element.ChildNodes[aIndex]);
		}

		public override void ReplaceChild(int aIndex, ObjectElement aElement)
		{
			_element.ReplaceChild((XmlElement)aElement.WrappedElement, _element.ChildNodes[aIndex]);
		}

		public override void AddChild(ObjectElement aElement)
		{
			_element.AppendChild((XmlElement)aElement.WrappedElement);
		}

		public override void RemoveChild(ObjectElement aElement)
		{
			_element.RemoveChild((XmlElement)aElement.WrappedElement);
		}

		public override ObjectElement GetChild(int aIndex)
		{
			return new XmlObjectElement(_element.ChildNodes[aIndex]);
		}

		public override ObjectElement GetChildByName(string aName)
		{
			XmlElement childByName = _element.GetChildByName(aName);
			if (childByName == null)
			{
				return null;
			}
			return new XmlObjectElement(childByName);
		}

		public override ObjectElement GetChildByID(string aID)
		{
			XmlElement childByID = _element.GetChildByID(aID);
			if (childByID == null)
			{
				return null;
			}
			return new XmlObjectElement(childByID);
		}
	}
}
