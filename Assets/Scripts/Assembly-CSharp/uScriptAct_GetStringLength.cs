using System;

[NodePath("Actions/Variables/String")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Returns the number of characters in a string as a float, integer, and string.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_String_Length")]
[FriendlyName("Get String Length", "Returns the number of characters in a string as a float, integer, and string.")]
public class uScriptAct_GetStringLength : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The Target string to get the length of.")] string Target, [FriendlyName("Result", "The length of the Target string, expressed as an integer.")] out int IntValue, [SocketState(false, false)][FriendlyName("Float Result", "The length of the Target string, expressed as a floating-point number.")] out float FloatValue, [SocketState(false, false)][FriendlyName("String Result", "The length of the Target string, expressed as a string.")] out string StringValue)
	{
		int num;
		float num2;
		string text;
		if (!string.IsNullOrEmpty(Target))
		{
			num = Target.Length;
			num2 = Convert.ToSingle(Target.Length);
			text = Target.Length.ToString();
		}
		else
		{
			num = 0;
			num2 = 0f;
			text = "0";
		}
		IntValue = num;
		FloatValue = num2;
		StringValue = text;
	}
}
