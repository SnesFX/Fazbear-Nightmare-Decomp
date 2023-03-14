using UnityEngine;

[FriendlyName("Set Color", "Sets the value of a Color variable using the value of another Color variable.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Color")]
[NodePath("Actions/Variables/Color")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Sets the value of a Color variable using the value of another Color variable.")]
public class uScriptAct_SetColor : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The variable you wish to use to set the target's value.")] Color Value, [FriendlyName("Target", "The Target variable you wish to set.")] out Color TargetColor)
	{
		TargetColor = Value;
	}
}
