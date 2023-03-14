using UnityEngine;

[FriendlyName("Set GameObject", "Sets the value of a GameObject variable using the value of another GameOject variable.")]
[NodePath("Actions/Variables/GameObject")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_GameObject")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Sets the value of a GameObject variable using the value of another GameOject variable.")]
public class uScriptAct_SetGameObject : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The variable you wish to use to set the target's value.")] GameObject Value, [FriendlyName("Target", "The Target variable you wish to set.")] out GameObject TargetGameObject)
	{
		TargetGameObject = Value;
	}
}
