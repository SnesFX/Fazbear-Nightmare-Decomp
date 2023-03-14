using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Access_GameObject_List")]
[FriendlyName("Access List (Int)", "Access the contents of a list. May return the first or last item, a random item, or the item at a specific index.")]
[NodePath("Actions/Variables/Lists/Int")]
[NodeToolTip("Access different elements in a Int List. Can access first, last, random or by index.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_AccessListInt : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void First(int[] IntList, int Index, out int Value)
	{
		Value = IntList[0];
	}

	public void Last(int[] IntList, int Index, out int Value)
	{
		Value = IntList[IntList.Length - 1];
	}

	public void Random(int[] IntList, int Index, out int Value)
	{
		int num = UnityEngine.Random.Range(0, IntList.Length);
		Value = IntList[num];
	}

	[FriendlyName("At Index")]
	public void AtIndex([FriendlyName("List", "The list to operate on.")] int[] IntList, [FriendlyName("Index", "The index or position of the item to return. If the list contains 5 items, the valid range is 0-4, where 0 is the first item. (this parameter is only used with the At Index input).")] int Index, [FriendlyName("Selected", "The selected variable.")] out int Value)
	{
		bool flag = false;
		if (Index < 0 || Index >= IntList.Length)
		{
			flag = true;
		}
		if (flag)
		{
			uScriptDebug.Log("[Access List (Int)] You are trying to use an index number that is out of range for this list variable. Index 0 was returned instead.", uScriptDebug.Type.Error);
			Value = IntList[0];
		}
		else
		{
			Value = IntList[Index];
		}
	}
}
