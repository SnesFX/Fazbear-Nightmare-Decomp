using System;

[AttributeUsage(AttributeTargets.Class)]
public class NodeDescription : Attribute
{
	public string Value;

	public NodeDescription(string value)
	{
		Value = value;
	}
}
