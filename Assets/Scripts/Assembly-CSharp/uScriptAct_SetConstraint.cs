using UnityEngine;

[NodeToolTip("Sets the value(s) of a Constraint variable using the value of another Constraint variable.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Constraint")]
[FriendlyName("Set Constraint", "Sets the value(s) of a Constraint variable using the value of another Constraint variable.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_SetConstraint : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The variable(s) you wish to use to set the target's value.")] RigidbodyConstraints[] Value, [FriendlyName("Target", "The Target variable you wish to set.")] out RigidbodyConstraints Target)
	{
		Target = RigidbodyConstraints.None;
		foreach (RigidbodyConstraints rigidbodyConstraints in Value)
		{
			Target |= rigidbodyConstraints;
		}
	}
}
