using System.Collections.Generic;
using UnityEngine;

[NodePath("Actions/Variables/Lists/Texture2D")]
[FriendlyName("Modify List (Texture2D)", "Modify a list by adding/removing the specified variable(s), or by emptying it entirely.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeToolTip("Adds/removes Texture2D from a Texture2D List. Can also empty the Texture2D List.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_ModifyListTexture2D : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Add To List")]
	public void AddToList(Texture2D[] Target, ref Texture2D[] List, out int ListCount)
	{
		List<Texture2D> list = new List<Texture2D>(List);
		foreach (Texture2D item in Target)
		{
			list.Add(item);
		}
		List = list.ToArray();
		ListCount = List.Length;
	}

	[FriendlyName("Remove From List")]
	public void RemoveFromList(Texture2D[] Target, ref Texture2D[] List, out int ListCount)
	{
		List<Texture2D> list = new List<Texture2D>(List);
		foreach (Texture2D item in Target)
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
	public void EmptyList([FriendlyName("Target", "The Target variable(s) to add or remove from the list.")] Texture2D[] Target, [FriendlyName("List", "The list to modify.")] ref Texture2D[] List, [FriendlyName("List Size", "The remaining number of items in the list after the modification has taken place.")] out int ListCount)
	{
		List = new Texture2D[0];
		ListCount = 0;
	}
}
