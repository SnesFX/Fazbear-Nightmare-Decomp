using UnityEngine;

[FriendlyName("Set Anchor", "Sets an Anchor for a TextMesh component.")]
[NodePath("Actions/Variables/TextMesh")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Sets an Anchor for a TextMesh component.")]
public class uScriptAct_SetAnchor : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The variable you wish to use to set the target's value.")] TextAnchor Value, [FriendlyName("Target", "The Target variable you wish to set.")] out TextAnchor Target)
	{
		Target = Value;
	}
}
