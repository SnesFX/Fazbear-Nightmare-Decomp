using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Set_Background_Color")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Sets the current background tint color for the GUI.")]
[FriendlyName("GUI Set Background Color", "Sets the current background tint color for the GUI.\n\nNOTE: This color selection only lasts for the current frame or until another color is set.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/GUI/Global")]
public class uScriptAct_GUISetBackgroundColor : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Color", "The color to tint the GUI background objects with.")] Color color)
	{
		GUI.backgroundColor = color;
	}
}
