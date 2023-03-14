using UnityEngine;

[FriendlyName("Play AudioSource (One Shot)", "Plays the specified AudioSource on the target GameObject(s) using Unity's PlayOneShot option (fire and forget). If you wish to change settings on the AudioSource (such as volume or pitch) at runtime, use the reflected properties to do so.\n\nNOTE: To use this node, you must have already setup an AudioSource component on the target GameObject(s). If you want to simply play a sound with default AudioSource settings without needing to have an existing AudioSource component on the GameObject, use the Play Sound node.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Audio")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Plays the specified AudioClip on the target GameObject.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_PlayAudioSourceOneShot : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void Play([FriendlyName("Target", "The GameObject with the AudioSource component to play.")] GameObject[] target, [RequiresLink][FriendlyName("Audio Clip", "(optional) You can specify an AudioClip to play instead of the deault AudioClip assigned to the target's component.")] AudioClip audioClip)
	{
		if (target.Length <= 0)
		{
			return;
		}
		AudioClip[] array = new AudioClip[target.Length];
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
					array[i] = audioClip;
				}
				else
				{
					array[i] = component.clip;
				}
				component.PlayOneShot(array[i], component.volume);
			}
			else
			{
				uScriptDebug.Log("[Play AudioSource (One Shot)] The target GameObject (" + target[i].name + ") does not have an existing AudioSource. Please add an AudioSource component to the target or try using the Play Sound node instead.", uScriptDebug.Type.Warning);
			}
		}
	}
}
