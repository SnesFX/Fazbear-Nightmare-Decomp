using System.Collections.Generic;
using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Variables/Lists/Vector3")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Replaces all instances of a value in the list with the new value.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Replace Value In List (Vector3)", "Replaces all instances of a value in the list with the new value. It will also return the number of values replaced.")]
public class uScriptAct_ReplaceValueInListVector3 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] Vector3[] TargetList, [FriendlyName("Old Value", "The value to be replaced.")] Vector3 OldValue, [FriendlyName("New Value", "The new value to replace the old one.")] Vector3 NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out Vector3[] ModifiedList, [SocketState(false, false)][FriendlyName("Found", "The number of values that were found and replaced in the list.")] out int ValuesFound)
	{
		List<Vector3> list = new List<Vector3>();
		int num = 0;
		if (TargetList.Length > 0)
		{
			foreach (Vector3 vector in TargetList)
			{
				if (vector == OldValue)
				{
					list.Add(NewValue);
					num++;
				}
				else
				{
					list.Add(vector);
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
