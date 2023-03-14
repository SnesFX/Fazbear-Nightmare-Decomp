using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/GUI/Global")]
[NodeToolTip("Gets the current tint color for the GUI.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Get_Color")]
[FriendlyName("GUI Get Color", "Gets the current tint color for the GUI.")]
public class uScriptAct_GUIGetColor : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Color", "The current color that the GUI is tinted with.")] out Color color)
	{
		color = GUI.color;
	}
}
