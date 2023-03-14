using UnityEngine;

[NodeToolTip("Compares two Vector2 variables and outputs accordingly.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Compare Vector2", "Compares two Vector2 variables and outputs accordingly.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Compare_Vector2")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Conditions/Comparison")]
public class uScriptCon_CompareVector2 : uScriptLogic
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

	public void In([FriendlyName("A", "First value to compare.")] Vector2 A, [FriendlyName("B", "Second value to compare.")] Vector2 B)
	{
		m_CompareValue = A == B;
	}
}
