[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Sets a float to the defined value.")]
[NodePath("Actions/Variables/Float")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Float")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Set Float", "Sets a float to the defined value.")]
public class uScriptAct_SetFloat : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The variable you wish to use to set the target's value.")] float Value, [FriendlyName("Target", "The Target variable you wish to set.")] out float Target)
	{
		Target = Value;
	}
}
