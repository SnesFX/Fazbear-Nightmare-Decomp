using UnityEngine;

[NodePath("Actions/Variables/Lists/Color")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Iterate through each Color in a Color List (node will automatically iterate through the list).")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("For Each In List Auto (Color)", "Iterate through each Color in a Color List (node will automatically iterate through the list).")]
public class uScriptAct_ForEachListColorAuto : uScriptLogic
{
	private Color[] m_List;

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

	public void In([FriendlyName("Color List", "The list of Color variables to iterate over.")] Color[] List, [FriendlyName("Current Color", "The Color for the current loop iteration.")] out Color Value, [FriendlyName("Current Index", "The index value for the current loop iteration.")][SocketState(false, false)] out int currentIndex)
	{
		m_List = List;
		m_CurrentIndex = 0;
		m_Done = false;
		Value = Color.white;
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
	public bool Driven(out Color Value, out int CurrentIndex)
	{
		Value = Color.white;
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
