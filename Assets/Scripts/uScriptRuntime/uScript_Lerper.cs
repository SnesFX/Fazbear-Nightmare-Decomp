using UnityEngine;

public class uScript_Lerper
{
	public enum LoopType
	{
		None = 0,
		Repeat = 1,
		PingPong = 2
	}

	private float m_TotalTime;

	private float m_CurrentTime;

	private float m_LoopDelay;

	private int m_LoopCount;

	private int m_LoopIteration;

	private float m_LoopRestartCountdown;

	private bool m_IsReversed;

	private bool m_Running;

	private LoopType m_LoopType;

	public bool AllowStoppedOutput;

	public bool AllowStartedOutput;

	public bool AllowInterpolatingOutput;

	public bool AllowFinishedOutput;

	public void Set(float time, LoopType loopType, float loopDelay, int loopCount)
	{
		m_CurrentTime = 0f;
		m_TotalTime = time;
		m_LoopIteration = 0;
		m_LoopDelay = loopDelay;
		m_LoopRestartCountdown = 0f;
		m_Running = true;
		AllowInterpolatingOutput = false;
		AllowStoppedOutput = false;
		AllowFinishedOutput = false;
		AllowStartedOutput = true;
		m_IsReversed = false;
		m_LoopType = loopType;
		m_LoopCount = loopCount;
		if (m_LoopType == LoopType.None)
		{
			m_LoopCount = 1;
		}
		if (m_LoopIteration == m_LoopCount)
		{
			m_Running = false;
		}
		else
		{
			m_LoopIteration++;
		}
	}

	public bool Run(out float currentTime)
	{
		AllowInterpolatingOutput = false;
		AllowStoppedOutput = false;
		AllowFinishedOutput = false;
		AllowStartedOutput = false;
		if (m_LoopRestartCountdown > 0f)
		{
			m_LoopRestartCountdown -= Time.deltaTime;
			if (m_LoopRestartCountdown <= 0f)
			{
				m_Running = true;
				m_LoopRestartCountdown = 0f;
				m_LoopIteration++;
			}
		}
		currentTime = 1f;
		if (m_TotalTime != 0f)
		{
			currentTime = m_CurrentTime / m_TotalTime;
		}
		if (m_Running)
		{
			if (!m_IsReversed && m_CurrentTime == m_TotalTime)
			{
				m_Running = false;
			}
			else if (m_IsReversed && m_CurrentTime == 0f)
			{
				m_Running = false;
			}
			if (!m_Running && (m_LoopIteration < m_LoopCount || m_LoopCount == -1))
			{
				m_LoopRestartCountdown = m_LoopDelay;
				if (LoopType.PingPong == m_LoopType)
				{
					m_IsReversed = !m_IsReversed;
					if (0f == m_LoopRestartCountdown)
					{
						m_Running = true;
					}
				}
				else if (LoopType.Repeat == m_LoopType)
				{
					m_CurrentTime = ((!m_IsReversed) ? 0f : m_TotalTime);
					if (0f == m_LoopRestartCountdown)
					{
						m_Running = true;
					}
				}
			}
			if (m_Running)
			{
				AllowInterpolatingOutput = true;
				m_CurrentTime = ((!m_IsReversed) ? (m_CurrentTime + Time.deltaTime) : (m_CurrentTime - Time.deltaTime));
				m_CurrentTime = Mathf.Clamp(m_CurrentTime, 0f, m_TotalTime);
			}
			else if (m_LoopRestartCountdown == 0f)
			{
				AllowFinishedOutput = true;
			}
			return true;
		}
		return m_LoopRestartCountdown > 0f;
	}

	public void Stop()
	{
		m_Running = false;
		AllowInterpolatingOutput = false;
		AllowStoppedOutput = true;
		AllowFinishedOutput = false;
		AllowStartedOutput = false;
	}

	public void Resume()
	{
		AllowStoppedOutput = false;
		AllowStartedOutput = false;
		AllowFinishedOutput = false;
		AllowInterpolatingOutput = false;
		if (m_CurrentTime < m_TotalTime)
		{
			m_Running = true;
			AllowStartedOutput = true;
		}
		else
		{
			AllowStoppedOutput = true;
		}
	}
}
