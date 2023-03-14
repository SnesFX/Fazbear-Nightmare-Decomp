using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Dot_Vector3")]
[FriendlyName("Dot (Vector3)", "Dot Product of two Vector3 variables.\n\n[ Dot(A,B) ]")]
[NodePath("Actions/Math/Vectors")]
[NodeToolTip("Dot product of two Vector3 variables.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_DotVector3 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "The first variable.")] Vector3 A, [FriendlyName("B", "The second variable.")] Vector3 B, [FriendlyName("Result", "The float result of the operation.")] out float Result)
	{
		Result = Vector3.Dot(A, B);
	}
}
