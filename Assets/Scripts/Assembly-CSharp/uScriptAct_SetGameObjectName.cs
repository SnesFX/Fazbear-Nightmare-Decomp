using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Sets a GameObject's name.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_GameObject_Name")]
[FriendlyName("Set GameObject Name", "Sets the name of a GameObject.")]
[NodePath("Actions/GameObjects")]
public class uScriptAct_SetGameObjectName : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The GameObject's name to change.")] GameObject Target, [FriendlyName("Name", "The new name.")] string Name)
	{
		Target.name = Name;
	}
}
