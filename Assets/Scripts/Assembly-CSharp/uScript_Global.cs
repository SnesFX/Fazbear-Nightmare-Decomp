using System;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#uScript_Events")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Fires an event signal when uScript starts.")]
[FriendlyName("uScript Events", "Fires an event signal when uScript starts.")]
[NodeAutoAssignMasterInstance(true)]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Events/Game Events")]
public class uScript_Global : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private bool m_UpdateSent;

	private bool m_LateUpdateSent;

	[FriendlyName("On Graph Start")]
	public event uScriptEventHandler uScriptStart;

	[FriendlyName("On Graph Late Start")]
	public event uScriptEventHandler uScriptLateStart;

	private void Update()
	{
		if (!m_UpdateSent)
		{
			m_UpdateSent = true;
			if (this.uScriptStart != null)
			{
				this.uScriptStart(this, new EventArgs());
			}
		}
	}

	private void LateUpdate()
	{
		if (!m_LateUpdateSent)
		{
			m_LateUpdateSent = true;
			if (this.uScriptLateStart != null)
			{
				this.uScriptLateStart(this, new EventArgs());
			}
		}
	}
}
