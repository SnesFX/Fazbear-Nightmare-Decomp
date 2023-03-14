using System;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Add_Float")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Adds two float variables together and returns the result.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Math/Float")]
[FriendlyName("Add Float", "Adds float variables together and returns the result.\n\n[ A + B ]\n\nIf more than one variable is connected to A, they will be added together before being added to B.\n\nIf more than one variable is connected to B, they will be added together before being added to A.")]
public class uScriptAct_AddFloat : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "The first variable or variable list.")] float[] A, [FriendlyName("B", "The second variable or variable list.")] float[] B, [FriendlyName("Result", "The floating-point result of the operation.")] out float FloatResult, [SocketState(false, false)][FriendlyName("Int Result", "The integer result of the operation.")] out int IntResult)
	{
		float num = 0f;
		float num2 = 0f;
		foreach (float num3 in A)
		{
			num += num3;
		}
		foreach (float num4 in B)
		{
			num2 += num4;
		}
		FloatResult = num + num2;
		IntResult = Convert.ToInt32(FloatResult);
	}
}
