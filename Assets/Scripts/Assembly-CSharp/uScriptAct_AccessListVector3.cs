using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Access List (Vector3)", "Access the contents of a list. May return the first or last item, a random item, or the item at a specific index.")]
[NodePath("Actions/Variables/Lists/Vector3")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Access different elements in a Vector3 List. Can access first, last, random or by index.")]
public class uScriptAct_AccessListVector3 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void First(Vector3[] List, int Index, out Vector3 Value)
	{
		Value = List[0];
	}

	public void Last(Vector3[] List, int Index, out Vector3 Value)
	{
		Value = List[List.Length - 1];
	}

	public void Random(Vector3[] List, int Index, out Vector3 Value)
	{
		int num = UnityEngine.Random.Range(0, List.Length);
		Value = List[num];
	}

	[FriendlyName("At Index")]
	public void AtIndex([FriendlyName("List", "The list to operate on.")] Vector3[] List, [FriendlyName("Index", "The index or position of the item to return. If the list contains 5 items, the valid range is 0-4, where 0 is the first item. (this parameter is only used with the At Index input).")] int Index, [FriendlyName("Selected", "The selected variable.")] out Vector3 Value)
	{
		bool flag = false;
		if (Index < 0 || Index >= List.Length)
		{
			flag = true;
		}
		if (flag)
		{
			uScriptDebug.Log("[Access List (Vector3)] You are trying to use an index number that is out of range for this list variable. Index 0 was returned instead.", uScriptDebug.Type.Error);
			Value = List[0];
		}
		else
		{
			Value = List[Index];
		}
	}
}
