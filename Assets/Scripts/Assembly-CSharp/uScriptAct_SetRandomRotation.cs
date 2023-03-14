using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/GameObjects/Movement")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Set Random Rotation", "Randomly sets the rotation of a GameObject.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Randomly sets the rotation of a GameObject.")]
public class uScriptAct_SetRandomRotation : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The GameObject(s) that the random rotation is applied to.")] GameObject[] Target, [FriendlyName("Min Angle X", "Minimum allowable angle. (0-360 degrees)")][DefaultValue(0f)][SocketState(false, false)] float MinX, [DefaultValue(360f)][SocketState(false, false)][FriendlyName("Max Angle X", "Maximum allowable angle. (0-360 degrees)")] float MaxX, [DefaultValue(0f)][SocketState(false, false)][FriendlyName("Min Angle Y", "Minimum allowable angle. (0-360 degrees)")] float MinY, [FriendlyName("Max Angle Y", "Maximum allowable angle. (0-360 degrees)")][DefaultValue(360f)][SocketState(false, false)] float MaxY, [SocketState(false, false)][DefaultValue(0f)][FriendlyName("Min Angle Z", "Minimum allowable angle. (0-360 degrees)")] float MinZ, [SocketState(false, false)][FriendlyName("Max Angle Z", "Maximum allowable angle. (0-360 degrees)")][DefaultValue(360f)] float MaxZ, [SocketState(false, false)][FriendlyName("Preserve X", "If checked, the existing value will be passed into the new rotation, overriding the random value for this axis.")] bool PreserveX_Axis, [SocketState(false, false)][FriendlyName("Preserve Y", "If checked, the existing value will be passed into the new rotation, overriding the random value for this axis.")] bool PreserveY_Axis, [FriendlyName("Preserve Z", "If checked, the existing value will be passed into the new rotation, overriding the random value for this axis.")][SocketState(false, false)] bool PreserveZ_Axis)
	{
		if (MinX > MaxX)
		{
			MinX = MaxX;
		}
		if (MaxX < MinX)
		{
			MaxX = MinX;
		}
		if (MinY > MaxY)
		{
			MinY = MaxY;
		}
		if (MaxY < MinY)
		{
			MaxY = MinY;
		}
		if (MinZ > MaxZ)
		{
			MinZ = MaxZ;
		}
		if (MaxZ < MinZ)
		{
			MaxZ = MinZ;
		}
		if (MinX < 0f)
		{
			MinX = 0f;
		}
		if (MaxX > 360f)
		{
			MaxX = 360f;
		}
		if (MinY < 0f)
		{
			MinY = 0f;
		}
		if (MaxY > 360f)
		{
			MaxY = 360f;
		}
		if (MinZ < 0f)
		{
			MinZ = 0f;
		}
		if (MaxZ > 360f)
		{
			MaxZ = 360f;
		}
		foreach (GameObject gameObject in Target)
		{
			float x = ((!PreserveX_Axis) ? Random.Range(MinX, MaxX) : gameObject.transform.eulerAngles.x);
			float y = ((!PreserveY_Axis) ? Random.Range(MinY, MaxY) : gameObject.transform.eulerAngles.y);
			float z = ((!PreserveZ_Axis) ? Random.Range(MinZ, MaxZ) : gameObject.transform.eulerAngles.z);
			Vector3 eulerAngles = new Vector3(x, y, z);
			gameObject.transform.eulerAngles = eulerAngles;
		}
	}
}
