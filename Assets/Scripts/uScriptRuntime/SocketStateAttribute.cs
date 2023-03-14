using System;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
public class SocketStateAttribute : Attribute
{
	public bool Visible;

	public bool Locked;

	public SocketStateAttribute(bool visible, bool locked)
	{
		Visible = visible;
		Locked = locked;
	}
}
