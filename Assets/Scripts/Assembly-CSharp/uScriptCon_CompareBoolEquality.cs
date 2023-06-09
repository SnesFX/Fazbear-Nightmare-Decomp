[NodePath("Conditions/Comparison")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Compare_Bool_Equality")]
[FriendlyName("Compare Bool Equality", "Fires the appropriate output link(s) depending on the comparison of the attached bool variables.")]
[NodeToolTip("Fires the appropriate output link(s) depending on the comparison of the attached bool variables.")]
public class uScriptCon_CompareBoolEquality : uScriptLogic
{
	private bool m_EqualTo;

	[FriendlyName("(Equal To)   =")]
	public bool EqualTo
	{
		get
		{
			return m_EqualTo;
		}
	}

	[FriendlyName("(Not Equal To)  !=")]
	public bool NotEqualTo
	{
		get
		{
			return !m_EqualTo;
		}
	}

	public void In([FriendlyName("A", "First value to compare.")] bool A, [FriendlyName("B", "Second value to compare.")] bool B)
	{
		m_EqualTo = false;
		if (A == B)
		{
			m_EqualTo = true;
		}
	}
}
