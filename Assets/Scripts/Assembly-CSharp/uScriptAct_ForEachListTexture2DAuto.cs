using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Variables/Lists/Texture2D")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Iterate through each Texture2D in a Texture2D List (node will automatically iterate through the list).")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("For Each In List Auto (Texture2D)", "Iterate through each Texture2D in a Texture2D List (node will automatically iterate through the list).")]
public class uScriptAct_ForEachListTexture2DAuto : uScriptLogic
{
	private Texture2D[] m_List;

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

	public void In([FriendlyName("Texture2D List", "The list of Texture2D variables to iterate over.")] Texture2D[] List, [FriendlyName("Current Texture2D", "The Texture2D for the current loop iteration.")] out Texture2D Value, [FriendlyName("Current Index", "The index value for the current loop iteration.")][SocketState(false, false)] out int currentIndex)
	{
		m_List = List;
		m_CurrentIndex = 0;
		m_Done = false;
		Value = null;
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
	public bool Driven(out Texture2D Value, out int CurrentIndex)
	{
		Value = null;
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
