using System;
using UnityEngine;

[NodeAuthor("Detox Studios LLC. Original node by Krillbite", "http://www.detoxstudios.com")]
[NodePath("Actions/Animation")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Play Animation (Additive)", "Blend animations from specific bodyparts using a mixing transform.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Blend animations from specific bodyparts using a mixing transform.")]
public class uScriptAct_PlayAnimationAdditive : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private GameObject m_GameObject;

	private string m_Animation;

	private float m_playNextTime;

	private bool m_PlayNextFired;

	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Finished", "Fired when the animation is done playing.")]
	public event uScriptEventHandler Finished;

	[FriendlyName("Play Next", "Fired when the animation has reached the time remaining specified in Play Next Time.")]
	public event uScriptEventHandler PlayNext;

	[FriendlyName("Stopped", "Fired when the animaiton is stopped.")]
	public event uScriptEventHandler Stopped;

	public void In([FriendlyName("Target", "The target GameObject to play the animation on.")] GameObject Target, [FriendlyName("Animation", "The name of the animation to play. Animation must exist in the GameObject's AnimationClip.")] string Animation, [FriendlyName("Mixing Transform", "The target GameObject in the character's hierarchy where the blending should occur.")] GameObject[] MixingTransform, [SocketState(false, false)][FriendlyName("Speed Factor", "The speed at which to play the animation.")][DefaultValue(1f)] float SpeedFactor, [DefaultValue(1f)][FriendlyName("Blend Weight", "The strength of the blending between animations.")][SocketState(false, false)] float BlendWeight, [DefaultValue(1f)][FriendlyName("Fade Length", "How long (in seconds) the blend should take to reach the Blend Weight")][SocketState(false, false)] float FadeLength, [DefaultValue(0f)][SocketState(false, false)][FriendlyName("Play Next Time", "The time remaining (in seconds) in the current animation to fire the Play Next output socket.")] float PlayNextTime, [DefaultValue(2)][FriendlyName("Set to layer", "The animaiton layer to use for the blend.")][SocketState(false, false)] int setLayer, [FriendlyName("Wrap Mode", "Specifies what should happen at the end of the animation.")][SocketState(false, false)] WrapMode AnimWrapMode)
	{
		m_GameObject = null;
		m_playNextTime = PlayNextTime;
		m_PlayNextFired = false;
		if (Target != null)
		{
			m_GameObject = Target;
			m_Animation = Animation;
			if (SpeedFactor == 0f)
			{
				Target.GetComponent<Animation>()[m_Animation].speed = 1f;
			}
			else
			{
				Target.GetComponent<Animation>()[m_Animation].speed = SpeedFactor;
			}
			if (SpeedFactor < 0f)
			{
				Target.GetComponent<Animation>()[m_Animation].time = Target.GetComponent<Animation>()[m_Animation].length;
			}
			else
			{
				Target.GetComponent<Animation>()[m_Animation].time = 0f;
			}
			Target.GetComponent<Animation>()[m_Animation].wrapMode = AnimWrapMode;
			foreach (GameObject gameObject in MixingTransform)
			{
				Target.GetComponent<Animation>()[m_Animation].AddMixingTransform(gameObject.transform);
				Target.GetComponent<Animation>()[m_Animation].layer = setLayer;
				Target.GetComponent<Animation>().Blend(m_Animation, BlendWeight, FadeLength);
			}
		}
	}

	public void Stop([FriendlyName("Target", "The target GameObject to play the animation on.")] GameObject Target, [FriendlyName("Animation", "The name of the animation to play. Animation must exist in the GameObject's AnimationClip.")] string Animation, [FriendlyName("Mixing Transform", "The target GameObject in the character's hierarchy where the blending should occur.")] GameObject[] MixingTransform, [DefaultValue(1f)][SocketState(false, false)][FriendlyName("Speed Factor", "The speed at which to play the animation.")] float SpeedFactor, [DefaultValue(1f)][FriendlyName("Blend Weight", "The strength of the blending between animations.")][SocketState(false, false)] float BlendWeight, [FriendlyName("Fade Length", "How long (in seconds) the blend should take to reach the Blend Weight")][SocketState(false, false)][DefaultValue(1f)] float FadeLength, [DefaultValue(0f)][FriendlyName("Play Next Time", "The time remaining (in seconds) in the current animation to fire the Play Next output socket.")][SocketState(false, false)] float PlayNextTime, [DefaultValue(2)][SocketState(false, false)][FriendlyName("Set to layer", "The animaiton layer to use for the blend.")] int setLayer, [FriendlyName("Wrap Mode", "Specifies what should happen at the end of the animation.")][SocketState(false, false)] WrapMode AnimWrapMode)
	{
		m_GameObject = null;
		m_PlayNextFired = true;
		if (Target != null)
		{
			foreach (GameObject gameObject in MixingTransform)
			{
				Target.GetComponent<Animation>()[m_Animation].RemoveMixingTransform(gameObject.transform);
				Target.GetComponent<Animation>()[m_Animation].layer = setLayer;
				Target.GetComponent<Animation>().Blend(m_Animation, 0f, FadeLength);
			}
		}
		this.Stopped(this, new EventArgs());
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
