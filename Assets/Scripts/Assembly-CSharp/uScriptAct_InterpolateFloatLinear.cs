using UnityEngine;

[NodeToolTip("Linearly interpolate a float over time.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Math/Interpolation")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Interpolate_Float_Linear")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Interpolate Float Linear", "Linearly interpolate a float over time.")]
public class uScriptAct_InterpolateFloatLinear : uScriptLogic
{
	private float m_Start;

	private float m_End;

	private float m_LastValue;

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

	public void Begin(float startValue, float endValue, float time, uScript_Lerper.LoopType loopType, float loopDelay, int loopCount, out float currentValue)
	{
		m_Lerper.Set(time, loopType, loopDelay, loopCount);
		m_Start = startValue;
		m_LastValue = startValue;
		m_End = endValue;
		m_Began = true;
		currentValue = startValue;
	}

	public void Stop(float startValue, float endValue, float time, uScript_Lerper.LoopType loopType, float loopDelay, int loopCount, out float currentValue)
	{
		m_Lerper.Stop();
		currentValue = m_LastValue;
		if (!m_Began)
		{
			currentValue = startValue;
		}
	}

	public void Resume([FriendlyName("Start Value", "Starting value to interpolate from.")] float startValue, [FriendlyName("End Value", "Ending value to interpolate to.")] float endValue, [FriendlyName("Time", "Time to take to complete the interpolation (in seconds).")] float time, [FriendlyName("Loop Type", "The type of looping to use (available values are None, Repeat, and PingPong).")][SocketState(false, false)] uScript_Lerper.LoopType loopType, [SocketState(false, false)][FriendlyName("Loop Delay", "Time delay (in seconds) between loops.")] float loopDelay, [SocketState(false, false)][DefaultValue(-1)][FriendlyName("Loop Count", "Number of times to loop. For infinite looping, use -1 or connect the out socket of this node to its own in and use any positive value.")] int loopCount, [FriendlyName("Output Value", "Current interpolated value.")][SocketState(true, false)] out float currentValue)
	{
		m_Lerper.Resume();
		currentValue = m_LastValue;
		if (!m_Began)
		{
			currentValue = startValue;
		}
	}

	[Driven]
	public bool Driven(out float currentValue)
	{
		float currentTime;
		bool flag = m_Lerper.Run(out currentTime);
		if (flag)
		{
			m_LastValue = Mathf.Lerp(m_Start, m_End, currentTime);
		}
		currentValue = m_LastValue;
		return flag;
	}
}
