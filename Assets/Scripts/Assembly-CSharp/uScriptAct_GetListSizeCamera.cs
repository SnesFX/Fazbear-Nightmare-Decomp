using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Get the number of things currently in the list.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Get List Size (Camera)", "Get the number of things currently in the list.")]
[NodePath("Actions/Variables/Lists/Camera")]
public class uScriptAct_GetListSizeCamera : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The list to get the size on.")] Camera[] List, [FriendlyName("Size", "The size of the list specified.")] out int ListSize)
	{
		ListSize = List.Length;
	}
}
