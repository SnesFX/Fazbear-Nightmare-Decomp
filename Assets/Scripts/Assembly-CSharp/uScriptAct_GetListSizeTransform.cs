using UnityEngine;

[FriendlyName("Get List Size (Transform)", "Get the number of things currently in the list.")]
[NodePath("Actions/Variables/Lists/Transform")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Get the number of things currently in the list.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_GetListSizeTransform : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The list to get the size on.")] Transform[] List, [FriendlyName("Size", "The size of the list specified.")] out int ListSize)
	{
		ListSize = List.Length;
	}
}
