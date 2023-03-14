using System.Collections.Generic;
using UnityEngine;

[NodeToolTip("Adds/removes Vector2 from a Vector2 List. Can also empty the Vector2 List.")]
[FriendlyName("Modify List (Vector2)", "Modify a list by adding/removing the specified variable(s), or by emptying it entirely.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Lists/Vector2")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_ModifyListVector2 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Add To List")]
	public void AddToList(Vector2[] Target, ref Vector2[] List, out int ListCount)
	{
		List<Vector2> list = new List<Vector2>(List);
		foreach (Vector2 item in Target)
		{
			list.Add(item);
		}
		List = list.ToArray();
		ListCount = List.Length;
	}

	[FriendlyName("Remove From List")]
	public void RemoveFromList(Vector2[] Target, ref Vector2[] List, out int ListCount)
	{
		List<Vector2> list = new List<Vector2>(List);
		foreach (Vector2 item in Target)
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
	public void EmptyList([FriendlyName("Target", "The Target variable(s) to add or remove from the list.")] Vector2[] Target, [FriendlyName("List", "The list to modify.")] ref Vector2[] List, [FriendlyName("List Size", "The remaining number of items in the list after the modification has taken place.")] out int ListCount)
	{
		List = new Vector2[0];
		ListCount = 0;
	}
}
