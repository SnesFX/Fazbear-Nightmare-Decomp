using System;
using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Camera_Depth")]
[FriendlyName("Set Camera Depth", "Sets the Target GameObject's camera's depth to the specified float value.")]
[NodeToolTip("Sets the Target GameObject's camera's depth to the specified float value.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Camera")]
public class uScriptAct_SetCameraDepth : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The GameObject whose camera to set the depth of.")] GameObject Target, [FriendlyName("Depth", "The new depth of the specified GameObject's camera.")] float Depth)
	{
		if (Target != null)
		{
			try
			{
				Component component = Target.GetComponent("Camera");
				component.GetComponent<Camera>().depth = Depth;
			}
			catch (Exception ex)
			{
				uScriptDebug.Log(ex.ToString(), uScriptDebug.Type.Error);
			}
		}
	}
}
