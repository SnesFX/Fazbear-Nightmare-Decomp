using System.Collections.Generic;
using UnityEngine;

[NodeToolTip("Replaces all instances of a value in the list with the new value.")]
[NodePath("Actions/Variables/Lists/Texture2D")]
[FriendlyName("Replace Value In List (Texture2D)", "Replaces all instances of a value in the list with the new value. It will also return the number of values replaced.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_ReplaceValueInListTexture2D : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] Texture2D[] TargetList, [FriendlyName("Old Value", "The value to be replaced.")] Texture2D OldValue, [FriendlyName("New Value", "The new value to replace the old one.")] Texture2D NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out Texture2D[] ModifiedList, [SocketState(false, false)][FriendlyName("Found", "The number of values that were found and replaced in the list.")] out int ValuesFound)
	{
		List<Texture2D> list = new List<Texture2D>();
		int num = 0;
		if (TargetList.Length > 0)
		{
			foreach (Texture2D texture2D in TargetList)
			{
				if (texture2D == OldValue)
				{
					list.Add(NewValue);
					num++;
				}
				else
				{
					list.Add(texture2D);
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
