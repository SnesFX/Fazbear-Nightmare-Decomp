using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Shows a label on the screen.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Print_Text")]
[FriendlyName("Print Text", "Shows a label on the screen. This node does NOT need to be hooked up to an OnGUI Events node.")]
[NodePath("Actions/GUI")]
public class uScriptAct_PrintText : uScriptLogic
{
	private string m_Text;

	private float m_Width;

	private float m_Height;

	private float m_RemoveTime;

	private bool m_DisplayText;

	private GUIStyle m_Style = new GUIStyle();

	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Show Text")]
	public void ShowLabel(string Text, int FontSize, FontStyle FontStyle, Color FontColor, TextAnchor textAnchor, int EdgePadding, float time)
	{
		m_Text = Text;
		m_Width = Screen.width - EdgePadding;
		m_Height = Screen.height - EdgePadding;
		m_Style.fontSize = FontSize;
		m_Style.fontStyle = FontStyle;
		m_Style.alignment = textAnchor;
		m_Style.normal.textColor = FontColor;
		m_DisplayText = true;
		m_RemoveTime = time;
	}

	[FriendlyName("Hide Text")]
	public void HideLabel([FriendlyName("Text", "The text you want to display.")] string Text, [SocketState(false, false)][FriendlyName("Font Size", "The size of the font.")][DefaultValue(16)] int FontSize, [SocketState(false, false)][FriendlyName("Font Style", "The font style (Normal,Bold, Italic, BoldAndItalic).")] FontStyle FontStyle, [SocketState(false, false)][FriendlyName("Color", "The color of the font.")] Color FontColor, [FriendlyName("Alignment", "The position of the text on the screen.")][SocketState(false, false)] TextAnchor textAnchor, [DefaultValue(8)][SocketState(false, false)][FriendlyName("Edge Padding", "The number of pixels tp offset the text from the edge of the screen.")] int EdgePadding, [DefaultValue(0f)][SocketState(false, false)][FriendlyName("Remove After", "The amount of time (in seconds) to wait before automatically removing the text.")] float time)
	{
		m_DisplayText = false;
	}

	public void OnGUI()
	{
		if (m_RemoveTime > 0f)
		{
			m_RemoveTime -= Time.deltaTime;
			if (m_RemoveTime <= 0f)
			{
				m_RemoveTime = 0f;
				m_DisplayText = false;
			}
		}
		if (m_DisplayText)
		{
			float num = m_Height / 2f;
			float num2 = m_Width / 2f;
			GUI.Label(new Rect((float)(Screen.width / 2) - num2, (float)(Screen.height / 2) - num, m_Width, m_Height), m_Text, m_Style);
		}
	}
}
