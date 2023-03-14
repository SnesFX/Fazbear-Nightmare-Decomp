using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("KeyCode To String", "Converts a KeyCode into a string.")]
[NodePath("Actions/Variables/KeyCode")]
[NodeToolTip("Converts a KeyCode into a string.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Int")]
public class uScriptAct_KeyCodeToString : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The variable you wish to use to set the target's value.")] KeyCode Value, [FriendlyName("Target", "The Target variable you wish to set.")] out string Target)
	{
		Target = Value.ToString();
	}
}
