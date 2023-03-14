using UnityEngine;

[NodePath("Actions/Screen")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_Mouse_Position")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Gets the position of the mouse.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Get Mouse Position", "Gets the position of the mouse.")]
public class uScriptAct_GetMousePosition : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([SocketState(false, false)][DefaultValue(true)][FriendlyName("Invert Y", "Flips the Y value, effectivly making the top left corner of the screen be (0,0) instead of the bottom left corner.")] bool InvertY, [FriendlyName("Position", "Returns the position of the mouse as a Vector2.")] out Vector2 positionV2, [FriendlyName("X Position", "Returns just the X position of the mouse as a float.")] out float XPosition, [FriendlyName("Y Position", "Returns just the Y position of the mouse as a float.")] out float YPosition, [SocketState(false, false)][FriendlyName("Position (Vector 3)", "Returns the position of the mouse as a Vector3.")] out Vector3 position)
	{
		Vector3 mousePosition = Input.mousePosition;
		float x = mousePosition.x;
		if (InvertY)
		{
			float num = (float)Screen.height - mousePosition.y;
			positionV2 = new Vector2(x, num);
			position = new Vector3(x, num, mousePosition.z);
			YPosition = num;
			XPosition = x;
		}
		else
		{
			float num = mousePosition.y;
			positionV2 = new Vector2(x, num);
			position = mousePosition;
			YPosition = num;
			XPosition = x;
		}
	}
}
