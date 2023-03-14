using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Set_Focused_Control")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Moves keyboard focus to a named control.")]
[NodeCopyright("Copyright 2013 by Detox Studios LLC")]
[NodePath("Actions/GUI/Global")]
[FriendlyName("GUI Set Focused Control", "Move keyboard focus to a named control.")]
public class uScriptAct_GUISetFocusedControl : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Control Name", "The name of the control that should receive keyboard focus.")][DefaultValue("")] string ControlName)
	{
		GUI.FocusControl(ControlName);
	}
}
