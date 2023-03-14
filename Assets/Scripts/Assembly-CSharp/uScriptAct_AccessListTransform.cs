using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Access_Transform_List")]
[NodeToolTip("Access different elements in a Transform List. Can access first, last, random or by index.")]
[NodePath("Actions/Variables/Lists/Transform")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Access List (Transform)", "Access the contents of a list. May return the first or last item, a random item, or the item at a specific index.")]
public class uScriptAct_AccessListTransform : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void First(Transform[] TransformList, int Index, out Transform GameObj)
	{
		GameObj = TransformList[0];
	}

	public void Last(Transform[] TransformList, int Index, out Transform GameObj)
	{
		GameObj = TransformList[TransformList.Length - 1];
	}

	public void Random(Transform[] TransformList, int Index, out Transform GameObj)
	{
		int num = UnityEngine.Random.Range(0, TransformList.Length);
		GameObj = TransformList[num];
	}

	[FriendlyName("At Index")]
	public void AtIndex([FriendlyName("List", "The list to operate on.")] Transform[] TransformList, [FriendlyName("Index", "The index or position of the item to return. If the list contains 5 items, the valid range is 0-4, where 0 is the first item. (this parameter is only used with the At Index input).")] int Index, [FriendlyName("Selected", "The selected variable.")] out Transform GameObj)
	{
		bool flag = false;
		if (Index < 0 || Index >= TransformList.Length)
		{
			flag = true;
		}
		if (flag)
		{
			uScriptDebug.Log("[Access List (Transform)] You are trying to use an index number that is out of range for this list variable. Index 0 was returned instead.", uScriptDebug.Type.Error);
			GameObj = TransformList[0];
		}
		else
		{
			GameObj = TransformList[Index];
		}
	}
}
