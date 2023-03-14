using System.Collections.Generic;

[NodeToolTip("Checks to see if a int is in a Int List.")]
[NodePath("Actions/Variables/Lists/Int")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Is In List (Int)", "Checks to see if a int is in a Int List.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_IsInListInt : uScriptLogic
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
	public void TestIfInList([FriendlyName("Target", "The target int(s) to check for membership in the Int List.")] int[] Target, [FriendlyName("Int List", "The Int List to check.")] ref int[] List)
	{
		List<int> list = new List<int>(List);
		m_InList = false;
		foreach (int item in Target)
		{
			if (!list.Contains(item))
			{
				return;
			}
		}
		m_InList = true;
	}
}
