using UnityEngine;

[NodePath("Actions/Math/Vectors")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Subtracts two Vector4 variables and returns the result.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Subtract_Vector4")]
[FriendlyName("Subtract Vector4", "Subtracts two Vector4 variables and returns the result.")]
public class uScriptAct_SubtractVector4 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "The Vector4 to subtract from. If more than one Vector4 variable is connected to A, they will be subtracted from (0, 0, 0, 0) before B is subtracted from them.")] Vector4[] A, [FriendlyName("B", "The Vector4 to subtract from A. If more than one Vector4 variable is connected to B, they will be subtracted from (0, 0, 0, 0) before being subtracted from A.")] Vector4[] B, [FriendlyName("Result", "The Vector4 result of the subtraction operation.")] out Vector4 Result)
	{
		Vector4 vector = new Vector4(0f, 0f, 0f, 0f);
		Vector4 vector2 = new Vector4(0f, 0f, 0f, 0f);
		foreach (Vector4 vector3 in A)
		{
			vector -= vector3;
		}
		foreach (Vector4 vector4 in B)
		{
			vector2 -= vector4;
		}
		Result = vector - vector2;
	}
}
