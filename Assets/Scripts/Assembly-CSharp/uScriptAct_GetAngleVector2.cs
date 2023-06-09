using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Get Angle (Vector2)", "Returns the angle in degrees between target A and target B.")]
[NodeToolTip("Returns the angle in degrees between target A and target B.")]
[NodePath("Actions/Math/Angles")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_GetAngleVector2 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "The first target.")] Vector2 A, [FriendlyName("B", "The second target.")] Vector2 B, [FriendlyName("Angle", "The resulting angle between the two targets in degrees.")] out float Angle)
	{
		Angle = Vector2.Angle(A, B);
	}
}
