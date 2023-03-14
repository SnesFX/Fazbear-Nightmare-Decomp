using System;
using UnityEngine;

[NodePropertiesPath("Properties/CustomEventObject")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Events/Custom Events")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Custom_Event_.28Object.29")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Fires an event signal when Instance receives a custom event with an Object.")]
[FriendlyName("Custom Event (Object)", "Fires an event signal when Instance receives a custom event with an Object.")]
public class uScript_CustomEventObject : uScriptEvent
{
	public class CustomEventObjectArgs : EventArgs
	{
		private string m_EventName;

		private UnityEngine.Object m_EventData;

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
		public UnityEngine.Object EventData
		{
			get
			{
				return m_EventData;
			}
		}

		public CustomEventObjectArgs(string eventName, UnityEngine.Object eventData, GameObject sender)
		{
			m_Sender = sender;
			m_EventData = eventData;
			m_EventName = eventName;
		}
	}

	public delegate void uScriptEventHandler(object sender, CustomEventObjectArgs args);

	[FriendlyName("On Custom Event Object")]
	public event uScriptEventHandler OnCustomEventObject;

	private void CustomEvent(uScriptCustomEvent.CustomEventData cEventData)
	{
		if (this.OnCustomEventObject != null && cEventData.EventData != null && typeof(UnityEngine.Object).IsAssignableFrom(cEventData.EventData.GetType()))
		{
			this.OnCustomEventObject(this, new CustomEventObjectArgs(cEventData.EventName, (UnityEngine.Object)cEventData.EventData, cEventData.Sender));
		}
	}
}
