using System;

[NodeAutoAssignMasterInstance(true)]
[NodePath("Events/Input Events")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Fires an event signal when an on-screen keyboard event happens.")]
[FriendlyName("On-Screen Keyboard Events", "Fires an event signal when an on-screen keyboard event happens.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#On-Screen_Keyboard_Events")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScript_OnScreenKeyboard : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private bool showLog = true;

	private bool m_LastKeyboardOut;

	[FriendlyName("On Keyboard Slid Out")]
	public event uScriptEventHandler OnKeyboardSlidOut;

	private void Update()
	{
		if (showLog)
		{
			uScriptDebug.Log("The 'On-Screen Keyboard Events' node will only work with iOS devices! Upgrade to Unity 3.5+ for Android support.", uScriptDebug.Type.Warning);
			showLog = false;
		}
	}
}
