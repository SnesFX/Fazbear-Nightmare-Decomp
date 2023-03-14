using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUILayout_TextArea")]
[NodeToolTip("Shows a GUI Text Area on the screen using Unity's automatic layout system.")]
[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
[FriendlyName("GUILayout Text Area", "Shows a GUI Text Area on the screen using Unity's automatic layout system.\n\nThis control creates a multi-line text field where the user can edit a string. The Changed event will fire when the string value changes.")]
[NodePath("Actions/GUI/Controls")]
public class uScriptAct_GUILayoutTextArea : uScriptLogic
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

	public void In([FriendlyName("Value", "The value of the text field.")] ref string Value, [DefaultValue(50)][FriendlyName("Max Length", "The maximum allowable string length for this text field.")] int MaxLength, [SocketState(false, false)][DefaultValue("")][FriendlyName("Style", "The style to use. If left out, the \"textarea\" style from the current GUISkin is used.")] string Style, [FriendlyName("Options", "An optional list of layout parameters.  Any values passed in here will override settings defined by the style.")][SocketState(false, false)] GUILayoutOption[] Options, [DefaultValue("")][SocketState(false, false)][FriendlyName("Control Name", "The name which will be assigned to the control.")] string ControlName)
	{
		m_Changed = false;
		GUIStyle style = ((!string.IsNullOrEmpty(Style)) ? GUI.skin.GetStyle(Style) : GUI.skin.textArea);
		if (!string.IsNullOrEmpty(ControlName))
		{
			GUI.SetNextControlName(ControlName);
		}
		string text = GUILayout.TextArea(Value, MaxLength, style, Options);
		if (Value != text)
		{
			m_Changed = true;
		}
		Value = text;
	}
}
