using UnityEngine;

[FriendlyName("Get Transform", "Returns the transform of a GameObject.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/GameObjects")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Returns the transform of a GameObject.")]
public class uScriptAct_GetTransform : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The GameObject you wish to get the transform for.")] GameObject Target, [FriendlyName("Transform", "The transform of the target GameObject.")] out Transform targetTransform)
	{
		if (null != Target)
		{
			targetTransform = Target.transform;
			return;
		}
		uScriptDebug.Log("(Node - Get Transform): The specified Target GameObject was null.", uScriptDebug.Type.Warning);
		targetTransform = null;
	}
}
