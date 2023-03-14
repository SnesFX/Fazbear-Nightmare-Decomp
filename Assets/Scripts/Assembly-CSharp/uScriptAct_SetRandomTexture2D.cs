using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Set Random Texture2D", "Randomly sets the value of a Texture2D variable from a set of choices.")]
[NodePath("Actions/Variables/Texture2D")]
[NodeToolTip("Randomly sets the value of a Texture2D variable from a set of choices.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_SetRandomTexture2D : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Texture2D", "The Texture2D to randomly choose from. Connect an Texture2D List or individual Texture2D variables to this socket.")] Texture2D[] ObjectSet, [FriendlyName("Target Texture2D", "The Texture2D value that gets set.")] out Texture2D Target)
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
