using System;

[AttributeUsage(AttributeTargets.Class)]
public class NodeToolTip : Attribute
{
	public string Value;

	public NodeToolTip(string value)
	{
		Value = value;
	}
}
