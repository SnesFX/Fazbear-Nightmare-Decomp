using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Filters the On Input Event output from the Input Events node to a specific input (key, mouse, joystick) pressed down, held, or released.")]
[FriendlyName("Input Events Filter", "Filters the On Input Event output from the Input Events node to a specific input (key, mouse, joystick) pressed down, held, or released.")]
[NodePath("Actions/Events/Filters")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Input_Events_Filter")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_OnInputEventFilter : uScriptLogic
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

	public void In([FriendlyName("Key Code", "The key to listen for events from.")] KeyCode KeyCode)
	{
		m_InputDown = Input.GetKeyDown(KeyCode);
		m_InputHeld = Input.GetKey(KeyCode);
		m_InputUp = Input.GetKeyUp(KeyCode);
	}
}
