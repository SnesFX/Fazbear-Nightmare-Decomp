using UnityEngine;

[NodePath("Conditions/Comparison")]
[NodeToolTip("Checks the distance of two GameObjects against a specified distance.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Check Distance", "Checks the distance of two GameObjects against a specified distance and fires the appropriate output.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Check_Distance")]
public class uScriptCon_CheckDistance : uScriptLogic
{
	private bool m_Closer;

	private bool m_Further;

	private bool m_Equal;

	public bool Closer
	{
		get
		{
			return m_Closer;
		}
	}

	public bool Further
	{
		get
		{
			return m_Further;
		}
	}

	public bool Equal
	{
		get
		{
			return m_Equal;
		}
	}

	public void In([FriendlyName("A", "First GameObject.")] GameObject A, [FriendlyName("B", "Second GameObject.")] GameObject B, [FriendlyName("Distance", "The distance value for the test.")] float Distance)
	{
		m_Closer = false;
		m_Further = false;
		m_Equal = false;
		if (A != null && B != null)
		{
			float sqrMagnitude = (A.transform.position - B.transform.position).sqrMagnitude;
			if (sqrMagnitude < Distance * Distance)
			{
				m_Closer = true;
			}
			else if (sqrMagnitude == Distance * Distance)
			{
				m_Equal = true;
			}
			else
			{
				m_Further = true;
			}
		}
	}
}
