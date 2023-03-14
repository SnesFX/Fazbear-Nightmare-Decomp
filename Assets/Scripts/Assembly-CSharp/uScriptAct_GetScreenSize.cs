using UnityEngine;

[NodeToolTip("Gets the current size informaiton for the screen.")]
[NodePath("Actions/Screen")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Get Screen Size", "Gets the current size informaiton for the screen.")]
public class uScriptAct_GetScreenSize : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Width", "Returns the width of the screen size in pixels as an integer.")] out int ScreenWidth, [FriendlyName("Height", "Returns the height of the screen size in pixels as an integer.")] out int ScreenHeight, [FriendlyName("Float Width", "Returns the width of the screen size in pixels as a float.")] out float fScreenWidth, [FriendlyName("Float Height", "Returns the height of the screen size in pixels as a float.")] out float fScreenHeight, [SocketState(false, false)][FriendlyName("Screen Rect", "Returns the screen size as a Rect variable.")] out Rect ScreenRect, [FriendlyName("Screen Center", "The center of the screen as a Vector2.")][SocketState(false, false)] out Vector2 ScreenCenter)
	{
		Rect rect = new Rect(0f, 0f, Screen.width, Screen.height);
		Vector2 vector = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
		ScreenWidth = Screen.width;
		ScreenHeight = Screen.height;
		fScreenWidth = Screen.width;
		fScreenHeight = Screen.height;
		ScreenRect = rect;
		ScreenCenter = vector;
	}
}
