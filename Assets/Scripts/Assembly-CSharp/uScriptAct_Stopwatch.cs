using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Stopwatch")]
[FriendlyName("Stopwatch", "Used for measuring time like a stopwatch. Start, stop, reset, and check time functions.")]
[NodePath("Actions/Time")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Used for measuring time like a stopwatch. Start, stop, reset, and check time functions.")]
public class uScriptAct_Stopwatch : uScriptLogic
{
	private bool m_TimerRunning;

	private bool m_GoStarted;

	private bool m_GoStopped;

	private bool m_GoReset;

	private bool m_GoCheckedTime;

	private float m_TimeSoFar;

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

	public bool Reset
	{
		get
		{
			return m_GoReset;
		}
	}

	public bool CheckedTime
	{
		get
		{
			return m_GoCheckedTime;
		}
	}

	[FriendlyName("Start")]
	public void StartTimer([FriendlyName("Seconds", "Amount of seconds which passed since stopwatch was started.")] out float Seconds)
	{
		m_GoStarted = true;
		m_GoStopped = false;
		m_GoReset = false;
		m_GoCheckedTime = false;
		m_TimerRunning = true;
		Seconds = m_TimeSoFar;
	}

	public void Stop([FriendlyName("Seconds", "Amount of seconds which passed since stopwatch was started.")] out float Seconds)
	{
		m_GoStarted = false;
		m_GoStopped = true;
		m_GoReset = false;
		m_GoCheckedTime = false;
		m_TimerRunning = false;
		Seconds = m_TimeSoFar;
	}

	[FriendlyName("Reset")]
	public void ResetTimer([FriendlyName("Seconds", "Amount of seconds which passed since stopwatch was started.")] out float Seconds)
	{
		m_GoStarted = false;
		m_GoStopped = false;
		m_GoReset = true;
		m_GoCheckedTime = false;
		m_TimeSoFar = 0f;
		Seconds = m_TimeSoFar;
	}

	public void CheckTime([FriendlyName("Seconds", "Amount of seconds which passed since stopwatch was started.")] out float Seconds)
	{
		m_GoStarted = false;
		m_GoStopped = false;
		m_GoReset = false;
		m_GoCheckedTime = true;
		Seconds = m_TimeSoFar;
	}

	public void Update()
	{
		m_GoStarted = false;
		m_GoStopped = false;
		m_GoReset = false;
		m_GoCheckedTime = false;
		if (m_TimerRunning)
		{
			m_TimeSoFar += Time.deltaTime;
		}
	}
}
