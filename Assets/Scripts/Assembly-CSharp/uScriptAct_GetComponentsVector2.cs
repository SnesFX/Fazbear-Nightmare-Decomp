using UnityEngine;

[NodePath("Actions/Variables/Vector2")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_Vector2_Components")]
[FriendlyName("Get Components (Vector2)", "Gets the components of a Vector2 as floats.")]
[NodeToolTip("Gets the components of a Vector2 as floats.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_GetComponentsVector2 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Input Vector2", "The input vector to get components of.")] Vector2 InputVector2, [FriendlyName("X", "The X value of the Input Vector2.")] out float X, [FriendlyName("Y", "The Y value of the Input Vector2.")] out float Y)
	{
		X = InputVector2.x;
		Y = InputVector2.y;
	}
}
