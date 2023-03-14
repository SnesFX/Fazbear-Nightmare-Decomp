[NodePath("Conditions/Switches")]
[FriendlyName("String Switch", "Fires out any socket where the target matches its corresponding socket value.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeToolTip("Fires out any socket where the target matches its corresponding socket value.")]
public class uScriptCon_StringSwitch : uScriptLogic
{
	private bool m_CompareValueNone;

	private bool m_CompareValueAny;

	private bool m_CompareValueAll;

	private bool m_CompareValueA;

	private bool m_CompareValueB;

	private bool m_CompareValueC;

	private bool m_CompareValueD;

	[FriendlyName("None", "Will fire if no output socket value matches a target value.")]
	public bool None
	{
		get
		{
			return m_CompareValueNone;
		}
	}

	[FriendlyName("Any", "Will fire if any output socket value matches a target value.")]
	public bool Any
	{
		get
		{
			return m_CompareValueAny;
		}
	}

	[FriendlyName("All", "Will fire if all the output socket values matches a target value.")]
	public bool All
	{
		get
		{
			return m_CompareValueAll;
		}
	}

	[FriendlyName("A Matched", "Will fire if the A output socket value matches a target value.")]
	public bool AMatch
	{
		get
		{
			return m_CompareValueA;
		}
	}

	[FriendlyName("B Matched", "Will fire if the B output socket value matches a target value.")]
	public bool BMatch
	{
		get
		{
			return m_CompareValueB;
		}
	}

	[FriendlyName("C Matched", "Will fire if the C output socket value matches a target value.")]
	public bool CMatch
	{
		get
		{
			return m_CompareValueC;
		}
	}

	[FriendlyName("D Matched", "Will fire if the D output socket value matches a target value.")]
	public bool DMatch
	{
		get
		{
			return m_CompareValueD;
		}
	}

	public void In([FriendlyName("Target", "The string value to compare against the socket values to determine which out sockets should fire.")] string[] Targets, [FriendlyName("A", "A ouput socket value.")] string A, [FriendlyName("B", "B ouput socket value.")] string B, [FriendlyName("C", "C ouput socket value.")] string C, [FriendlyName("D", "D ouput socket value.")] string D)
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		bool compareValueNone = false;
		bool compareValueAny = false;
		bool compareValueAll = false;
		foreach (string text in Targets)
		{
			if (!flag)
			{
				flag = text == A;
			}
			if (!flag2)
			{
				flag2 = text == B;
			}
			if (!flag3)
			{
				flag3 = text == C;
			}
			if (!flag4)
			{
				flag4 = text == D;
			}
		}
		if (flag || flag2 || flag3 || flag4)
		{
			compareValueAny = true;
		}
		else
		{
			compareValueNone = true;
		}
		if (flag && flag2 && flag3 && flag4)
		{
			compareValueAll = true;
		}
		m_CompareValueA = flag;
		m_CompareValueB = flag2;
		m_CompareValueC = flag3;
		m_CompareValueD = flag4;
		m_CompareValueNone = compareValueNone;
		m_CompareValueAny = compareValueAny;
		m_CompareValueAll = compareValueAll;
	}
}
