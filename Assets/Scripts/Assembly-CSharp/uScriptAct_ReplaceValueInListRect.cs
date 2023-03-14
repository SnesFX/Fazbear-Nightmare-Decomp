using System.Collections.Generic;
using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Variables/Lists/Rect")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Replace Value In List (Rect)", "Replaces all instances of a value in the list with the new value. It will also return the number of values replaced.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeToolTip("Replaces all instances of a value in the list with the new value.")]
public class uScriptAct_ReplaceValueInListRect : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] Rect[] TargetList, [FriendlyName("Old Value", "The value to be replaced.")] Rect OldValue, [FriendlyName("New Value", "The new value to replace the old one.")] Rect NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out Rect[] ModifiedList, [FriendlyName("Found", "The number of values that were found and replaced in the list.")][SocketState(false, false)] out int ValuesFound)
	{
		List<Rect> list = new List<Rect>();
		int num = 0;
		if (TargetList.Length > 0)
		{
			foreach (Rect rect in TargetList)
			{
				if (rect == OldValue)
				{
					list.Add(NewValue);
					num++;
				}
				else
				{
					list.Add(rect);
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
