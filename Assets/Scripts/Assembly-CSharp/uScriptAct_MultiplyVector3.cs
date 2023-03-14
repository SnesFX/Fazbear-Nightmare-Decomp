using UnityEngine;

[NodePath("Actions/Math/Vectors")]
[NodeToolTip("Multiplies two Vector3 variables together and returns the result.")]
[FriendlyName("Multiply Vector3", "Multiplies Vector3 variables together and returns the result.\n\n[ A + B ]\n\nIf more than one variable is connected to A, they will be multiplied together before being multiplied to B.\n\nIf more than one variable is connected to B, they will be multiplied together before being multiplied to A.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Multiply_Vector3")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_MultiplyVector3 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "The first variable or variable list.")] Vector3[] A, [FriendlyName("B", "The second variable or variable list.")] Vector3[] B, [FriendlyName("Result", "The Vector3 result of the operation.")] out Vector3 Result)
	{
		Vector3 vector = A[0];
		Vector3 vector2 = B[0];
		for (int i = 1; i < A.Length; i++)
		{
			vector.x *= A[i].x;
			vector.y *= A[i].y;
			vector.z *= A[i].z;
		}
		for (int j = 1; j < B.Length; j++)
		{
			vector2.x *= B[j].x;
			vector2.y *= B[j].y;
			vector2.z *= B[j].z;
		}
		Result.x = vector.x * vector2.x;
		Result.y = vector.y * vector2.y;
		Result.z = vector.z * vector2.z;
	}
}
