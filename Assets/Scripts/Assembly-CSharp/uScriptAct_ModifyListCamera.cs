using System.Collections.Generic;
using UnityEngine;

[NodePath("Actions/Variables/Lists/Camera")]
[FriendlyName("Modify List (Camera)", "Modify a list by adding/removing the specified variable(s), or by emptying it entirely.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Adds/removes cameras from a Camera List. Can also empty the Camera List.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
public class uScriptAct_ModifyListCamera : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Add To List")]
	public void AddToList(Camera[] Target, ref Camera[] List, out int ListCount)
	{
		List<Camera> list = new List<Camera>(List);
		foreach (Camera item in Target)
		{
			list.Add(item);
		}
		List = list.ToArray();
		ListCount = List.Length;
	}

	[FriendlyName("Remove From List")]
	public void RemoveFromList(Camera[] Target, ref Camera[] List, out int ListCount)
	{
		List<Camera> list = new List<Camera>(List);
		foreach (Camera item in Target)
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
	public void EmptyList([FriendlyName("Target", "The Target variable(s) to add or remove from the list.")] Camera[] Target, [FriendlyName("List", "The list to modify.")] ref Camera[] List, [FriendlyName("List Size", "The remaining number of items in the list after the modification has taken place.")] out int ListCount)
	{
		List = new Camera[0];
		ListCount = 0;
	}
}
