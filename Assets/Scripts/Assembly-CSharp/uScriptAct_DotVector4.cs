using UnityEngine;

[NodeToolTip("Dot product of two Vector4 variables.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Dot_Vector4")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Math/Vectors")]
[FriendlyName("Dot (Vector4)", "Dot Product of two Vector4 variables.\n\n[ Dot(A,B) ]")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_DotVector4 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "The first variable.")] Vector4 A, [FriendlyName("B", "The second variable.")] Vector4 B, [FriendlyName("Result", "The float result of the operation.")] out float Result)
	{
		Result = Vector4.Dot(A, B);
	}
}
