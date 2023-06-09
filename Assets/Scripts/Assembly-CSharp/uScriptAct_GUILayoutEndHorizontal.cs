using UnityEngine;

[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
[NodeToolTip("Close a control group started with a \"GUILayout Begin Horizontal\" node.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUILayout_EndHorizontal")]
[FriendlyName("GUILayout End Horizontal", "Close a control group started with a \"GUILayout Begin Horizontal\" node.")]
[NodePath("Actions/GUI/Controls")]
public class uScriptAct_GUILayoutEndHorizontal : uScriptLogic
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
		GUILayout.EndHorizontal();
	}
}
