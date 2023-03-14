using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Math/Vectors")]
[NodeToolTip("Normalizes the vector.")]
[FriendlyName("Normalize Vector4", "Normalizes the vector. A normalized vector keeps the same direction but its length is 1.0. If the vector is too small to be normalized, a zero vector will be returned instead.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Normalize_Vector4")]
public class uScriptAct_NormalizeVector4 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The vector to normalize.")] Vector4 Target, [FriendlyName("Normalized", "The result.")] out Vector4 NormalizedVector)
	{
		NormalizedVector = Target.normalized;
	}
}
