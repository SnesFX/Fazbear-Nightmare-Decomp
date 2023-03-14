using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Compare_Vector4")]
[NodePath("Conditions/Comparison")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Compares two Vector4 variables and outputs accordingly.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Compare Vector4", "Compares two Vector4 variables and outputs accordingly.")]
public class uScriptCon_CompareVector4 : uScriptLogic
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

	public void In([FriendlyName("A", "First value to compare.")] Vector4 A, [FriendlyName("B", "Second value to compare.")] Vector4 B)
	{
		m_CompareValue = A == B;
	}
}
