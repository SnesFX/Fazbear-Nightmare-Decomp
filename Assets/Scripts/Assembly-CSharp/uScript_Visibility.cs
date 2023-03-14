using System;

[FriendlyName("Visibility Events", "Fires an event signal when various GameObject visibility events (Became Visible, Became Invisible) take place.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Visibility_Events")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Fires an event signal when various GameObject visibility events (Became Visible, Became Invisible) take place.")]
[NodePath("Events/GameObject Events")]
public class uScript_Visibility : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	[FriendlyName("On Became Visible")]
	public event uScriptEventHandler BecameVisible;

	[FriendlyName("On Became Invisible")]
	public event uScriptEventHandler BecameInvisible;

	private void OnBecameVisible()
	{
		if (this.BecameVisible != null)
		{
			this.BecameVisible(this, new EventArgs());
		}
	}

	private void OnBecameInvisible()
	{
		if (this.BecameInvisible != null)
		{
			this.BecameInvisible(this, new EventArgs());
		}
	}
}
