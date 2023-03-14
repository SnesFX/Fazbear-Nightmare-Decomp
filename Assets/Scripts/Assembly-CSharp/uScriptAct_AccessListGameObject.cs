using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Access different elements in a GameObject List. Can access first, last, random or by index.")]
[NodePath("Actions/Variables/Lists/GameObject")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Access_GameObject_List")]
[FriendlyName("Access List (GameObject)", "Access the contents of a list. May return the first or last item, a random item, or the item at a specific index.")]
public class uScriptAct_AccessListGameObject : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void First(GameObject[] GameObjectList, int Index, out GameObject GameObj)
	{
		GameObj = GameObjectList[0];
	}

	public void Last(GameObject[] GameObjectList, int Index, out GameObject GameObj)
	{
		GameObj = GameObjectList[GameObjectList.Length - 1];
	}

	public void Random(GameObject[] GameObjectList, int Index, out GameObject GameObj)
	{
		int num = UnityEngine.Random.Range(0, GameObjectList.Length);
		GameObj = GameObjectList[num];
	}

	[FriendlyName("At Index")]
	public void AtIndex([FriendlyName("List", "The list to operate on.")] GameObject[] GameObjectList, [FriendlyName("Index", "The index or position of the item to return. If the list contains 5 items, the valid range is 0-4, where 0 is the first item. (this parameter is only used with the At Index input).")] int Index, [FriendlyName("Selected", "The selected variable.")] out GameObject GameObj)
	{
		bool flag = false;
		if (Index < 0 || Index >= GameObjectList.Length)
		{
			flag = true;
		}
		if (flag)
		{
			uScriptDebug.Log("[Access List (GameObject)] You are trying to use an index number that is out of range for this list variable. Index 0 was returned instead.", uScriptDebug.Type.Error);
			GameObj = GameObjectList[0];
		}
		else
		{
			GameObj = GameObjectList[Index];
		}
	}
}
