using System.Collections.Generic;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Is In List (String)", "Checks to see if a string is in a String List.")]
[NodePath("Actions/Variables/Lists/String")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Checks to see if a string is in a String List.")]
public class uScriptAct_IsInListString : uScriptLogic
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
	public void TestIfInList([FriendlyName("Target", "The target string(s) to check for membership in the String List.")] string[] Target, [FriendlyName("String List", "The String List to check.")] ref string[] List)
	{
		List<string> list = new List<string>(List);
		m_InList = false;
		foreach (string item in Target)
		{
			if (!list.Contains(item))
			{
				return;
			}
		}
		m_InList = true;
	}
}
