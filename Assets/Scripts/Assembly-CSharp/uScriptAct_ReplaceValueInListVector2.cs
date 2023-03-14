using System.Collections.Generic;
using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Replace Value In List (Vector2)", "Replaces all instances of a value in the list with the new value. It will also return the number of values replaced.")]
[NodePath("Actions/Variables/Lists/Vector2")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Replaces all instances of a value in the list with the new value.")]
public class uScriptAct_ReplaceValueInListVector2 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] Vector2[] TargetList, [FriendlyName("Old Value", "The value to be replaced.")] Vector2 OldValue, [FriendlyName("New Value", "The new value to replace the old one.")] Vector2 NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out Vector2[] ModifiedList, [FriendlyName("Found", "The number of values that were found and replaced in the list.")][SocketState(false, false)] out int ValuesFound)
	{
		List<Vector2> list = new List<Vector2>();
		int num = 0;
		if (TargetList.Length > 0)
		{
			foreach (Vector2 vector in TargetList)
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
