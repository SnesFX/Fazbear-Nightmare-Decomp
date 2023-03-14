using System.Collections.Generic;

[NodeToolTip("Adds/removes floats from a Float List. Can also empty the Float List.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Modify List (Float)", "Modify a list by adding/removing the specified variable(s), or by emptying it entirely.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Variables/Lists/Float")]
public class uScriptAct_ModifyListFloat : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Add To List")]
	public void AddToList(float[] Target, ref float[] FloatList, out int ListCount)
	{
		List<float> list = new List<float>(FloatList);
		foreach (float item in Target)
		{
			list.Add(item);
		}
		FloatList = list.ToArray();
		ListCount = FloatList.Length;
	}

	[FriendlyName("Remove From List")]
	public void RemoveFromList(float[] Target, ref float[] FloatList, out int ListCount)
	{
		List<float> list = new List<float>(FloatList);
		foreach (float item in Target)
		{
			if (list.Contains(item))
			{
				list.Remove(item);
			}
		}
		FloatList = list.ToArray();
		ListCount = FloatList.Length;
	}

	[FriendlyName("Empty List")]
	public void EmptyList([FriendlyName("Target", "The Target variable(s) to add or remove from the list.")] float[] Target, [FriendlyName("List", "The list to modify.")] ref float[] FloatList, [FriendlyName("List Size", "The remaining number of items in the list after the modification has taken place.")] out int ListCount)
	{
		FloatList = new float[0];
		ListCount = 0;
	}
}
