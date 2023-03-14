using UnityEngine;

[FriendlyName("For Each In List (Vector4)", "Iterates through a list, one item at a time, and returns the current item.\n\nNote: uScript events must drive each iteration.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Iterate through each Vector4 in a Vector4 List (uScript events must drive each iteration). Note that the list will be stored until all items have been iterated through or Reset is hit with a new list (which can only be done using named list variables).")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Lists/Vector4")]
public class uScriptAct_ForEachListVector4 : uScriptLogic
{
	private Vector4[] m_List;

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

	[FriendlyName("Reset")]
	public void Reset(Vector4[] List, out Vector4 Value, out int currentIndex)
	{
		Value = new Vector4(0f, 0f, 0f, 0f);
		m_List = List;
		m_CurrentIndex = 0;
		currentIndex = m_CurrentIndex;
		m_Done = false;
		m_ImmediateDone = false;
	}

	public void In([FriendlyName("List", "The list to iterate over.")] Vector4[] List, [FriendlyName("Current", "The item for the current loop iteration.")] out Vector4 Value, [FriendlyName("Current Index", "The index value for the current loop iteration.")][SocketState(false, false)] out int currentIndex)
	{
		if (m_List == null)
		{
			m_List = List;
			m_CurrentIndex = 0;
			m_Done = false;
		}
		m_ImmediateDone = m_List == null || m_CurrentIndex != 0;
		Value = new Vector4(0f, 0f, 0f, 0f);
		currentIndex = m_CurrentIndex;
		if (m_List != null)
		{
			if (m_CurrentIndex < m_List.Length)
			{
				Value = m_List[m_CurrentIndex];
				currentIndex = m_CurrentIndex;
			}
			m_CurrentIndex++;
			if (m_CurrentIndex > m_List.Length)
			{
				m_List = null;
				m_Done = true;
			}
		}
	}
}
