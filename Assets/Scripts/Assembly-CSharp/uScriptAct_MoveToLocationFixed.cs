using System;
using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Move To Location Fixed", "Moves a GameObject to a Vector3 Location.")]
[NodePath("Actions/GameObjects/Movement")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Moves a GameObject to a Vector3 Location.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Move_To_Location")]
public class uScriptAct_MoveToLocationFixed : uScriptLogic
{
	private GameObject[] m_TargetArray;

	private Vector3 m_EndingLocation;

	private Vector3[] m_StartingLocations;

	private bool m_TreatAsOffset;

	private float m_Speed;

	private bool m_Complete = true;

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

	public void In([FriendlyName("Target", "The Target GameObject(s) to be moved.")] GameObject[] targetArray, [FriendlyName("End Location", "The ending location to move the Targets to.")] Vector3 location, [SocketState(false, false)][FriendlyName("Use as Offset", "Whether or not to treat End Location as an offset, rather than an absolute position.")] bool asOffset, [DefaultValue(1f)][FriendlyName("Speed", "The units per second you wish your object to move.")] float speed)
	{
		m_Speed = speed;
		m_TreatAsOffset = asOffset;
		m_TargetArray = targetArray;
		m_EndingLocation = location;
		m_Complete = false;
		m_Cancelled = false;
		m_StartingLocations = new Vector3[m_TargetArray.Length];
		for (int i = 0; i < m_TargetArray.Length; i++)
		{
			GameObject gameObject = m_TargetArray[i];
			if (!(null == gameObject))
			{
				m_StartingLocations[i] = gameObject.transform.position;
			}
		}
	}

	public void Cancel([FriendlyName("Target", "The Target GameObject(s) to be moved.")] GameObject[] targetArray, [FriendlyName("End Location", "The ending location to move the Targets to.")] Vector3 location, [FriendlyName("Use as Offset", "Whether or not to treat End Location as an offset, rather than an absolute position.")][SocketState(false, false)] bool asOffset, [DefaultValue(1f)][FriendlyName("Speed", "The units per second you wish your object to move.")] float speed)
	{
		if (!m_Complete)
		{
			m_Complete = true;
			m_Cancelled = true;
		}
	}

	public void Update()
	{
		if (m_Complete)
		{
			return;
		}
		float num = m_Speed * (Time.deltaTime / (1f / 30f));
		bool flag = true;
		if (m_TreatAsOffset)
		{
			for (int i = 0; i < m_TargetArray.Length; i++)
			{
				GameObject gameObject = m_TargetArray[i];
				if (!(null == gameObject))
				{
					Vector3 vector = m_EndingLocation + m_StartingLocations[i] - gameObject.transform.position;
					if (Vector3.Dot(vector, vector) < num * num)
					{
						gameObject.transform.position = m_EndingLocation + m_StartingLocations[i];
						continue;
					}
					flag = false;
					vector = Vector3.Normalize(vector);
					gameObject.transform.position = gameObject.transform.position + vector * num;
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
					Vector3 vector2 = m_EndingLocation - gameObject2.transform.position;
					if (Vector3.Dot(vector2, vector2) < num * num)
					{
						gameObject2.transform.position = m_EndingLocation;
						continue;
					}
					flag = false;
					vector2 = Vector3.Normalize(vector2);
					gameObject2.transform.position = gameObject2.transform.position + vector2 * num;
				}
			}
		}
		if (flag)
		{
			m_Complete = true;
			if (this.Finished != null)
			{
				this.Finished(this, new EventArgs());
			}
		}
	}
}
