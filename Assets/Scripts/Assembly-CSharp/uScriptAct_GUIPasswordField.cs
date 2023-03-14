using UnityEngine;

[FriendlyName("GUI Password Field", "Shows a GUIPasswordField on the screen and allows getting/setting of its string contents and repsonses to changed event.")]
[NodeToolTip("Shows a GUIPasswordField on the screen and allows getting/setting of its string contents and repsonses to changed event.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/GUI/Controls")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Password_Field")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_GUIPasswordField : uScriptLogic
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

	public void In([FriendlyName("Value", "The value of this text field.")] ref string Value, [FriendlyName("Position", "The position and size of the text field.")] Rect Position, [DefaultValue(50)][FriendlyName("Max Length", "The maximum allowable string length for this text field.")] int maxLength, [FriendlyName("Control Name", "Name to give to this text field GUI control.")][DefaultValue("")][SocketState(false, false)] string ControlName, [DefaultValue("")][SocketState(false, false)][FriendlyName("GUI Style", "The name of a custom GUI style to use when displaying this text field.")] string guiStyle)
	{
		m_Changed = false;
		if (!string.IsNullOrEmpty(ControlName))
		{
			GUI.SetNextControlName(ControlName);
		}
		string text = ((!string.IsNullOrEmpty(guiStyle)) ? GUI.PasswordField(Position, Value, '*', maxLength, GUI.skin.GetStyle(guiStyle)) : GUI.PasswordField(Position, Value, '*', maxLength));
		if (Value != text)
		{
			m_Changed = true;
		}
		Value = text;
	}
}
