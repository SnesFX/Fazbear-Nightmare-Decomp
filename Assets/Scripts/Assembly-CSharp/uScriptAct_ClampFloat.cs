using System;
using UnityEngine;

[NodeToolTip("Clamps a float variable between a min and a max value and returns the result.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Clamp_Float")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Math/Float")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Clamp Float", "Clamps a float variable between a min and a max value and returns the result.")]
public class uScriptAct_ClampFloat : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The value to be clamped.")] float Target, [FriendlyName("Min", "The minimum value to clamp to.")] float Min, [FriendlyName("Max", "The maximum value to clamp to.")] float Max, [FriendlyName("Result", "Floating-point result of the clamp operation.")] out float FloatResult, [FriendlyName("Int Result", "Integer result of the clamp operation.")][SocketState(false, false)] out int IntResult)
	{
		FloatResult = Mathf.Clamp(Target, Min, Max);
		IntResult = Convert.ToInt32(FloatResult);
	}
}
