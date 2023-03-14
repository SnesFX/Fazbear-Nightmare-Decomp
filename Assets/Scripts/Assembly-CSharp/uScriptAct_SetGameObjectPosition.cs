using UnityEngine;

[NodePath("Actions/GameObjects/Movement")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Position")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Set Position", "Sets the position (Vector3) of a GameObject as world coordinates.")]
[NodeToolTip("Sets the position (Vector3) of a GameObject as world coordinates.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_SetGameObjectPosition : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The Target GameObject(s) to set the position of.")] GameObject[] Target, [FriendlyName("Position", "The position to set the Target GameObjects to. If \"As Offset\" is enabled, this value will be used as an offest from the target's current position.")] Vector3 Position, [SocketState(false, false)][FriendlyName("As Offset", "Whether or not to use Position as an offset from the Target GameObjects' position(s).")] bool AsOffset)
	{
		foreach (GameObject gameObject in Target)
		{
			if (gameObject != null)
			{
				if (AsOffset)
				{
					gameObject.transform.position += Position;
				}
				else
				{
					gameObject.transform.position = Position;
				}
			}
		}
	}
}
