using System.Collections.Generic;

[NodeToolTip("Adds/removes strings from a String List. Can also empty the String List.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Lists/String")]
[FriendlyName("Modify List (String)", "Modify a list by adding/removing the specified variable(s), or by emptying it entirely.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_ModifyListString : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Add To List")]
	public void AddToList(string[] Target, ref string[] StringList, out int ListCount)
	{
		List<string> list = new List<string>(StringList);
		foreach (string item in Target)
		{
			list.Add(item);
		}
		StringList = list.ToArray();
		ListCount = StringList.Length;
	}

	[FriendlyName("Remove From List")]
	public void RemoveFromList(string[] Target, ref string[] StringList, out int ListCount)
	{
		List<string> list = new List<string>(StringList);
		foreach (string item in Target)
		{
			if (list.Contains(item))
			{
				list.Remove(item);
			}
		}
		StringList = list.ToArray();
		ListCount = StringList.Length;
	}

	[FriendlyName("Empty List")]
	public void EmptyList([FriendlyName("Target", "The Target variable(s) to add or remove from the list.")] string[] Target, [FriendlyName("List", "The list to modify.")] ref string[] StringList, [FriendlyName("List Size", "The remaining number of items in the list after the modification has taken place.")] out int ListCount)
	{
		StringList = new string[0];
		ListCount = 0;
	}
}
