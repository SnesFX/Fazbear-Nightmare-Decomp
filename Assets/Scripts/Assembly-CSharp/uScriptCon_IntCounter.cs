[FriendlyName("Int Counter", "Increments the first attached integer variable and then performs a comparison with the second attached integer variable and fires the appropriate output link based on that comparison.")]
[NodeToolTip("Increments the first attached integer variable and then performs a comparison with the second attached integer variable and fires the appropriate output link based on that comparison.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Conditions/Counter")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Int_Counter")]
public class uScriptCon_IntCounter : uScriptLogic
{
	private int m_IntTotal;

	private bool m_InitialPulse = true;

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

	public void In(int A, int B, out int currentValue)
	{
		m_GreaterThan = false;
		m_GreaterThanOrEqualTo = false;
		m_EqualTo = false;
		m_LessThanOrEqualTo = false;
		m_LessThan = false;
		if (m_InitialPulse)
		{
			m_IntTotal = A;
			m_InitialPulse = false;
		}
		m_IntTotal++;
		currentValue = m_IntTotal;
		if (m_IntTotal > B)
		{
			m_GreaterThan = true;
		}
		if (m_IntTotal >= B)
		{
			m_GreaterThanOrEqualTo = true;
		}
		if (m_IntTotal == B)
		{
			m_EqualTo = true;
		}
		if (m_IntTotal <= B)
		{
			m_LessThanOrEqualTo = true;
		}
		if (m_IntTotal < B)
		{
			m_LessThan = true;
		}
	}

	public void Reset([FriendlyName("A", "Variable to increment.")] int A, [FriendlyName("B", "Variable to compare with incremented variable.")] int B, [FriendlyName("Current Value", "Value of A after increment has taken place.")] out int currentValue)
	{
		m_InitialPulse = true;
		m_GreaterThan = false;
		m_GreaterThanOrEqualTo = false;
		m_EqualTo = false;
		m_LessThanOrEqualTo = false;
		m_LessThan = false;
		currentValue = A;
	}
}
