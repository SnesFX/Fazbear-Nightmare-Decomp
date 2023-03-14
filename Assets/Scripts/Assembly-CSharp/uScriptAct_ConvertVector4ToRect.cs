using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Math/Conversions")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Convert Vector4 To Rect", "Converts a Vector4 variable to a Rect variable.")]
[NodeToolTip("Converts a Vector4 variable to a Rect variable.")]
public class uScriptAct_ConvertVector4ToRect : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Vector4", "The Vector4 variable to be converted.")] Vector4 TargetVector4, [FriendlyName("Rect", "The new Rect variable created from the Vector4.")] out Rect NewRect)
	{
		float x = TargetVector4.x;
		float y = TargetVector4.y;
		float z = TargetVector4.z;
		float w = TargetVector4.w;
		NewRect = new Rect(x, y, z, w);
	}
}
