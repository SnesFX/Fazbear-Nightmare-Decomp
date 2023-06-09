using UnityEngine;

[NodePath("Actions/Variables/AudioClip")]
[NodeToolTip("Sets the value of a AudioClip variable using the value of another AudioClip variable.")]
[FriendlyName("Set AudioClip", "Sets the value of a AudioClip variable using the value of another AudioClip variable.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_GameObject")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_SetAudioClip : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The variable you wish to use to set the target's value.")] AudioClip Value, [FriendlyName("Target", "The Target variable you wish to set.")] out AudioClip TargetGameObject)
	{
		TargetGameObject = Value;
	}
}
