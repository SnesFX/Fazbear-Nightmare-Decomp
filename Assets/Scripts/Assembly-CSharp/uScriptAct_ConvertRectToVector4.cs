using UnityEngine;

[FriendlyName("Convert Rect To Vector4", "Converts a Rect variable to a Vector4 variable.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Math/Conversions")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Converts a Rect variable to a Vector4 variable.")]
public class uScriptAct_ConvertRectToVector4 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Rect", "The Rect variable to be converted.")] Rect TargetRect, [FriendlyName("Vector4", "The new Vector4 variable created from the Rect.")] out Vector4 NewVector4)
	{
		float xMin = TargetRect.xMin;
		float yMin = TargetRect.yMin;
		float width = TargetRect.width;
		float height = TargetRect.height;
		NewVector4 = new Vector4(xMin, yMin, width, height);
	}
}
