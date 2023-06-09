using System.Collections.Generic;
using UnityEngine;

[NodeToolTip("Checks to see if a Texture2D is in a Texture2D List.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Variables/Lists/Texture2D")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Is In List (Texture2D)", "Checks to see if a Texture2D is in a Texture2D List.")]
public class uScriptAct_IsInListTexture2D : uScriptLogic
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
	public void TestIfInList([FriendlyName("Target", "The target Texture2D(s) to check for membership in the Texture2D List.")] Texture2D[] Target, [FriendlyName("Texture2D List", "The Texture2D List to check.")] ref Texture2D[] List)
	{
		List<Texture2D> list = new List<Texture2D>(List);
		m_InList = false;
		foreach (Texture2D item in Target)
		{
			if (!list.Contains(item))
			{
				return;
			}
		}
		m_InList = true;
	}
}
