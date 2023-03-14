using UnityEngine;

[FriendlyName("GUI Begin ScrollView", "When you begin a group, the coordinate system for GUI controls are set so (0,0) is the top-left corner of the group.  All controls are clipped to the group.  Groups can be nested - if they are, children are clipped to their parents.\n\nNOTE: Each use of those node. must be matched with a call to \"GUI End ScrollView\".")]
[NodePath("Actions/GUI/Controls")]
[NodeToolTip("Begins a GUI group view which can be scrolled.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Begin_Scroll_View")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_GUIBeginScrollView : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Position", "Rectangle on the screen to use for the ScrollView.")] Rect Position, [FriendlyName("ViewRect", "The rectangle used inside the scrollview.")] Rect ViewRect, [FriendlyName("Scroll Position", "The position to use display.")] Vector2 startingScrollPosition, [SocketState(false, false)][FriendlyName("Always Show Horizontal", "Always show horizontal scrollbar regardless if it is required.")] bool alwaysShowHorizontal, [FriendlyName("Always Show Vertical", "Always show vertical scrollbar regardless if it is required.")][SocketState(false, false)] bool alwaysShowVertical, [DefaultValue("")][FriendlyName("Horizontal Style", "GUI Style for the horizontal scroll bar.")][SocketState(false, false)] string horizontalStyle, [DefaultValue("")][SocketState(false, false)][FriendlyName("Vertical Style", "GUI Style for the vertical scroll bar.")] string verticalStyle, [SocketState(false, false)][FriendlyName("Scroll Position", "The new position of the scroll bar.")] out Vector2 scrollPosition)
	{
		if (string.IsNullOrEmpty(horizontalStyle) && string.IsNullOrEmpty(verticalStyle))
		{
			scrollPosition = GUI.BeginScrollView(Position, startingScrollPosition, ViewRect, alwaysShowHorizontal, alwaysShowVertical);
		}
		else
		{
			scrollPosition = GUI.BeginScrollView(Position, startingScrollPosition, ViewRect, alwaysShowHorizontal, alwaysShowVertical, GUI.skin.GetStyle(horizontalStyle), GUI.skin.GetStyle(verticalStyle));
		}
	}
}
