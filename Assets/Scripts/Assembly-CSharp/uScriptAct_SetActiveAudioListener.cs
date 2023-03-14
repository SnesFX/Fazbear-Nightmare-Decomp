using System;
using UnityEngine;

[NodePath("Actions/Audio")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Set Active Audio Listener", "Sets the active AudioListener to the one on the specified GameObject.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Active_Audio_Listener")]
[NodeToolTip("Sets the active AudioListener to the one on the specified GameObject.")]
public class uScriptAct_SetActiveAudioListener : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The GameObject to use as the new AudioListener.")] GameObject Target)
	{
		AudioListener component = Target.GetComponent<AudioListener>();
		if (!(component != null))
		{
			return;
		}
		try
		{
			AudioListener[] array = UnityEngine.Object.FindObjectsOfType(typeof(AudioListener)) as AudioListener[];
			AudioListener[] array2 = array;
			foreach (AudioListener audioListener in array2)
			{
				audioListener.enabled = false;
			}
			component.enabled = true;
		}
		catch (Exception ex)
		{
			uScriptDebug.Log(ex.ToString(), uScriptDebug.Type.Error);
		}
	}
}
