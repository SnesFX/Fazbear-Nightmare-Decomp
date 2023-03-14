using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUILayout_EndVertical")]
[NodeToolTip("Close a control group started with a \"GUILayout Begin Vertical\" node.")]
[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
[FriendlyName("GUILayout End Vertical", "Close a control group started with a \"GUILayout Begin Vertical\" node.")]
[NodePath("Actions/GUI/Controls")]
public class uScriptAct_GUILayoutEndVertical : uScriptLogic
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
		GUILayout.EndVertical();
	}
}
