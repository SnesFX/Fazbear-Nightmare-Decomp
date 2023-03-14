using System.Collections.Generic;
using UnityEngine;

[NodePath("Actions/Variables/Lists/AudioClip")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Checks to see if a AudioClip is in a AudioClip List.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Is In List (AudioClip)", "Checks to see if a AudioClip is in a AudioClip List.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_IsInListAudioClip : uScriptLogic
{
	private bool m_InList;

	[FriendlyName("In List")]
	public bool InList
	{
		get
		{
			return m_InList;
		}
	}

	[FriendlyName("Not In List")]
	public bool NotInList
	{
		get
		{
			return !m_InList;
		}
	}

	[FriendlyName("Test If In List")]
	public void TestIfInList([FriendlyName("Target", "The target AudioClip(s) to check for membership in the AudioClip List.")] AudioClip[] Target, [FriendlyName("AudioClip List", "The AudioClip List to check.")] ref AudioClip[] List)
	{
		List<AudioClip> list = new List<AudioClip>(List);
		m_InList = false;
		foreach (AudioClip item in Target)
		{
			if (!list.Contains(item))
			{
				return;
			}
		}
		m_InList = true;
	}
}
