[NodePath("Conditions/Comparison")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Compare_String")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Compares two string variables and outputs accordingly.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Compare String", "Compares two string variables and outputs accordingly.")]
public class uScriptCon_CompareString : uScriptLogic
{
	private bool m_CompareValue;

	public bool Same
	{
		get
		{
			return m_CompareValue;
		}
	}

	public bool Different
	{
		get
		{
			return !m_CompareValue;
		}
	}

	public void In([FriendlyName("A", "First value to compare.")] string A, [FriendlyName("B", "Second value to compare.")] string B)
	{
		m_CompareValue = A == B;
	}
}
