using UnityEngine;

[NodeToolTip("Sets the current content tint color for the GUI.")]
[NodePath("Actions/GUI/Global")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Set_Content_Color")]
[FriendlyName("GUI Set Content Color", "Sets the current content tint color for the GUI.\n\nNOTE: This color selection only lasts for the current frame or until another color is set.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_GUISetContentColor : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Color", "The color to tint the GUI content objects with.")] Color color)
	{
		GUI.contentColor = color;
	}
}
