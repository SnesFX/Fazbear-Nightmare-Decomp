[NodePath("Actions/Math/Bool")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Invert_Bool")]
[NodeToolTip("Inverse a boolean variable to its opposite value.")]
[FriendlyName("Invert Bool", "Returns the inverse value of a boolean variable.\n\nExamples:\n\tTrue -> False\n\tFalse -> True")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_InvertBool : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "Value to invert.")] bool Target, [FriendlyName("Value", "The inverted value.")] out bool Value)
	{
		Value = !Target;
	}
}
