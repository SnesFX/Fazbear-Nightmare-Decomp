using System;
using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Move_To_Location_Relative")]
[FriendlyName("Move To Location Relative", "Moves a GameObject to a Vector3 Location relative to another GameObject.")]
[NodePath("Actions/GameObjects/Movement")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Moves a GameObject to a Vector3 Location Relative to another GameObject.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_MoveToLocationRelative : uScriptLogic
{
	private GameObject[] m_TargetArray;

	private Vector3 m_EndingLocation;

	private Vector3[] m_StartingLocations;

	private GameObject m_Source;

	private float m_TotalTime;

	private float m_CurrentTime;

	private bool m_Cancelled;

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public bool Cancelled
	{
		get
		{
			return m_Cancelled;
		}
	}

	public event EventHandler Finished;

	public void In([FriendlyName("Target", "The Target GameObject(s) to be moved.")] GameObject[] targetArray, [FriendlyName("End Location", "The ending location to move the Targets to.")] Vector3 location, [FriendlyName("Source", "The Source GameObject which Target will move relative to.")] GameObject source, [FriendlyName("Transition Time", "Time to take to move from current position to desired position.")] float totalTime)
	{
		if (null == source)
		{
			return;
		}
		m_Cancelled = false;
		m_TotalTime = totalTime;
		m_CurrentTime = 0f;
		m_Source = source;
		m_EndingLocation = location;
		if (m_TotalTime == 0f)
		{
			foreach (GameObject gameObject in targetArray)
			{
				if (!(null == gameObject))
				{
					gameObject.transform.position = m_Source.transform.position + m_EndingLocation;
				}
			}
			return;
		}
		m_TargetArray = targetArray;
		m_StartingLocations = new Vector3[m_TargetArray.Length];
		for (int j = 0; j < m_TargetArray.Length; j++)
		{
			GameObject gameObject2 = m_TargetArray[j];
			if (!(null == gameObject2))
			{
				m_StartingLocations[j] = gameObject2.transform.position;
			}
		}
	}

	public void Cancel([FriendlyName("Target", "The Target GameObject(s) to be moved.")] GameObject[] targetArray, [FriendlyName("End Location", "The ending location to move the Targets to.")] Vector3 location, [FriendlyName("Source", "The Source GameObject which Target will move relative to.")] GameObject source, [FriendlyName("Transition Time", "Time to take to move from current position to desired position.")] float totalTime)
	{
		if (m_CurrentTime != m_TotalTime)
		{
			m_Cancelled = true;
			m_CurrentTime = m_TotalTime;
		}
	}

	public void Update()
	{
		if (m_CurrentTime == m_TotalTime)
		{
			return;
		}
		m_CurrentTime += Time.deltaTime;
		if (m_CurrentTime >= m_TotalTime)
		{
			m_CurrentTime = m_TotalTime;
		}
		float t = m_CurrentTime / m_TotalTime;
		for (int i = 0; i < m_TargetArray.Length; i++)
		{
			GameObject gameObject = m_TargetArray[i];
			if (!(null == gameObject))
			{
				gameObject.transform.position = Vector3.Lerp(m_StartingLocations[i], m_Source.transform.position + m_EndingLocation, t);
			}
		}
		if (m_CurrentTime == m_TotalTime && this.Finished != null)
		{
			this.Finished(this, new EventArgs());
		}
	}
}
