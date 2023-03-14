using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Text_Area")]
[FriendlyName("GUI Text Area", "Shows a GUITextArea on the screen and allows getting/setting of its string contents and repsonses to changed event.")]
[NodePath("Actions/GUI/Controls")]
[NodeToolTip("Shows a GUITextArea on the screen and allows getting/setting of its string contents and repsonses to changed event.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_GUITextArea : uScriptLogic
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

	public void In([FriendlyName("Value", "The value of this text area.")] ref string Value, [FriendlyName("Position", "The position and size of the text area.")] Rect Position, [FriendlyName("Max Length", "The maximum allowable string length for this text area. A value of -1 means there is no limit.")][DefaultValue(50)] int maxLength, [SocketState(false, false)][DefaultValue("")][FriendlyName("Control Name", "Name to give to this text area GUI control.")] string ControlName, [DefaultValue("")][SocketState(false, false)][FriendlyName("GUI Style", "The name of a custom GUI style to use when displaying this text aera.")] string guiStyle)
	{
		m_Changed = false;
		if (!string.IsNullOrEmpty(ControlName))
		{
			GUI.SetNextControlName(ControlName);
		}
		string text = ((!string.IsNullOrEmpty(guiStyle)) ? GUI.TextArea(Position, Value, maxLength, GUI.skin.GetStyle(guiStyle)) : GUI.TextArea(Position, Value, maxLength));
		if (Value != text)
		{
			m_Changed = true;
		}
		Value = text;
	}
}
