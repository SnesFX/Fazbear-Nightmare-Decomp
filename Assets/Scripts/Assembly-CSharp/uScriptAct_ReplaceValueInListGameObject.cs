using System.Collections.Generic;
using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Replace Value In List (GameObject)", "Replaces all instances of a value in the list with the new value. It will also return the number of values replaced.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Replaces all instances of a value in the list with the new value.")]
[NodePath("Actions/Variables/Lists/GameObject")]
public class uScriptAct_ReplaceValueInListGameObject : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] GameObject[] TargetList, [FriendlyName("Old Value", "The value to be replaced.")] GameObject OldValue, [FriendlyName("New Value", "The new value to replace the old one.")] GameObject NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out GameObject[] ModifiedList, [FriendlyName("Found", "The number of values that were found and replaced in the list.")][SocketState(false, false)] out int ValuesFound)
	{
		List<GameObject> list = new List<GameObject>();
		int num = 0;
		if (TargetList.Length > 0)
		{
			foreach (GameObject gameObject in TargetList)
			{
				if (gameObject == OldValue)
				{
					list.Add(NewValue);
					num++;
				}
				else
				{
					list.Add(gameObject);
				}
			}
			ModifiedList = list.ToArray();
			ValuesFound = num;
		}
		else
		{
			ModifiedList = TargetList;
			ValuesFound = num;
		}
	}
}
