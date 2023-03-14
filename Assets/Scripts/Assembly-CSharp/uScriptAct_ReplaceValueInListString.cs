using System.Collections.Generic;

[NodeToolTip("Replaces all instances of a value in the list with the new value.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Replace Value In List (String)", "Replaces all instances of a value in the list with the new value. It will also return the number of values replaced.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Variables/Lists/String")]
public class uScriptAct_ReplaceValueInListString : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] string[] TargetList, [FriendlyName("Old Value", "The value to be replaced.")] string OldValue, [FriendlyName("New Value", "The new value to replace the old one.")] string NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out string[] ModifiedList, [FriendlyName("Found", "The number of values that were found and replaced in the list.")][SocketState(false, false)] out int ValuesFound)
	{
		List<string> list = new List<string>();
		int num = 0;
		if (TargetList.Length > 0)
		{
			foreach (string text in TargetList)
			{
				if (text == OldValue)
				{
					list.Add(NewValue);
					num++;
				}
				else
				{
					list.Add(text);
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
