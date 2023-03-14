using System;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Subtract_Float")]
[NodePath("Actions/Math/Float")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Subtracts two float variables and returns the result.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Subtract Float", "Subtracts two float variables and returns the result.\n\n[ A - B ]")]
public class uScriptAct_SubtractFloat : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "The first variable.")] float A, [FriendlyName("B", "The second variable.")] float B, [FriendlyName("Result", "The floating-point result of the operation.")] out float FloatResult, [SocketState(false, false)][FriendlyName("Int Result", "The integer result of the operation.")] out int IntResult)
	{
		FloatResult = A - B;
		IntResult = Convert.ToInt32(FloatResult);
	}
}
