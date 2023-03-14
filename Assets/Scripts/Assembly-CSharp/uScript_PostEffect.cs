using System;
using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Post_Effect_Events")]
[FriendlyName("Post Effect Events", "Fires an event signal when a post-effect is rendered.")]
[NodeToolTip("Fires an event signal when a post-effect is rendered.")]
[NodePath("Events/Renderer Events")]
public class uScript_PostEffect : uScriptEvent
{
	public class PostEffectEventArgs : EventArgs
	{
		private RenderTexture m_SourceTexture;

		private RenderTexture m_DestinationTexture;

		[FriendlyName("Source Texture", "The source texture used in the post-effect.")]
		public RenderTexture SourceTexture
		{
			get
			{
				return m_SourceTexture;
			}
		}

		[FriendlyName("Destination Texture", "The destination texture used in the post-effect.")]
		public RenderTexture DestinationTexture
		{
			get
			{
				return m_DestinationTexture;
			}
		}

		public PostEffectEventArgs(RenderTexture source, RenderTexture dest)
		{
			m_SourceTexture = source;
			m_DestinationTexture = dest;
		}
	}

	public delegate void uScriptEventHandler(object sender, PostEffectEventArgs args);

	[FriendlyName("On Render Image")]
	public event uScriptEventHandler RenderImage;

	private void OnRenderImage(RenderTexture source, RenderTexture dest)
	{
		if (this.RenderImage != null)
		{
			this.RenderImage(this, new PostEffectEventArgs(source, dest));
		}
	}
}
