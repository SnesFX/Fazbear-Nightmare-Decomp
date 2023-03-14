using UnityEngine;

[FriendlyName("GUILayout Vertical Slider", "Shows a vertical slider that the user can drag to change a value.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUILayout_VerticalSlider")]
[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
[NodePath("Actions/GUI/Controls")]
[NodeToolTip("Shows a vertical slider that the user can drag to change a value.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_GUILayoutVerticalSlider : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The value the slider shows. This determines the position of the draggable thumb.")] ref float Value, [FriendlyName("Top Value", "The value at the top end of the slider.")][DefaultValue(0f)][SocketState(false, false)] float TopValue, [SocketState(false, false)][DefaultValue(10f)][FriendlyName("Bottom Value", "The value at the bottom end of the slider.")] float BottomValue, [SocketState(false, false)][DefaultValue("")][FriendlyName("Slider Style", "The style to use for the dragging area. If left out, the \"verticalslider\" style from the current GUISkin is used.")] string SliderStyle, [FriendlyName("Thumb Style", "The style to use for the draggable thumb. If left out, the \"verticalsliderthumb\" style from the current GUISkin is used.")][SocketState(false, false)][DefaultValue("")] string ThumbStyle, [SocketState(false, false)][FriendlyName("Options", "An optional list of layout parameters.  Any values passed in here will override settings defined by the style.")] GUILayoutOption[] Options)
	{
		GUIStyle slider = ((!string.IsNullOrEmpty(SliderStyle)) ? GUI.skin.GetStyle(SliderStyle) : GUI.skin.verticalSlider);
		GUIStyle thumb = ((!string.IsNullOrEmpty(ThumbStyle)) ? GUI.skin.GetStyle(ThumbStyle) : GUI.skin.verticalSliderThumb);
		Value = GUILayout.VerticalSlider(Value, TopValue, BottomValue, slider, thumb, Options);
	}
}
