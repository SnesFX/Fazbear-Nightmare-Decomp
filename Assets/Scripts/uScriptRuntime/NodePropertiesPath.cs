using System;

[AttributeUsage(AttributeTargets.Class)]
public class NodePropertiesPath : Attribute
{
	public string Value;

	public NodePropertiesPath(string value)
	{
		Value = value;
	}
}
