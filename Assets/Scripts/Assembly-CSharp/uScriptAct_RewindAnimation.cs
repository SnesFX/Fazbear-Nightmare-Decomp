using System;
using UnityEngine;

[FriendlyName("Rewind Animation", "Rewind all animations on the target GameObjects. You can specify an optional animation name to rewind just that animation on the target GameObjects.")]
[NodePath("Actions/Animation")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeToolTip("Rewind all animations on the target GameObjects.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_RewindAnimation : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The target GameObject(s) you wish to rewind animations on.")] GameObject[] Target, [FriendlyName("Animation Name", "(optional) Provide an animation name to just rewind a specific animation.")][SocketState(false, false)] string AnimationName)
	{
		foreach (GameObject gameObject in Target)
		{
			if (gameObject != null)
			{
				if (string.Empty != AnimationName)
				{
					gameObject.GetComponent<Animation>().Rewind(AnimationName);
				}
				else
				{
					gameObject.GetComponent<Animation>().Rewind();
				}
			}
		}
	}
}
