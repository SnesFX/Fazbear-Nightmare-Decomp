using UnityEngine;

[NodePath("Actions/GUI/Controls")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Make a grid of buttons.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Text_Area")]
[FriendlyName("GUI Selection Grid", "Make a grid of buttons.")]
public class uScriptAct_GUISelectionGrid : uScriptLogic
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

	public void In([FriendlyName("Selected", "The index of the selected grid button.")] ref int Value, [FriendlyName("Position", "Rectangle on the screen to use for the grid.")] Rect Position, [FriendlyName("Text Content", "An array of strings to show on the grid buttons.")] string[] Content, [FriendlyName("xCount", "How many elements to fit in the horizontal direction. The controls will be scaled to fit unless the style defines a fixedWidth to use.")][DefaultValue(50)] int xCount, [DefaultValue("")][FriendlyName("GUI Style", "The name of a custom GUI style to use when displaying this text aera.")][SocketState(false, false)] string guiStyle)
	{
		m_Changed = false;
		int num = ((!string.IsNullOrEmpty(guiStyle)) ? GUI.SelectionGrid(Position, Value, Content, xCount, GUI.skin.GetStyle(guiStyle)) : GUI.SelectionGrid(Position, Value, Content, xCount));
		if (Value != num)
		{
			m_Changed = true;
		}
		Value = num;
	}
}
