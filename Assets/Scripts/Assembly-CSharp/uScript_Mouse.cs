using System;

[NodePath("Events/Input Events")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Mouse_Cursor_Events")]
[FriendlyName("Mouse Cursor Events", "Fires an event signal when the mouse enters, is over, exits, is pressed down, released, or dragged over Instance.")]
[NodeToolTip("Fires an event signal when the mouse enters, is over, exits, is pressed down, released, or dragged over Instance.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScript_Mouse : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	[FriendlyName("On Mouse Enter")]
	public event uScriptEventHandler OnEnter;

	[FriendlyName("On Mouse Over")]
	public event uScriptEventHandler OnOver;

	[FriendlyName("On Mouse Exit")]
	public event uScriptEventHandler OnExit;

	[FriendlyName("On Mouse Down")]
	public event uScriptEventHandler OnDown;

	[FriendlyName("On Mouse Up")]
	public event uScriptEventHandler OnUp;

	[FriendlyName("On Mouse Drag")]
	public event uScriptEventHandler OnDrag;

	private void OnMouseEnter()
	{
		if (this.OnEnter != null)
		{
			this.OnEnter(this, new EventArgs());
		}
	}

	private void OnMouseOver()
	{
		if (this.OnOver != null)
		{
			this.OnOver(this, new EventArgs());
		}
	}

	private void OnMouseExit()
	{
		if (this.OnExit != null)
		{
			this.OnExit(this, new EventArgs());
		}
	}

	private void OnMouseDown()
	{
		if (this.OnDown != null)
		{
			this.OnDown(this, new EventArgs());
		}
	}

	private void OnMouseUp()
	{
		if (this.OnUp != null)
		{
			this.OnUp(this, new EventArgs());
		}
	}

	private void OnMouseDrag()
	{
		if (this.OnDrag != null)
		{
			this.OnDrag(this, new EventArgs());
		}
	}
}
