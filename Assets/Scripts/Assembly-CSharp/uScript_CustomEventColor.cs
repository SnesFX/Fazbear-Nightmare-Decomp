using System;
using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Custom_Event_.28Color.29")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Fires an event signal when Instance receives a custom event with a Color.")]
[NodePropertiesPath("Properties/CustomEventColor")]
[NodePath("Events/Custom Events")]
[FriendlyName("Custom Event (Color)", "Fires an event signal when Instance receives a custom event with a Color.")]
public class uScript_CustomEventColor : uScriptEvent
{
	public class CustomEventColorArgs : EventArgs
	{
		private string m_EventName;

		private Color m_EventData;

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
		public Color EventData
		{
			get
			{
				return m_EventData;
			}
		}

		public CustomEventColorArgs(string eventName, Color eventData, GameObject sender)
		{
			m_Sender = sender;
			m_EventData = eventData;
			m_EventName = eventName;
		}
	}

	public delegate void uScriptEventHandler(object sender, CustomEventColorArgs args);

	[FriendlyName("On Custom Event Color")]
	public event uScriptEventHandler OnCustomEventColor;

	private void CustomEvent(uScriptCustomEvent.CustomEventData cEventData)
	{
		if (this.OnCustomEventColor != null && cEventData.EventData != null && cEventData.EventData.GetType() == typeof(Color))
		{
			this.OnCustomEventColor(this, new CustomEventColorArgs(cEventData.EventName, (Color)cEventData.EventData, cEventData.Sender));
		}
	}
}
