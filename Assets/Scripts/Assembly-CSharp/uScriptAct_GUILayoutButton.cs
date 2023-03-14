using System;
using System.Collections.Generic;
using UnityEngine;

[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
[NodeToolTip("Shows a GUI Button on the screen using Unity's automatic layout system.")]
[FriendlyName("GUILayout Button", "Shows a GUI Button on the screen using Unity's automatic layout system. The button will trigger events when Clicked, but also on Down, Held, and Up events.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUILayout_Button")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/GUI/Controls")]
public class uScriptAct_GUILayoutButton : uScriptLogic
{
	private class Identifier
	{
		public int id;

		public bool wasDown;

		public Identifier(int id)
		{
			this.id = id;
			wasDown = false;
		}
	}

	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private List<Identifier> m_Identifiers = new List<Identifier>();

	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Button Clicked")]
	public event uScriptEventHandler OnButtonClicked;

	[FriendlyName("Button Down")]
	public event uScriptEventHandler OnButtonDown;

	[FriendlyName("Button Held")]
	public event uScriptEventHandler OnButtonHeld;

	[FriendlyName("Button Up")]
	public event uScriptEventHandler OnButtonUp;

	public void In([FriendlyName("Text", "Text to display on the button.")] string Text, [SocketState(false, false)][FriendlyName("Texture", "Texture to display on the button.")] Texture Texture, [DefaultValue("")][SocketState(false, false)][FriendlyName("Tooltip", "The tooltip associated with this control.")] string Tooltip, [SocketState(false, false)][FriendlyName("Style", "The style to use. If left out, the \"button\" style from the current GUISkin is used.")][DefaultValue("")] string Style, [FriendlyName("Options", "An optional list of layout parameters.  Any values passed in here will override settings defined by the style.")][SocketState(false, false)] GUILayoutOption[] Options, [FriendlyName("Unique Identifier", "If the same node is used to represent multiple buttons, specify a unique identifier.")][SocketState(false, false)][DefaultValue(0)] int identifier)
	{
		Identifier identifier2 = null;
		foreach (Identifier identifier3 in m_Identifiers)
		{
			if (identifier3.id == identifier)
			{
				identifier2 = identifier3;
			}
		}
		if (identifier2 == null)
		{
			identifier2 = new Identifier(identifier);
			m_Identifiers.Add(identifier2);
		}
		GUIContent content = new GUIContent(Text, Texture, Tooltip);
		GUIStyle style = ((!string.IsNullOrEmpty(Style)) ? GUI.skin.GetStyle(Style) : GUI.skin.button);
		bool flag = false;
		flag = GUILayout.RepeatButton(content, style, Options);
		if (Event.current.type != EventType.Repaint && !Event.current.isMouse)
		{
			return;
		}
		bool wasDown = identifier2.wasDown;
		identifier2.wasDown = flag;
		if (!wasDown && flag && this.OnButtonDown != null)
		{
			this.OnButtonDown(this, new EventArgs());
		}
		if (wasDown && flag && this.OnButtonHeld != null)
		{
			this.OnButtonHeld(this, new EventArgs());
		}
		if (wasDown && !flag)
		{
			if (this.OnButtonUp != null)
			{
				this.OnButtonUp(this, new EventArgs());
			}
			if (this.OnButtonClicked != null)
			{
				this.OnButtonClicked(this, new EventArgs());
			}
		}
	}
}
