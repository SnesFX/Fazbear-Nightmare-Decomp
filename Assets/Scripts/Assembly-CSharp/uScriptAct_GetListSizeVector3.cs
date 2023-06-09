using UnityEngine;

[NodePath("Actions/Variables/Lists/Vector3")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeToolTip("Get the number of things currently in the list.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Get List Size (Vector3)", "Get the number of things currently in the list.")]
public class uScriptAct_GetListSizeVector3 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The list to get the size on.")] Vector3[] List, [FriendlyName("Size", "The size of the list specified.")] out int ListSize)
	{
		ListSize = List.Length;
	}
}
