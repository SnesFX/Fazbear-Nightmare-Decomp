using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Random_Float")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Randomly sets the value of a Float variable.")]
[NodePath("Actions/Variables/Float")]
[FriendlyName("Set Random Float", "Randomly sets the value of a Float variable.")]
public class uScriptAct_SetRandomFloat : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Min", "Minimum allowable float value.")][SocketState(false, false)] float Min, [SocketState(false, false)][FriendlyName("Max", "Maximum allowable float value.")] float Max, [FriendlyName("Target Float", "The random float value that gets set.")] out float TargetFloat)
	{
		if (Min > Max)
		{
			Min = Max;
		}
		TargetFloat = Random.Range(Min, Max);
	}
}
