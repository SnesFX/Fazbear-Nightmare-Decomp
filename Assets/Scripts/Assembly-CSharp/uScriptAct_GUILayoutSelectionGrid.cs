using System.Collections.Generic;
using UnityEngine;

[NodeToolTip("Make a grid of buttons.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("GUILayout Selection Grid", "Make a grid of buttons.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUILayout_Selection_Grid")]
[NodeCopyright("Copyright 2013 by Detox Studios LLC")]
[NodePath("Actions/GUI/Controls")]
public class uScriptAct_GUILayoutSelectionGrid : uScriptLogic
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

	public void In([FriendlyName("Selected", "The index of the selected grid button.")] ref int Value, [FriendlyName("Text List", "An array of strings to show on the grid buttons.")] string[] TextList, [SocketState(false, false)][FriendlyName("Texture List", "An array of textures to show on the grid buttons.")] Texture[] TextureList, [SocketState(false, false)][FriendlyName("xCount", "How many elements to fit in the horizontal direction. The controls will be scaled to fit unless the style defines a fixedWidth to use.")][DefaultValue(50)] int xCount, [DefaultValue("")][SocketState(false, false)][FriendlyName("Style", "The style to use. If left out, the \"button\" style from the current GUISkin is used.")] string Style, [SocketState(false, false)][FriendlyName("Options", "An optional list of layout parameters.  Any values passed in here will override settings defined by the style.")] GUILayoutOption[] Options)
	{
		List<GUIContent> list = new List<GUIContent>();
		int num = Mathf.Max(TextList.Length, TextureList.Length);
		for (int i = 0; i < num; i++)
		{
			GUIContent gUIContent = new GUIContent();
			if (TextList.Length > i)
			{
				gUIContent.text = TextList[i];
			}
			if (TextureList.Length > i)
			{
				gUIContent.image = TextureList[i];
			}
			list.Add(gUIContent);
		}
		GUIStyle style = ((!string.IsNullOrEmpty(Style)) ? GUI.skin.GetStyle(Style) : GUI.skin.button);
		m_Changed = false;
		int num2 = GUILayout.SelectionGrid(Value, list.ToArray(), xCount, style, Options);
		if (Value != num2)
		{
			m_Changed = true;
		}
		Value = num2;
	}
}
