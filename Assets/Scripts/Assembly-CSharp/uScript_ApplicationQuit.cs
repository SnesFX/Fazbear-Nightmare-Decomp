using System;

[FriendlyName("Application Quit", "Fires an event signal when the application is going to quit.")]
[NodeAutoAssignMasterInstance(true)]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Fires an event signal when the application is going to quit.")]
[NodePath("Events/Application Events")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Application_Quit")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScript_ApplicationQuit : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	[FriendlyName("On Quit")]
	public event uScriptEventHandler QuitEvent;

	private void OnApplicationQuit()
	{
		if (this.QuitEvent != null)
		{
			this.QuitEvent(this, new EventArgs());
		}
	}
}
