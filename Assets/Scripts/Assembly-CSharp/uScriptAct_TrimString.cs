[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Trim String", "Trims characters from the begining and/or end of a string.  If no characters are provided, the node will trim whitespace by default.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Variables/String")]
[NodeToolTip("Trims characters from the begining and end of a string.")]
public class uScriptAct_TrimString : uScriptLogic
{
	public enum TrimType
	{
		Both = 0,
		Left = 1,
		Right = 2
	}

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The target string to be trimmed.")] string Target, [SocketState(false, false)][FriendlyName("Trim Type", "Specify the side of the string that will be trimmed.")] TrimType trimType, [SocketState(false, false)][FriendlyName("Characters", "(optional) Specify the characters to trim. If none are provided, whitespace will be trimmed by default.")] string trimChars, [FriendlyName("Result", "Resulting trimmed string.")] out string Result)
	{
		char[] trimChars2;
		if (trimChars == string.Empty)
		{
			string text = " ";
			trimChars2 = text.ToCharArray();
		}
		else
		{
			trimChars2 = trimChars.ToCharArray();
		}
		switch (trimType)
		{
		case TrimType.Both:
			Result = Target.Trim(trimChars2);
			break;
		case TrimType.Left:
			Result = Target.TrimStart(trimChars2);
			break;
		default:
			Result = Target.TrimEnd(trimChars2);
			break;
		}
	}
}
