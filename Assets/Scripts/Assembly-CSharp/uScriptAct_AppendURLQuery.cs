using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#WebAppendURLQuery")]
[FriendlyName("Append URL Query", "Add a simple field/value pair to the URL query.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Web/String")]
[NodeToolTip("Add a simple field/value pair to the URL query.")]
public class uScriptAct_AppendURLQuery : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("URL", "The original URL string.")] string URL, [FriendlyName("Field Name", "The field name.")] string Field, [FriendlyName("Value", "The field value. Non-string objects will be convertd to a string using ToString().")] object Value, [FriendlyName("Escape Value", "If True, the value string will be escaped before it is appended to the URL.")][DefaultValue(true)][SocketState(false, false)] bool EscapeValue, [FriendlyName("Semicolon Separator", "If True, the semicolon character, ';' will be used as the pair separator instead of the ampersand, '&'.  W3C recommends that all web servers support semicolon separators in the place of ampersand separators, but many servers do not.")][SocketState(false, false)] bool UseSemicolon, [FriendlyName("Result", "The resulting URL after the field has been appended.")] out string Result)
	{
		if (URL == null)
		{
			URL = string.Empty;
		}
		if (!URL.Contains("?"))
		{
			URL += "?";
		}
		if (!URL.EndsWith("?"))
		{
			URL += ((!UseSemicolon) ? "&" : ";");
		}
		string text = ((!EscapeValue) ? Value.ToString() : WWW.EscapeURL(Value.ToString()));
		URL += ((!(text == string.Empty)) ? (Field + "=" + text) : Field);
		Result = URL;
	}
}
