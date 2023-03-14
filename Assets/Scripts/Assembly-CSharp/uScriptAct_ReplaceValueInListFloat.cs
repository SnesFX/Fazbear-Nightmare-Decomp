using System.Collections.Generic;

[FriendlyName("Replace Value In List (Float)", "Replaces all instances of a value in the list with the new value. It will also return the number of values replaced.")]
[NodeToolTip("Replaces all instances of a value in the list with the new value.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Variables/Lists/Float")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_ReplaceValueInListFloat : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] float[] TargetList, [FriendlyName("Old Value", "The value to be replaced.")] float OldValue, [FriendlyName("New Value", "The new value to replace the old one.")] float NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out float[] ModifiedList, [SocketState(false, false)][FriendlyName("Found", "The number of values that were found and replaced in the list.")] out float ValuesFound)
	{
		List<float> list = new List<float>();
		int num = 0;
		if (TargetList.Length > 0)
		{
			foreach (float num2 in TargetList)
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
