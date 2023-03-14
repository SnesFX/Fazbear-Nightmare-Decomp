using System.Collections.Generic;
using UnityEngine;

[FriendlyName("Modify List (AudioClip)", "Modify a list by adding/removing the specified variable(s), or by emptying it entirely.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Adds/removes AudioClips from a AudioClip List. Can also empty the AudioClip List.")]
[NodePath("Actions/Variables/Lists/AudioClip")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_ModifyListAudioClip : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Add To List")]
	public void AddToList(AudioClip[] Target, ref AudioClip[] List, out int ListCount)
	{
		List<AudioClip> list = new List<AudioClip>(List);
		foreach (AudioClip item in Target)
		{
			list.Add(item);
		}
		List = list.ToArray();
		ListCount = List.Length;
	}

	[FriendlyName("Remove From List")]
	public void RemoveFromList(AudioClip[] Target, ref AudioClip[] List, out int ListCount)
	{
		List<AudioClip> list = new List<AudioClip>(List);
		foreach (AudioClip item in Target)
		{
			if (list.Contains(item))
			{
				list.Remove(item);
			}
		}
		List = list.ToArray();
		ListCount = List.Length;
	}

	[FriendlyName("Empty List")]
	public void EmptyList([FriendlyName("Target", "The Target variable(s) to add or remove from the list.")] AudioClip[] Target, [FriendlyName("List", "The list to modify.")] ref AudioClip[] List, [FriendlyName("List Size", "The remaining number of items in the list after the modification has taken place.")] out int ListCount)
	{
		List = new AudioClip[0];
		ListCount = 0;
	}
}
