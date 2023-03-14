using System;

[NodePath("Actions/Math/Int")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Subtract_Int")]
[FriendlyName("Subtract Int", "Subtracts two integer variables and returns the result.\n\n[ A - B ]")]
[NodeToolTip("Subtracts two integer variables and returns the result.")]
public class uScriptAct_SubtractInt : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "The first variable.")] int A, [FriendlyName("B", "The second variable.")] int B, [FriendlyName("Result", "The integer result of the operation.")] out int IntResult, [SocketState(false, false)][FriendlyName("Float Result", "The floating-point result of the operation.")] out float FloatResult)
	{
		IntResult = A - B;
		FloatResult = Convert.ToSingle(IntResult);
	}
}
