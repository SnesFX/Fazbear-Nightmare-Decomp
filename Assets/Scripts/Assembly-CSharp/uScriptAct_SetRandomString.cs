using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Set Random String", "Randomly sets the value of a String variable from a set of choices.")]
[NodeToolTip("Randomly sets the value of a String variable from a set of choices.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/String")]
public class uScriptAct_SetRandomString : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("String", "The String to randomly choose from. Connect an String List or individual String variables to this socket.")] string[] ObjectSet, [FriendlyName("Target String", "The String value that gets set.")] out string Target)
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
