using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUILayout_BeginHorizontal")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Begin a horizontal control group using Unity's automatic layout system.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/GUI/Controls")]
[FriendlyName("GUILayout Begin Horizontal", "Begin a horizontal control group using Unity's automatic layout system.\n\nAll controls rendered inside this element will be placed horiztonally next to each other.\n\nNOTE: The group must be closed using a \"GUILayout End Horizontal\" node.")]
public class uScriptAct_GUILayoutBeginHorizontal : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([SocketState(false, false)][FriendlyName("Text", "Text to display on group.")] string Text, [FriendlyName("Texture", "Texture to display on group.")][SocketState(false, false)] Texture Texture, [SocketState(false, false)][DefaultValue("")][FriendlyName("Tooltip", "The tooltip associated with this control.")] string Tooltip, [DefaultValue("")][SocketState(false, false)][FriendlyName("Style", "The style to use. If left out, none will be used.")] string Style, [SocketState(false, false)][FriendlyName("Options", "An optional list of layout parameters.  Any values passed in here will override settings defined by the style.")] GUILayoutOption[] Options)
	{
		GUIContent content = GUIContent.none;
		if (!string.IsNullOrEmpty(Text) || !string.IsNullOrEmpty(Tooltip) || Texture != null)
		{
			content = new GUIContent(Text, Texture, Tooltip);
		}
		GUIStyle style = ((!string.IsNullOrEmpty(Style)) ? GUI.skin.GetStyle(Style) : GUIStyle.none);
		GUILayout.BeginHorizontal(content, style, Options);
	}
}
