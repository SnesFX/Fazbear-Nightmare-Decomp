[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Conditions/Comparison")]
[NodeAuthor("Detox Studios LLC. Original node by SvdV on the uScript Community Forum", "http://www.detoxstudios.com")]
[FriendlyName("Between Floats", "Checks to see if the Target float is between a minimum and maximum range.")]
[NodeToolTip("Checks to see if the Target float is between a minimum and maximum range.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptCon_BetweenFloats : uScriptLogic
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

	public void In([FriendlyName("Target", "The target float variable to compare against the range.")] float Target, [FriendlyName("Min", "The minimum value of the range. This value is inclusive.")] float Min, [FriendlyName("Max", "The maximum value of the range. This value is inclusive.")] float Max)
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
