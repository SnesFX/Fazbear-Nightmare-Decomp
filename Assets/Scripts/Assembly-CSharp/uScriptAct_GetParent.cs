using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_All_Children")]
[FriendlyName("Get Parent", "Returns the parent of a GameObject.")]
[NodePath("Actions/GameObjects")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Returns the parent of a GameObject.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_GetParent : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The child GameObject.")] GameObject Target, [FriendlyName("Parent", "The parent GameObject of Target.")] out GameObject Parent)
	{
		if (null != Target.transform.parent)
		{
			Parent = Target.transform.parent.gameObject;
			return;
		}
		uScriptDebug.Log("(Node - Get Parent): The specified Target GameObject does not have a parent (was null).", uScriptDebug.Type.Warning);
		Parent = null;
	}
}
