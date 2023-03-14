using UnityEngine;

[FriendlyName("Add Rect", "Adds Rect variables together and returns the result.\n\n[ A + B ]\n\nIf more than one variable is connected to A, they will be added together before being added to B.\n\nIf more than one variable is connected to B, they will be added together before being added to A.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Math/Rect")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Add_Rect")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Adds two Rect variables together and returns the result.")]
public class uScriptAct_AddRect : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "The first variable or variable list.")] Rect[] A, [FriendlyName("B", "The second variable or variable list.")] Rect[] B, [FriendlyName("Result", "The Rect result of the operation.")] out Rect Result)
	{
		Rect rect = new Rect(0f, 0f, 0f, 0f);
		for (int i = 0; i < A.Length; i++)
		{
			Rect rect2 = A[i];
			rect.xMin += rect2.xMin;
			rect.xMax += rect2.xMax;
			rect.yMin += rect2.yMin;
			rect.yMax += rect2.yMax;
		}
		for (int j = 0; j < B.Length; j++)
		{
			Rect rect3 = B[j];
			rect.xMin += rect3.xMin;
			rect.xMax += rect3.xMax;
			rect.yMin += rect3.yMin;
			rect.yMax += rect3.yMax;
		}
		Result = rect;
	}
}
