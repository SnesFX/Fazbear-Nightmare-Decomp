using UnityEngine;

[NodeToolTip("Sets the world coordinates euler angle rotation of a GameObject.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Set Euler Angles", "Sets the world coordinates euler angle rotation of a GameObject by specifing the X, Y, and Z axis in degrees.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/GameObjects/Movement")]
public class uScriptAct_SetGameObjectEulerAngles : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The Target GameObject(s) to set Euler Angles for.")] GameObject[] Target, [FriendlyName("X Axis", "The X Axis Euler angle to set.")] float X_Axis, [FriendlyName("Preserve X Axis", "Whether or not to preserve the current X Axis Euler angle.")][SocketState(false, false)] bool PreserveX_Axis, [FriendlyName("Y Axis", "The Y Axis Euler angle to set.")] float Y_Axis, [FriendlyName("Preserve Y Axis", "Whether or not to preserve the current Y Axis Euler angle.")][SocketState(false, false)] bool PreserveY_Axis, [FriendlyName("Z Axis", "The Z Axis Euler angle to set.")] float Z_Axis, [FriendlyName("Preserve Z Axis", "Whether or not to preserve the current Z Axis Euler angle.")][SocketState(false, false)] bool PreserveZ_Axis)
	{
		if (!PreserveX_Axis)
		{
			while (X_Axis > 360f)
			{
				X_Axis -= 360f;
			}
			while (X_Axis < 0f)
			{
				X_Axis += 360f;
			}
		}
		if (!PreserveY_Axis)
		{
			while (Y_Axis > 360f)
			{
				Y_Axis -= 360f;
			}
			while (Y_Axis < 0f)
			{
				Y_Axis += 360f;
			}
		}
		if (!PreserveZ_Axis)
		{
			while (Z_Axis > 360f)
			{
				Z_Axis -= 360f;
			}
			while (Z_Axis < 0f)
			{
				Z_Axis += 360f;
			}
		}
		foreach (GameObject gameObject in Target)
		{
			Vector3 eulerAngles = gameObject.transform.eulerAngles;
			Vector3 eulerAngles2 = new Vector3(eulerAngles.x, eulerAngles.y, eulerAngles.z);
			if (!PreserveX_Axis)
			{
				eulerAngles2.x = X_Axis;
			}
			if (!PreserveY_Axis)
			{
				eulerAngles2.y = Y_Axis;
			}
			if (!PreserveZ_Axis)
			{
				eulerAngles2.z = Z_Axis;
			}
			gameObject.transform.eulerAngles = eulerAngles2;
		}
	}
}
