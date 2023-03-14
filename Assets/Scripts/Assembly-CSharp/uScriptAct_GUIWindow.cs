using System;
using UnityEngine;

[NodeToolTip("Shows a GUIWindow on the screen.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("GUI Window", "Shows a GUIWindow on the screen.")]
[NodePath("Actions/GUI/Controls")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
public class uScriptAct_GUIWindow : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private const int WINDOW_ID = 0;

	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Draw Window")]
	public event uScriptEventHandler DrawWindow;

	public void In([FriendlyName("Name", "The name displayed at the top of the window.")] string Name, [FriendlyName("Position", "The position and size of the window.")] Rect Position, [SocketState(false, false)][DefaultValue("")][FriendlyName("Texture", "The background image to use for the label.")] Texture2D Texture, [FriendlyName("Control Name", "Name to give to this label GUI control.")][DefaultValue("")][SocketState(false, false)] string ControlName, [SocketState(false, false)][FriendlyName("Tool Tip", "The tool tip to display when the label is being hovered over.")][DefaultValue("")] string ToolTip, [FriendlyName("GUI Style", "The name of a custom GUI style to use when displaying this box.")][DefaultValue("")][SocketState(false, false)] string guiStyle)
	{
		GUIContent gUIContent = new GUIContent(Name, Texture, ToolTip);
		if (!string.IsNullOrEmpty(ControlName))
		{
			GUI.SetNextControlName(ControlName);
		}
		if (string.IsNullOrEmpty(guiStyle))
		{
			GUI.Window(0, Position, Window, gUIContent);
		}
		else
		{
			GUI.Window(0, Position, Window, gUIContent, GUI.skin.GetStyle(guiStyle));
		}
	}

	private void Window(int id)
	{
		if (this.DrawWindow != null)
		{
			this.DrawWindow(this, EventArgs.Empty);
		}
	}
}
