[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Pad String", "Pads a string to reach the specified width.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Pads a string to reach the specified width.")]
[NodePath("Actions/Variables/String")]
public class uScriptAct_PadString : uScriptLogic
{
	public enum PadSide
	{
		Left = 0,
		Right = 1
	}

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The target string to be padded.")] string Target, [FriendlyName("Side", "Which side of the string to pad.")][SocketState(false, false)] PadSide padSide, [SocketState(false, false)][FriendlyName("Width", "Specifies the total width of the Result string after padding. If the width specified is smaller thatn the Target string's current width, the original string is returned instead.")] int TotalWidth, [SocketState(false, false)][FriendlyName("Pad Character", "(optional) Specify the character to use for padding. If none is provided, whitespace will be used by default. Note: If more than one character is provided in the string, only the first character will be used for padding.")] string padCharString, [FriendlyName("Result", "Resulting padded string.")] out string Result)
	{
		char paddingChar;
		if (padCharString == string.Empty)
		{
			string text = " ";
			char[] array = text.ToCharArray();
			paddingChar = array[0];
		}
		else
		{
			char[] array = padCharString.ToCharArray();
			paddingChar = array[0];
		}
		if (padSide == PadSide.Left)
		{
			Result = Target.PadLeft(TotalWidth, paddingChar);
		}
		else
		{
			Result = Target.PadRight(TotalWidth, paddingChar);
		}
	}
}
