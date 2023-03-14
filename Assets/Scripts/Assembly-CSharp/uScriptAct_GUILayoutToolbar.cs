using System.Collections.Generic;
using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/GUI/Controls")]
[NodeCopyright("Copyright 2013 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUILayout_Toolbar")]
[FriendlyName("GUILayout Toolbar", "Make a toolbar.")]
[NodeToolTip("Make a toolbar.")]
public class uScriptAct_GUILayoutToolbar : uScriptLogic
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

	public void In([FriendlyName("Selected", "The index of the selected button.")] ref int Value, [FriendlyName("Text List", "An array of strings to show on the buttons.")] string[] TextList, [SocketState(false, false)][FriendlyName("Texture List", "An array of textures to show on the buttons.")] Texture[] TextureList, [SocketState(false, false)][DefaultValue("")][FriendlyName("Style", "The style to use. If left out, the \"button\" style from the current GUISkin is used.")] string Style, [SocketState(false, false)][FriendlyName("Options", "An optional list of layout parameters.  Any values passed in here will override settings defined by the style.")] GUILayoutOption[] Options)
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
		int num2 = GUILayout.Toolbar(Value, list.ToArray(), style, Options);
		if (Value != num2)
		{
			m_Changed = true;
		}
		Value = num2;
	}
}
