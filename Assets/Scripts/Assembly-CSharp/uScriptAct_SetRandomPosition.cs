using UnityEngine;

[NodePath("Actions/GameObjects/Movement")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Set Random Position", "Randomly sets the world position of a GameObject based around an origin point in the world.\n\nNote: Preserving an axis will also override that axis for the specified Origin.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeToolTip("Randomly sets the world position of a GameObject based around an origin point in the world.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_SetRandomPosition : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The GameObject(s) that the random position is applied to.")] GameObject[] Target, [FriendlyName("Origin", "The starting location for the random position offset.")] Vector3 Origin, [SocketState(false, false)][FriendlyName("Min X", "Minimum allowable float value.")][DefaultValue(-10f)] float MinX, [DefaultValue(10f)][FriendlyName("Max X", "Maximum allowable float value.")][SocketState(false, false)] float MaxX, [SocketState(false, false)][FriendlyName("Min Y", "Minimum allowable float value.")][DefaultValue(-10f)] float MinY, [FriendlyName("Max Y", "Maximum allowable float value.")][DefaultValue(10f)][SocketState(false, false)] float MaxY, [DefaultValue(-10f)][SocketState(false, false)][FriendlyName("Min Z", "Minimum allowable float value.")] float MinZ, [DefaultValue(10f)][SocketState(false, false)][FriendlyName("Max Z", "Maximum allowable float value.")] float MaxZ, [SocketState(false, false)][FriendlyName("Preserve X", "If checked, the existing value will be passed into the new position, overriding the random value for this axis.")] bool PreserveX_Axis, [FriendlyName("Preserve Y", "If checked, the existing value will be passed into the new position, overriding the random value for this axis.")][SocketState(false, false)] bool PreserveY_Axis, [SocketState(false, false)][FriendlyName("Preserve Z", "If checked, the existing value will be passed into the new position, overriding the random value for this axis.")] bool PreserveZ_Axis, [DefaultValue(false)][SocketState(false, false)][FriendlyName("As Offset", "This will use the target GameObject's current position as the origin point (Origin is not used when this property is checked).")] bool AsOffset)
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
		foreach (GameObject gameObject in Target)
		{
			if (gameObject != null)
			{
				float x = ((!PreserveX_Axis) ? Random.Range(MinX, MaxX) : gameObject.transform.position.x);
				float y = ((!PreserveY_Axis) ? Random.Range(MinY, MaxY) : gameObject.transform.position.y);
				float z = ((!PreserveZ_Axis) ? Random.Range(MinZ, MaxZ) : gameObject.transform.position.z);
				Vector3 vector = new Vector3(x, y, z);
				Vector3 vector2 = Vector3.zero;
				if (AsOffset)
				{
					vector2 = gameObject.transform.position;
				}
				gameObject.transform.position = vector2 + vector;
			}
		}
	}
}
