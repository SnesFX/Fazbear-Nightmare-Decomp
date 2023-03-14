using System.Collections.Generic;
using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Is In List (Material)", "Checks to see if a Material is in a Material List.")]
[NodeToolTip("Checks to see if a Material is in a Material List.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Lists/Material")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_IsInListMaterial : uScriptLogic
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
	public void TestIfInList([FriendlyName("Target", "The target Material(s) to check for membership in the Material List.")] Material[] Target, [FriendlyName("Material List", "The Material List to check.")] ref Material[] List)
	{
		List<Material> list = new List<Material>(List);
		m_InList = false;
		foreach (Material item in Target)
		{
			if (!list.Contains(item))
			{
				return;
			}
		}
		m_InList = true;
	}
}
