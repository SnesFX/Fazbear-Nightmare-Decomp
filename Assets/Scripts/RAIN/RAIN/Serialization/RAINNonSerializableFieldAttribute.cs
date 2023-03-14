using System;

namespace RAIN.Serialization
{
	[AttributeUsage(AttributeTargets.Field)]
	public class RAINNonSerializableFieldAttribute : Attribute
	{
	}
}
