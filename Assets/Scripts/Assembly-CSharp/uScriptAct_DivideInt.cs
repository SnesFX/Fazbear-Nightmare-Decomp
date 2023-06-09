[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Divide_Int")]
[FriendlyName("Divide Int", "Divides two integer variables and returns the result.\n\n[ A / B ]")]
[NodePath("Actions/Math/Int")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Divides two integer variables and returns the result.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_DivideInt : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "The dividend.")] int A, [FriendlyName("B", "The divisor.  Must be a non-zero value.")] int B, [FriendlyName("Result", "The integer quotient or result of the operation.")] out int IntResult, [FriendlyName("Float Result", "The floating-point quotient or result of the operation.")][SocketState(false, false)] out float FloatResult)
	{
		if (B == 0)
		{
			uScriptDebug.Log("[Divide Int] You cannot divide by 0.  Returning 0 as the result.", uScriptDebug.Type.Error);
			FloatResult = 0f;
			IntResult = 0;
		}
		else
		{
			int num = A / B;
			IntResult = num;
			FloatResult = (float)A / (float)B;
		}
	}
}
