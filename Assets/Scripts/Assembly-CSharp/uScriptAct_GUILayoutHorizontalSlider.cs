using UnityEngine;

[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
[NodePath("Actions/GUI/Controls")]
[NodeToolTip("Shows a horizontal slider that the user can drag to change a value.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUILayout_HorizontalSlider")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("GUILayout Horizontal Slider", "Shows a horizontal slider that the user can drag to change a value.")]
public class uScriptAct_GUILayoutHorizontalSlider : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The value the slider shows. This determines the position of the draggable thumb.")] ref float Value, [DefaultValue(0f)][SocketState(false, false)][FriendlyName("Left Value", "The value at the left end of the slider.")] float LeftValue, [DefaultValue(10f)][SocketState(false, false)][FriendlyName("Right Value", "The value at the right end of the slider.")] float RightValue, [DefaultValue("")][FriendlyName("Slider Style", "The style to use for the dragging area. If left out, the \"horizontalslider\" style from the current GUISkin is used.")][SocketState(false, false)] string SliderStyle, [SocketState(false, false)][FriendlyName("Thumb Style", "The style to use for the draggable thumb. If left out, the \"horizontalsliderthumb\" style from the current GUISkin is used.")][DefaultValue("")] string ThumbStyle, [SocketState(false, false)][FriendlyName("Options", "An optional list of layout parameters.  Any values passed in here will override settings defined by the style.")] GUILayoutOption[] Options)
	{
		GUIStyle slider = ((!string.IsNullOrEmpty(SliderStyle)) ? GUI.skin.GetStyle(SliderStyle) : GUI.skin.horizontalSlider);
		GUIStyle thumb = ((!string.IsNullOrEmpty(ThumbStyle)) ? GUI.skin.GetStyle(ThumbStyle) : GUI.skin.horizontalSliderThumb);
		Value = GUILayout.HorizontalSlider(Value, LeftValue, RightValue, slider, thumb, Options);
	}
}
