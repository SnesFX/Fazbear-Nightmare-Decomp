using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Linearly interpolate a Rect over time.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Interpolate_Rect_Linear")]
[FriendlyName("Interpolate Rect Linear", "Linearly interpolate a Rect over time.")]
[NodePath("Actions/Math/Interpolation")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_InterpolateRectLinear : uScriptLogic
{
	private Rect m_Start;

	private Rect m_End;

	private Rect m_LastValue;

	private bool m_Began;

	private uScript_Lerper m_Lerper = new uScript_Lerper();

	public bool Started
	{
		get
		{
			return m_Lerper.AllowStartedOutput;
		}
	}

	public bool Stopped
	{
		get
		{
			return m_Lerper.AllowStoppedOutput;
		}
	}

	public bool Interpolating
	{
		get
		{
			return m_Lerper.AllowInterpolatingOutput;
		}
	}

	public bool Finished
	{
		get
		{
			return m_Lerper.AllowFinishedOutput;
		}
	}

	public void Begin(Rect startValue, Rect endValue, float time, uScript_Lerper.LoopType loopType, float loopDelay, int loopCount, out Rect currentValue)
	{
		m_Lerper.Set(time, loopType, loopDelay, loopCount);
		m_Start = startValue;
		m_LastValue = startValue;
		m_End = endValue;
		m_Began = true;
		currentValue = startValue;
	}

	public void Stop(Rect startValue, Rect endValue, float time, uScript_Lerper.LoopType loopType, float loopDelay, int loopCount, out Rect currentValue)
	{
		m_Lerper.Stop();
		currentValue = m_LastValue;
		if (!m_Began)
		{
			currentValue = startValue;
		}
	}

	public void Resume([FriendlyName("Start Value", "Starting value to interpolate from.")] Rect startValue, [FriendlyName("End Value", "Ending value to interpolate to.")] Rect endValue, [FriendlyName("Time", "Time to take to complete the interpolation (in seconds).")] float time, [SocketState(false, false)][FriendlyName("Loop Type", "The type of looping to use (available values are None, Repeat, and PingPong).")] uScript_Lerper.LoopType loopType, [SocketState(false, false)][FriendlyName("Loop Delay", "Time delay (in seconds) between loops.")] float loopDelay, [SocketState(false, false)][DefaultValue(-1)][FriendlyName("Loop Count", "Number of times to loop. For infinite looping, use -1 or connect the out socket of this node to its own in and use any positive value.")] int loopCount, [FriendlyName("Output Value", "Current interpolated value.")] out Rect currentValue)
	{
		m_Lerper.Resume();
		currentValue = m_LastValue;
		if (!m_Began)
		{
			currentValue = startValue;
		}
	}

	[Driven]
	public bool Driven(out Rect currentValue)
	{
		float currentTime;
		bool flag = m_Lerper.Run(out currentTime);
		if (flag)
		{
			float left = Mathf.Lerp(m_Start.x, m_End.x, currentTime);
			float top = Mathf.Lerp(m_Start.y, m_End.y, currentTime);
			float width = Mathf.Lerp(m_Start.width, m_End.width, currentTime);
			float height = Mathf.Lerp(m_Start.height, m_End.height, currentTime);
			m_LastValue = new Rect(left, top, width, height);
		}
		currentValue = m_LastValue;
		return flag;
	}
}
