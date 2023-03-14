using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Conditions/Comparison")]
[NodeToolTip("Determines if the target Int is a power of two number.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Is_Power_of_Two")]
[FriendlyName("Is Power of Two", "Determines if the target Int is a power of two number.")]
public class uScriptCon_IsPowerOfTwo : uScriptLogic
{
	private bool m_IsPOT;

	[FriendlyName("True")]
	public bool True
	{
		get
		{
			return m_IsPOT;
		}
	}

	[FriendlyName("False")]
	public bool False
	{
		get
		{
			return !m_IsPOT;
		}
	}

	public void In([FriendlyName("Target", "The integer variable to compare.")] int Target)
	{
		m_IsPOT = Mathf.IsPowerOfTwo(Target);
	}
}
