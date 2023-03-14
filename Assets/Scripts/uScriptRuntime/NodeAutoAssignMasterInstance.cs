using System;

[AttributeUsage(AttributeTargets.Class)]
public class NodeAutoAssignMasterInstance : Attribute
{
	public bool Value;

	public NodeAutoAssignMasterInstance(bool assign)
	{
		Value = assign;
	}
}
