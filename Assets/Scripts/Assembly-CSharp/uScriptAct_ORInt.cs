[NodeToolTip("ORs two integer variables together and returns the result.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#OR_Int")]
[FriendlyName("OR Int", "ORs integer variables together and returns the result.\n\n[ A | B ]")]
[NodePath("Actions/Math/Int")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_ORInt : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "The first variable or variable list.")] int[] A, [FriendlyName("B", "The second variable or variable list.")] int[] B, [FriendlyName("Result", "The integer result of the operation.")] out int IntResult)
	{
		int num = 0;
		int num2 = 0;
		foreach (int num3 in A)
		{
			num |= num3;
		}
		foreach (int num4 in B)
		{
			num2 |= num4;
		}
		IntResult = num | num2;
	}
}
