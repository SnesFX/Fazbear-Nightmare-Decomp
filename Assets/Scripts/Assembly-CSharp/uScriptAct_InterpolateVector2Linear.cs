using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Interpolate Vector2 Linear", "Linearly interpolate a Vector2 over time.")]
[NodeToolTip("Linearly interpolate a Vector2 over time.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Interpolate_Vector2_Linear")]
[NodePath("Actions/Math/Interpolation")]
public class uScriptAct_InterpolateVector2Linear : uScriptLogic
{
	private Vector2 m_Start;

	private Vector2 m_End;

	private Vector2 m_LastValue;

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

	public void Begin(Vector2 startValue, Vector2 endValue, float time, uScript_Lerper.LoopType loopType, float loopDelay, int loopCount, out Vector2 currentValue)
	{
		m_Lerper.Set(time, loopType, loopDelay, loopCount);
		m_Start = startValue;
		m_LastValue = startValue;
		m_End = endValue;
		m_Began = true;
		currentValue = startValue;
	}

	public void Stop(Vector2 startValue, Vector2 endValue, float time, uScript_Lerper.LoopType loopType, float loopDelay, int loopCount, out Vector2 currentValue)
	{
		m_Lerper.Stop();
		currentValue = m_LastValue;
		if (!m_Began)
		{
			currentValue = startValue;
		}
	}

	public void Resume([FriendlyName("Start Value", "Starting value to interpolate from.")] Vector2 startValue, [FriendlyName("End Value", "Ending value to interpolate to.")] Vector2 endValue, [FriendlyName("Time", "Time to take to complete the interpolation (in seconds).")] float time, [FriendlyName("Loop Type", "The type of looping to use (available values are None, Repeat, and PingPong).")][SocketState(false, false)] uScript_Lerper.LoopType loopType, [SocketState(false, false)][FriendlyName("Loop Delay", "Time delay (in seconds) between loops.")] float loopDelay, [DefaultValue(-1)][SocketState(false, false)][FriendlyName("Loop Count", "Number of times to loop. For infinite looping, use -1 or connect the out socket of this node to its own in and use any positive value.")] int loopCount, [FriendlyName("Output Value", "Current interpolated value.")] out Vector2 currentValue)
	{
		m_Lerper.Resume();
		currentValue = m_LastValue;
		if (!m_Began)
		{
			currentValue = startValue;
		}
	}

	[Driven]
	public bool Driven(out Vector2 currentValue)
	{
		float currentTime;
		bool flag = m_Lerper.Run(out currentTime);
		if (flag)
		{
			m_LastValue = Vector2.Lerp(m_Start, m_End, currentTime);
		}
		currentValue = m_LastValue;
		return flag;
	}
}
