using System.Collections.Generic;
using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Variables/Lists/GameObject")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Modify List By Index (GameObject)", "Modify a list by specifying a specific slot number (index) in the list.")]
[NodeToolTip("Modify a list by specifying a specific slot number (index) in the list.")]
[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
public class uScriptAct_ModifyListByIndexGameObject : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Insert Into List", "Inserts an item into the list at the specified index.")]
	public void InsertIntoList(ref GameObject[] GameObjectList, int Index, GameObject[] Target, out int ListCount)
	{
		List<GameObject> list = new List<GameObject>(GameObjectList);
		if (Index < 0)
		{
			Index = 0;
		}
		if (list.Count == 0)
		{
			foreach (GameObject item in Target)
			{
				list.Add(item);
			}
		}
		else if (Index + 1 >= list.Count)
		{
			foreach (GameObject item2 in Target)
			{
				list.Add(item2);
			}
		}
		else
		{
			foreach (GameObject item3 in Target)
			{
				list.Insert(Index, item3);
			}
		}
		GameObjectList = list.ToArray();
		ListCount = GameObjectList.Length;
	}

	[FriendlyName("Remove From List", "Removes an item from the list at the specified index.")]
	public void RemoveFromList([FriendlyName("List", "The list to modify.")] ref GameObject[] GameObjectList, [FriendlyName("Index", "The index number where you wish to perform the modification. If the number is larger than the elements contained in the list, the action will be performed on the largest valid index number of the list. Negative values are automatically converted to 0.")] int Index, [FriendlyName("Insert Target", "The Target variable(s) to add or remove from the list. This socket is ignored when using the Remove From List input socket.")] GameObject[] Target, [FriendlyName("List Size", "The remaining number of items in the list after the modification has taken place.")] out int ListCount)
	{
		List<GameObject> list = new List<GameObject>(GameObjectList);
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
		GameObjectList = list.ToArray();
		ListCount = GameObjectList.Length;
	}
}