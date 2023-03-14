[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Sets a string to the defined value.")]
[FriendlyName("Set String", "Sets a string to the defined value.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_String")]
[NodePath("Actions/Variables/String")]
public class uScriptAct_SetString : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The variable you wish to use to set the target's value.")] string Value, [SocketState(false, false)][FriendlyName("To Upper Case", "If True, the string set will be all upper case.")] bool ToUpperCase, [FriendlyName("To Lower Case", "If True, the string set will be all lower case.")][SocketState(false, false)] bool ToLowerCase, [FriendlyName("Trim Whitespace", "If True, the string's whitespace will be trimmed.")][SocketState(false, false)] bool TrimWhitespace, [FriendlyName("Target", "The Target variable you wish to set.")] out string Target)
	{
		string text = Value;
		if (ToLowerCase)
		{
			text = Value.ToLower();
		}
		else if (ToUpperCase)
		{
			text = Value.ToUpper();
		}
		if (TrimWhitespace)
		{
			text = text.Trim();
		}
		Target = text;
	}
}
