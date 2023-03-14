using System;
using UnityEngine;

[NodeToolTip("Fires an event signal when Instance receives a custom event with a float.")]
[NodePropertiesPath("Properties/CustomEventFloat")]
[NodePath("Events/Custom Events")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Custom_Event_.28Float.29")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Custom Event (Float)", "Fires an event signal when Instance receives a custom event with a float.")]
public class uScript_CustomEventFloat : uScriptEvent
{
	public class CustomEventFloatArgs : EventArgs
	{
		private string m_EventName;

		private float m_EventData;

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
		public float EventData
		{
			get
			{
				return m_EventData;
			}
		}

		public CustomEventFloatArgs(string eventName, float eventData, GameObject sender)
		{
			m_Sender = sender;
			m_EventData = eventData;
			m_EventName = eventName;
		}
	}

	public delegate void uScriptEventHandler(object sender, CustomEventFloatArgs args);

	[FriendlyName("On Custom Event Float")]
	public event uScriptEventHandler OnCustomEventFloat;

	private void CustomEvent(uScriptCustomEvent.CustomEventData cEventData)
	{
		if (this.OnCustomEventFloat != null && cEventData.EventData != null && cEventData.EventData.GetType() == typeof(float))
		{
			this.OnCustomEventFloat(this, new CustomEventFloatArgs(cEventData.EventName, (float)cEventData.EventData, cEventData.Sender));
		}
	}
}
