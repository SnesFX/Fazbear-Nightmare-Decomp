using UnityEngine;

[NodePath("Actions/Camera")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Takes a Camera and an analog control x/y pair and computes the world rotation relative to the current camera view.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Get Camera Relative Rotation", "Takes a Camera and an analog control x/y pair and computes the world rotation relative to the current camera view.")]
public class uScriptAct_GetCameraRelativeRotation : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Camera", "The Camera to use for calculations.")] Camera camera, [FriendlyName("X Axis Value", "The X value of the control stick. 1.0 is full right, -1.0 is full left.")] float stickX, [FriendlyName("Y Axis Value", "The Y value of the control stick. 1.0 is full up, -1.0 is full down.")] float stickY, [FriendlyName("Constrain to XZ Plane", "Whether or not to constrain the calculations to keep the resulting up perpendicular to the x/z plane.")] bool constrainToXZ, [FriendlyName("World Rotation", "")] out Quaternion worldRotation)
	{
		Vector3 v = new Vector3(stickX, 0f, stickY);
		Vector3 up = new Vector3(0f, 1f, 0f);
		worldRotation = camera.transform.rotation;
		Transform transform = camera.transform;
		Matrix4x4 localToWorldMatrix = transform.localToWorldMatrix;
		Vector3 rhs = new Vector3(localToWorldMatrix[1, 0], localToWorldMatrix[1, 1], localToWorldMatrix[1, 2]);
		v = localToWorldMatrix.MultiplyVector(v);
		if (constrainToXZ)
		{
			v.y = 0f;
		}
		v.Normalize();
		if (!constrainToXZ)
		{
			up = Vector3.Cross(v, rhs);
		}
		worldRotation.SetLookRotation(v, up);
	}
}
