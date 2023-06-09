using UnityEngine;

[FriendlyName("Get Angle (Vector3)", "Returns the angle in degrees between target A and target B.")]
[NodePath("Actions/Math/Angles")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Returns the angle in degrees between target A and target B.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_GetAngleVector3 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "The first target.")] Vector3 A, [FriendlyName("B", "The second target.")] Vector3 B, [FriendlyName("Angle", "The resulting angle between the two targets in degrees.")] out float Angle)
	{
		Angle = Vector3.Angle(A, B);
	}
}
