using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Show_Mouse_Cursor")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Show Mouse Cursor", "Shows or hides the mouse cursor.")]
[NodeToolTip("Shows or hides the mouse cursor.")]
[NodePath("Actions/Screen")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_ShowMouseCursor : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Show", "If true, the mouse cursor is shown, otherwise the mouse cursor is hidden.")] bool show)
	{
		Cursor.visible = show;
	}
}
