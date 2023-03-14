using System;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Gate")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Conditions/Gates")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Allows the signal to pass through to the Out link depending on the state of the gate.")]
[FriendlyName("Gate", "Allows the signal to pass through to the Out link depending on the state of the gate.")]
public class uScriptCon_Gate : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private bool m_GateOpen;

	private int m_AutoCloseCount;

	private bool m_UseSignalCount;

	private bool m_UseStartOpen = true;

	public event uScriptEventHandler Out;

	public void In(bool StartOpen, int AutoCloseCount, out bool IsOpen)
	{
		if (StartOpen && m_UseStartOpen)
		{
			m_GateOpen = true;
			IsOpen = m_GateOpen;
		}
		m_UseStartOpen = false;
		if (AutoCloseCount > 0 && !m_UseSignalCount)
		{
			m_UseSignalCount = true;
			m_AutoCloseCount = AutoCloseCount;
		}
		else if (AutoCloseCount <= 0)
		{
			m_UseSignalCount = false;
			m_AutoCloseCount = 0;
		}
		if (m_GateOpen)
		{
			if (m_UseSignalCount)
			{
				if (m_AutoCloseCount > 0)
				{
					m_AutoCloseCount--;
					if (this.Out != null)
					{
						this.Out(this, new EventArgs());
					}
					if (m_AutoCloseCount <= 0)
					{
						m_GateOpen = false;
					}
				}
				else
				{
					m_GateOpen = false;
				}
			}
			else if (this.Out != null)
			{
				this.Out(this, new EventArgs());
			}
		}
		IsOpen = m_GateOpen;
	}

	public void Open(bool StartOpen, int AutoCloseCount, out bool IsOpen)
	{
		m_GateOpen = true;
		IsOpen = m_GateOpen;
		if (AutoCloseCount > 0)
		{
			m_UseSignalCount = true;
			m_AutoCloseCount = AutoCloseCount;
		}
		else if (AutoCloseCount == 0)
		{
			m_UseSignalCount = false;
			m_AutoCloseCount = AutoCloseCount;
		}
	}

	public void Close(bool StartOpen, int AutoCloseCount, out bool IsOpen)
	{
		m_GateOpen = false;
		IsOpen = m_GateOpen;
	}

	public void Toggle([FriendlyName("Start Open", "If True, the gate will be open initially.")][DefaultValue(false)][SocketState(false, false)] bool StartOpen, [DefaultValue(false)][SocketState(false, false)][FriendlyName("Auto Close Count", "Allows you to specify how many signals the gate will pass through before it closes automatically. This value is re-checked and reset when the gate recieves a signal to the Open or Toggle signal sockets.")] int AutoCloseCount, [FriendlyName("Is Open", "Sets a Boolean variable to true when the gate is open and false when it is closed. Note: This will always return False if accessed before the gate has recieved a signal.")][SocketState(false, false)] out bool IsOpen)
	{
		m_GateOpen = !m_GateOpen;
		IsOpen = m_GateOpen;
		if (m_GateOpen)
		{
			if (AutoCloseCount > 0)
			{
				m_UseSignalCount = true;
				m_AutoCloseCount = AutoCloseCount;
			}
			else if (AutoCloseCount == 0)
			{
				m_UseSignalCount = false;
				m_AutoCloseCount = AutoCloseCount;
			}
		}
	}
}
