using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Get Vector4 Distance", "Returns the distance between two Vector4 locations as a float.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_Vector4_Distance")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Returns the distance between two Vector4 locations as a float.")]
[NodePath("Actions/Math/Vectors")]
public class uScriptAct_GetVector4Distance : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "The first Vector4.")] Vector4 A, [FriendlyName("B", "The second Vector4.")] Vector4 B, [FriendlyName("Distance", "The distance between the A and B vectors.")] out float Distance)
	{
		if (A != Vector4.zero && B != Vector4.zero)
		{
			Distance = Vector4.Distance(A, B);
		}
		else
		{
			Distance = 0f;
		}
	}
}
