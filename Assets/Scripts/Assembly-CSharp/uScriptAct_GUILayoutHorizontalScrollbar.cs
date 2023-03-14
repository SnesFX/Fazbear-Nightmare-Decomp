using UnityEngine;

[NodePath("Actions/GUI/Controls")]
[NodeToolTip("Shows a horizontal scrollbar that the user can drag.")]
[NodeCopyright("Copyright 2013 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("GUILayout Horizontal Scrollbar", "Shows a horizontal scrollbar that the user can drag.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUILayout_HorizontalScrollbar")]
public class uScriptAct_GUILayoutHorizontalScrollbar : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The position of the draggable thumb, which can be changed by the user.")] ref float Value, [DefaultValue(0f)][FriendlyName("Left Value", "The value at the left end of the scrollbar.")][SocketState(false, false)] float LeftValue, [SocketState(false, false)][DefaultValue(10f)][FriendlyName("Right Value", "The value at the right end of the scrollbar.")] float RightValue, [FriendlyName("Thumb Size", "The size of the thumb relative to the scrollbar.")][DefaultValue(1f)][SocketState(false, false)] float ThumbSize, [SocketState(false, false)][FriendlyName("Style", "The style to use for the scrollbar background. If left out, the \"horizontalscrollbar\" style from the current GUISkin is used.")][DefaultValue("")] string Style, [FriendlyName("Options", "An optional list of layout parameters.  Any values passed in here will override settings defined by the style.")][SocketState(false, false)] GUILayoutOption[] Options)
	{
		GUIStyle style = ((!string.IsNullOrEmpty(Style)) ? GUI.skin.GetStyle(Style) : GUI.skin.horizontalScrollbar);
		Value = GUILayout.HorizontalScrollbar(Value, ThumbSize, LeftValue, RightValue, style, Options);
	}
}
