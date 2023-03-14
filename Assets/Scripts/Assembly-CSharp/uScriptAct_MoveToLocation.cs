using System;
using UnityEngine;

[FriendlyName("Move To Location", "Moves a GameObject to a Vector3 Location.")]
[NodeToolTip("Moves a GameObject to a Vector3 Location.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Move_To_Location")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/GameObjects/Movement")]
public class uScriptAct_MoveToLocation : uScriptLogic
{
	private GameObject[] m_TargetArray;

	private Vector3 m_EndingLocation;

	private Vector3[] m_StartingLocations;

	private bool m_TreatAsOffset;

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

	public void In([FriendlyName("Target", "The Target GameObject(s) to be moved.")] GameObject[] targetArray, [FriendlyName("End Location", "The ending location to move the Targets to.")] Vector3 location, [FriendlyName("Use as Offset", "Whether or not to treat End Location as an offset, rather than an absolute position.")][SocketState(false, false)] bool asOffset, [FriendlyName("Transition Time", "Time to take to move from current position to desired position.")] float totalTime)
	{
		m_Cancelled = false;
		m_TotalTime = totalTime;
		m_CurrentTime = 0f;
		if (m_TotalTime == 0f)
		{
			if (asOffset)
			{
				foreach (GameObject gameObject in targetArray)
				{
					if (!(null == gameObject))
					{
						gameObject.transform.position = gameObject.transform.position + location;
					}
				}
				return;
			}
			foreach (GameObject gameObject2 in targetArray)
			{
				if (!(null == gameObject2))
				{
					gameObject2.transform.position = location;
				}
			}
			return;
		}
		m_TreatAsOffset = asOffset;
		m_TargetArray = targetArray;
		m_EndingLocation = location;
		m_StartingLocations = new Vector3[m_TargetArray.Length];
		for (int k = 0; k < m_TargetArray.Length; k++)
		{
			GameObject gameObject3 = m_TargetArray[k];
			if (!(null == gameObject3))
			{
				m_StartingLocations[k] = gameObject3.transform.position;
			}
		}
	}

	public void Cancel([FriendlyName("Target", "The Target GameObject(s) to be moved.")] GameObject[] targetArray, [FriendlyName("End Location", "The ending location to move the Targets to.")] Vector3 location, [FriendlyName("Use as Offset", "Whether or not to treat End Location as an offset, rather than an absolute position.")] bool asOffset, [FriendlyName("Transition Time", "Time to take to move from current position to desired position.")] float totalTime)
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
		if (m_TreatAsOffset)
		{
			for (int i = 0; i < m_TargetArray.Length; i++)
			{
				GameObject gameObject = m_TargetArray[i];
				if (!(null == gameObject))
				{
					gameObject.transform.position = Vector3.Lerp(m_StartingLocations[i], m_EndingLocation + m_StartingLocations[i], t);
				}
			}
		}
		else
		{
			for (int j = 0; j < m_TargetArray.Length; j++)
			{
				GameObject gameObject2 = m_TargetArray[j];
				if (!(null == gameObject2))
				{
					gameObject2.transform.position = Vector3.Lerp(m_StartingLocations[j], m_EndingLocation, t);
				}
			}
		}
		if (m_CurrentTime == m_TotalTime && this.Finished != null)
		{
			this.Finished(this, new EventArgs());
		}
	}
}
