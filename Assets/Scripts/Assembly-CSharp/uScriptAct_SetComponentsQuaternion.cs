using UnityEngine;

[FriendlyName("Set Components (Quaternion)", "Sets a Quaternion to the defined X, Y, Z and W float component values.")]
[NodeCopyright("Copyright 2013 by Detox Studios LLC")]
[NodeToolTip("Sets a Quaternion to the defined X, Y, Z and W float component values.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Variables/Quaternion")]
public class uScriptAct_SetComponentsQuaternion : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("X", "X value to use for the Output Quaternion.")] float X, [FriendlyName("Y", "Y value to use for the Output Quaternion.")] float Y, [FriendlyName("Z", "Z value to use for the Output Quaternion.")] float Z, [FriendlyName("W", "W value to use for the Output Quaternion.")] float W, [FriendlyName("Output Quaternion", "Quaternion variable built from the specified X, Y, Z, and W.")] out Quaternion OutputQuaternion)
	{
		OutputQuaternion = new Quaternion(X, Y, Z, W);
	}
}
