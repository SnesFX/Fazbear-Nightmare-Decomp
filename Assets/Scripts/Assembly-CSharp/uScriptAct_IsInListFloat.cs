using System.Collections.Generic;

[FriendlyName("Is In List (Float)", "Checks to see if a int is in a Float List.")]
[NodePath("Actions/Variables/Lists/Float")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Checks to see if a int is in a Float List.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_IsInListFloat : uScriptLogic
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
	public void TestIfInList([FriendlyName("Target", "The target int(s) to check for membership in the Float List.")] int[] Target, [FriendlyName("Float List", "The Float List to check.")] ref float[] List)
	{
		List<float> list = new List<float>(List);
		m_InList = false;
		foreach (float item in Target)
		{
			if (!list.Contains(item))
			{
				return;
			}
		}
		m_InList = true;
	}
}
