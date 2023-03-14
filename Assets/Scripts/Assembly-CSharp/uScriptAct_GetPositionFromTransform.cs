using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Get Position From Transform", "Gets the Vector3 position of a Transform variable.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Gets the Vector3 position of a Transform variable.")]
[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
[NodePath("Actions/Variables/Transform")]
public class uScriptAct_GetPositionFromTransform : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The Transform you wish to get the position information of.")] Transform target, [SocketState(false, false)][FriendlyName("Get Local", "Returns the local position of the target Transform when checked (true).")] bool getLocal, [FriendlyName("Position", "The Vector3 position of the target Transform.")] out Vector3 position, [FriendlyName("Forward", "The Vector3 forward of the target Transform.")][SocketState(false, false)] out Vector3 forward, [FriendlyName("Right", "The Vector3 right of the target Transform.")][SocketState(false, false)] out Vector3 right, [FriendlyName("Up", "The Vector3 up of the target Transform.")][SocketState(false, false)] out Vector3 up, [FriendlyName("World Matrix", "Returns the target Transform's world position as a local 4x4 matrix (transforms a point from world space into local space).")][SocketState(false, false)] out Matrix4x4 worldMatrix)
	{
		if (getLocal)
		{
			position = target.localPosition;
		}
		else
		{
			position = target.position;
		}
		forward = target.forward;
		right = target.right;
		up = target.up;
		worldMatrix = target.worldToLocalMatrix;
	}
}
