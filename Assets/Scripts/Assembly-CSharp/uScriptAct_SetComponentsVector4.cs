using UnityEngine;

[FriendlyName("Set Components (Vector4)", "Sets a Vector4 to the defined X, Y, Z and W float component values.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Vector4_Components")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Sets a Vector4 to the defined X, Y, Z and W float component values.")]
[NodePath("Actions/Variables/Vector4")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_SetComponentsVector4 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("X", "X value to use for the Output Vector.")] float X, [FriendlyName("Y", "Y value to use for the Output Vector.")] float Y, [FriendlyName("Z", "Z value to use for the Output Vector.")] float Z, [FriendlyName("W", "W value to use for the Output Vector.")] float W, [FriendlyName("Output Vector4", "Vector4 variable built from the specified X, Y, Z, and W.")] out Vector4 OutputVector4)
	{
		OutputVector4 = new Vector4(X, Y, Z, W);
	}
}
