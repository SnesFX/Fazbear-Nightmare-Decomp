using UnityEngine;

[NodePath("Actions/GUI/Controls")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Toggle")]
[NodeToolTip("Shows a GUIToggle on the screen and allows responses when changed.")]
[FriendlyName("GUI Toggle", "Shows a GUIToggle on the screen and allows responses when changed.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_GUIToggle : uScriptLogic
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

	public void In([FriendlyName("Value", "The value of this toggle.")] ref bool Value, [FriendlyName("Text", "The text you want to display with the toggle.")] string Text, [FriendlyName("Position", "The position and size of the toggle.")] Rect Position, [FriendlyName("Texture", "The background image to use for the toggle.")] Texture2D Texture, [DefaultValue("")][SocketState(false, false)][FriendlyName("Tool Tip", "The tool tip to display when the toggle is being hovered over.")] string ToolTip, [SocketState(false, false)][DefaultValue("")][FriendlyName("GUI Style", "The name of a custom GUI style to use when displaying this toggle.")] string guiStyle)
	{
		GUIContent content = new GUIContent(Text, Texture, ToolTip);
		bool flag = false;
		m_Changed = false;
		flag = ((!string.IsNullOrEmpty(guiStyle)) ? GUI.Toggle(Position, Value, content, GUI.skin.GetStyle(guiStyle)) : GUI.Toggle(Position, Value, content));
		if (Value != flag)
		{
			m_Changed = true;
		}
		Value = flag;
	}
}
