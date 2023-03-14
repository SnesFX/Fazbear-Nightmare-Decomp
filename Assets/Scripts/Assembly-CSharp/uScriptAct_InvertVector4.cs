using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Invert_Vector4")]
[NodePath("Actions/Math/Vectors")]
[NodeToolTip("Mirrors the X, Y, Z and W of a Vector4.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Invert Vector4", "Returns the inverse value of a Vector4 variable. Individual components can optionally be ignored by this operation.\n\nExample:\n\t[x, y, z, w] -> [-x, -y, -z, -w]")]
public class uScriptAct_InvertVector4 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "Value to invert.")] Vector4 Target, [SocketState(false, false)][FriendlyName("Ignore X", "If True, the X component will be ignored.")] bool IgnoreX, [FriendlyName("Ignore Y", "If True, the Y component will be ignored.")][SocketState(false, false)] bool IgnoreY, [FriendlyName("Ignore Z", "If True, the Z component will be ignored.")][SocketState(false, false)] bool IgnoreZ, [SocketState(false, false)][FriendlyName("Ignore W", "If True, the W component will be ignored.")] bool IgnoreW, [FriendlyName("Value", "The inverted value.")] out Vector4 Value)
	{
		Value = new Vector4(Target.x, Target.y, Target.z, Target.w);
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
		if (!IgnoreW)
		{
			Value.w = 0f - Value.w;
		}
	}
}
