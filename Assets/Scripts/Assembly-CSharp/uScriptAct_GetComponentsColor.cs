using UnityEngine;

[NodePath("Actions/Variables/Color")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Get Components (Color)", "Returns the individual components of a Color variable. The component values are normalized using the range of 0.0 (none) to 1.0 (full).")]
[NodeToolTip("Gets the components of a color variable as floats.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_Vector4_Components")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_GetComponentsColor : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Input Color", "The input color to get components of.")] Color InputColor, [FriendlyName("Red", "The Red value of the Input Color.")] out float Red, [FriendlyName("Green", "The Green value of the Input Color.")] out float Green, [FriendlyName("Blue", "The Blue value of the Input Color.")] out float Blue, [FriendlyName("Alpha", "The Alpha value of the Input Color.")] out float Alpha)
	{
		Red = InputColor.r;
		Green = InputColor.g;
		Blue = InputColor.b;
		Alpha = InputColor.a;
	}
}
