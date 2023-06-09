using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Vector4")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Gets the components of a Vector4 as floats.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_Vector4_Components")]
[FriendlyName("Get Components (Vector4)", "Gets the components of a Vector4 as floats.")]
public class uScriptAct_GetComponentsVector4 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Input Vector4", "The input vector to get components of.")] Vector4 InputVector4, [FriendlyName("X", "The X value of the Input Vector4.")] out float X, [FriendlyName("Y", "The Y value of the Input Vector4.")] out float Y, [FriendlyName("Z", "The Z value of the Input Vector4.")] out float Z, [FriendlyName("W", "The W value of the Input Vector4.")] out float W)
	{
		X = InputVector4.x;
		Y = InputVector4.y;
		Z = InputVector4.z;
		W = InputVector4.w;
	}
}
