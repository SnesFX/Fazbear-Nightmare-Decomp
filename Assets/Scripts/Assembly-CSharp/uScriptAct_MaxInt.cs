using UnityEngine;

[FriendlyName("Max Int", "Returns the value of the largest integer variable.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Max_Int")]
[NodePath("Actions/Math/Int")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Returns the value of the largest integer variable.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_MaxInt : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Values", "The variables to compare.")] int[] Values, [FriendlyName("Result", "Largest value passed in. If no variables are passed in, -2147483648 will be returned.")] out int Result)
	{
		Result = int.MinValue;
		foreach (int b in Values)
		{
			Result = Mathf.Max(Result, b);
		}
	}
}
