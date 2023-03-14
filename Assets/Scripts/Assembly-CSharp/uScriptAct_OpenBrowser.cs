using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Open Browser", "Opens the user's default web browser to the specified URL.")]
[NodePath("Actions/Application")]
[NodeToolTip("Opens the user's default web browser to the specified URL.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
public class uScriptAct_OpenBrowser : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("URL", "The web address you wish to open. Example: http://www.yourdomain.com/subdirectory/somepage.html")][DefaultValue("")] string URL)
	{
		Application.OpenURL(URL);
	}
}
