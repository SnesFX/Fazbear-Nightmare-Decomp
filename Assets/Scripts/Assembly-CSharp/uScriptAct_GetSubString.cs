[NodeToolTip("Returns part of the Target string as specified.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Variables/String")]
[FriendlyName("Get Sub-String", "Returns part of the Target string as specified. Note, if you supply values outside of a valid range, nothing will be returned in th new string.")]
public class uScriptAct_GetSubString : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The target string.")] string Target, [FriendlyName("Start Position", "The character position to start from. This value is zero-based, so the first character in the string is at position 0 (zero).")] int StartPos, [DefaultValue(0)][FriendlyName("Length", "(optional) The number of characters to include in the sub-string. If no length is given, the sub-string will return all characters from the Start Position to the end of the Target string.")][SocketState(false, false)] int StringLength, [FriendlyName("Result", "Resulting sub-string based on the Target string.")] out string Result)
	{
		if (string.Empty != Target)
		{
			bool flag = false;
			bool flag2 = false;
			int num = Target.Length - 1;
			if (StartPos < 0)
			{
				StartPos = 0;
			}
			if (StartPos > num)
			{
				StartPos = num;
			}
			if (StringLength > 0)
			{
				flag2 = true;
			}
			if (StringLength > Target.Length)
			{
				flag = true;
			}
			if (StartPos + StringLength > Target.Length)
			{
				flag = true;
			}
			if (!flag)
			{
				if (flag2)
				{
					Result = Target.Substring(StartPos, StringLength);
				}
				else
				{
					Result = Target.Substring(StartPos);
				}
			}
			else
			{
				Result = string.Empty;
			}
		}
		else
		{
			Result = string.Empty;
		}
	}
}
