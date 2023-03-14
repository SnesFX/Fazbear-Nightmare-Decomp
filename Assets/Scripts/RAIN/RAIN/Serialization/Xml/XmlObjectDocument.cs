namespace RAIN.Serialization.Xml
{
	public class XmlObjectDocument : ObjectDocument
	{
		private XmlDocument _document = new XmlDocument();

		private XmlObjectElement _documentElement;

		public override ObjectElement DocumentElement
		{
			get
			{
				return _documentElement;
			}
		}

		public override object WrappedDocument
		{
			get
			{
				return _document;
			}
		}

		public override string GetDocumentAsString()
		{
			return _documentElement.GetElementAsString();
		}

		public override void SetDocument(string aDocument)
		{
			_document.LoadXml(aDocument);
			_documentElement = new XmlObjectElement(_document.DocumentElement);
		}

		public override void SetDocument(ObjectElement aDocument)
		{
			_document.DocumentElement = (XmlElement)aDocument.WrappedElement;
			_documentElement = new XmlObjectElement(_document.DocumentElement);
		}

		public override void ClearDocument()
		{
			_document.DocumentElement = null;
			_documentElement = null;
		}

		public override ObjectElement CreateElement(string aName)
		{
			return new XmlObjectElement(new XmlElement(aName));
		}
	}
}
