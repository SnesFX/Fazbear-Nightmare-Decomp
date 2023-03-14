using System.Collections.Generic;
using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Modify List (Vector4)", "Modify a list by adding/removing the specified variable(s), or by emptying it entirely.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Adds/removes Vector4 from a Vector4 List. Can also empty the Vector4 List.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Lists/Vector4")]
public class uScriptAct_ModifyListVector4 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Add To List")]
	public void AddToList(Vector4[] Target, ref Vector4[] List, out int ListCount)
	{
		List<Vector4> list = new List<Vector4>(List);
		foreach (Vector4 item in Target)
		{
			list.Add(item);
		}
		List = list.ToArray();
		ListCount = List.Length;
	}

	[FriendlyName("Remove From List")]
	public void RemoveFromList(Vector4[] Target, ref Vector4[] List, out int ListCount)
	{
		List<Vector4> list = new List<Vector4>(List);
		foreach (Vector4 item in Target)
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
	public void EmptyList([FriendlyName("Target", "The Target variable(s) to add or remove from the list.")] Vector4[] Target, [FriendlyName("List", "The list to modify.")] ref Vector4[] List, [FriendlyName("List Size", "The remaining number of items in the list after the modification has taken place.")] out int ListCount)
	{
		List = new Vector4[0];
		ListCount = 0;
	}
}
