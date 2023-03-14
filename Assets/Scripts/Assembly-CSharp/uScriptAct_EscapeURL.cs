using UnityEngine;

[NodePath("Actions/Web/String")]
[NodeToolTip("Encodes string into a URL-friendly format.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Escape String", "Encodes string into a URL-friendly format by replacing illegal characters in the Target string with the correct URL-escaped code. Useful when building web page parameters.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#EscapeString")]
public class uScriptAct_EscapeURL : uScriptLogic
{
	[FriendlyName("Out")]
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The string to be escaped.")] string Target, [FriendlyName("Result", "A new string with all illegal characters replaced with %xx where xx is the hexadecimal code for the character code.")] out string Result)
	{
		if (Target == null)
		{
			Result = string.Empty;
		}
		else
		{
			Result = WWW.EscapeURL(Target);
		}
	}
}
