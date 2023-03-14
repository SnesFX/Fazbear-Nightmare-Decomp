using UnityEngine;

[NodePath("Actions/GameObjects/Movement")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Sets the rotation in degrees (float) of a GameObject in local or world coordinates.")]
[FriendlyName("Set Rotation", "Sets the rotation of a GameObject in local or world coordinates. Optionally, can set rotation as offest from the target's current rotation.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Position")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_SetGameObjectRotation : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The GameObject(s) to rotate")] GameObject[] Target, [FriendlyName("X Degrees", "Rotation amount on the X axis")] float XDegrees, [FriendlyName("Y Degrees", "Rotation amount on the Y axis")] float YDegrees, [FriendlyName("Z Degrees", "Rotation amount on the Z axis")] float ZDegrees, [FriendlyName("Ignore X", "Do not apply this rotation to the X axis")][SocketState(false, false)] bool IgnoreX, [SocketState(false, false)][FriendlyName("Ignore Y", "Do not apply this rotation to the Y axis")] bool IgnoreY, [FriendlyName("Ignore Z", "Do not apply this rotation to the Z axis")][SocketState(false, false)] bool IgnoreZ, [SocketState(false, false)][FriendlyName("Space", "Space to apply rotation")] Space CoordinateSystem, [FriendlyName("As Offset", "Treat this rotation as an offset of the current GameObject's rotation")][SocketState(false, false)] bool AsOffset)
	{
		foreach (GameObject gameObject in Target)
		{
			Vector3 euler = Vector3.zero;
			if (AsOffset)
			{
				if (!IgnoreX)
				{
					euler.x = XDegrees;
				}
				if (!IgnoreY)
				{
					euler.y = YDegrees;
				}
				if (!IgnoreZ)
				{
					euler.z = ZDegrees;
				}
			}
			else
			{
				euler = gameObject.transform.rotation.eulerAngles;
				if (!IgnoreX)
				{
					euler.x = XDegrees;
				}
				if (!IgnoreY)
				{
					euler.y = YDegrees;
				}
				if (!IgnoreZ)
				{
					euler.z = ZDegrees;
				}
			}
			Quaternion quaternion;
			if (CoordinateSystem == Space.World)
			{
				quaternion = Quaternion.Euler(euler);
			}
			else
			{
				Quaternion quaternion2 = Quaternion.AngleAxis(euler.x, gameObject.transform.right);
				Quaternion quaternion3 = Quaternion.AngleAxis(euler.y, gameObject.transform.up);
				Quaternion quaternion4 = Quaternion.AngleAxis(euler.z, gameObject.transform.forward);
				quaternion = quaternion3 * quaternion2;
				quaternion = quaternion4 * quaternion;
			}
			if (AsOffset)
			{
				gameObject.transform.rotation = quaternion * gameObject.transform.rotation;
			}
			else
			{
				gameObject.transform.rotation = quaternion;
			}
		}
	}
}
