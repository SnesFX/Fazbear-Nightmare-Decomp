using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Clamps an integer variable between a min and a max value and returns the result.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Clamp_Int")]
[NodePath("Actions/Math/Int")]
[FriendlyName("Clamp Int", "Clamps an integer variable between a min and a max value and returns the result.")]
public class uScriptAct_ClampInt : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The value to be clamped.")] int Target, [FriendlyName("Min", "The minimum value to clamp to.")] int Min, [FriendlyName("Max", "The maximum value to clamp to.")] int Max, [FriendlyName("Result", "Integer result of the clamp operation.")] out int IntResult, [SocketState(false, false)][FriendlyName("Float Result", "Floating-point result of the clamp operation.")] out float FloatResult)
	{
		IntResult = Mathf.Clamp(Target, Min, Max);
		FloatResult = IntResult;
	}
}
