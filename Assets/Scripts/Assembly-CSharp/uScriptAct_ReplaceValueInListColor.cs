using System.Collections.Generic;
using UnityEngine;

[NodeToolTip("Replaces all instances of a value in the list with the new value.")]
[FriendlyName("Replace Value In List (Color)", "Replaces all instances of a value in the list with the new value. It will also return the number of values replaced.")]
[NodePath("Actions/Variables/Lists/Color")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_ReplaceValueInListColor : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] Color[] TargetList, [FriendlyName("Old Value", "The value to be replaced.")] Color OldValue, [FriendlyName("New Value", "The new value to replace the old one.")] Color NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out Color[] ModifiedList, [SocketState(false, false)][FriendlyName("Found", "The number of values that were found and replaced in the list.")] out int ValuesFound)
	{
		List<Color> list = new List<Color>();
		int num = 0;
		if (TargetList.Length > 0)
		{
			foreach (Color color in TargetList)
			{
				if (color == OldValue)
				{
					list.Add(NewValue);
					num++;
				}
				else
				{
					list.Add(color);
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
