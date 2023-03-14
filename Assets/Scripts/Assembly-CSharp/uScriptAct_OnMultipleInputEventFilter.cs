using UnityEngine;

[FriendlyName("Multiple Input Events Filter", "Filters the On Input Event output from the Input Events node to a specific input (key, mouse, joystick) pressed down, held, or released.")]
[NodePath("Actions/Events/Filters")]
[NodeToolTip("Filters the On Input Event output from the Input Events node to a specific input (key, mouse, joystick) pressed down, held, or released.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Input_Events_Filter")]
public class uScriptAct_OnMultipleInputEventFilter : uScriptLogic
{
	public bool m_InputHeld;

	public bool m_InputDown;

	public bool m_InputUp;

	[FriendlyName("Input Held")]
	public bool KeyHeld
	{
		get
		{
			return m_InputHeld;
		}
	}

	[FriendlyName("Input Down")]
	public bool KeyDown
	{
		get
		{
			return m_InputDown;
		}
	}

	[FriendlyName("Input Up")]
	public bool KeyUp
	{
		get
		{
			return m_InputUp;
		}
	}

	public void In([FriendlyName("Key Code", "The key to listen for events from.")] KeyCode[] KeyCode)
	{
		m_InputDown = false;
		m_InputHeld = false;
		m_InputUp = false;
		foreach (KeyCode key in KeyCode)
		{
			m_InputDown |= Input.GetKeyDown(key);
			m_InputHeld |= Input.GetKey(key);
			m_InputUp |= Input.GetKeyUp(key);
		}
	}
}
