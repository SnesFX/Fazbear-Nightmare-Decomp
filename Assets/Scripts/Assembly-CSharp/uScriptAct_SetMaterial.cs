using UnityEngine;

[FriendlyName("Set Material", "Sets the value of a Material variable using the value of another Material variable.")]
[NodePath("Actions/Variables/Material")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Sets the value of a Color variable using the value of another Color variable.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_SetMaterial : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The variable you wish to use to set the target's value.")] Material Value, [FriendlyName("Target", "The Target variable you wish to set.")] out Material TargetMat)
	{
		TargetMat = Value;
	}
}
