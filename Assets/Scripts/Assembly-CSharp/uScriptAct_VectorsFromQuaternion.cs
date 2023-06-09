using UnityEngine;

[NodePath("Actions/Math/Conversions")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Converts a quaternion into forward and up vectors.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Vectors_From_Quaternion")]
[FriendlyName("Vectors From Quaternion", "Converts a quaternion into forward and up vectors.")]
public class uScriptAct_VectorsFromQuaternion : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Quaternion", "The quaternion to get the forward and up vectors from.")] Quaternion quaternion, [FriendlyName("Forward Vector", "The forward vector component of the quaternion.")] out Vector3 forward, [FriendlyName("Up Vector", "The up vector component of the quaternion.")] out Vector3 up)
	{
		Matrix4x4 matrix4x = Matrix4x4.TRS(Vector3.zero, quaternion, Vector3.one);
		forward = matrix4x.GetColumn(2);
		up = matrix4x.GetColumn(1);
	}
}
