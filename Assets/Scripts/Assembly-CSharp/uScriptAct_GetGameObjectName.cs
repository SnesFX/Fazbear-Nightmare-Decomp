using UnityEngine;

[FriendlyName("Get GameObject Name", "Gets the name of a GameObject.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/GameObjects")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_Game_Object_Name")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Gets the name of a GameObject.")]
public class uScriptAct_GetGameObjectName : uScriptLogic
{
	[FriendlyName("Out")]
	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("In")]
	public void In([FriendlyName("Target", "The GameObject.")] GameObject gameObject, [FriendlyName("Name", "The returned name of the GameObject.")] out string name)
	{
		name = gameObject.name;
	}
}
