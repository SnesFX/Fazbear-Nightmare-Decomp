using System;
using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Returns the machine's IP address as a string")]
[NodeAuthor("Detox Studios LLC. Original node by xzlashed on the uScript Community Forum", "http://www.detoxstudios.com")]
[FriendlyName("Set Animation Wrap Mode", "Sets the wrap mode of the specified animation.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Animation")]
public class uScriptAct_SetAnimationWrapMode : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	public event uScriptEventHandler Out;

	public void In([FriendlyName("Target", "The GameObject containing the animtion clip.")] GameObject target, [FriendlyName("Animation", "The animation clip name you wish to use.")] string animationName, [DefaultValue(WrapMode.Once)][FriendlyName("Wrap Mode", "The wrap mode you wish to set.")] WrapMode wrapMode)
	{
		target.GetComponent<Animation>()[animationName].wrapMode = wrapMode;
		if (this.Out != null)
		{
			this.Out(this, new EventArgs());
		}
	}
}
