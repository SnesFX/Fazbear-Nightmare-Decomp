using System.Collections.Generic;
using UnityEngine;

[NodePath("Actions/Variables/Lists/Transform")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Is_In_Transform_List")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Checks to see if Transforms are in a Transform List.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Is In List (Transform)", "Checks to see if Transforms are in a Transform List.")]
public class uScriptAct_IsInListTransform : uScriptLogic
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
	public void TestIfInList([FriendlyName("Target", "The target Transform(s) to check for membership in the Transform list.")] Transform[] Target, [FriendlyName("Transform List", "The Transform List to check.")] ref Transform[] TransformList)
	{
		List<Transform> list = new List<Transform>(TransformList);
		m_InList = false;
		foreach (Transform item in Target)
		{
			if (!list.Contains(item))
			{
				return;
			}
		}
		m_InList = true;
	}
}
