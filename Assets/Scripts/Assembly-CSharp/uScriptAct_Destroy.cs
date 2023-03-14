using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Destroys the target GameObject.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/GameObjects")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Destroy")]
[FriendlyName("Destroy", "Destroys the target GameObject.")]
public class uScriptAct_Destroy : uScriptLogic
{
	private bool m_GuaranteedOneTick;

	private bool m_ObjectsDestroyed;

	private bool m_Out;

	private float m_DelayTime;

	public bool Out
	{
		get
		{
			return m_Out;
		}
	}

	[FriendlyName("Objects Destroyed")]
	public bool ObjectsDestroyed
	{
		get
		{
			return m_ObjectsDestroyed;
		}
	}

	public void In([FriendlyName("Target", "The target GameObject(s) to destroy.")] GameObject[] Target, [SocketState(false, false)][FriendlyName("Delay", "The time to wait before destroying the target object(s).")] float DelayTime)
	{
		m_Out = true;
		m_ObjectsDestroyed = false;
		m_GuaranteedOneTick = false;
		m_DelayTime = Time.time + DelayTime;
		if (DelayTime > 0f)
		{
			foreach (GameObject gameObject in Target)
			{
				if (gameObject != null)
				{
					Object.Destroy(gameObject, DelayTime);
				}
			}
			return;
		}
		foreach (GameObject gameObject2 in Target)
		{
			if (gameObject2 != null)
			{
				Object.Destroy(gameObject2);
			}
		}
	}

	[Driven]
	public bool WaitOneTick()
	{
		m_Out = false;
		if (Time.time <= m_DelayTime)
		{
			return true;
		}
		if (!m_GuaranteedOneTick)
		{
			m_GuaranteedOneTick = true;
			return true;
		}
		if (!m_ObjectsDestroyed)
		{
			m_ObjectsDestroyed = true;
			return true;
		}
		return false;
	}
}
