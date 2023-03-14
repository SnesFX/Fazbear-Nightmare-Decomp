using System.Collections.Generic;
using UnityEngine;

[NodePath("Actions/Variables/Lists/Vector4")]
[FriendlyName("Modify List By Index (Vector4)", "Modify a list by specifying a specific slot number (index) in the list.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Modify a list by specifying a specific slot number (index) in the list.")]
[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
public class uScriptAct_ModifyListByIndexVector4 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Insert Into List", "Inserts an item into the list at the specified index.")]
	public void InsertIntoList(ref Vector4[] VariableList, int Index, Vector4[] Target, out int ListCount)
	{
		List<Vector4> list = new List<Vector4>(VariableList);
		if (Index < 0)
		{
			Index = 0;
		}
		if (list.Count == 0)
		{
			foreach (Vector4 item in Target)
			{
				list.Add(item);
			}
		}
		else if (Index + 1 >= list.Count)
		{
			foreach (Vector4 item2 in Target)
			{
				list.Add(item2);
			}
		}
		else
		{
			foreach (Vector4 item3 in Target)
			{
				list.Insert(Index, item3);
			}
		}
		VariableList = list.ToArray();
		ListCount = VariableList.Length;
	}

	[FriendlyName("Remove From List", "Removes an item from the list at the specified index.")]
	public void RemoveFromList([FriendlyName("List", "The list to modify.")] ref Vector4[] VariableList, [FriendlyName("Index", "The index number where you wish to perform the modification. If the number is larger than the elements contained in the list, the action will be performed on the largest valid index number of the list. Negative values are automatically converted to 0.")] int Index, [FriendlyName("Insert Target", "The Target variable(s) to add or remove from the list. This socket is ignored when using the Remove From List input socket.")] Vector4[] Target, [FriendlyName("List Size", "The remaining number of items in the list after the modification has taken place.")] out int ListCount)
	{
		List<Vector4> list = new List<Vector4>(VariableList);
		if (Index < 0)
		{
			Index = 0;
		}
		if (list.Count > 0)
		{
			if (Index >= list.Count)
			{
				list.RemoveAt(list.Count - 1);
			}
			else
			{
				list.RemoveAt(Index);
			}
		}
		VariableList = list.ToArray();
		ListCount = VariableList.Length;
	}
}
