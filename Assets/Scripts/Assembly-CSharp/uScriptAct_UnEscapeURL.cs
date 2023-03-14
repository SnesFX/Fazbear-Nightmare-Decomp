using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("UnEscape String", "Decodes string from a URL-friendly format.")]
[NodePath("Actions/Web/String")]
[NodeToolTip("Decodes string from a URL-friendly format.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#UnEscapeString")]
public class uScriptAct_UnEscapeURL : uScriptLogic
{
	[FriendlyName("Out")]
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The URL-escaped string to be converted.")] string Target, [FriendlyName("Result", "A new string with all occurrences of %xx replaced with the corresponding character.")] out string Result)
	{
		if (Target == null)
		{
			Result = string.Empty;
		}
		else
		{
			Result = WWW.UnEscapeURL(Target);
		}
	}
}
