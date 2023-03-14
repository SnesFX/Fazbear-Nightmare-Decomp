using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Teleport", "Causes the targeted GameObject to be relocated to the destination GameObject.")]
[NodeToolTip("Causes the targeted GameObject to be relocated to the destination GameObject.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/GameObjects/Movement")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Teleport")]
public class uScriptAct_Teleport : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The Target GameObject(s) to teleport.")] GameObject[] Target, [FriendlyName("Destination", "The destination GameObject to teleport to.")] GameObject Destination, [FriendlyName("Update Rotation", "Whether or not to also update the rotation of the teleported GameObject to match the Destination's rotation.")][SocketState(false, false)] bool UpdateRotation)
	{
		foreach (GameObject gameObject in Target)
		{
			if (gameObject != null && Destination != null)
			{
				gameObject.transform.position = Destination.transform.position;
				if (UpdateRotation)
				{
					gameObject.transform.rotation = Destination.transform.rotation;
				}
			}
		}
	}
}
