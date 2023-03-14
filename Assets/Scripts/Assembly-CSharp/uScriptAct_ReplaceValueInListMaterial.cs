using System.Collections.Generic;
using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Replaces all instances of a value in the list with the new value.")]
[FriendlyName("Replace Value In List (Material)", "Replaces all instances of a value in the list with the new value. It will also return the number of values replaced.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Variables/Lists/Material")]
public class uScriptAct_ReplaceValueInListMaterial : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] Material[] TargetList, [FriendlyName("Old Value", "The value to be replaced.")] Material OldValue, [FriendlyName("New Value", "The new value to replace the old one.")] Material NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out Material[] ModifiedList, [FriendlyName("Found", "The number of values that were found and replaced in the list.")][SocketState(false, false)] out int ValuesFound)
	{
		List<Material> list = new List<Material>();
		int num = 0;
		if (TargetList.Length > 0)
		{
			foreach (Material material in TargetList)
			{
				if (material == OldValue)
				{
					list.Add(NewValue);
					num++;
				}
				else
				{
					list.Add(material);
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
