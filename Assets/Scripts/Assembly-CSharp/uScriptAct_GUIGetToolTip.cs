using UnityEngine;

[FriendlyName("GUI Get Tooltip", "Gets the tooltip of the control that is currently being hovered over with the cursor.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Gets the tooltip of the control that is currently being hovered over with the cursor.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/GUI/Global")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Get_Tooltip")]
public class uScriptAct_GUIGetToolTip : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Tooltip", "The tooltip of the control that is currently being hovered over with the cursor.")] out string tooltip)
	{
		tooltip = GUI.tooltip;
	}
}
