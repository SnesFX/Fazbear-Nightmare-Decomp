using System;
using System.Text.RegularExpressions;
using UnityEngine;

[FriendlyName("GUILayout Text Field", "Shows a GUI Text Field on the screen using Unity's automatic layout system.\n\nThis control creates a single-line text field where the user can edit a string. Special events are triggered when the string value changes, the user presses the Return key, and the control focus state changes.\n\nNOTE: An individual GUILayout Text Field node should not be used in a loop or triggered multiple times per frame.  Multiple nodes should be used if you wish to display more than one text field.\n\nNOTE: Only the Out socket should be used to call additional GUI nodes.  Events such as Changed, Submitted, and Received Focus are not triggered every frame, and errors will be generated if other GUI nodes are changed to them.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUILayout_TextField")]
[NodeToolTip("Shows a GUI Text Field on the screen using Unity's automatic layout system.")]
[NodePath("Actions/GUI/Controls")]
[NodeCopyright("Copyright 2013 by Detox Studios LLC")]
public class uScriptAct_GUILayoutTextField : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private bool _changed;

	private bool _submited;

	private bool _hadFocus;

	private string _initialValue;

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
			return _changed;
		}
	}

	[FriendlyName("Submitted")]
	public bool Submitted
	{
		get
		{
			return _submited;
		}
	}

	[FriendlyName("Received Focus")]
	public event uScriptEventHandler OnReceivedFocus;

	[FriendlyName("Has Focus")]
	public event uScriptEventHandler OnHasFocus;

	[FriendlyName("Lost Focus")]
	public event uScriptEventHandler OnLostFocus;

	public void In([FriendlyName("Value", "The value of the text field.")] ref string Value, [DefaultValue(50)][FriendlyName("Max Length", "The maximum allowable string length for this text field.")][SocketState(false, false)] int MaxLength, [FriendlyName("Reset Value on Escape", "When True, the value will be set to what it was when the control first received focus.")][DefaultValue(true)][SocketState(false, false)] bool ResetOnEscape, [DefaultValue(true)][SocketState(false, false)][FriendlyName("Drop Focus on Escape", "When True, the control will lose focus when the Escape key is pressed.")] bool DropFocusOnEscape, [DefaultValue(true)][SocketState(false, false)][FriendlyName("Drop Focus on Return", "When True, the control will lose focus when the Return key is pressed.")] bool DropFocusOnReturn, [FriendlyName("Mask Character", "Character to mask the string with.  Allows the control to be used as a password field that masks the keyboard input.  While this parameter accepts a String variable, only the first character in the string will be used.  For exapmle, if the Mask is \"XYZ\", and you enter a 5-letter password, it will appear as \"XXXXX\".")][DefaultValue("")][SocketState(false, true)] string MaskCharacter, [SocketState(false, false)][FriendlyName("Style", "The style to use. If left out, the \"textfield\" style from the current GUISkin is used.")][DefaultValue("")] string Style, [SocketState(false, false)][FriendlyName("Options", "An optional list of layout parameters.  Any values passed in here will override settings defined by the style.")] GUILayoutOption[] Options, [DefaultValue("")][SocketState(false, false)][FriendlyName("Control Name", "The name which will be assigned to the control.  If no name is specified, one will be dynamically generated and assigned.")] ref string ControlName)
	{
		_changed = false;
		_submited = false;
		string empty = string.Empty;
		GUIStyle style = ((!string.IsNullOrEmpty(Style)) ? GUI.skin.GetStyle(Style) : GUI.skin.textField);
		if (string.IsNullOrEmpty(ControlName))
		{
			ControlName = Guid.NewGuid().ToString();
		}
		GUI.SetNextControlName(ControlName);
		if (GUI.GetNameOfFocusedControl() == ControlName && Event.current.type == EventType.KeyDown)
		{
			bool flag = false;
			if (Event.current.keyCode == KeyCode.Escape)
			{
				if (ResetOnEscape)
				{
					Value = _initialValue;
					flag = true;
				}
				if (DropFocusOnEscape)
				{
					GUI.FocusControl(string.Empty);
					flag = true;
				}
			}
			if (Event.current.keyCode == KeyCode.Return)
			{
				if (DropFocusOnReturn)
				{
					GUI.FocusControl(string.Empty);
					flag = true;
				}
				else
				{
					_initialValue = Value;
					flag = true;
				}
				_submited = true;
			}
			if (flag)
			{
				Event.current.Use();
			}
		}
		Regex.Replace(MaskCharacter, "\\s+", string.Empty);
		empty = ((!string.IsNullOrEmpty(MaskCharacter)) ? GUILayout.PasswordField(Value, MaskCharacter[0], MaxLength, style, Options) : GUILayout.TextField(Value, MaxLength, style, Options));
		empty = empty.Replace("\n", string.Empty);
		if (Event.current.type == EventType.Repaint)
		{
			bool hadFocus = _hadFocus;
			_hadFocus = GUI.GetNameOfFocusedControl() == ControlName;
			if (!hadFocus && _hadFocus && this.OnReceivedFocus != null)
			{
				_initialValue = Value;
				this.OnReceivedFocus(this, new EventArgs());
			}
			if (hadFocus && _hadFocus && this.OnHasFocus != null)
			{
				this.OnHasFocus(this, new EventArgs());
			}
			if (hadFocus && !_hadFocus && this.OnLostFocus != null)
			{
				this.OnLostFocus(this, new EventArgs());
			}
		}
		if (Value != empty)
		{
			_changed = true;
		}
		Value = empty;
	}
}
