using UnityEngine;

[NodePath("Actions/GameObjects")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Detaches all children GameObjects from the target parent GameObject.")]
[FriendlyName("Detach Children", "Detaches all children GameObjects from the target parent GameObject.")]
public class uScriptAct_DetachChildren : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The target GameObject.")] GameObject[] Target)
	{
		foreach (GameObject gameObject in Target)
		{
			if (null != gameObject)
			{
				gameObject.transform.DetachChildren();
			}
		}
	}
}
