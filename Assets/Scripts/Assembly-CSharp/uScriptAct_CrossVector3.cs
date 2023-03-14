using UnityEngine;

[NodeToolTip("Cross product of two Vector3 variables.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Math/Vectors")]
[FriendlyName("Cross (Vector3)", "Cross Product of two Vector3 variables.\n\n[ Cross(A,B) ]")]
public class uScriptAct_CrossVector3 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "The first variable.")] Vector3 A, [FriendlyName("B", "The second variable.")] Vector3 B, [FriendlyName("Result", "The resulting cross product.")] out Vector3 Result)
	{
		Result = Vector3.Cross(A, B);
	}
}
