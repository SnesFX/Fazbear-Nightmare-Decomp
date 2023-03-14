using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Randomly sets the value of a Quaternion variable from a set of choices.")]
[FriendlyName("Set Random Quaternion", "Randomly sets the value of a Quaternion variable from a set of choices.")]
[NodePath("Actions/Variables/Quaternion")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_SetRandomQuaternion : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Quaternion", "The Quaternion to randomly choose from. Connect an Quaternion List or individual Quaternion variables to this socket.")] Quaternion[] ObjectSet, [FriendlyName("Target Quaternion", "The Quaternion value that gets set.")] out Quaternion Target)
	{
		if (ObjectSet == null)
		{
			Target = new Quaternion(0f, 0f, 0f, 0f);
			return;
		}
		int num = Random.Range(0, ObjectSet.Length);
		Target = ObjectSet[num];
	}
}
