using System;

[FriendlyName("Manual Switch", "Manually pick an Output to fire the signal to.\n\nThe specified Output To Use value will be clamped within the range of 1 to 6.")]
[NodeToolTip("Manually pick an Output to fire the signal to.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Manual_Switch")]
[NodePath("Conditions/Switches")]
public class uScriptCon_ManualSwitch : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private int m_CurrentOutput = 1;

	private bool m_SwitchOpen = true;

	[FriendlyName("Output 1")]
	public event uScriptEventHandler Output1;

	[FriendlyName("Output 2")]
	public event uScriptEventHandler Output2;

	[FriendlyName("Output 3")]
	public event uScriptEventHandler Output3;

	[FriendlyName("Output 4")]
	public event uScriptEventHandler Output4;

	[FriendlyName("Output 5")]
	public event uScriptEventHandler Output5;

	[FriendlyName("Output 6")]
	public event uScriptEventHandler Output6;

	public void In([FriendlyName("Output To Use", "The output switch to use.")] int CurrentOutput)
	{
		m_CurrentOutput = CurrentOutput;
		if (m_SwitchOpen)
		{
			switch (m_CurrentOutput)
			{
			case 1:
				this.Output1(this, new EventArgs());
				break;
			case 2:
				this.Output2(this, new EventArgs());
				break;
			case 3:
				this.Output3(this, new EventArgs());
				break;
			case 4:
				this.Output4(this, new EventArgs());
				break;
			case 5:
				this.Output5(this, new EventArgs());
				break;
			case 6:
				this.Output6(this, new EventArgs());
				break;
			}
		}
	}
}
