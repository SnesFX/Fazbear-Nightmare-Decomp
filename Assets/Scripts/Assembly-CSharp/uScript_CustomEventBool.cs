using System;
using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Custom_Event_.28Bool.29")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Custom Event (Bool)", "Fires an event signal when Instance receives a custom event with a bool.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Fires an event signal when Instance receives a custom event with a bool.")]
[NodePropertiesPath("Properties/CustomEventBool")]
[NodePath("Events/Custom Events")]
public class uScript_CustomEventBool : uScriptEvent
{
	public class CustomEventBoolArgs : EventArgs
	{
		private string m_EventName;

		private bool m_EventData;

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

		[FriendlyName("Event Data", "The variable that was sent with this event.")]
		public bool EventData
		{
			get
			{
				return m_EventData;
			}
		}

		public CustomEventBoolArgs(string eventName, bool eventData, GameObject sender)
		{
			m_Sender = sender;
			m_EventData = eventData;
			m_EventName = eventName;
		}
	}

	public delegate void uScriptEventHandler(object sender, CustomEventBoolArgs args);

	[FriendlyName("On Custom Event Bool")]
	public event uScriptEventHandler OnCustomEventBool;

	private void CustomEvent(uScriptCustomEvent.CustomEventData cEventData)
	{
		if (this.OnCustomEventBool != null && cEventData.EventData != null && cEventData.EventData.GetType() == typeof(bool))
		{
			this.OnCustomEventBool(this, new CustomEventBoolArgs(cEventData.EventName, (bool)cEventData.EventData, cEventData.Sender));
		}
	}
}
