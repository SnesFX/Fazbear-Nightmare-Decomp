using UnityEngine;

[NodeToolTip("Get the number of things currently in the list.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Get List Size (GameObject)", "Get the number of things currently in the list.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Lists/GameObject")]
public class uScriptAct_GetListSizeGameObject : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The list to get the size on.")] GameObject[] List, [FriendlyName("Size", "The size of the list specified.")] out int ListSize)
	{
		ListSize = List.Length;
	}
}
