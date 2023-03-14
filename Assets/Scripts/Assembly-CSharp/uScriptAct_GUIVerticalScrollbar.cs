using UnityEngine;

[NodeToolTip("Shows a GUIVerticalScrollbar on the screen.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Label")]
[FriendlyName("GUI Vertical Scrollbar", "Shows a GUIVerticalScrollbar on the screen.")]
[NodePath("Actions/GUI/Controls")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_GUIVerticalScrollbar : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Position", "The position and size of the label.")] Rect Position, [FriendlyName("Value", "The position between min and max.")] float Value, [SocketState(false, false)][FriendlyName("Size", "How much can we see?")] float Size, [SocketState(false, false)][DefaultValue(0)][FriendlyName("Top Value", "The value at the top of the scrollbar.")] float topValue, [FriendlyName("Bottom Value", "The value at the bottom of the scrollbar.")][DefaultValue(1)][SocketState(false, false)] float bottomValue, [DefaultValue("")][SocketState(false, false)][FriendlyName("GUI Style", "The name of a custom GUI style to use when displaying this label.")] string guiStyle)
	{
		if (string.IsNullOrEmpty(guiStyle))
		{
			GUI.VerticalScrollbar(Position, Value, Size, topValue, bottomValue);
		}
		else
		{
			GUI.VerticalScrollbar(Position, Value, Size, topValue, bottomValue, GUI.skin.GetStyle(guiStyle));
		}
	}
}
