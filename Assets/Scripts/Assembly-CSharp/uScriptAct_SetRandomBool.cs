using UnityEngine;

[NodeToolTip("Randomly sets the value of a Bool variable.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Set Random Bool", "Randomly sets the value of a Bool variable to True or False.")]
[NodePath("Actions/Variables/Bool")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Random_Bool")]
public class uScriptAct_SetRandomBool : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target Bool", "The random bool value that gets set.")] out bool TargetBool)
	{
		TargetBool = Random.Range(0, 2) > 0;
	}
}
