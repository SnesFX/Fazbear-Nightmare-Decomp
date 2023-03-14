using System.Collections.Generic;
using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Lists/Transform")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Replace Value In List (Transform)", "Replaces all instances of a value in the list with the new value. It will also return the number of values replaced.")]
[NodeToolTip("Replaces all instances of a value in the list with the new value.")]
public class uScriptAct_ReplaceValueInListTransform : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] Transform[] TargetList, [FriendlyName("Old Value", "The value to be replaced.")] Transform OldValue, [FriendlyName("New Value", "The new value to replace the old one.")] Transform NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out Transform[] ModifiedList, [SocketState(false, false)][FriendlyName("Found", "The number of values that were found and replaced in the list.")] out int ValuesFound)
	{
		List<Transform> list = new List<Transform>();
		int num = 0;
		if (TargetList.Length > 0)
		{
			foreach (Transform transform in TargetList)
			{
				if (transform == OldValue)
				{
					list.Add(NewValue);
					num++;
				}
				else
				{
					list.Add(transform);
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
