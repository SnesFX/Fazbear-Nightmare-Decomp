using UnityEngine;

[FriendlyName("GUILayout Begin Area", "Begin a GUILayout block of GUI controls in a fixed screen area.\n\nBy default, any GUI controls made using GUILayout are placed in the top-left corner of the screen. If you want to place a series of automatically laid out controls in an arbitrary area, use this node to define a new area for the automatic layout system to use.\n\nNOTE: The group must be closed using a \"GUILayout End Area\" node.")]
[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUILayout_BeginArea")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Begin a GUILayout block of GUI controls in a fixed screen area.")]
[NodePath("Actions/GUI/Controls")]
public class uScriptAct_GUILayoutBeginArea : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([SocketState(true, false)][FriendlyName("Position", "Position and size of the area.")] Rect Position, [FriendlyName("Text", "Text to display on group.")][SocketState(false, false)] string Text, [FriendlyName("Texture", "Texture to display on group.")][SocketState(false, false)] Texture Texture, [SocketState(false, false)][FriendlyName("Tooltip", "The tooltip associated with this control.")][DefaultValue("")] string Tooltip, [FriendlyName("Style", "The style to use. If left out, none will be used.")][SocketState(false, false)][DefaultValue("")] string Style)
	{
		GUIContent content = GUIContent.none;
		if (!string.IsNullOrEmpty(Text) || !string.IsNullOrEmpty(Tooltip) || Texture != null)
		{
			content = new GUIContent(Text, Texture, Tooltip);
		}
		GUIStyle style = ((!string.IsNullOrEmpty(Style)) ? GUI.skin.GetStyle(Style) : GUIStyle.none);
		GUILayout.BeginArea(Position, content, style);
	}
}
