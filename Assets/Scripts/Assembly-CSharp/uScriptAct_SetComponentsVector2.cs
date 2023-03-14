using UnityEngine;

[NodeToolTip("Sets a Vector2 to the defined X and Y float component values.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Vector2")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Set Components (Vector2)", "Sets a Vector2 to the defined X and Y float component values.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Vector2_Components")]
public class uScriptAct_SetComponentsVector2 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("X", "X value to use for the Output Vector.")] float X, [FriendlyName("Y", "Y value to use for the Output Vector.")] float Y, [FriendlyName("Output Vector2", "Vector2 variable built from the specified X and Y.")] out Vector2 OutputVector2)
	{
		OutputVector2 = new Vector2(X, Y);
	}
}
