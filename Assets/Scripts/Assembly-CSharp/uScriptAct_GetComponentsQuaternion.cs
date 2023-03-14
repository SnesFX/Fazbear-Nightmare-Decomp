using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeToolTip("Gets the components of a Quaternion as floats.")]
[NodeCopyright("Copyright 2013 by Detox Studios LLC")]
[NodePath("Actions/Variables/Quaternion")]
[FriendlyName("Get Components (Quaternion)", "Gets the components of a Quaternion as floats.")]
public class uScriptAct_GetComponentsQuaternion : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Input Vector4", "The input vector to get components of.")] Quaternion InputQuaternion, [FriendlyName("X", "The X value of the Input Quaternion.")] out float X, [FriendlyName("Y", "The Y value of the Input Quaternion.")] out float Y, [FriendlyName("Z", "The Z value of the Input Quaternion.")] out float Z, [FriendlyName("W", "The W value of the Input Quaternion.")] out float W)
	{
		X = InputQuaternion.x;
		Y = InputQuaternion.y;
		Z = InputQuaternion.z;
		W = InputQuaternion.w;
	}
}
