using System.Collections.Generic;
using UnityEngine;

[NodePath("Actions/Variables/Lists/Camera")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Checks to see if a camera is in a Camera List.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Is In List (Camera)", "Checks to see if a camera is in a Camera List.")]
public class uScriptAct_IsInListCamera : uScriptLogic
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
	public void TestIfInList([FriendlyName("Target", "The target camera(s) to check for membership in the Camera List.")] Camera[] Target, [FriendlyName("Camera List", "The Camera List to check.")] ref Camera[] List)
	{
		List<Camera> list = new List<Camera>(List);
		m_InList = false;
		foreach (Camera item in Target)
		{
			if (!list.Contains(item))
			{
				return;
			}
		}
		m_InList = true;
	}
}
