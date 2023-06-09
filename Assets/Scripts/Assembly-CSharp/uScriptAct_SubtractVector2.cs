using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Subtracts two Vector2 variables and returns the result.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Subtract_Vector2")]
[FriendlyName("Subtract Vector2", "Subtracts two Vector2 variables and returns the result.")]
[NodePath("Actions/Math/Vectors")]
public class uScriptAct_SubtractVector2 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "The Vector2 to subtract from.")] Vector2 A, [FriendlyName("B", "The Vector2 to subtract from A.")] Vector2 B, [FriendlyName("Result", "The Vector2 result of the subtraction operation.")] out Vector2 Result)
	{
		Result = A - B;
	}
}
