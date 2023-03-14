using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Count_Time")]
[NodeDeprecated]
[FriendlyName("Count Time", "Counts the amount of time that elapses between starting and stopping.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Time")]
[NodeToolTip("Counts the amount of time that elapses between starting and stopping.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_CountTime : uScriptLogic
{
	private bool m_TimerStarted;

	private bool m_GoStarted;

	private bool m_GoStopped;

	private float m_TotalTime;

	private float m_StartTime;

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public bool Started
	{
		get
		{
			return m_GoStarted;
		}
	}

	public bool Stopped
	{
		get
		{
			return m_GoStopped;
		}
	}

	[FriendlyName("Start")]
	public void In([FriendlyName("Seconds", "Amount of seconds which passed since Start was called.")] out float seconds)
	{
		m_TotalTime = 0f;
		seconds = m_TotalTime;
		m_TimerStarted = true;
		m_GoStarted = true;
		m_GoStopped = false;
	}

	public void Stop([FriendlyName("Seconds", "Amount of seconds which passed since Start was called.")] out float Seconds)
	{
		m_GoStarted = false;
		m_GoStopped = true;
		m_TimerStarted = false;
		Seconds = m_TotalTime - m_StartTime;
		m_TotalTime = 0f;
	}

	public void Update()
	{
		m_GoStarted = false;
		m_GoStopped = false;
		if (m_TimerStarted)
		{
			m_TotalTime = Time.time;
		}
		else
		{
			m_StartTime = Time.time;
		}
	}
}
