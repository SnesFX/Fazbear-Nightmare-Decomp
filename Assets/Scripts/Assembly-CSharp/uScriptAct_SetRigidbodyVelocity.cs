using UnityEngine;

[NodePath("Actions/Physics")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Set Rigidbody Velocity", "Sets the velocity of a GameObject's Rigidbody as a Vector3.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Rigidbody_Velocity")]
[NodeToolTip("Sets the velocity of a GameObject's Rigidbody as a Vector3.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_SetRigidbodyVelocity : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "GameObject(s) to set the velocity of.")] GameObject[] Target, [FriendlyName("Velocity", "The velocity to give to the rigidbody component attached to the Target GameObject(s).")] Vector3 Velocity)
	{
		foreach (GameObject gameObject in Target)
		{
			if (gameObject != null && gameObject.GetComponent<Rigidbody>() != null)
			{
				gameObject.GetComponent<Rigidbody>().velocity = Velocity;
			}
		}
	}
}
