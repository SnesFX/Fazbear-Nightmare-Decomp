using System;

[AttributeUsage(AttributeTargets.Class)]
public class NodePath : Attribute
{
	public string Value;

	public NodePath(string value)
	{
		Value = value;
	}
}
