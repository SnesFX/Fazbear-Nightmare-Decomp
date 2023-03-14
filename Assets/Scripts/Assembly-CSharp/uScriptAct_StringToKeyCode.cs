using System;
using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Int")]
[FriendlyName("String To KeyCode", "Converts a string into a KeyCode. If a match is not found, the specified default is returned.")]
[NodePath("Actions/Variables/KeyCode")]
[NodeToolTip("Converts a string into a KeyCode.")]
public class uScriptAct_StringToKeyCode : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The string you wish to use to convert to a KeyCode. Note: casing of the dtring must match that of the KeyCode!")] string Value, [DefaultValue(KeyCode.None)][SocketState(false, false)][FriendlyName("Default", "The default KeyCode to return if a match is not found.")] KeyCode Default, [FriendlyName("Target", "The Target variable you wish to set.")] out KeyCode Target)
	{
		string[] names = Enum.GetNames(typeof(KeyCode));
		KeyCode keyCode = Default;
		string[] array = names;
		foreach (string text in array)
		{
			if (text == Value)
			{
				keyCode = (KeyCode)(int)Enum.Parse(typeof(KeyCode), text, false);
			}
		}
		Target = keyCode;
	}
}
