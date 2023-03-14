using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUILayout_EndScrollView")]
[NodePath("Actions/GUI/Controls")]
[FriendlyName("GUILayout End ScrollView", "Close a control group started with a \"GUILayout Begin ScrollView\" node.")]
[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
[NodeToolTip("Close a control group started with a \"GUILayout Begin ScrollView\" node.")]
public class uScriptAct_GUILayoutEndScrollView : uScriptLogic
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
		GUILayout.EndScrollView();
	}
}
