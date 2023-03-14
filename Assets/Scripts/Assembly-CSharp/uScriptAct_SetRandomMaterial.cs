using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Material")]
[NodeToolTip("Randomly sets the value of a Material variable from a set of choices.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Set Random Material", "Randomly sets the value of a Material variable from a set of choices.")]
public class uScriptAct_SetRandomMaterial : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Material", "The Material to randomly choose from. Connect an Material List or individual Material variables to this socket.")] Material[] ObjectSet, [FriendlyName("Target Material", "The Material value that gets set.")] out Material Target)
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
