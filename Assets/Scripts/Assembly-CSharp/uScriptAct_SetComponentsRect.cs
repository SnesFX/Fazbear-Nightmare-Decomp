using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Vector4_Components")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Sets a Rect to the defined Left, Top, Width and Height float component values.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Rect")]
[FriendlyName("Set Components (Rect)", "Sets a Rect to the defined Left, Top, Width and Height float component values.")]
public class uScriptAct_SetComponentsRect : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Left", "Left value to use for the Output Rect.")] float Left, [FriendlyName("Top", "Top value to use for the Output Rect.")] float Top, [DefaultValue(32f)][FriendlyName("Width", "Width value to use for the Output Rect.")] float Width, [FriendlyName("Height", "Height value to use for the Output Rect.")][DefaultValue(32f)] float Height, [FriendlyName("Output Rect", "Rect variable built from the specified Left, Top, Width, and Height.")] out Rect OutputRect)
	{
		OutputRect = new Rect(Left, Top, Width, Height);
	}
}
