using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("GUI End ScrollView", "Ends a GUI control scrollview with a local coordinate system. Each use of this node must be matched with a prior call to \"GUI Begin ScrollView\".\n\nNOTE: The node can directly follow a \"GUI Begin ScrollView\" node, although any GUI controls that follow the \"GUI End ScrollView\" will not appear inside the scrollview.  Controls that follow the \"GUI Begin ScrollView\" in separate chains will appear in the scrollview.  Single node chains are the easiest way to determine which GUI controls will appear within a given scrollview.")]
[NodePath("Actions/GUI/Controls")]
[NodeToolTip("Ends a GUI control scrollview with a local coordinate system.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Label")]
public class uScriptAct_GUIEndScrollView : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In()
	{
		GUI.EndScrollView();
	}
}
