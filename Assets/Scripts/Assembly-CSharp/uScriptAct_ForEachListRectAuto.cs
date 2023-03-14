using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeToolTip("Iterate through each Rect in a Rect List (node will automatically iterate through the list).")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Lists/Rect")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("For Each In List Auto (Rect)", "Iterate through each Rect in a Rect List (node will automatically iterate through the list).")]
public class uScriptAct_ForEachListRectAuto : uScriptLogic
{
	private Rect[] m_List;

	private int m_CurrentIndex;

	private bool m_Done;

	private bool m_ImmediateDone;

	public bool Immediate
	{
		get
		{
			if (!m_ImmediateDone)
			{
				m_ImmediateDone = true;
				return true;
			}
			return false;
		}
	}

	[FriendlyName("Done Iterating")]
	public bool Done
	{
		get
		{
			return m_Done;
		}
	}

	[FriendlyName("Iteration")]
	public bool Iteration
	{
		get
		{
			return m_List != null && m_CurrentIndex <= m_List.Length && m_CurrentIndex != 0;
		}
	}

	public void In([FriendlyName("Rect List", "The list of Rect variables to iterate over.")] Rect[] List, [FriendlyName("Current Rect", "The Rect for the current loop iteration.")] out Rect Value, [SocketState(false, false)][FriendlyName("Current Index", "The index value for the current loop iteration.")] out int currentIndex)
	{
		m_List = List;
		m_CurrentIndex = 0;
		m_Done = false;
		Value = new Rect(0f, 0f, 0f, 0f);
		currentIndex = m_CurrentIndex;
		if (m_List != null)
		{
			if (m_CurrentIndex < m_List.Length)
			{
				Value = m_List[m_CurrentIndex];
				currentIndex = m_CurrentIndex;
			}
			m_CurrentIndex++;
		}
		m_ImmediateDone = false;
	}

	[Driven]
	public bool Driven(out Rect Value, out int CurrentIndex)
	{
		Value = new Rect(0f, 0f, 0f, 0f);
		CurrentIndex = m_CurrentIndex;
		if (!m_Done && m_List != null)
		{
			if (m_CurrentIndex < m_List.Length)
			{
				Value = m_List[m_CurrentIndex];
				CurrentIndex = m_CurrentIndex;
			}
			m_CurrentIndex++;
			if (m_CurrentIndex > m_List.Length)
			{
				m_List = null;
				m_Done = true;
			}
			return true;
		}
		return false;
	}
}
