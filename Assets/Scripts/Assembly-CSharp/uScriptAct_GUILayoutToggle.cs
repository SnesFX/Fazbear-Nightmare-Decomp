using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUILayout_Toggle")]
[FriendlyName("GUILayout Toggle", "Shows a GUI Toggle on the screen using Unity's automatic layout system. The Changed event will fire when the control state changes.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Shows a GUI Toggle on the screen using Unity's automatic layout system.")]
[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
[NodePath("Actions/GUI/Controls")]
public class uScriptAct_GUILayoutToggle : uScriptLogic
{
	private bool m_Changed;

	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Changed")]
	public bool Changed
	{
		get
		{
			return m_Changed;
		}
	}

	public void In([FriendlyName("Value", "The value of the toggle.")] ref bool Value, [FriendlyName("Text", "Text to display on the toggle.")] string Text, [FriendlyName("Texture", "Texture to display on the toggle.")][SocketState(false, false)] Texture Texture, [DefaultValue("")][SocketState(false, false)][FriendlyName("Tooltip", "The tooltip associated with this control.")] string Tooltip, [DefaultValue("")][FriendlyName("Style", "The style to use. If left out, the \"toggle\" style from the current GUISkin is used.")][SocketState(false, false)] string Style, [FriendlyName("Options", "An optional list of layout parameters.  Any values passed in here will override settings defined by the style.")][SocketState(false, false)] GUILayoutOption[] Options)
	{
		GUIContent content = new GUIContent(Text, Texture, Tooltip);
		GUIStyle style = ((!string.IsNullOrEmpty(Style)) ? GUI.skin.GetStyle(Style) : GUI.skin.toggle);
		bool flag = false;
		m_Changed = false;
		flag = GUILayout.Toggle(Value, content, style, Options);
		if (Value != flag)
		{
			m_Changed = true;
		}
		Value = flag;
	}
}
