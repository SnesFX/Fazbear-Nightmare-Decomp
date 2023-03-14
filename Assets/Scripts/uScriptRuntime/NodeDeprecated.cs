using System;

[AttributeUsage(AttributeTargets.Class)]
public class NodeDeprecated : Attribute
{
	public Type UpgradeType;

	public NodeDeprecated(Type upgradeToType)
	{
		UpgradeType = upgradeToType;
	}

	public NodeDeprecated()
	{
	}
}
