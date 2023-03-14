using System;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Application Focus", "Fires an event signal when the application's focus state changes.")]
[NodeAutoAssignMasterInstance(true)]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Application_Focus")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Events/Application Events")]
[NodeToolTip("Fires an event signal when the application's focus state changes.")]
public class uScript_ApplicationFocus : uScriptEvent
{
	public class ApplicationFocusEventArgs : EventArgs
	{
		private bool m_HasFocus;

		[FriendlyName("Has Focus", "When True, the application has focus.")]
		public bool HasFocus
		{
			get
			{
				return m_HasFocus;
			}
		}

		public ApplicationFocusEventArgs(bool hasFocus)
		{
			m_HasFocus = hasFocus;
		}
	}

	public delegate void uScriptEventHandler(object sender, ApplicationFocusEventArgs args);

	[FriendlyName("On Focus")]
	public event uScriptEventHandler FocusEvent;

	private void OnApplicationFocus(bool focus)
	{
		if (this.FocusEvent != null)
		{
			this.FocusEvent(this, new ApplicationFocusEventArgs(focus));
		}
	}
}
