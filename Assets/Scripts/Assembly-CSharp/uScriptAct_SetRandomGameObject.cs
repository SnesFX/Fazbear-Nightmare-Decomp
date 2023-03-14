using UnityEngine;

[NodePath("Actions/Variables/GameObject")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Random_GameObject")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Randomly sets the value of a GameObject variable from a set of choices.")]
[FriendlyName("Set Random GameObject", "Randomly sets the value of a GameObject variable from a set of choices.")]
public class uScriptAct_SetRandomGameObject : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("GameObjects", "The GameObjects to randomly choose from. Connect GameObject variables to this socket.")] GameObject[] ObjectSet, [FriendlyName("Target GameObject", "The GameObject value that gets set.")] out GameObject TargetGameObject)
	{
		if (ObjectSet == null)
		{
			TargetGameObject = null;
			return;
		}
		int num = Random.Range(0, ObjectSet.Length);
		TargetGameObject = ObjectSet[num];
	}
}
