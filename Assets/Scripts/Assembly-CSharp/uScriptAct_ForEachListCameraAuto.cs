using UnityEngine;

[NodeToolTip("Iterate through each camera in a Camera List (node will automatically iterate through the list).")]
[NodePath("Actions/Variables/Lists/Camera")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("For Each In List Auto (Camera)", "Iterate through each camera in a Camera List (node will automatically iterate through the list).")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_ForEachListCameraAuto : uScriptLogic
{
	private Camera[] m_List;

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

	public void In([FriendlyName("Camera List", "The list of Camera variables to iterate over.")] Camera[] List, [FriendlyName("Current Camera", "The Camera for the current loop iteration.")] out Camera Value, [FriendlyName("Current Index", "The index value for the current loop iteration.")][SocketState(false, false)] out int currentIndex)
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
	public bool Driven(out Camera Value, out int CurrentIndex)
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
