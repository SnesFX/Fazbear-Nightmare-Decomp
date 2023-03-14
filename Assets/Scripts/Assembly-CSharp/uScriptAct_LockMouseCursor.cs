using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Show_Mouse_Cursor")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Locks and hides (or unocks and shows) the mouse cursor.")]
[FriendlyName("Lock Mouse Cursor", "Locks and hides (or unocks and shows) the mouse cursor.")]
[NodePath("Actions/Screen")]
public class uScriptAct_LockMouseCursor : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Lock", "If true, the mouse cursor is locked and hidden, otherwise the mouse cursor is unlocked and shown.")][DefaultValue(false)] bool Lock)
	{
		Screen.lockCursor = Lock;
	}
}
