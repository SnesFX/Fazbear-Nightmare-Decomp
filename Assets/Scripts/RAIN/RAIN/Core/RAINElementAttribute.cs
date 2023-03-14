using System;

namespace RAIN.Core
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class RAINElementAttribute : Attribute
	{
		private string _elementName;

		public string ElementName
		{
			get
			{
				return _elementName;
			}
		}

		public RAINElementAttribute(string aElementName)
		{
			_elementName = aElementName;
		}
	}
}
