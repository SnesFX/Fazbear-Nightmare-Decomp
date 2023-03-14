using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Randomly sets the value of a AudioClip variable from a set of choices.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/AudioClip")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Set Random AudioClip", "Randomly sets the value of a AudioClip variable from a set of choices.")]
public class uScriptAct_SetRandomAudioClip : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("AudioClips", "The AudioClip to randomly choose from. Connect AudioClip variables to this socket.")] AudioClip[] ObjectSet, [FriendlyName("Target AudioClip", "The AudioClip value that gets set.")] out AudioClip TargetAudioClip)
	{
		if (ObjectSet == null)
		{
			TargetAudioClip = null;
			return;
		}
		int num = Random.Range(0, ObjectSet.Length);
		TargetAudioClip = ObjectSet[num];
	}
}
