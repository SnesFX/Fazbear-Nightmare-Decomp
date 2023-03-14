[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Variables/Int")]
[NodeToolTip("Sets an integer to the defined value.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Int")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Set Int", "Sets an integer to the defined value.")]
public class uScriptAct_SetInt : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The variable you wish to use to set the target's value.")] int Value, [FriendlyName("Target", "The Target variable you wish to set.")] out int Target)
	{
		Target = Value;
	}
}
