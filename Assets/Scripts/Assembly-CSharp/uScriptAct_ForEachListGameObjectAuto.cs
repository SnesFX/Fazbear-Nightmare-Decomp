using UnityEngine;

[FriendlyName("For Each In List Auto (GameObject)", "Iterate through each GameObject in a GameObject list (node will automatically iterate through the list).")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#For_Each_GameObject_In_List_.28Auto.29")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Variables/Lists/GameObject")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Iterate through each GameObject in a GameObject list (node will automatically iterate through the list).")]
public class uScriptAct_ForEachListGameObjectAuto : uScriptLogic
{
	private GameObject[] m_List;

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

	public void In([FriendlyName("GameObject List", "The list of GameObject variables to iterate over.")] GameObject[] GameObjectList, [FriendlyName("Current GameObject", "The GameObject for the current loop iteration.")] out GameObject go, [SocketState(false, false)][FriendlyName("Current Index", "The index value for the current loop iteration.")] out int currentIndex)
	{
		m_List = GameObjectList;
		m_CurrentIndex = 0;
		m_Done = false;
		go = null;
		currentIndex = m_CurrentIndex;
		if (m_List != null)
		{
			if (m_CurrentIndex < m_List.Length)
			{
				go = m_List[m_CurrentIndex];
				currentIndex = m_CurrentIndex;
			}
			m_CurrentIndex++;
		}
		m_ImmediateDone = false;
	}

	[Driven]
	public bool Driven(out GameObject go, out int CurrentIndex)
	{
		go = null;
		CurrentIndex = m_CurrentIndex;
		if (!m_Done && m_List != null)
		{
			if (m_CurrentIndex < m_List.Length)
			{
				go = m_List[m_CurrentIndex];
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
