using System;

[AttributeUsage(AttributeTargets.Class)]
public class NodeComponentType : Attribute
{
	public NodeComponentType(Type type)
	{
	}

	public NodeComponentType(Type type1, Type type2)
	{
	}
}
