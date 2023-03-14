using System;
using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Custom Event (Int)", "Fires an event signal when Instance receives a custom event with a int.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Custom_Event_.28Int.29")]
[NodePath("Events/Custom Events")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Fires an event signal when Instance receives a custom event with a int.")]
[NodePropertiesPath("Properties/CustomEventInt")]
public class uScript_CustomEventInt : uScriptEvent
{
	public class CustomEventIntArgs : EventArgs
	{
		private string m_EventName;

		private int m_EventData;

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
		public int EventData
		{
			get
			{
				return m_EventData;
			}
		}

		public CustomEventIntArgs(string eventName, int eventData, GameObject sender)
		{
			m_Sender = sender;
			m_EventData = eventData;
			m_EventName = eventName;
		}
	}

	public delegate void uScriptEventHandler(object sender, CustomEventIntArgs args);

	[FriendlyName("On Custom Event Int")]
	public event uScriptEventHandler OnCustomEventInt;

	private void CustomEvent(uScriptCustomEvent.CustomEventData cEventData)
	{
		if (this.OnCustomEventInt != null && cEventData.EventData != null && cEventData.EventData.GetType() == typeof(int))
		{
			this.OnCustomEventInt(this, new CustomEventIntArgs(cEventData.EventName, (int)cEventData.EventData, cEventData.Sender));
		}
	}
}
