using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Lists/Float")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Access_GameObject_List")]
[NodeToolTip("Access different elements in a Float List. Can access first, last, random or by index.")]
[FriendlyName("Access List (Float)", "Access the contents of a list. May return the first or last item, a random item, or the item at a specific index.")]
public class uScriptAct_AccessListFloat : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void First(float[] FloatList, int Index, out float Value)
	{
		Value = FloatList[0];
	}

	public void Last(float[] FloatList, int Index, out float Value)
	{
		Value = FloatList[FloatList.Length - 1];
	}

	public void Random(float[] FloatList, int Index, out float Value)
	{
		int num = UnityEngine.Random.Range(0, FloatList.Length);
		Value = FloatList[num];
	}

	[FriendlyName("At Index")]
	public void AtIndex([FriendlyName("List", "The list to operate on.")] float[] FloatList, [FriendlyName("Index", "The index or position of the item to return. If the list contains 5 items, the valid range is 0-4, where 0 is the first item. (this parameter is only used with the At Index input).")] int Index, [FriendlyName("Selected", "The selected variable.")] out float Value)
	{
		bool flag = false;
		if (Index < 0 || Index >= FloatList.Length)
		{
			flag = true;
		}
		if (flag)
		{
			uScriptDebug.Log("[Access List (Float)] You are trying to use an index number that is out of range for this list variable. Index 0 was returned instead.", uScriptDebug.Type.Error);
			Value = FloatList[0];
		}
		else
		{
			Value = FloatList[Index];
		}
	}
}
