using UnityEngine;

[NodeToolTip("Returns true while the virtual button identified by Button Name is held down.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Events/Filters")]
[FriendlyName("Get Button", "Returns true while the virtual button identified by Button Name is held down. This will return true as long as the button is held down. Note, for detecting standard keyboard and mouse button presses, you should use the Input Filter node.\n\nFor use with the Input Events event node.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_GetButton : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Button Name", "The name of the virtual button. Example - 'Fire1'")] string buttonName, [FriendlyName("Result", "Returns true while the virtual button specified is held down. Think auto fire - this will return true as long as the button is held down.")] out bool result)
	{
		result = Input.GetButton(buttonName);
	}
}
