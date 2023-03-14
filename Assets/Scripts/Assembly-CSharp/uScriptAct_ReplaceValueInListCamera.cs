using System.Collections.Generic;
using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Replaces all instances of a value in the list with the new value.")]
[FriendlyName("Replace Value In List (Camera)", "Replaces all instances of a value in the list with the new value. It will also return the number of values replaced.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Lists/Camera")]
public class uScriptAct_ReplaceValueInListCamera : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] Camera[] TargetList, [FriendlyName("Old Value", "The value to be replaced.")] Camera OldValue, [FriendlyName("New Value", "The new value to replace the old one.")] Camera NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out Camera[] ModifiedList, [FriendlyName("Found", "The number of values that were found and replaced in the list.")][SocketState(false, false)] out int ValuesFound)
	{
		List<Camera> list = new List<Camera>();
		int num = 0;
		if (TargetList.Length > 0)
		{
			foreach (Camera camera in TargetList)
			{
				if (camera == OldValue)
				{
					list.Add(NewValue);
					num++;
				}
				else
				{
					list.Add(camera);
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
