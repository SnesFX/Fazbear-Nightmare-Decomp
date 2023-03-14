[NodeToolTip("Fires the appropriate output link depending on the comparison of the attached bool variable.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Compare Bool", "Fires the appropriate output link depending on the comparison of the attached bool variable.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Conditions/Comparison")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Compare_Bool")]
public class uScriptCon_CompareBool : uScriptLogic
{
	private bool m_CompareValue;

	public bool True
	{
		get
		{
			return m_CompareValue;
		}
	}

	public bool False
	{
		get
		{
			return !m_CompareValue;
		}
	}

	public void In([FriendlyName("Bool", "The boolean value to compare.")] bool Bool)
	{
		m_CompareValue = Bool;
	}
}
