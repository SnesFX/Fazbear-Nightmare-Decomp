using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Vector3_Components")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Set Components (Vector3)", "Sets a Vector3 to the defined X, Y, and Z float component values.")]
[NodePath("Actions/Variables/Vector3")]
[NodeToolTip("Sets a Vector3 to the defined X, Y, and Z float component values.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_SetComponentsVector3 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("X", "X value to use for the Output Vector.")] float X, [FriendlyName("Y", "Y value to use for the Output Vector.")] float Y, [FriendlyName("Z", "Z value to use for the Output Vector.")] float Z, [FriendlyName("Output Vector3", "Vector3 variable built from the specified X, Y, and Z.")] out Vector3 OutputVector3)
	{
		OutputVector3 = new Vector3(X, Y, Z);
	}
}
