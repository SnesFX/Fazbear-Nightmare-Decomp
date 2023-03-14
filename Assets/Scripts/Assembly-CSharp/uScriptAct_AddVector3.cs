using UnityEngine;

[NodePath("Actions/Math/Vectors")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Add Vector3", "Adds Vector3 variables together and returns the result.\n\n[ A + B ]\n\nIf more than one variable is connected to A, they will be added together before being added to B.\n\nIf more than one variable is connected to B, they will be added together before being added to A.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Add_Vector3")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Adds two Vector3 variables together and returns the result.")]
public class uScriptAct_AddVector3 : uScriptLogic
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
		Vector3 vector = new Vector3(0f, 0f, 0f);
		Vector3 vector2 = new Vector3(0f, 0f, 0f);
		foreach (Vector3 vector3 in A)
		{
			vector += vector3;
		}
		foreach (Vector3 vector4 in B)
		{
			vector2 += vector4;
		}
		Result = vector + vector2;
	}
}
