using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_Vector4_Components")]
[FriendlyName("Get Components (Rect)", "Gets the components of a Rect as floats.")]
[NodeToolTip("Gets the components of a Rect as floats.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Rect")]
public class uScriptAct_GetComponentsRect : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Input Rect", "The input Rect to get components of.")] Rect InputRect, [FriendlyName("Left", "The Left value of the Input Rect.")] out float Left, [FriendlyName("Top", "The Top value of the Input Rect.")] out float Top, [FriendlyName("Width", "The Width value of the Input Rect.")] out float Width, [FriendlyName("Height", "The Height value of the Input Rect.")] out float Height)
	{
		Left = InputRect.xMin;
		Top = InputRect.yMin;
		Width = InputRect.width;
		Height = InputRect.height;
	}
}
