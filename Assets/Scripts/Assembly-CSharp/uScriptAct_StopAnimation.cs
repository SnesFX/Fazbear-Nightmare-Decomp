using System;
using UnityEngine;

[NodePath("Actions/Animation")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Stop all animation on the target GameObjects.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Stop Animation", "Stop and rewinds all animations on the target GameObjects. You can specify an optional animation name to stop and rewind just that animation on the target GameObjects.")]
public class uScriptAct_StopAnimation : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The target GameObject(s) you wish to stop animations on.")] GameObject[] Target, [FriendlyName("Animation Name", "(optional) Provide an animation name to just stop a specific animation.")][SocketState(false, false)] string AnimationName)
	{
		foreach (GameObject gameObject in Target)
		{
			if (gameObject != null)
			{
				if (string.Empty != AnimationName)
				{
					gameObject.GetComponent<Animation>().Stop(AnimationName);
				}
				else
				{
					gameObject.GetComponent<Animation>().Stop();
				}
			}
		}
	}
}
