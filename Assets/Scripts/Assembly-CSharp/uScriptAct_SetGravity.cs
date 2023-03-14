using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Gravity")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Sets the gravity applied to all rigid bodies.")]
[FriendlyName("Set Gravity", "Sets the gravity force and direction applied to all rigid bodies. Use a zero Vector3 to turn gravity off (0, 0, 0).")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Physics")]
public class uScriptAct_SetGravity : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Gravity", "Defines the force and direction of gravity.")] Vector3 Gravity)
	{
		Physics.gravity = Gravity;
	}
}
