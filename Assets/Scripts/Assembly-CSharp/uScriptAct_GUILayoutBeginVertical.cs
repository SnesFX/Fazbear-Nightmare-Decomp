using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUILayout_BeginVertical")]
[NodePath("Actions/GUI/Controls")]
[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
[NodeToolTip("Begin a vertical control group using Unity's automatic layout system.")]
[FriendlyName("GUILayout Begin Vertical", "Begin a vertical control group using Unity's automatic layout system.\n\nAll controls rendered inside this element will be placed vertically below each other.\n\nNOTE: The group must be closed using a \"GUILayout End Vertical\" node.")]
public class uScriptAct_GUILayoutBeginVertical : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Text", "Text to display on group.")][SocketState(false, false)] string Text, [SocketState(false, false)][FriendlyName("Texture", "Texture to display on group.")] Texture Texture, [SocketState(false, false)][DefaultValue("")][FriendlyName("Tooltip", "The tooltip associated with this control.")] string Tooltip, [SocketState(false, false)][FriendlyName("Style", "The style to use. If left out, none will be used.")][DefaultValue("")] string Style, [FriendlyName("Options", "An optional list of layout parameters.  Any values passed in here will override settings defined by the style.")][SocketState(false, false)] GUILayoutOption[] Options)
	{
		GUIContent content = GUIContent.none;
		if (!string.IsNullOrEmpty(Text) || !string.IsNullOrEmpty(Tooltip) || Texture != null)
		{
			content = new GUIContent(Text, Texture, Tooltip);
		}
		GUIStyle style = ((!string.IsNullOrEmpty(Style)) ? GUI.skin.GetStyle(Style) : GUIStyle.none);
		GUILayout.BeginVertical(content, style, Options);
	}
}
