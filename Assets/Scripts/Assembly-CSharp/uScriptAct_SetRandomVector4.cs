using UnityEngine;

[NodeToolTip("Randomly sets the value of a Vector4 variable.")]
[NodePath("Actions/Variables/Vector4")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Set Random Vector4", "Randomly sets the value of a Vector4 variable.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Random_Vector4")]
public class uScriptAct_SetRandomVector4 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([DefaultValue(0f)][SocketState(false, false)][FriendlyName("Min X", "Minimum allowable float value for X.")] float MinX, [SocketState(false, false)][DefaultValue(1f)][FriendlyName("Max X", "Maximum allowable float value for X.")] float MaxX, [SocketState(false, false)][DefaultValue(0f)][FriendlyName("Min Y", "Minimum allowable float value for Y.")] float MinY, [SocketState(false, false)][DefaultValue(1f)][FriendlyName("Max Y", "Maximum allowable float value for Y.")] float MaxY, [SocketState(false, false)][FriendlyName("Min Z", "Minimum allowable float value for Z.")][DefaultValue(0f)] float MinZ, [SocketState(false, false)][DefaultValue(1f)][FriendlyName("Max Z", "Maximum allowable float value for Z.")] float MaxZ, [DefaultValue(0f)][SocketState(false, false)][FriendlyName("Min W", "Minimum allowable float value for W.")] float MinW, [FriendlyName("Max W", "Maximum allowable float value for W.")][DefaultValue(1f)][SocketState(false, false)] float MaxW, [FriendlyName("Target Vector4", "The Vector4 variable that gets set.")] out Vector4 TargetVector4)
	{
		if (MinX > MaxX)
		{
			MinX = MaxX;
		}
		if (MinY > MaxY)
		{
			MinY = MaxY;
		}
		if (MinZ > MaxZ)
		{
			MinZ = MaxZ;
		}
		if (MinW > MaxW)
		{
			MinW = MaxW;
		}
		float x = Random.Range(MinX, MaxX);
		float y = Random.Range(MinY, MaxY);
		float z = Random.Range(MinZ, MaxZ);
		float w = Random.Range(MinW, MaxW);
		TargetVector4 = new Vector4(x, y, z, w);
	}
}
