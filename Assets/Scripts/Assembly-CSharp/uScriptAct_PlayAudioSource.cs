using System;
using System.Collections.Generic;
using UnityEngine;

[NodeToolTip("Plays the specified AudioClip on the target GameObject.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Audio")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Play AudioSource", "Plays the specified AudioSource on the target GameObject(s). Playing the AudioSource will immediately replace any existing sound playing from it. If you wish to change settings on the AudioSource (such as volume or pitch) at runtime, use the reflected properties to do so.\n\nNOTE: To use this node, you must have already setup an AudioSource component on the target GameObject(s). If you want to simply play a sound with default AudioSource settings without needing to have an existing AudioSource component on the GameObject, use the Play Sound node.")]
public class uScriptAct_PlayAudioSource : uScriptLogic
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

	public void Play(GameObject[] target, AudioClip audioClip)
	{
		if (target.Length <= 0)
		{
			return;
		}
		for (int i = 0; i < target.Length; i++)
		{
			if (!(null != target[i]))
			{
				continue;
			}
			if (null != target[i].GetComponent<AudioSource>())
			{
				AudioSource component = target[i].GetComponent<AudioSource>();
				if (null != audioClip)
				{
					component.clip = audioClip;
				}
				component.Play();
				m_AudioSources.Add(component);
			}
			else
			{
				uScriptDebug.Log("[Play AudioSource] The target GameObject (" + target[i].name + ") does not have an existing AudioSource. Please add an AudioSource component to the target or try using the Play Sound node instead.", uScriptDebug.Type.Warning);
			}
		}
	}

	public void Stop([FriendlyName("Target", "The GameObject to emit the sound from.")] GameObject[] target, [FriendlyName("Audio Clip", "(optional) You can specify an AudioClip to play instead of the deault AudioClip assigned to the target's AudioSource component.")][RequiresLink] AudioClip audioClip)
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
