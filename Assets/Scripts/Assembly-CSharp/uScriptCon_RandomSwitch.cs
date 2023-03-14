using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Random Switch", "Randomly picks an Output to fire the signal to.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Random_Switch")]
[NodePath("Conditions/Switches")]
[NodeToolTip("Randomly picks an Output to fire the signal to.")]
public class uScriptCon_RandomSwitch : uScriptLogic
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

	public void In([DefaultValue(6)][SocketState(false, false)][FriendlyName("Max Output Used", "Highest valid output switch to use.")] int MaxOutputUsed, [FriendlyName("Current Output", "The output switch that was randomly chosen.")] out int CurrentOutput)
	{
		m_Output1 = false;
		m_Output2 = false;
		m_Output3 = false;
		m_Output4 = false;
		m_Output5 = false;
		m_Output6 = false;
		MaxOutputUsed = Mathf.Clamp(MaxOutputUsed, 1, 6);
		m_CurrentOutput = Random.Range(1, MaxOutputUsed + 1);
		if (m_SwitchOpen)
		{
			switch (m_CurrentOutput)
			{
			case 1:
				CurrentOutput = m_CurrentOutput;
				m_Output1 = true;
				break;
			case 2:
				CurrentOutput = m_CurrentOutput;
				m_Output2 = true;
				break;
			case 3:
				CurrentOutput = m_CurrentOutput;
				m_Output3 = true;
				break;
			case 4:
				CurrentOutput = m_CurrentOutput;
				m_Output4 = true;
				break;
			case 5:
				CurrentOutput = m_CurrentOutput;
				m_Output5 = true;
				break;
			case 6:
				CurrentOutput = m_CurrentOutput;
				m_Output6 = true;
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
}
