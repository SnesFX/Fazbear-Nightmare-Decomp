using UnityEngine;

[NodePath("Actions/GUI/Global")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Sets the current skin for the GUI.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Set_Skin")]
[FriendlyName("GUI Set Skin", "Sets the current skin for the GUI.\n\nNOTE: This skin selection only lasts for the current frame or until another skin is set.")]
public class uScriptAct_GUISetSkin : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Skin", "The skin to render the GUI with.")][RequiresLink] GUISkin skin)
	{
		GUI.skin = skin;
	}
}
