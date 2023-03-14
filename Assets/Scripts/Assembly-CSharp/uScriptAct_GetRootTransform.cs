using UnityEngine;

[NodePath("Actions/Variables/Transform")]
[FriendlyName("Get Root Transform", "Gets the topmost Transform in the hierarchy. If the target Transform is the root transofrm, it will return itself.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Gets the topmost Transform in the hierarchy.")]
[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
public class uScriptAct_GetRootTransform : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The Transform you wish to get the rotation information of.")] Transform target, [FriendlyName("Root", "The root Transform of the target Transform.")] out Transform rootTransform, [SocketState(false, false)][FriendlyName("Root GameObject", "The root GameObject of the target Transform.")] out GameObject rootGameObject)
	{
		rootTransform = target.root;
		rootGameObject = target.root.gameObject;
	}
}
