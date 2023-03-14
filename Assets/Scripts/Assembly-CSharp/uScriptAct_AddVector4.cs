using UnityEngine;

[NodeToolTip("Adds two Vector4 variables together and returns the result.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Add_Vector4")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Math/Vectors")]
[FriendlyName("Add Vector4", "Adds Vector4 variables together and returns the result.\n\n[ A + B ]\n\nIf more than one variable is connected to A, they will be added together before being added to B.\n\nIf more than one variable is connected to B, they will be added together before being added to A.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_AddVector4 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "The first variable or variable list.")] Vector4[] A, [FriendlyName("B", "The second variable or variable list.")] Vector4[] B, [FriendlyName("Result", "The Vector4 result of the operation.")] out Vector4 Result)
	{
		Vector4 vector = new Vector4(0f, 0f, 0f, 0f);
		Vector4 vector2 = new Vector4(0f, 0f, 0f, 0f);
		foreach (Vector4 vector3 in A)
		{
			vector += vector3;
		}
		foreach (Vector4 vector4 in B)
		{
			vector2 += vector4;
		}
		Result = vector + vector2;
	}
}
