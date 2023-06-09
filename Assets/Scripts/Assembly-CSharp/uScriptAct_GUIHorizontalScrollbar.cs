using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Label")]
[NodeToolTip("Shows a GUIHorizontalScrollbar on the screen.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("GUI Horizontal Scrollbar", "Shows a GUIHorizontalScrollbar on the screen.")]
[NodePath("Actions/GUI/Controls")]
public class uScriptAct_GUIHorizontalScrollbar : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Position", "The position and size of the GUI control.")] Rect Position, [FriendlyName("Value", "The position between min and max.")] float Value, [SocketState(false, false)][FriendlyName("Size", "How much can we see?")] float Size, [DefaultValue(0)][SocketState(false, false)][FriendlyName("Left Value", "The value at the left end of the scrollbar.")] float leftValue, [FriendlyName("Right Value", "The value at the right end of the scrollbar.")][DefaultValue(1)][SocketState(false, false)] float rightValue, [SocketState(false, false)][FriendlyName("GUI Style", "The name of a custom GUI style to use when displaying this label.")][DefaultValue("")] string guiStyle)
	{
		if (string.IsNullOrEmpty(guiStyle))
		{
			GUI.HorizontalScrollbar(Position, Value, Size, leftValue, rightValue);
		}
		else
		{
			GUI.HorizontalScrollbar(Position, Value, Size, leftValue, rightValue, GUI.skin.GetStyle(guiStyle));
		}
	}
}
