using System.Collections.Generic;
using UnityEngine;

[NodeToolTip("Checks to see if a Vector4 is in a Vector4 List.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Is In List (Vector4)", "Checks to see if a Vector4 is in a Vector4 List.")]
[NodePath("Actions/Variables/Lists/Vector4")]
public class uScriptAct_IsInListVector4 : uScriptLogic
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
	public void TestIfInList([FriendlyName("Target", "The target Vector4(s) to check for membership in the Vector4 List.")] Vector4[] Target, [FriendlyName("Vector4 List", "The Vector4 List to check.")] ref Vector4[] List)
	{
		List<Vector4> list = new List<Vector4>(List);
		m_InList = false;
		foreach (Vector4 item in Target)
		{
			if (!list.Contains(item))
			{
				return;
			}
		}
		m_InList = true;
	}
}
