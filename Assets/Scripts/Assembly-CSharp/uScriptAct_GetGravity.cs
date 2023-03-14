using UnityEngine;

[FriendlyName("Get Gravity", "Gets the current gravity as a Vector3.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_Gravity")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Physics")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Gets the current gravity as a Vector3.")]
public class uScriptAct_GetGravity : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Gravity", "Returns the current gravity value.")] out Vector3 Gravity)
	{
		Gravity = Physics.gravity;
	}
}
