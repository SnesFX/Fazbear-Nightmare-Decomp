using System.Collections.Generic;
using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Lists/Color")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Modify List (Color)", "Modify a list by adding/removing the specified variable(s), or by emptying it entirely.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeToolTip("Adds/removes Colors from a Color List. Can also empty the Color List.")]
public class uScriptAct_ModifyListColor : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Add To List")]
	public void AddToList(Color[] Target, ref Color[] List, out int ListCount)
	{
		List<Color> list = new List<Color>(List);
		foreach (Color item in Target)
		{
			list.Add(item);
		}
		List = list.ToArray();
		ListCount = List.Length;
	}

	[FriendlyName("Remove From List")]
	public void RemoveFromList(Color[] Target, ref Color[] List, out int ListCount)
	{
		List<Color> list = new List<Color>(List);
		foreach (Color item in Target)
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
	public void EmptyList([FriendlyName("Target", "The Target variable(s) to add or remove from the list.")] Color[] Target, [FriendlyName("List", "The list to modify.")] ref Color[] List, [FriendlyName("List Size", "The remaining number of items in the list after the modification has taken place.")] out int ListCount)
	{
		List = new Color[0];
		ListCount = 0;
	}
}
