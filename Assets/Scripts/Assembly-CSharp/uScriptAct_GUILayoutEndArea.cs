using UnityEngine;

[NodeToolTip("Close a control group started with a \"GUILayout Begin Area\" node.")]
[NodePath("Actions/GUI/Controls")]
[FriendlyName("GUILayout End Area", "Close a control group started with a \"GUILayout Begin Area\" node.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUILayout_EndArea")]
[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_GUILayoutEndArea : uScriptLogic
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
		GUILayout.EndArea();
	}
}
