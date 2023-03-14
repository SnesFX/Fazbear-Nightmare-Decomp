using UnityEngine;

[NodeCopyright("Copyright 2013 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUILayout_VerticalScrollbar")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Shows a vertical scrollbar that the user can drag.")]
[NodePath("Actions/GUI/Controls")]
[FriendlyName("GUILayout Vertical Scrollbar", "Shows a vertical scrollbar that the user can drag.")]
public class uScriptAct_GUILayoutVerticalScrollbar : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The position of the draggable thumb, which can be changed by the user.")] ref float Value, [DefaultValue(0f)][FriendlyName("Top Value", "The value at the top end of the scrollbar.")][SocketState(false, false)] float TopValue, [DefaultValue(10f)][SocketState(false, false)][FriendlyName("Bottom Value", "The value at the bottom end of the scrollbar.")] float BottomValue, [DefaultValue(1f)][SocketState(false, false)][FriendlyName("Thumb Size", "The size of the thumb relative to the scrollbar.")] float ThumbSize, [SocketState(false, false)][FriendlyName("Style", "The style to use for the scrollbar background. If left out, the \"verticalscrollbar\" style from the current GUISkin is used.")][DefaultValue("")] string Style, [SocketState(false, false)][FriendlyName("Options", "An optional list of layout parameters.  Any values passed in here will override settings defined by the style.")] GUILayoutOption[] Options)
	{
		GUIStyle style = ((!string.IsNullOrEmpty(Style)) ? GUI.skin.GetStyle(Style) : GUI.skin.verticalScrollbar);
		Value = GUILayout.VerticalScrollbar(Value, ThumbSize, TopValue, BottomValue, style, Options);
	}
}
