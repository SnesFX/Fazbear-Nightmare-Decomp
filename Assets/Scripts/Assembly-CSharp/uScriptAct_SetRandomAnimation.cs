using UnityEngine;

[FriendlyName("Set Random Animation", "Randomly sets the value of a Animation variable from a set of choices.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Variables/Animation")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Randomly sets the value of a Animation variable from a set of choices.")]
public class uScriptAct_SetRandomAnimation : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Animation", "The Animation to randomly choose from. Connect an Animation List or individual Animation variables to this socket.")] Animation[] ObjectSet, [FriendlyName("Target Animation", "The Animation value that gets set.")] out Animation Target)
	{
		if (ObjectSet == null)
		{
			Target = null;
			return;
		}
		int num = Random.Range(0, ObjectSet.Length);
		Target = ObjectSet[num];
	}
}
