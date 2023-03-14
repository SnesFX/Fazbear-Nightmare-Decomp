using System;
using UnityEngine;

[NodeToolTip("Returns the machine's IP address as a string")]
[NodeAuthor("Detox Studios LLC. Original node by xzlashed on the uScript Community Forum", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Animation")]
[FriendlyName("Set Animation Blend Weight", "Sets the blend weight of the specified animation.")]
public class uScriptAct_SetAnimationWeight : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	public event uScriptEventHandler Out;

	public void In([FriendlyName("Target", "The GameObject containing the animtion clip.")] GameObject target, [FriendlyName("Animation", "The animation clip name you wish to use.")] string animationName, [DefaultValue(1)][FriendlyName("Blend Weight", "The blend weight you wish to set (0.0 - 1.0).")] float weight)
	{
		if (weight >= 0f && weight <= 1f)
		{
			target.GetComponent<Animation>()[animationName].weight = weight;
		}
		else if (weight < 0f)
		{
			target.GetComponent<Animation>()[animationName].weight = 0f;
		}
		else if (weight > 1f)
		{
			target.GetComponent<Animation>()[animationName].weight = 1f;
		}
		if (this.Out != null)
		{
			this.Out(this, new EventArgs());
		}
	}
}
