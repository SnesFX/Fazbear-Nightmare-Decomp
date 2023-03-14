[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Replace Characters", "Replaces characters in a string with the new ones specified.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Replaces characters in a string with the new ones specified.")]
[NodePath("Actions/Variables/String")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_ReplaceCharacters : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The target string.")] string Target, [FriendlyName("Old Chars", "The current characters in the string you wish to replace.")][SocketState(false, false)] string OldChars, [SocketState(false, false)][FriendlyName("New Chars", "The new characters you wish to use. If you leave this property empty/blank, the old characters will be deleted from the string.")] string NewChars, [FriendlyName("Result", "Resulting string with replaced characters.")] out string Result)
	{
		if (OldChars.Length > 0)
		{
			Result = Target.Replace(OldChars, NewChars);
		}
		else
		{
			Result = Target;
		}
	}
}
