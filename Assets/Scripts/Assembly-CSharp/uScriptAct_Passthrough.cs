[NodeToolTip("Dummy node that just allows the signal to pass through.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Utilities")]
[FriendlyName("Pass", "Dummy node that just allows the signal to pass through. This can bu useful for routing connection lines in your graph.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
public class uScriptAct_Passthrough : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In()
	{
	}
}
