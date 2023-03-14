using System;

[NodeToolTip("Fires an event signal when various render events take place.")]
[FriendlyName("Render Events", "Fires an event signal when various render events (Pre Cull, Pre Render, Post Render, Render Object, and Will Render Object) take place.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Render_Events")]
[NodePath("Events/Renderer Events")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScript_Render : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	[FriendlyName("On Pre Cull")]
	public event uScriptEventHandler PreCull;

	[FriendlyName("On Pre Render")]
	public event uScriptEventHandler PreRender;

	[FriendlyName("On Post Render")]
	public event uScriptEventHandler PostRender;

	[FriendlyName("On Render Object")]
	public event uScriptEventHandler RenderObject;

	[FriendlyName("On Will Render Object")]
	public event uScriptEventHandler WillRenderObject;

	private void OnPreCull()
	{
		if (this.PreCull != null)
		{
			this.PreCull(this, new EventArgs());
		}
	}

	private void OnPreRender()
	{
		if (this.PreRender != null)
		{
			this.PreRender(this, new EventArgs());
		}
	}

	private void OnPostRender()
	{
		if (this.PostRender != null)
		{
			this.PostRender(this, new EventArgs());
		}
	}

	private void OnRenderObject()
	{
		if (this.RenderObject != null)
		{
			this.RenderObject(this, new EventArgs());
		}
	}

	private void OnWillRenderObject()
	{
		if (this.WillRenderObject != null)
		{
			this.WillRenderObject(this, new EventArgs());
		}
	}
}
