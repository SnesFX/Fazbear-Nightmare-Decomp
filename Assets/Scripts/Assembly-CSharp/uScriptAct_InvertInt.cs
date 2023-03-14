[NodeToolTip("Takes any non-zero target integer and outputs its inverse version.")]
[FriendlyName("Invert Int", "Returns the inverse value of an integer variable.\n\nExamples:\n\t3 -> -3\n\t-1 -> 1")]
[NodePath("Actions/Math/Int")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Invert_Int")]
public class uScriptAct_InvertInt : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "Value to invert.")] int Target, [FriendlyName("Value", "The inverted value.")] out int Value)
	{
		Value = -Target;
	}
}
