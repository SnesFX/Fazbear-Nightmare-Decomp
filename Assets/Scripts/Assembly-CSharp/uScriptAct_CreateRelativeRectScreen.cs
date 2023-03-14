using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Creates a Rect based off the current screen resolution.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Rect")]
[FriendlyName("Create Relative Rect (Screen)", "Creates a Rect based off the current screen resolution. Useful for quickly laying out GUI elements based on the screen.")]
public class uScriptAct_CreateRelativeRectScreen : uScriptLogic
{
	public enum Position
	{
		TopLeft = 0,
		TopCenter = 1,
		TopRight = 2,
		MiddleLeft = 3,
		MiddleCenter = 4,
		MiddleRight = 5,
		BottomLeft = 6,
		BottomCenter = 7,
		BottomRight = 8
	}

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Width", "The width of the Rect in pixels you wish to make. Can not be less than 2 or greater than the screen width (will be automatically capped if you specify a value outside this range).")][DefaultValue(32)] int RectWidth, [FriendlyName("Height", "The height of the Rect in pixels you wish to make. Can not be less than 2 or greater than the screen height (will be automatically capped if you specify a value outside this range).")][DefaultValue(32)] int RectHeight, [DefaultValue(0)][FriendlyName("Position", "The position on the screen you wish to locate the new Rect.")][SocketState(false, false)] Position RectPosition, [SocketState(false, false)][DefaultValue(0)][FriendlyName("X Offset", "An optional X (horizontal) offset in pixels you wish to use for the new Rect.")] int xOffset, [SocketState(false, false)][FriendlyName("Y Offset", "An optional Y (vertical) offset in pixels you wish to use for the new Rect.")][DefaultValue(0)] int yOffset, [FriendlyName("Output Rect", "The new Rect.")] out Rect OutputRect)
	{
		int width = Screen.width;
		int height = Screen.height;
		if (RectWidth < 2)
		{
			RectWidth = 2;
		}
		if (RectWidth + xOffset > width)
		{
			RectWidth = width;
		}
		if (RectHeight < 2)
		{
			RectHeight = 2;
		}
		if (RectHeight + yOffset > height)
		{
			RectHeight = height;
		}
		int num = 0;
		int num2 = 0;
		switch (RectPosition)
		{
		case Position.TopLeft:
			num = 0 + xOffset;
			num2 = 0 + yOffset;
			break;
		case Position.TopCenter:
			num = Screen.width / 2 - RectWidth / 2 + xOffset;
			num2 = 0 + yOffset;
			break;
		case Position.TopRight:
			num = Screen.width - (RectWidth - xOffset);
			num2 = 0 + yOffset;
			break;
		case Position.MiddleLeft:
			num = 0 + xOffset;
			num2 = Screen.height / 2 - RectHeight / 2 + yOffset;
			break;
		case Position.MiddleCenter:
			num = Screen.width / 2 - RectWidth / 2 + xOffset;
			num2 = Screen.height / 2 - RectHeight / 2 + yOffset;
			break;
		case Position.MiddleRight:
			num = Screen.width - (RectWidth - xOffset);
			num2 = Screen.height / 2 - RectHeight / 2 + yOffset;
			break;
		case Position.BottomLeft:
			num = 0 + xOffset;
			num2 = Screen.height - (RectHeight - yOffset);
			break;
		case Position.BottomCenter:
			num = Screen.width / 2 - RectWidth / 2 + xOffset;
			num2 = Screen.height - (RectHeight - yOffset);
			break;
		case Position.BottomRight:
			num = Screen.width - (RectWidth - xOffset);
			num2 = Screen.height - (RectHeight - yOffset);
			break;
		default:
			num = Screen.width / 2 - RectWidth / 2 + xOffset;
			num2 = Screen.height / 2 - RectHeight / 2 + yOffset;
			break;
		}
		Rect rect = new Rect(num, num2, RectWidth, RectHeight);
		OutputRect = rect;
	}
}
