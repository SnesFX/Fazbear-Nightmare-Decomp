using System;
using UnityEngine;

[FriendlyName("Custom Event", "Fires an event signal when Instance receives a standard custom event.")]
[NodeToolTip("Fires an event signal when Instance receives a standard custom event.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Custom_Event")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Events/Custom Events")]
[NodePropertiesPath("Properties/CustomEvent")]
public class uScript_CustomEvent : uScriptEvent
{
	public class CustomEventBoolArgs : EventArgs
	{
		private string m_EventName;

		private GameObject m_Sender;

		[FriendlyName("Sender", "The GameObject that sent this event (if available).")]
		public GameObject Sender
		{
			get
			{
				return m_Sender;
			}
		}

		[FriendlyName("Event Name", "The name of the custom event.")]
		public string EventName
		{
			get
			{
				return m_EventName;
			}
		}

		public CustomEventBoolArgs(string eventName, GameObject sender)
		{
			m_Sender = sender;
			m_EventName = eventName;
		}
	}

	public delegate void uScriptEventHandler(object sender, CustomEventBoolArgs args);

	[FriendlyName("On Custom Event")]
	public event uScriptEventHandler OnCustomEvent;

	private void CustomEvent(uScriptCustomEvent.CustomEventData cEventData)
	{
		if (this.OnCustomEvent != null && cEventData.EventData == null)
		{
			this.OnCustomEvent(this, new CustomEventBoolArgs(cEventData.EventName, cEventData.Sender));
		}
	}
}
