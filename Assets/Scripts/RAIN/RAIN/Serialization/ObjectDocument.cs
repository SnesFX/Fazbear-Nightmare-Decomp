using RAIN.Serialization.Xml;

namespace RAIN.Serialization
{
	public abstract class ObjectDocument
	{
		public abstract ObjectElement DocumentElement { get; }

		public abstract object WrappedDocument { get; }

		public static ObjectDocument CreateFieldDocument(FieldDocumentType aType)
		{
			if (aType == FieldDocumentType.Xml)
			{
				return new XmlObjectDocument();
			}
			return null;
		}

		public abstract string GetDocumentAsString();

		public abstract void SetDocument(string aDocument);

		public abstract void SetDocument(ObjectElement aDocument);

		public abstract void ClearDocument();

		public abstract ObjectElement CreateElement(string aName);
	}
}
