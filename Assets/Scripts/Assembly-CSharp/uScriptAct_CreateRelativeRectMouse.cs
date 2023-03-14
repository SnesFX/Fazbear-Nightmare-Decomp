using UnityEngine;

[NodePath("Actions/Variables/Rect")]
[NodeToolTip("Creates a Rect based off the current mouse cursor position.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Create Relative Rect (Mouse)", "Creates a Rect based off the current mouse cursor position. Useful for attaching GUI elements to the mouse.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_CreateRelativeRectMouse : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([DefaultValue(32)][FriendlyName("Width", "The width of the Rect in pixels you wish to make. Can not be less than 2 (will be automatically set to 2 if you specify a value less than 2).")] int RectWidth, [DefaultValue(32)][FriendlyName("Height", "The height of the Rect in pixels you wish to make. Can not be less than 2 (will be automatically set to 2 if you specify a value less than 2).")] int RectHeight, [DefaultValue(0)][SocketState(false, false)][FriendlyName("X Offset", "An optional X (horizontal) offset in pixels you wish to use for the new Rect.")] int xOffset, [SocketState(false, false)][DefaultValue(0)][FriendlyName("Y Offset", "An optional Y (vertical) offset in pixels you wish to use for the new Rect.")] int yOffset, [FriendlyName("Output Rect", "The new Rect.")] out Rect OutputRect)
	{
		Vector3 mousePosition = Input.mousePosition;
		float left = mousePosition.x + (float)xOffset;
		float top = (float)Screen.height - mousePosition.y + (float)yOffset;
		if (RectWidth < 2)
		{
			RectWidth = 2;
		}
		if (RectHeight < 2)
		{
			RectHeight = 2;
		}
		Rect rect = new Rect(left, top, RectWidth, RectHeight);
		OutputRect = rect;
	}
}
