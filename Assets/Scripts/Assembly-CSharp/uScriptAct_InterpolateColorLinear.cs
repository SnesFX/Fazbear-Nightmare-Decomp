using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Linearly interpolate a color over time.")]
[FriendlyName("Interpolate Color Linear", "Linearly interpolate a color over time.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Interpolate_Color_Linear")]
[NodePath("Actions/Math/Interpolation")]
public class uScriptAct_InterpolateColorLinear : uScriptLogic
{
	private Color m_Start;

	private Color m_End;

	private Color m_LastValue;

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

	public void Begin(Color startValue, Color endValue, float time, uScript_Lerper.LoopType loopType, float loopDelay, int loopCount, out Color currentValue)
	{
		m_Lerper.Set(time, loopType, loopDelay, loopCount);
		m_Start = startValue;
		m_LastValue = startValue;
		m_End = endValue;
		m_Began = true;
		currentValue = startValue;
	}

	public void Stop(Color startValue, Color endValue, float time, uScript_Lerper.LoopType loopType, float loopDelay, int loopCount, out Color currentValue)
	{
		m_Lerper.Stop();
		currentValue = m_LastValue;
		if (!m_Began)
		{
			currentValue = startValue;
		}
	}

	public void Resume([FriendlyName("Start Value", "Starting value to interpolate from.")] Color startValue, [FriendlyName("End Value", "Ending value to interpolate to.")] Color endValue, [FriendlyName("Time", "Time to take to complete the interpolation (in seconds).")] float time, [SocketState(false, false)][FriendlyName("Loop Type", "The type of looping to use (available values are None, Repeat, and PingPong).")] uScript_Lerper.LoopType loopType, [FriendlyName("Loop Delay", "Time delay (in seconds) between loops.")][SocketState(false, false)] float loopDelay, [FriendlyName("Loop Count", "Number of times to loop. For infinite looping, use -1 or connect the out socket of this node to its own in and use any positive value.")][DefaultValue(-1)][SocketState(false, false)] int loopCount, [FriendlyName("Output Value", "Current interpolated value.")] out Color currentValue)
	{
		m_Lerper.Resume();
		currentValue = m_LastValue;
		if (!m_Began)
		{
			currentValue = startValue;
		}
	}

	[Driven]
	public bool Driven(out Color currentValue)
	{
		float currentTime;
		bool flag = m_Lerper.Run(out currentTime);
		if (flag)
		{
			m_LastValue = Color.Lerp(m_Start, m_End, currentTime);
		}
		currentValue = m_LastValue;
		return flag;
	}
}
