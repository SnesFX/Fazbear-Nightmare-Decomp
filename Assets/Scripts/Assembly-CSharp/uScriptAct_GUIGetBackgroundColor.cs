using UnityEngine;

[NodePath("Actions/GUI/Global")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Get_Background_Color")]
[NodeToolTip("Gets the current background tint color for the GUI.")]
[FriendlyName("GUI Get Background Color", "Gets the current background tint color for the GUI.")]
public class uScriptAct_GUIGetBackgroundColor : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Color", "The current color that the GUI background objects are tinted with.")] out Color color)
	{
		color = GUI.backgroundColor;
	}
}
