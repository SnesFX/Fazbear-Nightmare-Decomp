[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Toggle_Bool")]
[FriendlyName("Toggle Bool", "Toggles a boolean variable.")]
[NodeToolTip("Toggles a boolean variable.")]
[NodePath("Actions/Toggle")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_ToggleBool : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Toggle")]
	public void Toggle([FriendlyName("Target", "The Target bool(s) to toggle.")] bool Target, [FriendlyName("Result", "The toggled result.")] out bool Result)
	{
		Result = !Target;
	}
}
