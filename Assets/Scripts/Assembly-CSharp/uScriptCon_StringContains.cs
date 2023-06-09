[FriendlyName("String Contains", "Determines if the target string contains the specified text.")]
[NodePath("Conditions/Comparison")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeToolTip("Determines if the target string contains the specified text.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptCon_StringContains : uScriptLogic
{
	private bool m_ContainsValue;

	[FriendlyName("True")]
	public bool True
	{
		get
		{
			return m_ContainsValue;
		}
	}

	[FriendlyName("False")]
	public bool False
	{
		get
		{
			return !m_ContainsValue;
		}
	}

	public void In([FriendlyName("Target", "The target string you wish to check.")] string Target, [FriendlyName("Value", "The text you want to search for in the Target string.")] string Value)
	{
		m_ContainsValue = Target.Contains(Value);
	}
}
