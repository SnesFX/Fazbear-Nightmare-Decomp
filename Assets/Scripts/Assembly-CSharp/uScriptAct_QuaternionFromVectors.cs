using UnityEngine;

[NodePath("Actions/Math/Conversions")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Quaternion_From_Vectors")]
[NodeToolTip("Converts a forward and up vector into a quaternion.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Quaternion From Vectors", "Converts a forward and up vector into a quaternion.")]
public class uScriptAct_QuaternionFromVectors : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Foward Vector", "The forward vector component of the quaternion.")] Vector3 forward, [FriendlyName("Up Vector", "The up vector component of the quaternion.")] Vector3 up, [FriendlyName("Result Quaternion", "The quaternion calculated using the forward and up vectors passed in.")] out Quaternion result)
	{
		if (forward == Vector3.zero)
		{
			forward = Vector3.forward;
			if (up != Vector3.zero)
			{
				if (forward == up || forward == -up)
				{
					forward = Vector3.right;
				}
				Vector3 rhs = Vector3.Cross(forward, up);
				forward = Vector3.Cross(up, rhs);
			}
		}
		if (up == Vector3.zero)
		{
			up = Vector3.up;
			if (forward != Vector3.zero)
			{
				if (forward == up || forward == -up)
				{
					up = Vector3.forward;
				}
				Vector3 lhs = Vector3.Cross(forward, up);
				up = Vector3.Cross(lhs, forward);
			}
		}
		result = Quaternion.LookRotation(forward, up);
	}
}
