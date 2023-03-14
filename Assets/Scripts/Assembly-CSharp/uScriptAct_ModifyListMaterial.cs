using System.Collections.Generic;
using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Adds/removes Material from a Material List. Can also empty the Material List.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Variables/Lists/Material")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Modify List (Material)", "Modify a list by adding/removing the specified variable(s), or by emptying it entirely.")]
public class uScriptAct_ModifyListMaterial : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Add To List")]
	public void AddToList(Material[] Target, ref Material[] List, out int ListCount)
	{
		List<Material> list = new List<Material>(List);
		foreach (Material item in Target)
		{
			list.Add(item);
		}
		List = list.ToArray();
		ListCount = List.Length;
	}

	[FriendlyName("Remove From List")]
	public void RemoveFromList(Material[] Target, ref Material[] List, out int ListCount)
	{
		List<Material> list = new List<Material>(List);
		foreach (Material item in Target)
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
	public void EmptyList([FriendlyName("Target", "The Target variable(s) to add or remove from the list.")] Material[] Target, [FriendlyName("List", "The list to modify.")] ref Material[] List, [FriendlyName("List Size", "The remaining number of items in the list after the modification has taken place.")] out int ListCount)
	{
		List = new Material[0];
		ListCount = 0;
	}
}
