using System.Collections.Generic;
using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Modify_GameObject_List")]
[FriendlyName("Modify List (GameObject)", "Modify a list by adding/removing the specified variable(s), or by emptying it entirely.")]
[NodeToolTip("Adds/removes GameObjects from a GameObject List. Can also empty the GameObject List.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Lists/GameObject")]
public class uScriptAct_ModifyListGameObject : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Add To List")]
	public void AddToList(GameObject[] Target, ref GameObject[] GameObjectList, out int ListCount)
	{
		List<GameObject> list = new List<GameObject>(GameObjectList);
		foreach (GameObject item in Target)
		{
			list.Add(item);
		}
		GameObjectList = list.ToArray();
		ListCount = GameObjectList.Length;
	}

	[FriendlyName("Remove From List")]
	public void RemoveFromList(GameObject[] Target, ref GameObject[] GameObjectList, out int ListCount)
	{
		List<GameObject> list = new List<GameObject>(GameObjectList);
		foreach (GameObject item in Target)
		{
			if (list.Contains(item))
			{
				list.Remove(item);
			}
		}
		GameObjectList = list.ToArray();
		ListCount = GameObjectList.Length;
	}

	[FriendlyName("Empty List")]
	public void EmptyList([FriendlyName("Target", "The Target variable(s) to add or remove from the list.")] GameObject[] Target, [FriendlyName("List", "The list to modify.")] ref GameObject[] GameObjectList, [FriendlyName("List Size", "The remaining number of items in the list after the modification has taken place.")] out int ListCount)
	{
		GameObjectList = new GameObject[0];
		ListCount = 0;
	}
}
