using System;

[AttributeUsage(AttributeTargets.All)]
public class FriendlyNameAttribute : Attribute
{
	public string Name;

	public string Desc;

	public FriendlyNameAttribute(string name)
	{
		Name = name;
		Desc = string.Empty;
	}

	public FriendlyNameAttribute(string name, string desc)
	{
		Name = name;
		Desc = desc;
	}
}
