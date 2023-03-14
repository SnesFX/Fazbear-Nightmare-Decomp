[NodePath("Conditions/Comparison")]
[FriendlyName("Between Ints", "Checks to see if the Target int is between a minimum and maximum range.")]
[NodeToolTip("Checks to see if the Target float is between a minimum and maximum range.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC. Original node by SvdV on the uScript Community Forum", "http://www.detoxstudios.com")]
public class uScriptCon_BetweenInts : uScriptLogic
{
	private bool m_Between;

	public bool True
	{
		get
		{
			return m_Between;
		}
	}

	public bool False
	{
		get
		{
			return !m_Between;
		}
	}

	public void In([FriendlyName("Target", "The target float variable to compare against the range.")] int Target, [FriendlyName("Min", "The minimum value of the range. This value is inclusive.")] int Min, [FriendlyName("Max", "The maximum value of the range. This value is inclusive.")] int Max)
	{
		if (Min > Max || Min == Max)
		{
			m_Between = false;
		}
		else if (Target >= Min && Target <= Max)
		{
			m_Between = true;
		}
		else
		{
			m_Between = false;
		}
	}
}
