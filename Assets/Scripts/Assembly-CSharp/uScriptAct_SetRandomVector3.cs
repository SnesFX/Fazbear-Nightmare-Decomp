using UnityEngine;

[FriendlyName("Set Random Vector3", "Randomly sets the value of a Vector3 variable.")]
[NodeToolTip("Randomly sets the value of a Vector3 variable.")]
[NodePath("Actions/Variables/Vector3")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Random_Vector3")]
public class uScriptAct_SetRandomVector3 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([SocketState(false, false)][FriendlyName("Min X", "Minimum allowable float value for X.")][DefaultValue(0f)] float MinX, [FriendlyName("Max X", "Maximum allowable float value for X.")][DefaultValue(1f)][SocketState(false, false)] float MaxX, [DefaultValue(0f)][SocketState(false, false)][FriendlyName("Min Y", "Minimum allowable float value for Y.")] float MinY, [FriendlyName("Max Y", "Maximum allowable float value for Y.")][DefaultValue(1f)][SocketState(false, false)] float MaxY, [DefaultValue(0f)][SocketState(false, false)][FriendlyName("Min Z", "Minimum allowable float value for Z.")] float MinZ, [DefaultValue(1f)][SocketState(false, false)][FriendlyName("Max Z", "Maximum allowable float value for Z.")] float MaxZ, [FriendlyName("Target Vector3", "The Vector3 variable that gets set.")] out Vector3 TargetVector3)
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
		float x = Random.Range(MinX, MaxX);
		float y = Random.Range(MinY, MaxY);
		float z = Random.Range(MinZ, MaxZ);
		TargetVector3 = new Vector3(x, y, z);
	}
}
