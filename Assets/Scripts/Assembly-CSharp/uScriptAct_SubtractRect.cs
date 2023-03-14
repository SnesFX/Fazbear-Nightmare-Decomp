using UnityEngine;

[NodeToolTip("Subtracts two Rect variables together and returns the result.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Subtract_Rect")]
[FriendlyName("Subtract Rect", "Subtracts Rect variables and returns the result.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Math/Rect")]
public class uScriptAct_SubtractRect : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "The first Rect.  If more than one Rect variable is connected to A, they will be subtracted from each other before B will be subtracted from them.")] Rect[] A, [FriendlyName("B", "The second Rect.  If more than one Rect variable is connected to B, they will be subtracted from each other before being subtracted from A.")] Rect[] B, [FriendlyName("Result", "The Rect result of the subtractition operation.")] out Rect Result)
	{
		Rect rect = ((A.Length <= 0) ? new Rect(0f, 0f, 0f, 0f) : A[0]);
		Rect rect2 = ((B.Length <= 0) ? new Rect(0f, 0f, 0f, 0f) : B[0]);
		for (int i = 1; i < A.Length; i++)
		{
			rect.xMin -= A[i].xMin;
			rect.xMax -= A[i].xMax;
			rect.yMin -= A[i].yMin;
			rect.yMax -= A[i].yMax;
		}
		for (int j = 1; j < B.Length; j++)
		{
			rect2.xMin -= B[j].xMin;
			rect2.xMax -= B[j].xMax;
			rect2.yMin -= B[j].yMin;
			rect2.yMax -= B[j].yMax;
		}
		Result = rect;
		Result.xMin -= rect2.xMin;
		Result.xMax -= rect2.xMax;
		Result.yMin -= rect2.yMin;
		Result.yMax -= rect2.yMax;
	}
}
