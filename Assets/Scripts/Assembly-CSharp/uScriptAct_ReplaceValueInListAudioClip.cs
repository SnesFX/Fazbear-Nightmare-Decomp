using System.Collections.Generic;
using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Lists/AudioClip")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeToolTip("Replaces all instances of a value in the list with the new value.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Replace Value In List (AudioClip)", "Replaces all instances of a value in the list with the new value. It will also return the number of values replaced.")]
public class uScriptAct_ReplaceValueInListAudioClip : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] AudioClip[] TargetList, [FriendlyName("Old Value", "The value to be replaced.")] AudioClip OldValue, [FriendlyName("New Value", "The new value to replace the old one.")] AudioClip NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out AudioClip[] ModifiedList, [SocketState(false, false)][FriendlyName("Found", "The number of values that were found and replaced in the list.")] out int ValuesFound)
	{
		List<AudioClip> list = new List<AudioClip>();
		int num = 0;
		if (TargetList.Length > 0)
		{
			foreach (AudioClip audioClip in TargetList)
			{
				if (audioClip == OldValue)
				{
					list.Add(NewValue);
					num++;
				}
				else
				{
					list.Add(audioClip);
				}
			}
			ModifiedList = list.ToArray();
			ValuesFound = num;
		}
		else
		{
			ModifiedList = TargetList;
			ValuesFound = num;
		}
	}
}
