using System;
using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Conditions/Gates")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Timed_Gate")]
[FriendlyName("Timed Gate", "Blocks signals until Closed Duration is finished, then will allow one signal through and resart Closed Duration. Closed Duration time can be updated at any time and will go into effect on next cycle.")]
[NodeToolTip("Blocks signals until Closed Duration is finished.")]
public class uScriptCon_TimedGate : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private bool m_GateOpen = true;

	private bool m_TooSoon;

	private bool m_OpenStateSet;

	private float m_TimeToTrigger;

	[FriendlyName("Gate Closed")]
	public bool TooSoon
	{
		get
		{
			return m_TooSoon;
		}
	}

	[FriendlyName("Gate Open")]
	public event uScriptEventHandler Out;

	public void In([DefaultValue(1f)][FriendlyName("Closed Duration", "Amount of time (in seconds) to keep the gate closed for.")] float Duration, [DefaultValue(true)][SocketState(false, false)][FriendlyName("Start Open", "Setting this to true will allow the signal to pass through immediately when the node receives it's first signal instead of waiting the specified amount of time before the first signal is allowed through.")] bool StartOpen)
	{
		if (!m_OpenStateSet)
		{
			m_GateOpen = StartOpen;
			m_OpenStateSet = true;
			if (!m_GateOpen)
			{
				m_TimeToTrigger = Duration;
			}
		}
		m_TooSoon = false;
		if (m_GateOpen)
		{
			m_GateOpen = false;
			m_TimeToTrigger = Duration;
			if (this.Out != null)
			{
				this.Out(this, new EventArgs());
			}
		}
		else
		{
			m_TooSoon = true;
		}
	}

	public void Update()
	{
		if (m_TimeToTrigger > 0f)
		{
			m_TimeToTrigger -= Time.deltaTime;
			if (m_TimeToTrigger <= 0f)
			{
				m_GateOpen = true;
			}
		}
	}
}
