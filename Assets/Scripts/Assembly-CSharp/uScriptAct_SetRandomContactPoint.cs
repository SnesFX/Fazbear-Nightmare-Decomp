using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Randomly sets the value of a ContactPoint variable from a set of choices.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Set Random ContactPoint", "Randomly sets the value of a ContactPoint variable from a set of choices.")]
[NodePath("Actions/Variables/ContactPoint")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
public class uScriptAct_SetRandomContactPoint : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("ContactPoint", "The ContactPoint to randomly choose from. Connect a ContactPoint List or individual ContactPoint variables to this socket.")] ContactPoint[] ObjectSet, [FriendlyName("Target ContactPoint", "The ContactPoint value that gets set.")] out ContactPoint Target)
	{
		if (ObjectSet == null)
		{
			Target = default(ContactPoint);
			return;
		}
		int num = Random.Range(0, ObjectSet.Length);
		Target = ObjectSet[num];
	}
}
