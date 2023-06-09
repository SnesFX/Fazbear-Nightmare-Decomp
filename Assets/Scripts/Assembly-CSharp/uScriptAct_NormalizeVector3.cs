using UnityEngine;

[NodePath("Actions/Math/Vectors")]
[FriendlyName("Normalize Vector3", "Normalizes the vector. A normalized vector keeps the same direction but its length is 1.0. If the vector is too small to be normalized, a zero vector will be returned instead.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Normalize_Vector3")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Normalizes the vector.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_NormalizeVector3 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The vector to normalize.")] Vector3 Target, [FriendlyName("Normalized", "The result.")] out Vector3 NormalizedVector)
	{
		NormalizedVector = Target.normalized;
	}
}
