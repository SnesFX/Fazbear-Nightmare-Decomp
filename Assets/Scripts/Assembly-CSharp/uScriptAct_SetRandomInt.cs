using UnityEngine;

[NodeToolTip("Randomly sets the value of a Int variable.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Int")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Random_Int")]
[FriendlyName("Set Random Int", "Randomly sets the value of a Int variable.")]
public class uScriptAct_SetRandomInt : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([SocketState(false, false)][FriendlyName("Min", "Minimum allowable integer value.")] int Min, [FriendlyName("Max", "Maximum allowable integer value. This value is inclusive and may be returned.")][SocketState(false, false)] int Max, [FriendlyName("Target Int", "The integer value that gets set.")] out int TargetInt)
	{
		if (Min > Max)
		{
			Min = Max;
		}
		TargetInt = Random.Range(Min, Max + 1);
	}
}
