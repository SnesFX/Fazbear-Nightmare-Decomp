using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Set Quaternion", "Sets the value of a Quaternion variable using the value of another Quaternion variable.")]
[NodeToolTip("Sets the value of a Quaternion variable using the value of another Vector4 variable.")]
[NodePath("Actions/Variables/Quaternion")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_SetQuaternion : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The variable you wish to use to set the target's value.")] Quaternion Value, [FriendlyName("Target", "The Target variable you wish to set.")] out Quaternion TargetQuaternion)
	{
		TargetQuaternion = Value;
	}
}
