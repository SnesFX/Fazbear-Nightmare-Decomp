using UnityEngine;

[FriendlyName("Set Vector2", "Sets the value of a Vector2 variable using the value of another Vector2 variable.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Vector2")]
[NodeToolTip("Sets the value of a Vector2 variable using the value of another Vector2 variable.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Vector2")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_SetVector2 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The variable you wish to use to set the target's value.")] Vector2 Value, [FriendlyName("Target", "The Target variable you wish to set.")] out Vector2 TargetVector2)
	{
		TargetVector2 = Value;
	}
}
