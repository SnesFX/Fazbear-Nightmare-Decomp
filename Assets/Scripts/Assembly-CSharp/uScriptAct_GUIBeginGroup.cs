using UnityEngine;

[FriendlyName("GUI Begin Group", "When you begin a group, the coordinate system for GUI controls are set so (0,0) is the top-left corner of the group.  All controls are clipped to the group.  Groups can be nested - if they are, children are clipped to their parents.\n\nNOTE: Each use of this node must be matched with a call to \"GUI End Group\".")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Label")]
[NodePath("Actions/GUI/Controls")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Begins a GUI control group with a local coordinate system.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_GUIBeginGroup : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Position", "The position and size of the control.")] Rect Position, [FriendlyName("Text", "The text you want to display.")] string Text, [FriendlyName("Texture", "The background image to display.")] Texture2D Texture, [DefaultValue("")][SocketState(false, false)][FriendlyName("Tool Tip", "The tool tip to display when the control is being hovered over.")] string ToolTip, [FriendlyName("GUI Style", "The name of a custom GUI style to use when displaying this control.")][SocketState(false, false)][DefaultValue("")] string guiStyle)
	{
		GUIContent content = new GUIContent(Text, Texture, ToolTip);
		if (string.IsNullOrEmpty(guiStyle))
		{
			GUI.BeginGroup(Position, content);
		}
		else
		{
			GUI.BeginGroup(Position, content, GUI.skin.GetStyle(guiStyle));
		}
	}
}
