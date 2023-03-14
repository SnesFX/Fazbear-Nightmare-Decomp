using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Sets the current tint color for the GUI.")]
[FriendlyName("GUI Set Color", "Sets the current tint color for the GUI.\n\nNOTE: This color selection only lasts for the current frame or until another color is set.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Set_Color")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/GUI/Global")]
public class uScriptAct_GUISetColor : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Color", "The color to tint the GUI with.")] Color color)
	{
		GUI.color = color;
	}
}
