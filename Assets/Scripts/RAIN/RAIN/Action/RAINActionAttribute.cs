using System;

namespace RAIN.Action
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class RAINActionAttribute : Attribute
	{
		public readonly string actionName;

		public RAINActionAttribute()
		{
			actionName = "";
		}

		public RAINActionAttribute(string aName)
		{
			actionName = aName;
		}
	}
}
