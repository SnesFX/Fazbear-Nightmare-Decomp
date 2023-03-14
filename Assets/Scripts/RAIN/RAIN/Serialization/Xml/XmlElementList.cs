using System.Collections;
using System.Collections.Generic;

namespace RAIN.Serialization.Xml
{
	public class XmlElementList : IList<XmlElement>, ICollection<XmlElement>, IEnumerable<XmlElement>, IEnumerable
	{
		private XmlElement _parent;

		private List<XmlElement> _elements = new List<XmlElement>();

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public int Count
		{
			get
			{
				return _elements.Count;
			}
		}

		public XmlElement this[int aIndex]
		{
			get
			{
				return _elements[aIndex];
			}
			set
			{
				value.Parent = _elements[aIndex].Parent;
				value.PreviousSibling = _elements[aIndex].PreviousSibling;
				value.NextSibling = _elements[aIndex].NextSibling;
				_elements[aIndex].Parent = null;
				_elements[aIndex].PreviousSibling = null;
				_elements[aIndex].NextSibling = null;
				_elements[aIndex] = value;
			}
		}

		public XmlElementList(XmlElement aParent)
		{
			_parent = aParent;
		}

		public int IndexOf(XmlElement aItem)
		{
			return _elements.IndexOf(aItem);
		}

		public void Add(XmlElement aItem)
		{
			aItem.Parent = _parent;
			if (_elements.Count > 0)
			{
				aItem.PreviousSibling = _elements[_elements.Count - 1];
				_elements[_elements.Count - 1].NextSibling = aItem;
			}
			_elements.Add(aItem);
		}

		public void Insert(int aIndex, XmlElement aItem)
		{
			aItem.Parent = _elements[aIndex].Parent;
			aItem.PreviousSibling = _elements[aIndex].PreviousSibling;
			aItem.NextSibling = _elements[aIndex];
			if (_elements[aIndex].PreviousSibling != null)
			{
				_elements[aIndex].PreviousSibling.NextSibling = aItem;
			}
			_elements[aIndex].PreviousSibling = aItem;
			_elements.Insert(aIndex, aItem);
		}

		public bool Remove(XmlElement aItem)
		{
			if (!_elements.Remove(aItem))
			{
				return false;
			}
			if (aItem.PreviousSibling != null)
			{
				aItem.PreviousSibling.NextSibling = aItem.NextSibling;
			}
			if (aItem.NextSibling != null)
			{
				aItem.NextSibling.PreviousSibling = aItem.PreviousSibling;
			}
			aItem.Parent = null;
			aItem.PreviousSibling = null;
			aItem.NextSibling = null;
			return true;
		}

		public void RemoveAt(int aIndex)
		{
			_parent.RemoveChild(_elements[aIndex]);
		}

		public void Clear()
		{
			for (int i = 0; i < _elements.Count; i++)
			{
				_elements[i].Parent = null;
				_elements[i].PreviousSibling = null;
				_elements[i].NextSibling = null;
			}
			_elements.Clear();
		}

		public bool Contains(XmlElement aItem)
		{
			return _elements.Contains(aItem);
		}

		public void CopyTo(XmlElement[] aArray, int aArrayIndex)
		{
			_elements.CopyTo(aArray, aArrayIndex);
		}

		public IEnumerator<XmlElement> GetEnumerator()
		{
			return _elements.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _elements.GetEnumerator();
		}
	}
}
