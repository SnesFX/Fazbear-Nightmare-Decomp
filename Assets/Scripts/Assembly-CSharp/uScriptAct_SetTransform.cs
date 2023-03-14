using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Variables/Transform")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Sets the value of a Transform variable using the value of another Transform variable.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Set Transform", "Sets the value of a Transform variable using the value of another Transform variable.")]
public class uScriptAct_SetTransform : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The variable you wish to use to set the target's value.")] Transform Value, [FriendlyName("Target", "The Target variable you wish to set.")] out Transform TargetTransform)
	{
		TargetTransform = Value;
	}
}
