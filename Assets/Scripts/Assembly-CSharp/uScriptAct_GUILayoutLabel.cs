using UnityEngine;

[NodePath("Actions/GUI/Controls")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUILayout_Label")]
[NodeToolTip("Shows a GUI Label on the screen using Unity's automatic layout system.")]
[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
[FriendlyName("GUILayout Label", "Shows a GUI Label on the screen using Unity's automatic layout system.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_GUILayoutLabel : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Text", "Text to display on the label.")] string Text, [FriendlyName("Texture", "Texture to display on the label.")][SocketState(false, false)] Texture Texture, [DefaultValue("")][FriendlyName("Tooltip", "The tooltip associated with this control.")][SocketState(false, false)] string Tooltip, [DefaultValue("")][FriendlyName("Style", "The style to use. If left out, the \"label\" style from the current GUISkin is used.")][SocketState(false, false)] string Style, [SocketState(false, false)][FriendlyName("Options", "An optional list of layout parameters.  Any values passed in here will override settings defined by the style.")] GUILayoutOption[] Options)
	{
		GUIContent content = new GUIContent(Text, Texture, Tooltip);
		GUIStyle style = ((!string.IsNullOrEmpty(Style)) ? GUI.skin.GetStyle(Style) : GUI.skin.label);
		GUILayout.Label(content, style, Options);
	}
}
