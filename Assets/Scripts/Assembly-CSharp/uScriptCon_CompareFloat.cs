[NodePath("Conditions/Comparison")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Fires the appropriate output link(s) depending on the comparison of the attached float variables.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Compare_Float")]
[FriendlyName("Compare Float", "Fires the appropriate output link(s) depending on the comparison of the attached float variables.")]
public class uScriptCon_CompareFloat : uScriptLogic
{
	private bool m_GreaterThan;

	private bool m_GreaterThanOrEqualTo;

	private bool m_EqualTo;

	private bool m_LessThanOrEqualTo;

	private bool m_LessThan;

	[FriendlyName("(Greater Than)   >")]
	public bool GreaterThan
	{
		get
		{
			return m_GreaterThan;
		}
	}

	[FriendlyName("(Greater Than or Equal To) >=")]
	public bool GreaterThanOrEqualTo
	{
		get
		{
			return m_GreaterThanOrEqualTo;
		}
	}

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

	[FriendlyName("(Less Than or Equal To) <=")]
	public bool LessThanOrEqualTo
	{
		get
		{
			return m_LessThanOrEqualTo;
		}
	}

	[FriendlyName("(Less Than)   <")]
	public bool LessThan
	{
		get
		{
			return m_LessThan;
		}
	}

	public void In([FriendlyName("A", "First value to compare.")] float A, [FriendlyName("B", "Second value to compare.")] float B)
	{
		m_GreaterThan = false;
		m_GreaterThanOrEqualTo = false;
		m_EqualTo = false;
		m_LessThanOrEqualTo = false;
		m_LessThan = false;
		if (A > B)
		{
			m_GreaterThan = true;
		}
		if (A >= B)
		{
			m_GreaterThanOrEqualTo = true;
		}
		if (A == B)
		{
			m_EqualTo = true;
		}
		if (A <= B)
		{
			m_LessThanOrEqualTo = true;
		}
		if (A < B)
		{
			m_LessThan = true;
		}
	}
}
