using System.Collections.Generic;
using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Is In List (Rect)", "Checks to see if a Rect is in a Rect List.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeToolTip("Checks to see if a Rect is in a Rect List.")]
[NodePath("Actions/Variables/Lists/Rect")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_IsInListRect : uScriptLogic
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
	public void TestIfInList([FriendlyName("Target", "The target Rect(s) to check for membership in the Rect List.")] Rect[] Target, [FriendlyName("Rect List", "The Rect List to check.")] ref Rect[] List)
	{
		List<Rect> list = new List<Rect>(List);
		m_InList = false;
		foreach (Rect item in Target)
		{
			if (!list.Contains(item))
			{
				return;
			}
		}
		m_InList = true;
	}
}
