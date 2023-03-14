using System;

[AttributeUsage(AttributeTargets.Class)]
public class NodeNeedsGuiLayout : Attribute
{
	public bool Value;

	public NodeNeedsGuiLayout(bool value)
	{
		Value = value;
	}

	public NodeNeedsGuiLayout()
	{
	}
}
