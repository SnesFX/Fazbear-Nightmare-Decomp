using System;
using UnityEngine;

[FriendlyName("Switch Cameras", "Disables the 'From' GameObject camera and enables the 'To' GameObject camera. Good for switching from one main camera to another.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Switch_Cameras")]
[NodeToolTip("Switches from 'From Camera' to 'To Camera'.")]
[NodePath("Actions/Camera")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_SwitchCameras : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("From", "The GameObject containing the camera to switch from.")] GameObject FromCamera, [FriendlyName("To", "The GameObject containing the camera to switch to.")] GameObject Target, [FriendlyName("Enable AudioListener", "Whether or not to enable the 'To' camera's AudioListener component (if it has one).")][SocketState(false, false)][DefaultValue(true)] bool EnableAudioListener)
	{
		if (!(FromCamera != null) || !(Target != null))
		{
			return;
		}
		try
		{
			Component component = FromCamera.GetComponent("Camera");
			Component component2 = Target.GetComponent("Camera");
			component.GetComponent<Camera>().enabled = false;
			component2.GetComponent<Camera>().enabled = true;
		}
		catch (Exception ex)
		{
			uScriptDebug.Log(ex.ToString(), uScriptDebug.Type.Error);
		}
		if (!EnableAudioListener)
		{
			return;
		}
		AudioListener component3 = Target.GetComponent<AudioListener>();
		if (!(component3 != null))
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
			component3.enabled = true;
		}
		catch (Exception ex2)
		{
			uScriptDebug.Log(ex2.ToString(), uScriptDebug.Type.Error);
		}
	}
}
