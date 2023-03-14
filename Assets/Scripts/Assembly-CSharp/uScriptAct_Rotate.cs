using UnityEngine;

[NodePath("Actions/GameObjects/Movement")]
[FriendlyName("Rotate", "Rotates the target GameObject by a number of degrees over X seconds.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Rotates the target GameObject by a number of degrees over X seconds.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Rotate")]
public class uScriptAct_Rotate : uScriptLogic
{
	private GameObject[] m_Target;

	private float m_Seconds;

	private float m_Time;

	private bool m_Loop;

	private bool m_Done;

	private float m_Degrees;

	private Quaternion[] m_TargetTransforms;

	private Vector3 m_VectorAxis;

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The Target GameObject(s) to rotate.")] GameObject[] Target, [FriendlyName("Degrees", "The number of degrees to rotate.")] float Degrees, [FriendlyName("Axis", "The axis to rotate around.")] string Axis, [FriendlyName("Seconds", "The number of seconds to complete the full rotation.")] float Seconds, [SocketState(false, false)][FriendlyName("Loop", "Whether or not to loop the rotation.")] bool Loop)
	{
		m_Target = new GameObject[Target.Length];
		m_TargetTransforms = new Quaternion[Target.Length];
		if ("x" == Axis || "X" == Axis)
		{
			m_VectorAxis = Vector3.right;
		}
		else if ("y" == Axis || "Y" == Axis)
		{
			m_VectorAxis = Vector3.up;
		}
		else
		{
			m_VectorAxis = Vector3.forward;
		}
		if (Degrees < 0f)
		{
			m_VectorAxis *= -1f;
			Degrees *= -1f;
		}
		int num = 0;
		foreach (GameObject gameObject in Target)
		{
			m_TargetTransforms[num] = gameObject.transform.rotation;
			m_Target[num] = gameObject;
			num++;
		}
		m_Seconds = Seconds;
		m_Degrees = Degrees;
		m_Loop = Loop;
		m_Time = 0f;
		m_Done = false;
	}

	public void Update()
	{
		if (m_Target != null && !m_Done)
		{
			int num = 0;
			if (m_Time > m_Seconds && m_Loop)
			{
				m_Time -= m_Seconds;
			}
			else if (m_Time > m_Seconds)
			{
				m_Time = m_Seconds;
				m_Done = true;
			}
			float num2 = 1f;
			if (m_Seconds != 0f)
			{
				num2 = m_Time / m_Seconds;
			}
			float angle = m_Degrees * num2;
			GameObject[] target = m_Target;
			foreach (GameObject gameObject in target)
			{
				Quaternion quaternion = Quaternion.AngleAxis(angle, m_VectorAxis);
				gameObject.transform.rotation = m_TargetTransforms[num] * quaternion;
				num++;
			}
			m_Time += Time.deltaTime;
		}
	}
}
