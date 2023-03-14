using System;
using UnityEngine;

[FriendlyName("Input Events", "Input Events fires out any time input is detected from the keyboard, mouse, or joystick.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Input_Events")]
[NodeToolTip("Input Events fires out any time input is detected from the keyboard, mouse, or joystick.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Events/Input Events")]
[NodeAutoAssignMasterInstance(true)]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScript_Input : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private bool m_AnyKeyWasDown;

	[FriendlyName("On Input Event")]
	public event uScriptEventHandler KeyEvent;

	private void Update()
	{
		if (Input.anyKey)
		{
			m_AnyKeyWasDown = true;
			if (this.KeyEvent != null)
			{
				this.KeyEvent(this, new EventArgs());
			}
		}
		else if (m_AnyKeyWasDown)
		{
			m_AnyKeyWasDown = false;
			if (this.KeyEvent != null)
			{
				this.KeyEvent(this, new EventArgs());
			}
		}
	}
}
