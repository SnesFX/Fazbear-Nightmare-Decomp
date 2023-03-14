using System.Collections.Generic;
using UnityEngine;

[NodePath("Actions/Variables/Lists/Vector4")]
[NodeToolTip("Replaces all instances of a value in the list with the new value.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Replace Value In List (Vector4)", "Replaces all instances of a value in the list with the new value. It will also return the number of values replaced.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_ReplaceValueInListVector4 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] Vector4[] TargetList, [FriendlyName("Old Value", "The value to be replaced.")] Vector4 OldValue, [FriendlyName("New Value", "The new value to replace the old one.")] Vector4 NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out Vector4[] ModifiedList, [FriendlyName("Found", "The number of values that were found and replaced in the list.")][SocketState(false, false)] out int ValuesFound)
	{
		List<Vector4> list = new List<Vector4>();
		int num = 0;
		if (TargetList.Length > 0)
		{
			foreach (Vector4 vector in TargetList)
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
