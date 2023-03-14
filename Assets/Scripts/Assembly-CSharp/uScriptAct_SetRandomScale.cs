using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/GameObjects/Movement")]
[NodeToolTip("Sets the scale of a GameObject.")]
[FriendlyName("Set Random Scale", "Randomly sets the scale of a GameObject.")]
public class uScriptAct_SetRandomScale : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The GameObject(s) that the random scale is applied to.")] GameObject[] Target, [FriendlyName("Min X", "Minimum allowable float value.")][SocketState(false, false)][DefaultValue(0.5f)] float MinX, [DefaultValue(2f)][SocketState(false, false)][FriendlyName("Max X", "Maximum allowable float value.")] float MaxX, [FriendlyName("Min Y", "Minimum allowable float value.")][DefaultValue(0.5f)][SocketState(false, false)] float MinY, [SocketState(false, false)][DefaultValue(2f)][FriendlyName("Max Y", "Maximum allowable float value.")] float MaxY, [SocketState(false, false)][DefaultValue(0.5f)][FriendlyName("Min Z", "Minimum allowable float value.")] float MinZ, [FriendlyName("Max Z", "Maximum allowable float value.")][DefaultValue(2f)][SocketState(false, false)] float MaxZ, [SocketState(false, false)][FriendlyName("Preserve X", "If checked, the existing value will be passed into the new scale, overriding the random value for that axis.")] bool PreserveX_Axis, [FriendlyName("Preserve Y", "If checked, the existing value will be passed into the new scale, overriding the random value for that axis.")][SocketState(false, false)] bool PreserveY_Axis, [SocketState(false, false)][FriendlyName("Preserve Z", "If checked, the existing value will be passed into the new scale, overriding the random value for that axis.")] bool PreserveZ_Axis, [FriendlyName("Uniform", "Should the node scale the GameObject uniformly on all three axis. When set to true, only the Min and Max for X is used to determine the random scale range. Also, the Preserve(X/Y/Z) flags are ignored.")][DefaultValue(true)] bool Uniform)
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
			if (Uniform)
			{
				PreserveX_Axis = false;
				PreserveY_Axis = false;
				PreserveZ_Axis = false;
			}
			if (gameObject != null)
			{
				float num = ((!PreserveX_Axis) ? Random.Range(MinX, MaxX) : gameObject.transform.localScale.x);
				float y = ((!PreserveY_Axis) ? Random.Range(MinY, MaxY) : gameObject.transform.localScale.y);
				float z = ((!PreserveZ_Axis) ? Random.Range(MinZ, MaxZ) : gameObject.transform.localScale.z);
				Vector3 localScale = ((!Uniform) ? new Vector3(num, y, z) : new Vector3(num, num, num));
				gameObject.transform.localScale = localScale;
			}
		}
	}
}
