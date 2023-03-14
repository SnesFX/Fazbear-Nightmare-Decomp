using UnityEngine;

[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
[FriendlyName("Get GameObject From Transform", "Gets the GameObject of a Transform variable.")]
[NodePath("Actions/Variables/Transform")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Gets the GameObject of a Transform variable.")]
public class uScriptAct_GetGameObjectFromTransform : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Input Transform", "The Transform you wish to get the GameObject of.")] Transform InputTransform, [FriendlyName("GameObject", "The GameObject of the Transform.")] out GameObject transGameObject)
	{
		transGameObject = InputTransform.gameObject;
	}
}
