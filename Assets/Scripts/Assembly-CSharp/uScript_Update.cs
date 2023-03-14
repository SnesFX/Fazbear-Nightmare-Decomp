using System;

[NodeAutoAssignMasterInstance(true)]
[NodePath("Events/Game Events")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Global_Update")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Fires an event signal when various global events take place.")]
[FriendlyName("Global Update", "Fires an event signal when various global events (Update, LateUpdate, and FixedUpdate) take place.")]
public class uScript_Update : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	[FriendlyName("On Update")]
	public event uScriptEventHandler OnUpdate;

	[FriendlyName("On LateUpdate")]
	public event uScriptEventHandler OnLateUpdate;

	[FriendlyName("On FixedUpdate")]
	public event uScriptEventHandler OnFixedUpdate;

	private void Update()
	{
		if (this.OnUpdate != null)
		{
			this.OnUpdate(this, new EventArgs());
		}
	}

	private void LateUpdate()
	{
		if (this.OnLateUpdate != null)
		{
			this.OnLateUpdate(this, new EventArgs());
		}
	}

	private void FixedUpdate()
	{
		if (this.OnFixedUpdate != null)
		{
			this.OnFixedUpdate(this, new EventArgs());
		}
	}
}
