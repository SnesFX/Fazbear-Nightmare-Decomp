using System;
using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#GUI_Events")]
[FriendlyName("GUI Events", "Fires GUI-related events.")]
[NodePath("Events/Game Events")]
[NodeToolTip("Fires GUI-related events.")]
[NodeAutoAssignMasterInstance(true)]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScript_GUI : uScriptEvent
{
	public class GUIEventArgs : EventArgs
	{
		private bool m_GUIChanged;

		private string m_FocusedControl;

		[FriendlyName("GUI Changed", "Returns True when any active GUI control has its content data changed. NOTE: This is not control-specific, it is global.")]
		[SocketState(false, false)]
		public bool GUIChanged
		{
			get
			{
				return m_GUIChanged;
			}
		}

		[FriendlyName("Focused Control", "Returns the GUI control that has focus.")]
		[SocketState(false, false)]
		public string FocusedControl
		{
			get
			{
				return m_FocusedControl;
			}
		}

		public GUIEventArgs(bool guiChanged, string focusedControl)
		{
			m_GUIChanged = guiChanged;
			m_FocusedControl = focusedControl;
		}
	}

	public delegate void uScriptEventHandler(object sender, GUIEventArgs args);

	[FriendlyName("On GUI")]
	public event uScriptEventHandler OnGui;

	private void OnGUI()
	{
		if (this.OnGui != null)
		{
			this.OnGui(this, new GUIEventArgs(GUI.changed, GUI.GetNameOfFocusedControl()));
		}
	}
}
