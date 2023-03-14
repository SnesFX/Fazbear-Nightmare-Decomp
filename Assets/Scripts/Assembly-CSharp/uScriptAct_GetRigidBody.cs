using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_Rigidbody")]
[NodePath("Actions/Physics")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Gets the Rigidbody component of a GameObject.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Get Rigidbody", "Gets the Rigidbody component of a GameObject.")]
public class uScriptAct_GetRigidBody : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "GameObject which contains a Rigidbody.")] GameObject Target, [FriendlyName("Rigidbody", "The Rigidbody of the GameObject.")] out Rigidbody rigidBody)
	{
		rigidBody = Target.GetComponent<Rigidbody>();
	}
}
