using UnityEngine;

[NodeToolTip("Sets the value of a Vector4 variable using the value of another Vector4 variable.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Set Vector4", "Sets the value of a Vector4 variable using the value of another Vector4 variable.")]
[NodePath("Actions/Variables/Vector4")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Vector4")]
public class uScriptAct_SetVector4 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The variable you wish to use to set the target's value.")] Vector4 Value, [FriendlyName("Target", "The Target variable you wish to set.")] out Vector4 TargetVector4)
	{
		TargetVector4 = Value;
	}
}
