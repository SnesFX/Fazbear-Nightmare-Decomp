using System;
using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Animation")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Returns the machine's IP address as a string")]
[NodeAuthor("Detox Studios LLC. Original node by xzlashed on the uScript Community Forum", "http://www.detoxstudios.com")]
[FriendlyName("Set Animation Speed", "Sets the animation speed of the specified animation.")]
public class uScriptAct_SetAnimationSpeed : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	public event uScriptEventHandler Out;

	public void In([FriendlyName("Target", "The GameObject containing the animtion clip.")] GameObject target, [FriendlyName("Animation", "The animation clip name you wish to use.")] string animationName, [DefaultValue(1f)][FriendlyName("Speed Factor", "The speed of the animation.")] float speed)
	{
		target.GetComponent<Animation>()[animationName].speed = speed;
		if (this.Out != null)
		{
			this.Out(this, new EventArgs());
		}
	}
}
