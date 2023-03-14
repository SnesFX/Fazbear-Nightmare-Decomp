using System;
using System.Collections.Generic;
using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/GUI/Controls")]
[FriendlyName("GUI Button", "Shows a GUIButton on the screen and allows responses when held down, released, and clicked.")]
[NodeToolTip("Shows a GUIButton on the screen and allows responses when held down, released, and clicked.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Button")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_GUIButton : uScriptLogic
{
	private class Identifier
	{
		public bool wasDown;

		public int id;

		public Identifier(int _id)
		{
			id = _id;
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

	[FriendlyName("Button Down")]
	public event uScriptEventHandler OnButtonDown;

	[FriendlyName("Button Held")]
	public event uScriptEventHandler OnButtonHeld;

	[FriendlyName("Button Up")]
	public event uScriptEventHandler OnButtonUp;

	[FriendlyName("Button Clicked")]
	public event uScriptEventHandler OnButtonClicked;

	public void In([FriendlyName("Text", "The text you want to display on the button.")] string Text, [SocketState(false, false)][DefaultValue(0)][FriendlyName("Unique Identifier", "A unique identifier if the same node is used to represent multiple buttons.")] int identifier, [FriendlyName("Position", "The position and size of the button.")] Rect Position, [FriendlyName("Texture", "The background image to use for the button.")] Texture2D Texture, [DefaultValue("")][FriendlyName("Tool Tip", "The tool tip to display when the button is being hovered over.")][SocketState(false, false)] string ToolTip, [SocketState(false, false)][DefaultValue("")][FriendlyName("GUI Style", "The name of a custom GUI style to use when displaying this button.")] string guiStyle)
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
		GUIContent content = new GUIContent(Text, Texture, ToolTip);
		bool flag = false;
		flag = ((!string.IsNullOrEmpty(guiStyle)) ? GUI.RepeatButton(Position, content, GUI.skin.GetStyle(guiStyle)) : GUI.RepeatButton(Position, content));
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
