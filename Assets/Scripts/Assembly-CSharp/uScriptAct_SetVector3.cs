using UnityEngine;

[NodePath("Actions/Variables/Vector3")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Sets the value of a Vector3 variable using the value of another Vector3 variable.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Vector3")]
[FriendlyName("Set Vector3", "Sets the value of a Vector3 variable using the value of another Vector3 variable.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_SetVector3 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The variable you wish to use to set the target's value.")] Vector3 Value, [FriendlyName("Target", "The Target variable you wish to set.")] out Vector3 TargetVector3)
	{
		TargetVector3 = Value;
	}
}
