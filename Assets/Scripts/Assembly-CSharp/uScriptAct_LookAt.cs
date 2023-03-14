using System;
using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/GameObjects/Movement")]
[FriendlyName("Look At", "Tells a GameObject (target) to look at another GameObject (focus) transform or Vector3 position in a specified amount of time (seconds).")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Look_At")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Tells a GameObject to look at another GameObject transform or Vector3 position.")]
public class uScriptAct_LookAt : uScriptLogic
{
	public enum LockAxis
	{
		None = 0,
		X = 1,
		Y = 2,
		Z = 3
	}

	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private float m_Time;

	private float m_TotalTime;

	private GameObject[] m_Targets;

	private Quaternion[] m_StartRotations;

	private Vector3[] m_StartPositions;

	private GameObject m_Focus;

	private Vector3 m_FocusPosition;

	private LockAxis m_LockAxis;

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public event uScriptEventHandler Finished;

	public void In([FriendlyName("Target", "The Target GameObject(s) whose look direction will be adjusted.")] GameObject[] Target, [FriendlyName("Focus", "The item to focus on - can be a Vector3 position or a GameObject.")] object Focus, [FriendlyName("Seconds", "The amount of time (in seconds) it takes to complete the look.  Use 0 for an instantaneous look.")] float time, [SocketState(false, false)][FriendlyName("Lock Axis", "Use this to lock rotation on the specified axis.")] LockAxis lockAxis)
	{
		if (Focus != null)
		{
			m_Time = 0f;
			m_TotalTime = time;
			m_Targets = null;
			m_Focus = null;
			m_LockAxis = lockAxis;
			if (typeof(GameObject) == Focus.GetType())
			{
				m_Focus = (GameObject)Focus;
				m_FocusPosition = ((GameObject)Focus).transform.position;
			}
			else if (typeof(Vector3) == Focus.GetType())
			{
				m_FocusPosition = (Vector3)Focus;
			}
			else
			{
				m_FocusPosition = Vector3.forward;
			}
			m_Targets = Target;
			m_StartRotations = new Quaternion[m_Targets.Length];
			m_StartPositions = new Vector3[m_Targets.Length];
			for (int i = 0; i < m_Targets.Length; i++)
			{
				if (!(null == m_Targets[i]))
				{
					m_StartRotations[i] = m_Targets[i].transform.rotation;
					m_StartPositions[i] = m_Targets[i].transform.position;
				}
			}
		}
		if (m_TotalTime == 0f)
		{
			Update();
		}
	}

	public void Update()
	{
		if (m_Targets == null)
		{
			return;
		}
		float num = ((m_TotalTime == 0f) ? 1f : (m_Time / m_TotalTime));
		if (num > 1f)
		{
			num = 1f;
		}
		if (null != m_Focus)
		{
			m_FocusPosition = m_Focus.transform.position;
		}
		for (int i = 0; i < m_Targets.Length; i++)
		{
			if (null == m_Targets[i])
			{
				continue;
			}
			Vector3 upwards = Vector3.up;
			Vector3 forward = m_FocusPosition - m_StartPositions[i];
			if (m_LockAxis != 0)
			{
				if (m_LockAxis == LockAxis.X)
				{
					upwards = Vector3.right;
					forward.x = 0f;
				}
				else if (m_LockAxis == LockAxis.Y)
				{
					upwards = Vector3.up;
					forward.y = 0f;
				}
				else if (m_LockAxis == LockAxis.Z)
				{
					upwards = Vector3.forward;
					forward.z = 0f;
				}
				forward.Normalize();
			}
			Quaternion to = Quaternion.LookRotation(forward, upwards);
			m_Targets[i].transform.rotation = Quaternion.Slerp(m_StartRotations[i], to, num);
		}
		if (num == 1f)
		{
			if (this.Finished != null)
			{
				this.Finished(this, EventArgs.Empty);
			}
			m_Targets = null;
		}
		m_Time += Time.deltaTime;
	}
}
