using UnityEngine;

[NodePath("Actions/GUI/Controls")]
[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUILayout_ScrollView")]
[FriendlyName("GUILayout Begin ScrollView", "Begin a scrollview control group using Unity's automatic layout system.\n\nAutomatically laid out scrollviews will take whatever content you have inside them and display normally. If it doesn't fit, scrollbars will appear.\n\nNOTE: The group must be closed using a \"GUILayout End ScrollView\" node.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Begin a scrollview control group using Unity's automatic layout system.")]
public class uScriptAct_GUILayoutBeginScrollView : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([SocketState(false, false)][FriendlyName("Scroll Position", "The position to use display.")] ref Vector2 ScrollPosition, [FriendlyName("Always Show Horizontal", "If False, the scrollbar is only shown when the content inside the ScrollView is wider than the scrollview itself.")][SocketState(false, false)] bool AlwaysShowHorizontal, [FriendlyName("Always Show Vertical", "If false, the scrollbar is only shown when content inside the ScrollView is taller than the scrollview itself.")][SocketState(false, false)] bool AlwaysShowVertical, [FriendlyName("Style", "The style to use. If left out, the \"scrollview\" style from the current GUISkin is used.")][DefaultValue("")][SocketState(false, false)] string Style, [DefaultValue("")][SocketState(false, false)][FriendlyName("Horizontal Scrollbar Style", "The style to use. If left out, the \"horizontalscrollbar\" style from the current GUISkin is used.")] string HorizontalScrollbarStyle, [DefaultValue("")][SocketState(false, false)][FriendlyName("Vertical Scrollbar Style", "The style to use. If left out, the \"verticalscrollbar\" style from the current GUISkin is used.")] string VerticalScrollbarStyle, [FriendlyName("Options", "An optional list of layout parameters.  Any values passed in here will override settings defined by the style.")][SocketState(false, false)] GUILayoutOption[] Options)
	{
		GUIStyle background = ((!string.IsNullOrEmpty(Style)) ? GUI.skin.GetStyle(Style) : GUI.skin.scrollView);
		GUIStyle horizontalScrollbar = ((!string.IsNullOrEmpty(Style)) ? GUI.skin.GetStyle(HorizontalScrollbarStyle) : GUI.skin.horizontalScrollbar);
		GUIStyle verticalScrollbar = ((!string.IsNullOrEmpty(Style)) ? GUI.skin.GetStyle(VerticalScrollbarStyle) : GUI.skin.verticalScrollbar);
		ScrollPosition = GUILayout.BeginScrollView(ScrollPosition, AlwaysShowHorizontal, AlwaysShowVertical, horizontalScrollbar, verticalScrollbar, background, Options);
	}
}
