[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Iterate through each float in a Float List (uScript events must drive each iteration). Note that the list will be stored until all items have been iterated through or Reset is hit with a new list (which can only be done using named list variables).")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("For Each In List (Float)", "Iterates through a list, one item at a time, and returns the current item.\n\nNote: uScript events must drive each iteration.")]
[NodePath("Actions/Variables/Lists/Float")]
public class uScriptAct_ForEachListFloat : uScriptLogic
{
	private float[] m_List;

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
	public void Reset(float[] List, out float Value, out int currentIndex)
	{
		Value = 0f;
		m_List = List;
		m_CurrentIndex = 0;
		currentIndex = m_CurrentIndex;
		m_Done = false;
		m_ImmediateDone = false;
	}

	public void In([FriendlyName("List", "The list to iterate over.")] float[] List, [FriendlyName("Current", "The item for the current loop iteration.")] out float Value, [SocketState(false, false)][FriendlyName("Current Index", "The index value for the current loop iteration.")] out int currentIndex)
	{
		if (m_List == null)
		{
			m_List = List;
			m_CurrentIndex = 0;
			m_Done = false;
		}
		m_ImmediateDone = m_List == null || m_CurrentIndex != 0;
		Value = 0f;
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
