using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Min_Float")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Returns the value of the smallest float variable.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Min Float", "Returns the value of the smallest float variable.")]
[NodePath("Actions/Math/Float")]
public class uScriptAct_MinFloat : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Values", "The variables to compare.")] float[] Values, [FriendlyName("Result", "Smallest value passed in. If no variables are passed in, 3.402823E+38 will be returned.")] out float Result)
	{
		Result = float.MaxValue;
		foreach (float b in Values)
		{
			Result = Mathf.Min(Result, b);
		}
	}
}
