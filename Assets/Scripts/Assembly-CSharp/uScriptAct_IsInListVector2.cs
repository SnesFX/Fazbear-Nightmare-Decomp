using System.Collections.Generic;
using UnityEngine;

[NodePath("Actions/Variables/Lists/Vector2")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Checks to see if a Vector2 is in a Vector2 List.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Is In List (Vector2)", "Checks to see if a Vector2 is in a Vector2 List.")]
public class uScriptAct_IsInListVector2 : uScriptLogic
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
	public void TestIfInList([FriendlyName("Target", "The target Vector2(s) to check for membership in the Vector2 List.")] Vector2[] Target, [FriendlyName("Vector2 List", "The Vector2 List to check.")] ref Vector2[] List)
	{
		List<Vector2> list = new List<Vector2>(List);
		m_InList = false;
		foreach (Vector2 item in Target)
		{
			if (!list.Contains(item))
			{
				return;
			}
		}
		m_InList = true;
	}
}
