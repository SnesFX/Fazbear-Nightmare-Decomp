using UnityEngine;

[NodeToolTip("Returns the value of the smallest integer variable.")]
[NodePath("Actions/Math/Int")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Min Int", "Returns the value of the smallest integer variable.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Min_Int")]
public class uScriptAct_MinInt : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Values", "The variables to compare.")] int[] Values, [FriendlyName("Result", "Smallest value passed in. If no variables are passed in, 2147483647 will be returned.")] out int Result)
	{
		Result = int.MaxValue;
		foreach (int b in Values)
		{
			Result = Mathf.Min(Result, b);
		}
	}
}
