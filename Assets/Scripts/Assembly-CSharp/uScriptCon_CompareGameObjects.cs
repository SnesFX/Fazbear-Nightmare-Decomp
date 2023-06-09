using UnityEngine;

[NodePath("Conditions/Comparison")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Compares the unique tag, name and InstanceID of the attached GameObject variables and outputs accordingly.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Compare_GameObjects")]
[FriendlyName("Compare GameObjects", "Compares the unique InstanceID of the attached GameObject variables and outputs accordingly.\n\nOptionally, you can compare by a GameObject's tag and/or name instead.  Please note, if Compare By Name and Compare By Tag are both checked, they must both match for the 'Same' signal to fire.")]
public class uScriptCon_CompareGameObjects : uScriptLogic
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

	public void In([FriendlyName("A", "The first GameObject.")] GameObject A, [FriendlyName("B", "The second GameObject.")] GameObject B, [SocketState(false, false)][FriendlyName("Compare By Tag", "Whether or not to compare the GameObjects' tags instead of the objects themselves.")] bool CompareByTag, [SocketState(false, false)][FriendlyName("Compare By Name", "Whether or not to compare the GameObjects' names instead of the objects themselves.")] bool CompareByName, [FriendlyName("Report Null", "Whether or not to report null GameObjects in the console.")][SocketState(false, false)][DefaultValue(true)] bool ReportNull)
	{
		m_CompareValue = false;
		if (ReportNull)
		{
			if (null == A)
			{
				uScriptDebug.Log("Compare GameObjects A is null", uScriptDebug.Type.Warning);
			}
			if (null == B)
			{
				uScriptDebug.Log("Compare GameObjects B is null", uScriptDebug.Type.Warning);
			}
		}
		if (null == A || null == B)
		{
			return;
		}
		if (CompareByTag || CompareByName)
		{
			m_CompareValue = true;
			if (CompareByTag)
			{
				m_CompareValue = m_CompareValue && A.tag == B.tag;
			}
			if (CompareByName)
			{
				m_CompareValue = m_CompareValue && A.name == B.name;
			}
		}
		else
		{
			m_CompareValue = A.GetInstanceID() == B.GetInstanceID();
		}
	}
}
