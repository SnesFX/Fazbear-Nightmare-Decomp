using System;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Events/Network Events")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Fires an event signal when a network server is initialized.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Network_Server_Initialized")]
[FriendlyName("Network Server Initialized", "Fires an event signal when a network server is initialized.")]
public class uScript_NetworkServerInitialized : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	[FriendlyName("On Server Initialized")]
	public event uScriptEventHandler OnInitialized;

	private void OnServerInitialized()
	{
		if (this.OnInitialized != null)
		{
			this.OnInitialized(this, new EventArgs());
		}
	}
}
