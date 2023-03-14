using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Calculate the size of a some content if it is rendered with this style.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("GUI Calculate Style Size", "Calculate the size of a some content if it is rendered with this style.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUIStyleCalcSize")]
[NodeDeprecated]
[NodePath("Actions/GUI/Global")]
public class uScriptAct_GUIStyleCalcSize : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Text", "The text you want to display.")] string text, [FriendlyName("Texture", "The background image to use for the label.")][SocketState(false, false)] Texture texture, [SocketState(true, false)][DefaultValue("")][FriendlyName("Style", "The name of a custom GUI style to use when displaying this label.")] string styleName, [FriendlyName("Size", "The size (in pixels) needed to display the content with the specified style.")] out Vector2 size, [FriendlyName("Width", "The width (in pixels) needed to display the content with the specified style.")][SocketState(false, false)] out int width, [FriendlyName("Height", "The height (in pixels) needed to display the content with the specified style.")][SocketState(false, false)] out int height)
	{
		GUIStyle style = GUI.skin.GetStyle(styleName);
		if (style == null)
		{
			Debug.LogError("No style was specified!\n");
			size = Vector2.zero;
			width = 0;
			height = 0;
		}
		else
		{
			GUIContent content = new GUIContent(text, texture);
			size = style.CalcSize(content);
			width = (int)size.x;
			height = (int)size.y;
		}
	}
}
