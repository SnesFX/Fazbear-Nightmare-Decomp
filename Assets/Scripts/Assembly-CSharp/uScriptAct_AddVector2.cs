using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Add_Vector3")]
[FriendlyName("Add Vector2", "Adds Vector2 variables together and returns the result.\n\n[ A + B ]\n\nIf more than one variable is connected to A, they will be added together before being added to B.\n\nIf more than one variable is connected to B, they will be added together before being added to A.")]
[NodePath("Actions/Math/Vectors")]
[NodeToolTip("Adds two or more Vector2 variables together and returns the result.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_AddVector2 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "The first variable or variable list.")] Vector2[] A, [FriendlyName("B", "The second variable or variable list.")] Vector2[] B, [FriendlyName("Result", "The Vector2 result of the operation.")] out Vector2 Result)
	{
		Vector2 vector = new Vector2(0f, 0f);
		Vector2 vector2 = new Vector2(0f, 0f);
		foreach (Vector2 vector3 in A)
		{
			vector += vector3;
		}
		foreach (Vector2 vector4 in B)
		{
			vector2 += vector4;
		}
		Result = vector + vector2;
	}
}
