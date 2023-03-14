using System;
using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Animation")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Play_Animation")]
[FriendlyName("Play Animation", "Play the specified animation on the target object.")]
[NodeToolTip("Play the specified animation on the target object.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_PlayAnimation : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private GameObject m_GameObject;

	private string m_Animation;

	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Finished")]
	public event uScriptEventHandler Finished;

	public void In([FriendlyName("Target", "The target GameObject(s) to play the animation on.")] GameObject[] Target, [FriendlyName("Animation", "The name of the animation to play. Animation must exist in the GameObject's AnimationClip.")] string Animation, [FriendlyName("Speed Factor", "The speed at which to play the animation.")][DefaultValue(1f)][SocketState(false, false)] float SpeedFactor, [FriendlyName("Wrap Mode", "Specifies what should happen at the end of the animation.")][SocketState(false, false)] WrapMode AnimWrapMode, [DefaultValue(true)][FriendlyName("Stop Other Animation", "Stop any currently playing animations before playing this one.")][SocketState(false, false)] bool StopOtherAnimations)
	{
		m_GameObject = null;
		foreach (GameObject gameObject in Target)
		{
			if (gameObject != null)
			{
				if ((bool)gameObject.GetComponent<Animation>().GetClip(Animation))
				{
					m_GameObject = gameObject;
					m_Animation = Animation;
					if (SpeedFactor == 0f)
					{
						gameObject.GetComponent<Animation>()[Animation].speed = 1f;
					}
					else
					{
						gameObject.GetComponent<Animation>()[Animation].speed = SpeedFactor;
					}
					if (StopOtherAnimations)
					{
						gameObject.GetComponent<Animation>().Play(PlayMode.StopAll);
					}
					if (SpeedFactor < 0f)
					{
						gameObject.GetComponent<Animation>()[Animation].time = gameObject.GetComponent<Animation>()[Animation].length;
					}
					gameObject.GetComponent<Animation>()[Animation].wrapMode = AnimWrapMode;
					gameObject.GetComponent<Animation>().Play(Animation);
				}
			}
			else
			{
				uScriptDebug.Log("The specified Target " + gameObject.name + " doesn't contain animation " + Animation, uScriptDebug.Type.Warning);
			}
		}
	}

	public void Update()
	{
		if (null != m_GameObject && !m_GameObject.GetComponent<Animation>().IsPlaying(m_Animation))
		{
			m_GameObject = null;
			if (this.Finished != null)
			{
				this.Finished(this, new EventArgs());
			}
		}
	}
}
