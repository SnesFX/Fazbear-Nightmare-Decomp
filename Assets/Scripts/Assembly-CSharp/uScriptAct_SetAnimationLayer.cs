using System;
using UnityEngine;

[NodePath("Actions/Animation")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Returns the machine's IP address as a string")]
[NodeAuthor("Detox Studios LLC. Original node by xzlashed on the uScript Community Forum", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Set Animation Layer", "Sets the animation layer of the specified animation.")]
public class uScriptAct_SetAnimationLayer : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	public event uScriptEventHandler Out;

	public void In([FriendlyName("Target", "The GameObject containing the animtion clip.")] GameObject target, [FriendlyName("Animation", "The animation clip name you wish to use.")] string animationName, [DefaultValue(0)][FriendlyName("Layer", "The animation layer value you wish to set.")] int layer)
	{
		target.GetComponent<Animation>()[animationName].layer = layer;
		if (this.Out != null)
		{
			this.Out(this, new EventArgs());
		}
	}
}
