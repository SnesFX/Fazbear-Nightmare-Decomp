using UnityEngine;

[FriendlyName("Set Scale", "Sets the scale of a GameObject.")]
[NodePath("Actions/GameObjects/Movement")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Scale")]
[NodeToolTip("Sets the scale of a GameObject.")]
public class uScriptAct_SetGameObjectScale : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The Target GameObject(s) to set the position of.")] GameObject[] Target, [FriendlyName("Scale", "The new X, Y and Z scale as a Vector3(X, Y, Z)")] Vector3 Scale)
	{
		foreach (GameObject gameObject in Target)
		{
			if (gameObject != null)
			{
				gameObject.transform.localScale = Scale;
			}
		}
	}
}
