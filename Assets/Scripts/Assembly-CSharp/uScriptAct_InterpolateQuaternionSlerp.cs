using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Interpolate_Quaternion_Slerp")]
[NodePath("Actions/Math/Interpolation")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Interpolate Quaternion Slerp", "Spherical Linear Interpolation of a Quaternion over time.")]
[NodeToolTip("Spherical Linear Interpolation of a Quaternion over time.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_InterpolateQuaternionSlerp : uScriptLogic
{
	private Quaternion m_Start;

	private Quaternion m_End;

	private Quaternion m_LastValue;

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

	public void Begin(Quaternion startValue, Quaternion endValue, float time, uScript_Lerper.LoopType loopType, float loopDelay, int loopCount, out Quaternion currentValue)
	{
		m_Lerper.Set(time, loopType, loopDelay, loopCount);
		m_Start = startValue;
		m_LastValue = startValue;
		m_End = endValue;
		m_Began = true;
		currentValue = startValue;
	}

	public void Stop(Quaternion startValue, Quaternion endValue, float time, uScript_Lerper.LoopType loopType, float loopDelay, int loopCount, out Quaternion currentValue)
	{
		m_Lerper.Stop();
		currentValue = m_LastValue;
		if (!m_Began)
		{
			currentValue = startValue;
		}
	}

	public void Resume([FriendlyName("Start Value", "Starting value to interpolate from.")] Quaternion startValue, [FriendlyName("End Value", "Ending value to interpolate to.")] Quaternion endValue, [FriendlyName("Time", "Time to take to complete the interpolation (in seconds).")] float time, [FriendlyName("Loop Type", "The type of looping to use (available values are None, Repeat, and PingPong).")][SocketState(false, false)] uScript_Lerper.LoopType loopType, [SocketState(false, false)][FriendlyName("Loop Delay", "Time delay (in seconds) between loops.")] float loopDelay, [FriendlyName("Loop Count", "Number of times to loop. For infinite looping, use -1 or connect the out socket of this node to its own in and use any positive value.")][SocketState(false, false)][DefaultValue(-1)] int loopCount, [FriendlyName("Output Value", "Current interpolated value.")] out Quaternion currentValue)
	{
		m_Lerper.Resume();
		currentValue = m_LastValue;
		if (!m_Began)
		{
			currentValue = startValue;
		}
	}

	[Driven]
	public bool Driven(out Quaternion currentValue)
	{
		float currentTime;
		bool flag = m_Lerper.Run(out currentTime);
		if (flag)
		{
			m_LastValue = Quaternion.Slerp(m_Start, m_End, currentTime);
		}
		currentValue = m_LastValue;
		return flag;
	}
}
