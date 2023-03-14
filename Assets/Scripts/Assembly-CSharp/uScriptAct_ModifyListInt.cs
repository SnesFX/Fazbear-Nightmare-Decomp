using System.Collections.Generic;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Variables/Lists/Int")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Modify List (Int)", "Modify a list by adding/removing the specified variable(s), or by emptying it entirely.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeToolTip("Adds/removes ints from a Int List. Can also empty the Int List.")]
public class uScriptAct_ModifyListInt : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Add To List")]
	public void AddToList(int[] Target, ref int[] IntList, out int ListCount)
	{
		List<int> list = new List<int>(IntList);
		foreach (int item in Target)
		{
			list.Add(item);
		}
		IntList = list.ToArray();
		ListCount = IntList.Length;
	}

	[FriendlyName("Remove From List")]
	public void RemoveFromList(int[] Target, ref int[] IntList, out int ListCount)
	{
		List<int> list = new List<int>(IntList);
		foreach (int item in Target)
		{
			if (list.Contains(item))
			{
				list.Remove(item);
			}
		}
		IntList = list.ToArray();
		ListCount = IntList.Length;
	}

	[FriendlyName("Empty List")]
	public void EmptyList([FriendlyName("Target", "The Target variable(s) to add or remove from the list.")] int[] Target, [FriendlyName("List", "The list to modify.")] ref int[] IntList, [FriendlyName("List Size", "The remaining number of items in the list after the modification has taken place.")] out int ListCount)
	{
		IntList = new int[0];
		ListCount = 0;
	}
}
