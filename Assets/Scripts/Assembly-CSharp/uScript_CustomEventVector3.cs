using System;
using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Custom_Event_.28Vector3.29")]
[NodePath("Events/Custom Events")]
[FriendlyName("Custom Event (Vector3)", "Fires an event signal when Instance receives a custom event with a Vector3.")]
[NodeToolTip("Fires an event signal when Instance receives a custom event with a Vector3.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePropertiesPath("Properties/CustomEventVector3")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScript_CustomEventVector3 : uScriptEvent
{
	public class CustomEventVector3Args : EventArgs
	{
		private string m_EventName;

		private Vector3 m_EventData;

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
		public Vector3 EventData
		{
			get
			{
				return m_EventData;
			}
		}

		public CustomEventVector3Args(string eventName, Vector3 eventData, GameObject sender)
		{
			m_Sender = sender;
			m_EventData = eventData;
			m_EventName = eventName;
		}
	}

	public delegate void uScriptEventHandler(object sender, CustomEventVector3Args args);

	[FriendlyName("On Custom Event Vector3")]
	public event uScriptEventHandler OnCustomEventVector3;

	private void CustomEvent(uScriptCustomEvent.CustomEventData cEventData)
	{
		if (this.OnCustomEventVector3 != null && cEventData.EventData != null && cEventData.EventData.GetType() == typeof(Vector3))
		{
			this.OnCustomEventVector3(this, new CustomEventVector3Args(cEventData.EventName, (Vector3)cEventData.EventData, cEventData.Sender));
		}
	}
}
