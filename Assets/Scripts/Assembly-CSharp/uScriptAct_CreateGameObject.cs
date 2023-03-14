using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/GameObjects")]
[FriendlyName("Create GameObject", "Creates a new GameObject at the specified location.")]
[NodeToolTip("Creates a new GameObject at the specified location.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
public class uScriptAct_CreateGameObject : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Name", "The name given to the new GameObject.")][DefaultValue("GameObject")] string Name, [FriendlyName("Location", "The world location where to place the new GameObject.")][SocketState(false, false)] Vector3 Location, [FriendlyName("GameObject", "The new GameObject.")] out GameObject newGameObject)
	{
		newGameObject = new GameObject(Name);
		newGameObject.transform.position = Location;
	}
}
