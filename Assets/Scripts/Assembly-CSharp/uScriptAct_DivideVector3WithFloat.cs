using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Math/Vectors")]
[NodeToolTip("Divides a Vector3 with a Float.")]
[FriendlyName("Divide Vector3 With Float", "Divides a Vector3 with a Float. This is useful for dividing things like Delta Time with a Vector3 velocity for example.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_DivideVector3WithFloat : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Vector3", "The Vector3 you wish to divide with.")] Vector3 targetVector3, [FriendlyName("Float", "The Float you wish to divide with.")] float targetFloat, [FriendlyName("Result", "The Vector3 result of the operation.")] out Vector3 Result)
	{
		Result = targetVector3 / targetFloat;
	}
}
