namespace RAIN.Serialization
{
	public abstract class ObjectElement
	{
		public abstract string Name { get; }

		public abstract ObjectElement NextSibling { get; }

		public abstract ObjectElement Parent { get; }

		public abstract int ChildCount { get; }

		public abstract object WrappedElement { get; }

		public abstract string GetElementAsString();

		public abstract bool HasAttribute(string aAttribute);

		public abstract void SetAttribute(string aAttribute, string aValue);

		public abstract string GetAttribute(string aAttribute);

		public abstract void InsertChild(int aIndex, ObjectElement aElement);

		public abstract void ReplaceChild(int aIndex, ObjectElement aElement);

		public abstract void AddChild(ObjectElement aElement);

		public abstract void RemoveChild(ObjectElement aElement);

		public abstract ObjectElement GetChild(int aIndex);

		public abstract ObjectElement GetChildByName(string aName);

		public abstract ObjectElement GetChildByID(string aID);
	}
}
