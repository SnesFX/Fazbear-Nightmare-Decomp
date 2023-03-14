using UnityEngine;

[FriendlyName("Switch", "Allows the signal to pass through each output socket in order.")]
[NodeToolTip("Allows the signal to pass through each output socket in order.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Switch")]
[NodePath("Conditions/Switches")]
public class uScriptCon_Switch : uScriptLogic
{
	private int m_CurrentOutput = 1;

	private bool m_SwitchOpen = true;

	private bool m_Output1;

	private bool m_Output2;

	private bool m_Output3;

	private bool m_Output4;

	private bool m_Output5;

	private bool m_Output6;

	[FriendlyName("Output 1")]
	public bool Output1
	{
		get
		{
			return m_Output1;
		}
	}

	[FriendlyName("Output 2")]
	public bool Output2
	{
		get
		{
			return m_Output2;
		}
	}

	[FriendlyName("Output 3")]
	public bool Output3
	{
		get
		{
			return m_Output3;
		}
	}

	[FriendlyName("Output 4")]
	public bool Output4
	{
		get
		{
			return m_Output4;
		}
	}

	[FriendlyName("Output 5")]
	public bool Output5
	{
		get
		{
			return m_Output5;
		}
	}

	[FriendlyName("Output 6")]
	public bool Output6
	{
		get
		{
			return m_Output6;
		}
	}

	public void In(bool Loop, int MaxOutputUsed, out int CurrentOutput)
	{
		m_Output1 = false;
		m_Output2 = false;
		m_Output3 = false;
		m_Output4 = false;
		m_Output5 = false;
		m_Output6 = false;
		Mathf.Clamp(MaxOutputUsed, 1, 6);
		if (m_SwitchOpen)
		{
			switch (m_CurrentOutput)
			{
			case 1:
				CurrentOutput = m_CurrentOutput;
				m_Output1 = true;
				if (m_CurrentOutput < MaxOutputUsed)
				{
					m_CurrentOutput = 2;
				}
				else if (Loop)
				{
					m_CurrentOutput = 1;
				}
				else
				{
					m_SwitchOpen = false;
				}
				break;
			case 2:
				CurrentOutput = m_CurrentOutput;
				m_Output2 = true;
				if (m_CurrentOutput < MaxOutputUsed)
				{
					m_CurrentOutput = 3;
				}
				else if (Loop)
				{
					m_CurrentOutput = 1;
				}
				else
				{
					m_SwitchOpen = false;
				}
				break;
			case 3:
				CurrentOutput = m_CurrentOutput;
				m_Output3 = true;
				if (m_CurrentOutput < MaxOutputUsed)
				{
					m_CurrentOutput = 4;
				}
				else if (Loop)
				{
					m_CurrentOutput = 1;
				}
				else
				{
					m_SwitchOpen = false;
				}
				break;
			case 4:
				CurrentOutput = m_CurrentOutput;
				m_Output4 = true;
				if (m_CurrentOutput < MaxOutputUsed)
				{
					m_CurrentOutput = 5;
				}
				else if (Loop)
				{
					m_CurrentOutput = 1;
				}
				else
				{
					m_SwitchOpen = false;
				}
				break;
			case 5:
				CurrentOutput = m_CurrentOutput;
				m_Output5 = true;
				if (m_CurrentOutput < MaxOutputUsed)
				{
					m_CurrentOutput = 6;
				}
				else if (Loop)
				{
					m_CurrentOutput = 1;
				}
				else
				{
					m_SwitchOpen = false;
				}
				break;
			case 6:
				CurrentOutput = m_CurrentOutput;
				m_Output6 = true;
				if (Loop)
				{
					m_CurrentOutput = 1;
				}
				else
				{
					m_SwitchOpen = false;
				}
				break;
			default:
				CurrentOutput = 0;
				break;
			}
		}
		else
		{
			CurrentOutput = m_CurrentOutput;
		}
	}

	public void Reset([FriendlyName("Loop", "If True, the switch will loop back to 1 once the Max Output value has been reached.")] bool Loop, [SocketState(false, false)][DefaultValue(6)][FriendlyName("Max Output Used", "Highest valid output switch to use.")] int MaxOutputUsed, [FriendlyName("Current Output", "The output switch that last executed.")] out int CurrentOutput)
	{
		m_Output1 = false;
		m_Output2 = false;
		m_Output3 = false;
		m_Output4 = false;
		m_Output5 = false;
		m_Output6 = false;
		m_CurrentOutput = 1;
		CurrentOutput = 1;
		m_SwitchOpen = true;
	}
}
