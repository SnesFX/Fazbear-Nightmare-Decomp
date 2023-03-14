using UnityEngine;

[NodeToolTip("Randomly sets the value of a Rect variable from a set of choices.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Rect")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Set Random Rect", "Randomly sets the value of a Rect variable from a set of choices.")]
public class uScriptAct_SetRandomRect : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Rect", "The Rect to randomly choose from. Connect an Rect List or individual Rect variables to this socket.")] Rect[] ObjectSet, [FriendlyName("Target Rect", "The Rect value that gets set.")] out Rect Target)
	{
		if (ObjectSet == null)
		{
			Target = new Rect(0f, 0f, 0f, 0f);
			return;
		}
		int num = Random.Range(0, ObjectSet.Length);
		Target = ObjectSet[num];
	}
}
