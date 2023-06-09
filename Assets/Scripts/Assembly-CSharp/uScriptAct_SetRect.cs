using UnityEngine;

[FriendlyName("Set Rect", "Sets the value of a Rect variable using the value of another Rect variable.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Rect")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Sets the value of a Rect variable using the value of another Rect variable.")]
[NodePath("Actions/Variables/Rect")]
public class uScriptAct_SetRect : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The variable you wish to use to set the target's value.")] Rect Value, [FriendlyName("Target", "The Target variable you wish to set.")] out Rect TargetRect)
	{
		TargetRect = Value;
	}
}
