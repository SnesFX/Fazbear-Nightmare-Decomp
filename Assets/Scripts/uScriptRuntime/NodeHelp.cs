using System;

[AttributeUsage(AttributeTargets.Class)]
public class NodeHelp : Attribute
{
	public string Value;

	public NodeHelp(string value)
	{
		Value = value;
	}
}
