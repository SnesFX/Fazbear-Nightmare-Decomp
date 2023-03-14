using System.Collections.Generic;
using UnityEngine;

[NodePath("Actions/Variables/Lists/Vector3")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Checks to see if a Vector3 is in a Vector3 List.")]
[FriendlyName("Is In List (Vector3)", "Checks to see if a Vector3 is in a Vector3 List.")]
public class uScriptAct_IsInListVector3 : uScriptLogic
{
	private bool m_InList;

	[FriendlyName("In List")]
	public bool InList
	{
		get
		{
			return m_InList;
		}
	}

	[FriendlyName("Not In List")]
	public bool NotInList
	{
		get
		{
			return !m_InList;
		}
	}

	[FriendlyName("Test If In List")]
	public void TestIfInList([FriendlyName("Target", "The target Vector3(s) to check for membership in the Vector3 List.")] Vector3[] Target, [FriendlyName("Vector3 List", "The Vector3 List to check.")] ref Vector3[] List)
	{
		List<Vector3> list = new List<Vector3>(List);
		m_InList = false;
		foreach (Vector3 item in Target)
		{
			if (!list.Contains(item))
			{
				return;
			}
		}
		m_InList = true;
	}
}
