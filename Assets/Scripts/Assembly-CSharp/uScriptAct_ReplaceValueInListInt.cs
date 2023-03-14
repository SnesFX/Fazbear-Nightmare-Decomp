using System.Collections.Generic;

[NodePath("Actions/Variables/Lists/Int")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Replaces all instances of a value in the list with the new value.")]
[FriendlyName("Replace Value In List (Int)", "Replaces all instances of a value in the list with the new value. It will also return the number of values replaced.")]
public class uScriptAct_ReplaceValueInListInt : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] int[] TargetList, [FriendlyName("Old Value", "The value to be replaced.")] int OldValue, [FriendlyName("New Value", "The new value to replace the old one.")] int NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out int[] ModifiedList, [SocketState(false, false)][FriendlyName("Found", "The number of values that were found and replaced in the list.")] out int ValuesFound)
	{
		List<int> list = new List<int>();
		int num = 0;
		if (TargetList.Length > 0)
		{
			foreach (int num2 in TargetList)
			{
				if (num2 == OldValue)
				{
					list.Add(NewValue);
					num++;
				}
				else
				{
					list.Add(num2);
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
