using UnityEngine;

[NodePath("Actions/GUI/Global")]
[FriendlyName("GUI Get Content Color", "Gets the current content tint color for the GUI.")]
[NodeToolTip("Gets the current content tint color for the GUI.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Get_Content_Color")]
public class uScriptAct_GUIGetContentColor : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Color", "The current color that the GUI content objects are tinted with.")] out Color color)
	{
		color = GUI.contentColor;
	}
}
