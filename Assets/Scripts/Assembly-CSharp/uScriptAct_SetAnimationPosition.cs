using System;
using UnityEngine;

[NodePath("Actions/Animation")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Returns the machine's IP address as a string")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Set Normalized Animation Position", "Sets the current position of the specified animation to a percentage of the whole animaiton (normalized position). For example, if you wish to have the animation play from middle of the animation, you would set the normalize position to 0.5 (50%).")]
[NodeAuthor("Detox Studios LLC. Original node by xzlashed on the uScript Community Forum", "http://www.detoxstudios.com")]
public class uScriptAct_SetAnimationPosition : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	public event uScriptEventHandler Out;

	public void In([FriendlyName("Target", "The GameObject containing the animtion clip.")] GameObject target, [FriendlyName("Animation", "The animation clip name you wish to use.")] string animationName, [DefaultValue(0)][FriendlyName("Normalized Position", "The normalized position (percentage) of the animation's start/play position you wish to set (0.0 - 1.0).")] float normalizedPosition)
	{
		if (normalizedPosition >= 0f && normalizedPosition <= 1f)
		{
			target.GetComponent<Animation>()[animationName].normalizedTime = normalizedPosition;
		}
		else if (normalizedPosition < 0f)
		{
			target.GetComponent<Animation>()[animationName].normalizedTime = 0f;
		}
		else if (normalizedPosition > 1f)
		{
			target.GetComponent<Animation>()[animationName].normalizedTime = 1f;
		}
		if (this.Out != null)
		{
			this.Out(this, new EventArgs());
		}
	}
}
