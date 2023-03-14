using UnityEngine;

[NodeToolTip("Gets the 2D facing angle in degrees between the direction of target A and the position of target B.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Get Facing Angle", "Returns the 2D angle in degrees between the direction of target A and the position of target B. The resulting 2D angle is how many degress must target A turn to face target B.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Math/Angles")]
public class uScriptAct_GetFacingAngle : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "The first target.")] GameObject A, [FriendlyName("B", "The second target.")] GameObject B, [FriendlyName("Angle", "The angle result in degrees.")] out float Angle)
	{
		if (null != A && null != B)
		{
			Vector3 position = B.transform.position;
			Vector3 vector = A.transform.InverseTransformPoint(position);
			float num = Mathf.Atan2(vector.x, vector.z) * 57.29578f;
			Angle = num;
		}
		else
		{
			Angle = 0f;
		}
	}
}
