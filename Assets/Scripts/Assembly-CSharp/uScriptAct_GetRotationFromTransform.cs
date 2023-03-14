using UnityEngine;

[NodeToolTip("Gets the rotation information of a Transform variable.")]
[NodePath("Actions/Variables/Transform")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Get Rotation From Transform", "Gets the rotation information of a Transform variable.")]
[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
public class uScriptAct_GetRotationFromTransform : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The Transform you wish to get the rotation information of.")] Transform target, [SocketState(false, false)][FriendlyName("Get Local", "Returns the local rotation of the target Transform when checked (true).")] bool getLocal, [FriendlyName("Rotation", "The Quaternion rotation value of the target Transform.")] out Quaternion rotation, [SocketState(false, false)][FriendlyName("Euler Angles", "The Vector3 rotation in Euler Angles of the target Transform.")] out Vector3 eulerAngle)
	{
		if (getLocal)
		{
			rotation = target.localRotation;
			eulerAngle = target.localEulerAngles;
		}
		else
		{
			rotation = target.rotation;
			eulerAngle = target.eulerAngles;
		}
	}
}
