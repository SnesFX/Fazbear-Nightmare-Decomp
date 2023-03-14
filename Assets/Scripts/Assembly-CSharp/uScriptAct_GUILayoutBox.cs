using UnityEngine;

[FriendlyName("GUILayout Box", "Shows a GUI Box on the screen using Unity's automatic layout system.\n\nThis will make a solid box. If you want to make a box containing other GUI controls, instead use a group layout node, such as \"GUILayout Begin Horizontal\", and then override the style paramenter with \"box\".")]
[NodePath("Actions/GUI/Controls")]
[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
[NodeToolTip("Shows a GUI Box on the screen using Unity's automatic layout system.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUILayout_Box")]
public class uScriptAct_GUILayoutBox : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Text", "Text to display on the box.")] string Text, [SocketState(false, false)][FriendlyName("Texture", "Texture to display on the box.")] Texture Texture, [FriendlyName("Tooltip", "The tooltip associated with this control.")][SocketState(false, false)][DefaultValue("")] string Tooltip, [FriendlyName("Style", "The style to use. If left out, the \"box\" style from the current GUISkin is used.")][SocketState(false, false)][DefaultValue("")] string Style, [SocketState(false, false)][FriendlyName("Options", "An optional list of layout parameters.  Any values passed in here will override settings defined by the style.")] GUILayoutOption[] Options)
	{
		GUIContent content = new GUIContent(Text, Texture, Tooltip);
		GUIStyle style = ((!string.IsNullOrEmpty(Style)) ? GUI.skin.GetStyle(Style) : GUI.skin.box);
		GUILayout.Box(content, style, Options);
	}
}
