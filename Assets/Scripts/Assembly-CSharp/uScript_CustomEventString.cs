using System;
using UnityEngine;

[FriendlyName("Custom Event (String)", "Fires an event signal when Instance receives a custom event with a string.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Events/Custom Events")]
[NodeToolTip("Fires an event signal when Instance receives a custom event with a string.")]
[NodePropertiesPath("Properties/CustomEventString")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Custom_Event_.28String.29")]
public class uScript_CustomEventString : uScriptEvent
{
	public class CustomEventStringArgs : EventArgs
	{
		private string m_EventName;

		private string m_EventData;

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
		public string EventData
		{
			get
			{
				return m_EventData;
			}
		}

		public CustomEventStringArgs(string eventName, string eventData, GameObject sender)
		{
			m_Sender = sender;
			m_EventData = eventData;
			m_EventName = eventName;
		}
	}

	public delegate void uScriptEventHandler(object sender, CustomEventStringArgs args);

	[FriendlyName("On Custom Event String")]
	public event uScriptEventHandler OnCustomEventString;

	private void CustomEvent(uScriptCustomEvent.CustomEventData cEventData)
	{
		if (this.OnCustomEventString != null && cEventData.EventData != null && cEventData.EventData.GetType() == typeof(string))
		{
			this.OnCustomEventString(this, new CustomEventStringArgs(cEventData.EventName, (string)cEventData.EventData, cEventData.Sender));
		}
	}
}
