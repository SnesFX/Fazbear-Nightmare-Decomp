using System.Collections.Generic;
using UnityEngine;

[NodeToolTip("Adds/removes Vector3 from a Vector3 List. Can also empty the Vector3 List.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Lists/Vector3")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Modify List (Vector3)", "Modify a list by adding/removing the specified variable(s), or by emptying it entirely.")]
public class uScriptAct_ModifyListVector3 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Add To List")]
	public void AddToList(Vector3[] Target, ref Vector3[] List, out int ListCount)
	{
		List<Vector3> list = new List<Vector3>(List);
		foreach (Vector3 item in Target)
		{
			list.Add(item);
		}
		List = list.ToArray();
		ListCount = List.Length;
	}

	[FriendlyName("Remove From List")]
	public void RemoveFromList(Vector3[] Target, ref Vector3[] List, out int ListCount)
	{
		List<Vector3> list = new List<Vector3>(List);
		foreach (Vector3 item in Target)
		{
			if (list.Contains(item))
			{
				list.Remove(item);
			}
		}
		List = list.ToArray();
		ListCount = List.Length;
	}

	[FriendlyName("Empty List")]
	public void EmptyList([FriendlyName("Target", "The Target variable(s) to add or remove from the list.")] Vector3[] Target, [FriendlyName("List", "The list to modify.")] ref Vector3[] List, [FriendlyName("List Size", "The remaining number of items in the list after the modification has taken place.")] out int ListCount)
	{
		List = new Vector3[0];
		ListCount = 0;
	}
}
