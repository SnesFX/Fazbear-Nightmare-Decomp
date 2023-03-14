using UnityEngine;

[FriendlyName("Set Tag", "Sets the tag of a GameObject. The tag must be created in Unity's tag manager before using them with this node. Use \"Untagged\" for the tag name if you wish to remove an existing tag.")]
[NodeToolTip("Sets the tag of a GameObject.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/GameObjects")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_SetTag : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The GameObject(s) you wish to assign the existing tag to.")] GameObject[] Target, [FriendlyName("Tag", "The tag(s) you wish to assign to the Target GameObject(s)")] string Tags)
	{
		foreach (GameObject gameObject in Target)
		{
			if (null != gameObject)
			{
				gameObject.tag = Tags;
			}
		}
	}
}
