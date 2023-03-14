using System.Collections.Generic;
using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Modify_Transform_List")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Adds/removes Transforms from a Transform List. Can also empty the Transform List.")]
[NodePath("Actions/Variables/Lists/Transform")]
[FriendlyName("Modify List (Transform)", "Modify a list by adding/removing the specified variable(s), or by emptying it entirely.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_ModifyListTransform : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Add To List")]
	public void AddToList(Transform[] Target, ref Transform[] TransformList, out int ListCount)
	{
		List<Transform> list = new List<Transform>(TransformList);
		foreach (Transform item in Target)
		{
			list.Add(item);
		}
		TransformList = list.ToArray();
		ListCount = TransformList.Length;
	}

	[FriendlyName("Remove From List")]
	public void RemoveFromList(Transform[] Target, ref Transform[] TransformList, out int ListCount)
	{
		List<Transform> list = new List<Transform>(TransformList);
		foreach (Transform item in Target)
		{
			if (list.Contains(item))
			{
				list.Remove(item);
			}
		}
		TransformList = list.ToArray();
		ListCount = TransformList.Length;
	}

	[FriendlyName("Empty List")]
	public void EmptyList([FriendlyName("Target", "The Target variable(s) to add or remove from the list.")] Transform[] Target, [FriendlyName("List", "The list to modify.")] ref Transform[] TransformList, [FriendlyName("List Size", "The remaining number of items in the list after the modification has taken place.")] out int ListCount)
	{
		TransformList = new Transform[0];
		ListCount = 0;
	}
}
