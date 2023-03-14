using System;

[AttributeUsage(AttributeTargets.Class)]
public class NodeCopyright : Attribute
{
	public string Value;

	public NodeCopyright(string value)
	{
		Value = value;
	}
}
