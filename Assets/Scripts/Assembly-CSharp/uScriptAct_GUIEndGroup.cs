using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("GUI End Group", "Ends a GUI control group.  Each use of this node must be matched with a prior call to \"GUI Begin Group\".\n\nNOTE: The node can directly follow a \"GUI Begin Group\" node, although any GUI controls that follow the \"GUI End Group\" will not appear inside the group.  Controls that follow the \"GUI Begin Group\" in separate chains will appear in the group.  Single node chains are the easiest way to determine which GUI controls will appear within a given group.")]
[NodeToolTip("Ends a GUI control group with a local coordinate system.")]
[NodePath("Actions/GUI/Controls")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Label")]
public class uScriptAct_GUIEndGroup : uScriptLogic
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
		GUI.EndGroup();
	}
}
