using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Get_Enabled_State")]
[NodeToolTip("Gets the current enabled state of the GUI.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/GUI/Global")]
[FriendlyName("GUI Get Enabled State", "Gets the current enabled state of the GUI.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_GUIGetEnabledState : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Enabled", "The current enabled state of the GUI.")] out bool Enabled)
	{
		Enabled = GUI.enabled;
	}
}
