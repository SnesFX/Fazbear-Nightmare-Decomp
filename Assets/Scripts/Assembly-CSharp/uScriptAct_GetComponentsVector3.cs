using UnityEngine;

[NodeToolTip("Gets the components of a Vector3 as floats.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Vector3")]
[FriendlyName("Get Components (Vector3)", "Gets the components of a Vector3 as floats.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_Vector3_Components")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_GetComponentsVector3 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Input Vector3", "The input vector to get components of.")] Vector3 InputVector3, [FriendlyName("X", "The X value of the Input Vector3.")] out float X, [FriendlyName("Y", "The Y value of the Input Vector3.")] out float Y, [FriendlyName("Z", "The Z value of the Input Vector3.")] out float Z)
	{
		X = InputVector3.x;
		Y = InputVector3.y;
		Z = InputVector3.z;
	}
}
