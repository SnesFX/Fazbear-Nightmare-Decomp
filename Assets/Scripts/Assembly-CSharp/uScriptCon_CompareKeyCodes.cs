using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Compare_Float")]
[FriendlyName("Compare KeyCodes", "Fires the appropriate output link depending on the comparison of the attached KeyCode variables.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Fires the appropriate output link depending on the comparison of the attached KeyCode variables.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Conditions/Comparison")]
public class uScriptCon_CompareKeyCodes : uScriptLogic
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

	public void In([FriendlyName("A", "First value to compare.")] KeyCode A, [FriendlyName("B", "Second value to compare.")] KeyCode B)
	{
		m_EqualTo = false;
		if (A == B)
		{
			m_EqualTo = true;
		}
	}
}
