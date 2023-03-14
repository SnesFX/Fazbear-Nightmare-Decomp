using System;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GameObject_Events")]
[NodePath("Events/GameObject Events")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Fires an event signal when Instance is enabled, disabled or destroyed.")]
[FriendlyName("GameObject Events", "Fires an event signal when Instance is enabled, disabled or destroyed.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScript_GameObject : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	[FriendlyName("On Enable")]
	public event uScriptEventHandler EnableEvent;

	[FriendlyName("On Disable")]
	public event uScriptEventHandler DisableEvent;

	[FriendlyName("On Destroy")]
	public event uScriptEventHandler DestroyEvent;

	private void OnEnable()
	{
		if (this.EnableEvent != null)
		{
			this.EnableEvent(this, new EventArgs());
		}
	}

	private void OnDisable()
	{
		if (this.DisableEvent != null)
		{
			this.DisableEvent(this, new EventArgs());
		}
	}

	private void OnDestroy()
	{
		if (this.DestroyEvent != null)
		{
			this.DestroyEvent(this, new EventArgs());
		}
	}
}
