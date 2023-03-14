using System;
using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Create Relative Rect", "Creates a new Rect within an existing target Rect. Useful for quickly laying out GUI elements based on another Rect.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Creates a new Rect within an existing target Rect.")]
[NodePath("Actions/Variables/Rect")]
public class uScriptAct_CreateRelativeRect : uScriptLogic
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

	public void In([FriendlyName("Target", "The target Rect variable to base the new Rect off of.")] Rect Target, [FriendlyName("Width", "The width of the Rect in pixels you wish to make. Can not be less than 2 (will be automatically set to 2 if you specify a value less than 2).")][DefaultValue(32)] int RectWidth, [FriendlyName("Height", "The height of the Rect in pixels you wish to make. Can not be less than 2 (will be automatically set to 2 if you specify a value less than 2).")][DefaultValue(32)] int RectHeight, [SocketState(false, false)][DefaultValue(0)][FriendlyName("Position", "The position within the Target Rect you wish to locate the new Rect.")] Position RectPosition, [SocketState(false, false)][DefaultValue(0)][FriendlyName("X Offset", "An optional X (horizontal) offset in pixels you wish to use for the new Rect.")] int xOffset, [FriendlyName("Y Offset", "An optional Y (vertical) offset in pixels you wish to use for the new Rect.")][SocketState(false, false)][DefaultValue(0)] int yOffset, [FriendlyName("Output Rect", "The new Rect.")] out Rect OutputRect)
	{
		int num = Convert.ToInt32(Target.width);
		int num2 = Convert.ToInt32(Target.height);
		if (RectWidth < 2)
		{
			RectWidth = 2;
		}
		if (RectHeight < 2)
		{
			RectHeight = 2;
		}
		int num3 = 0;
		int num4 = 0;
		switch (RectPosition)
		{
		case Position.TopLeft:
			num3 = 0 + xOffset;
			num4 = 0 + yOffset;
			break;
		case Position.TopCenter:
			num3 = num / 2 - RectWidth / 2 + xOffset;
			num4 = 0 + yOffset;
			break;
		case Position.TopRight:
			num3 = num - (RectWidth - xOffset);
			num4 = 0 + yOffset;
			break;
		case Position.MiddleLeft:
			num3 = 0 + xOffset;
			num4 = num2 / 2 - RectHeight / 2 + yOffset;
			break;
		case Position.MiddleCenter:
			num3 = num / 2 - RectWidth / 2 + xOffset;
			num4 = num2 / 2 - RectHeight / 2 + yOffset;
			break;
		case Position.MiddleRight:
			num3 = num - (RectWidth - xOffset);
			num4 = num2 / 2 - RectHeight / 2 + yOffset;
			break;
		case Position.BottomLeft:
			num3 = 0 + xOffset;
			num4 = num2 - (RectHeight - yOffset);
			break;
		case Position.BottomCenter:
			num3 = num / 2 - RectWidth / 2 + xOffset;
			num4 = num2 - (RectHeight - yOffset);
			break;
		case Position.BottomRight:
			num3 = num - (RectWidth - xOffset);
			num4 = num2 - (RectHeight - yOffset);
			break;
		default:
			num3 = num / 2 - RectWidth / 2 + xOffset;
			num4 = num2 / 2 - RectHeight / 2 + yOffset;
			break;
		}
		num3 += Convert.ToInt32(Target.x);
		num4 += Convert.ToInt32(Target.y);
		Rect rect = new Rect(num3, num4, RectWidth, RectHeight);
		OutputRect = rect;
	}
}
