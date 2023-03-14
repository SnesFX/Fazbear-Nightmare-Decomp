using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Vector2")]
[NodeToolTip("Randomly sets the value of a Vector2 variable.")]
[FriendlyName("Set Random Vector2", "Randomly sets the value of a Vector2 variable.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Random_Vector2")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_SetRandomVector2 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([SocketState(false, false)][DefaultValue(0f)][FriendlyName("Min X", "Minimum allowable float value for X.")] float MinX, [SocketState(false, false)][FriendlyName("Max X", "Maximum allowable float value for X.")][DefaultValue(1f)] float MaxX, [DefaultValue(0f)][SocketState(false, false)][FriendlyName("Min Y", "Minimum allowable float value for Y.")] float MinY, [DefaultValue(1f)][SocketState(false, false)][FriendlyName("Max Y", "Maximum allowable float value for Y.")] float MaxY, [FriendlyName("Target Vector2", "The Vector2 variable that gets set.")] out Vector3 TargetVector2)
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
		float x = Random.Range(MinX, MaxX);
		float y = Random.Range(MinY, MaxY);
		TargetVector2 = new Vector2(x, y);
	}
}
