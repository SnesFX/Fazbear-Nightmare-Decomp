using UnityEngine;

[NodePath("Actions/Math/Vectors")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Mirrors the X, Y, and Z of a Vector3.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Invert_Vector3")]
[FriendlyName("Invert Vector3", "Returns the inverse value of a Vector3 variable. Individual components can optionally be ignored by this operation.\n\nExample:\n\t[x, y, z] -> [-x, -y, -z]")]
public class uScriptAct_InvertVector3 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "Value to invert.")] Vector3 Target, [FriendlyName("Ignore X", "If True, the X component will be ignored.")][SocketState(false, false)] bool IgnoreX, [SocketState(false, false)][FriendlyName("Ignore Y", "If True, the Y component will be ignored.")] bool IgnoreY, [FriendlyName("Ignore Z", "If True, the Z component will be ignored.")][SocketState(false, false)] bool IgnoreZ, [FriendlyName("Value", "The inverted value.")] out Vector3 Value)
	{
		Value = new Vector3(Target.x, Target.y, Target.z);
		if (!IgnoreX)
		{
			Value.x = 0f - Value.x;
		}
		if (!IgnoreY)
		{
			Value.y = 0f - Value.y;
		}
		if (!IgnoreZ)
		{
			Value.z = 0f - Value.z;
		}
	}
}
