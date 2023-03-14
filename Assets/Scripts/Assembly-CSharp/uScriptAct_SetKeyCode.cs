using UnityEngine;

[FriendlyName("Set KeyCode", "Sets an KeyCode to the defined value.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Sets an KeyCode to the defined value.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Variables/KeyCode")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Int")]
public class uScriptAct_SetKeyCode : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The variable you wish to use to set the target's value.")] KeyCode Value, [FriendlyName("Target", "The Target variable you wish to set.")] out KeyCode Target)
	{
		Target = Value;
	}
}
