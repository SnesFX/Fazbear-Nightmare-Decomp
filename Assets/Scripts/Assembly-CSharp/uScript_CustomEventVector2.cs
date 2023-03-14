using System;
using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Fires an event signal when Instance receives a custom event with a Vector2.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePropertiesPath("Properties/CustomEventVector2")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Custom_Event_.28Vector2.29")]
[NodePath("Events/Custom Events")]
[FriendlyName("Custom Event (Vector2)", "Fires an event signal when Instance receives a custom event with a Vector2.")]
public class uScript_CustomEventVector2 : uScriptEvent
{
	public class CustomEventVector2Args : EventArgs
	{
		private string m_EventName;

		private Vector2 m_EventData;

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
		public Vector2 EventData
		{
			get
			{
				return m_EventData;
			}
		}

		public CustomEventVector2Args(string eventName, Vector2 eventData, GameObject sender)
		{
			m_Sender = sender;
			m_EventData = eventData;
			m_EventName = eventName;
		}
	}

	public delegate void uScriptEventHandler(object sender, CustomEventVector2Args args);

	[FriendlyName("On Custom Event Vector2")]
	public event uScriptEventHandler OnCustomEventVector2;

	private void CustomEvent(uScriptCustomEvent.CustomEventData cEventData)
	{
		if (this.OnCustomEventVector2 != null && cEventData.EventData != null && cEventData.EventData.GetType() == typeof(Vector2))
		{
			this.OnCustomEventVector2(this, new CustomEventVector2Args(cEventData.EventName, (Vector2)cEventData.EventData, cEventData.Sender));
		}
	}
}
