using UnityEngine;

[NodeToolTip("Normalizes the vector.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Normalize_Vector2")]
[FriendlyName("Normalize Vector2", "Normalizes the vector. A normalized vector keeps the same direction but its length is 1.0. If the vector is too small to be normalized, a zero vector will be returned instead.")]
[NodePath("Actions/Math/Vectors")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_NormalizeVector2 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The vector to normalize.")] Vector2 Target, [FriendlyName("Normalized", "The result.")] out Vector2 NormalizedVector)
	{
		NormalizedVector = Target.normalized;
	}
}
