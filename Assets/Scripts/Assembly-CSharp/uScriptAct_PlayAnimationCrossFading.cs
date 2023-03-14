using System;
using UnityEngine;

[NodeAuthor("Detox Studios LLC. Original node by Krillbite", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Play Animation (Cross Fading)", "Blend animations from one to the other. Use the Fire Before value to determine when to trigger the Play Next output socket.")]
[NodeToolTip("Blend animations from one to the other.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Animation")]
public class uScriptAct_PlayAnimationCrossFading : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private GameObject m_GameObject;

	private float m_playNextTime;

	private string m_Animation;

	private bool m_PlayNextFired;

	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Finished")]
	public event uScriptEventHandler Finished;

	[FriendlyName("Play Next", "Fired when the animation has reached the time remaining specified in Play Next Time.")]
	public event uScriptEventHandler PlayNext;

	public void In([FriendlyName("Target", "The target GameObject to play the animation on.")] GameObject[] Target, [FriendlyName("Animation", "The name of the animation to play. Animation must exist in the GameObject's AnimationClip.")] string Animation, [DefaultValue(1f)][FriendlyName("Speed Factor", "The speed at which to play the animation.")][SocketState(false, false)] float SpeedFactor, [FriendlyName("Fade Length", "How long (in seconds) the blend should take to reach the Blend Weight")][DefaultValue(1f)][SocketState(false, false)] float FadeLength, [FriendlyName("Play Next Time", "The time remaining (in seconds) in the current animation to fire the Play Next output socket.")][DefaultValue(0f)][SocketState(false, false)] float PlayNextTime, [FriendlyName("Wrap Mode", "Specifies what should happen at the end of the animation.")][SocketState(false, false)] WrapMode AnimWrapMode, [SocketState(false, false)][DefaultValue(true)][FriendlyName("Stop Other Animation", "Stop any currently playing animations before playing this one.")] bool StopOtherAnimations)
	{
		m_GameObject = null;
		m_playNextTime = PlayNextTime;
		m_PlayNextFired = false;
		foreach (GameObject gameObject in Target)
		{
			if (gameObject != null)
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
				gameObject.GetComponent<Animation>().CrossFade(Animation, FadeLength);
			}
		}
	}

	public void Update()
	{
		if (!(m_GameObject != null))
		{
			return;
		}
		if (m_playNextTime > 0f && m_GameObject.GetComponent<Animation>()[m_Animation].time >= m_GameObject.GetComponent<Animation>()[m_Animation].length - m_playNextTime)
		{
			m_GameObject = null;
			if (this.PlayNext != null)
			{
				m_PlayNextFired = true;
				this.PlayNext(this, new EventArgs());
			}
		}
		if (!m_GameObject.GetComponent<Animation>().IsPlaying(m_Animation))
		{
			m_GameObject = null;
			if (this.Finished != null)
			{
				this.Finished(this, new EventArgs());
			}
			if (!m_PlayNextFired && m_playNextTime > 0f && this.PlayNext != null)
			{
				m_PlayNextFired = true;
				this.PlayNext(this, new EventArgs());
			}
		}
	}
}
