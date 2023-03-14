using UnityEngine;

[FriendlyName("Access List (String)", "Access the contents of a list. May return the first or last item, a random item, or the item at a specific index.")]
[NodePath("Actions/Variables/Lists/String")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Access_GameObject_List")]
[NodeToolTip("Access different elements in a String List. Can access first, last, random or by index.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_AccessListString : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void First(string[] StringList, int Index, out string Value)
	{
		Value = StringList[0];
	}

	public void Last(string[] StringList, int Index, out string Value)
	{
		Value = StringList[StringList.Length - 1];
	}

	public void Random(string[] StringList, int Index, out string Value)
	{
		int num = UnityEngine.Random.Range(0, StringList.Length);
		Value = StringList[num];
	}

	[FriendlyName("At Index")]
	public void AtIndex([FriendlyName("List", "The list to operate on.")] string[] StringList, [FriendlyName("Index", "The index or position of the item to return. If the list contains 5 items, the valid range is 0-4, where 0 is the first item. (this parameter is only used with the At Index input).")] int Index, [FriendlyName("Selected", "The selected variable.")] out string Value)
	{
		bool flag = false;
		if (Index < 0 || Index >= StringList.Length)
		{
			flag = true;
		}
		if (flag)
		{
			uScriptDebug.Log("[Access List (String)] You are trying to use an index number that is out of range for this list variable. Index 0 was returned instead.", uScriptDebug.Type.Error);
			Value = StringList[0];
		}
		else
		{
			Value = StringList[Index];
		}
	}
}
