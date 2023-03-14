using System;

namespace RAIN.Action
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class RAINDecisionAttribute : Attribute
	{
		public readonly string decisionName;

		public RAINDecisionAttribute()
		{
			decisionName = "";
		}

		public RAINDecisionAttribute(string aName)
		{
			decisionName = aName;
		}
	}
}
