using UnityEngine;

[NodeToolTip("Sets the parent of a GameObject.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/GameObjects")]
[FriendlyName("Set Parent", "Sets the parent of a GameObject.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_SetParent : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The GameObject you wish to be the child.")] GameObject[] Target, [FriendlyName("Parent", "The GameObject you wish to set as the Target's parent.")] GameObject Parent)
	{
		foreach (GameObject gameObject in Target)
		{
			if (null != gameObject)
			{
				if (null != Parent)
				{
					gameObject.transform.parent = Parent.transform;
				}
				else
				{
					gameObject.transform.parent = null;
				}
			}
		}
	}
}
