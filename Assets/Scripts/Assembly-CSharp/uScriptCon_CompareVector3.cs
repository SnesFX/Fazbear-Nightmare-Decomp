using UnityEngine;

[FriendlyName("Compare Vector3", "Compares two Vector3 variables and outputs accordingly.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Compare_Vector3")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Compares two Vector3 variables and outputs accordingly.")]
[NodePath("Conditions/Comparison")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptCon_CompareVector3 : uScriptLogic
{
	private bool m_CompareValue;

	public bool Same
	{
		get
		{
			return m_CompareValue;
		}
	}

	public bool Different
	{
		get
		{
			return !m_CompareValue;
		}
	}

	public void In([FriendlyName("A", "First value to compare.")] Vector3 A, [FriendlyName("B", "Second value to compare.")] Vector3 B)
	{
		m_CompareValue = A == B;
	}
}
