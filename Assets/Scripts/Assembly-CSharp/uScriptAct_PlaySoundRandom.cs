using System;
using System.Collections.Generic;
using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Plays the specified AudioClip on the target GameObject.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Audio")]
[FriendlyName("Play Sound (Random)", "Plays a single random AudioClip on a single random Target GameObject from those specified. You can only specify a single target GameObject or AudioClip if you wish to just have one of the two things be random (either AudioClips or GameObjects).")]
public class uScriptAct_PlaySoundRandom : uScriptLogic
{
	private GameObject m_ChosenTarget;

	private AudioClip m_ChosenClip;

	private List<AudioSource> m_AudioSources = new List<AudioSource>();

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public event EventHandler Finished;

	public void Play(AudioClip[] audioClips, GameObject[] targets, float volume, bool loop, out GameObject chosenTarget, out AudioClip chosenClip)
	{
		if (targets.Length > 0 && audioClips.Length > 0)
		{
			GameObject gameObject = PickRandomTarget(targets);
			AudioClip audioClip = PickRandomAudioClip(audioClips);
			if (null != gameObject && null != audioClip)
			{
				AudioSource audioSource = gameObject.AddComponent<AudioSource>();
				audioSource.clip = audioClip;
				audioSource.volume = volume;
				audioSource.loop = loop;
				audioSource.Play();
				m_AudioSources.Add(audioSource);
			}
			else
			{
				uScriptDebug.Log("[Play Sound (Random)] A valid (non-null) GameObject and AudioClip combination could not be found. Please make sure you have at least one valid Target GameObject and AudioClip for the node to use.", uScriptDebug.Type.Warning);
			}
		}
		chosenTarget = m_ChosenTarget;
		chosenClip = m_ChosenClip;
	}

	[FriendlyName("Update Volume")]
	public void UpdateVolume(AudioClip[] audioClips, GameObject[] targets, float volume, bool loop, out GameObject chosenTarget, out AudioClip chosenClip)
	{
		foreach (AudioSource audioSource in m_AudioSources)
		{
			audioSource.volume = volume;
		}
		chosenTarget = m_ChosenTarget;
		chosenClip = m_ChosenClip;
	}

	public void Stop([RequiresLink][FriendlyName("Audio Clips", "The list of AudioClips to choose from.")] AudioClip[] audioClips, [FriendlyName("Targets", "The list of GameObjects to choose from.")] GameObject[] targets, [FriendlyName("Volume", "The volume level (0.0-1.0) to play the audio clip at.")][DefaultValue(1f)][SocketState(false, false)] float volume, [SocketState(false, false)][FriendlyName("Loop", "Whether or not to loop the sound.")] bool loop, [SocketState(false, false)][FriendlyName("Chosen Target", "The target GameObject that was chosen.")] out GameObject chosenTarget, [SocketState(false, false)][FriendlyName("Chosen AudioClip", "The target AudioClip that was chosen.")] out AudioClip chosenClip)
	{
		if (m_AudioSources != null)
		{
			foreach (AudioSource audioSource in m_AudioSources)
			{
				audioSource.Stop();
			}
		}
		chosenTarget = m_ChosenTarget;
		chosenClip = m_ChosenClip;
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

	private GameObject PickRandomTarget(GameObject[] Targets)
	{
		bool flag = false;
		foreach (GameObject gameObject in Targets)
		{
			if (null != gameObject)
			{
				flag = true;
			}
		}
		if (flag)
		{
			GameObject gameObject2 = null;
			while (gameObject2 == null)
			{
				gameObject2 = Targets[UnityEngine.Random.Range(0, Targets.Length)];
			}
			m_ChosenTarget = gameObject2;
			return gameObject2;
		}
		return null;
	}

	private AudioClip PickRandomAudioClip(AudioClip[] AudioClips)
	{
		bool flag = false;
		foreach (AudioClip audioClip in AudioClips)
		{
			if (null != audioClip)
			{
				flag = true;
			}
		}
		if (flag)
		{
			AudioClip audioClip2 = null;
			while (audioClip2 == null)
			{
				audioClip2 = AudioClips[UnityEngine.Random.Range(0, AudioClips.Length)];
			}
			m_ChosenClip = audioClip2;
			return audioClip2;
		}
		return null;
	}
}
