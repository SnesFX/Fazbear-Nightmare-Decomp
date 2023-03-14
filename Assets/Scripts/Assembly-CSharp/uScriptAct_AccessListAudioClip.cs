using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Access different elements in a AudioClip List. Can access first, last, random or by index.")]
[NodePath("Actions/Variables/Lists/AudioClip")]
[FriendlyName("Access List (AudioClip)", "Access the contents of a list. May return the first or last item, a random item, or the item at a specific index.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_AccessListAudioClip : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void First(AudioClip[] List, int Index, out AudioClip Value)
	{
		Value = List[0];
	}

	public void Last(AudioClip[] List, int Index, out AudioClip Value)
	{
		Value = List[List.Length - 1];
	}

	public void Random(AudioClip[] List, int Index, out AudioClip Value)
	{
		int num = UnityEngine.Random.Range(0, List.Length);
		Value = List[num];
	}

	[FriendlyName("At Index")]
	public void AtIndex([FriendlyName("List", "The list to operate on.")] AudioClip[] List, [FriendlyName("Index", "The index or position of the item to return. If the list contains 5 items, the valid range is 0-4, where 0 is the first item. (this parameter is only used with the At Index input).")] int Index, [FriendlyName("Selected", "The selected variable.")] out AudioClip Value)
	{
		bool flag = false;
		if (Index < 0 || Index >= List.Length)
		{
			flag = true;
		}
		if (flag)
		{
			uScriptDebug.Log("[Access List (AudioClip)] You are trying to use an index number that is out of range for this list variable. Index 0 was returned instead.", uScriptDebug.Type.Error);
			Value = List[0];
		}
		else
		{
			Value = List[Index];
		}
	}
}
