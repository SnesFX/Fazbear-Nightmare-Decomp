using System;

[AttributeUsage(AttributeTargets.Class)]
public class NodeAuthor : Attribute
{
	public string Value;

	public string URL;

	public NodeAuthor(string value, string url)
	{
		Value = value;
		URL = url;
	}
}
