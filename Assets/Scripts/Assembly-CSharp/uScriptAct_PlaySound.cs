using System;
using System.Collections.Generic;
using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Audio")]
[NodeToolTip("Plays the specified AudioClip on the target GameObject.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Play_Sound")]
[FriendlyName("Play Sound", "Plays the specified AudioClip on the target GameObject.\n\nNote: This node will create a new instance of an AudioSource component when playing. If you wish to play a soud using a GameObjects existing AudioSource component, please use the Play AudioSource or Play AudioSource (OneShot) nodes instead.")]
public class uScriptAct_PlaySound : uScriptLogic
{
	private List<AudioSource> m_AudioSources = new List<AudioSource>();

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public event EventHandler Finished;

	public void Play(AudioClip audioClip, GameObject[] target, float volume, bool loop)
	{
		if (target.Length > 0 && null != audioClip)
		{
			for (int i = 0; i < target.Length; i++)
			{
				AudioSource audioSource = target[i].AddComponent<AudioSource>();
				audioSource.clip = audioClip;
				audioSource.volume = volume;
				audioSource.loop = loop;
				audioSource.Play();
				m_AudioSources.Add(audioSource);
			}
		}
	}

	[FriendlyName("Update Volume")]
	public void UpdateVolume(AudioClip audioClip, GameObject[] target, float volume, bool loop)
	{
		foreach (AudioSource audioSource in m_AudioSources)
		{
			audioSource.volume = volume;
		}
	}

	public void Stop([RequiresLink][FriendlyName("Audio Clip", "The AudioClip to play.")] AudioClip audioClip, [FriendlyName("Target", "The GameObject to emit the sound from.")] GameObject[] target, [DefaultValue(1f)][SocketState(false, false)][FriendlyName("Volume", "The volume level (0.0-1.0) to play the audio clip at.")] float volume, [FriendlyName("Loop", "Whether or not to loop the sound.")][SocketState(false, false)] bool loop)
	{
		if (m_AudioSources == null)
		{
			return;
		}
		foreach (AudioSource audioSource in m_AudioSources)
		{
			audioSource.Stop();
		}
	}

	public void Update()
	{
		if (m_AudioSources.Count == 0)
		{
			return;
		}
		for (int i = 0; i < m_AudioSources.Count; i++)
		{
			if (!m_AudioSources[i].isPlaying)
			{
				AudioSource obj = m_AudioSources[i];
				UnityEngine.Object.Destroy(obj);
				m_AudioSources.RemoveAt(i);
				i--;
			}
		}
		if (m_AudioSources.Count == 0 && this.Finished != null)
		{
			this.Finished(this, new EventArgs());
		}
	}
}
