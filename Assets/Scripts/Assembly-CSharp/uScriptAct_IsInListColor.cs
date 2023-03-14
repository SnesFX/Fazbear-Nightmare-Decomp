using System.Collections.Generic;
using UnityEngine;

[FriendlyName("Is In List (Color)", "Checks to see if a Color is in a Color List.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Checks to see if a Color is in a Color List.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Lists/Color")]
public class uScriptAct_IsInListColor : uScriptLogic
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
	public void TestIfInList([FriendlyName("Target", "The target Color(s) to check for membership in the Color List.")] Color[] Target, [FriendlyName("Color List", "The Color List to check.")] ref Color[] List)
	{
		List<Color> list = new List<Color>(List);
		m_InList = false;
		foreach (Color item in Target)
		{
			if (!list.Contains(item))
			{
				return;
			}
		}
		m_InList = true;
	}
}
