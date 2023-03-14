using System;
using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePropertiesPath("Properties/CustomEventVector4")]
[NodeToolTip("Fires an event signal when Instance receives a custom event with a Vector4.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Custom_Event_.28Vector4.29")]
[FriendlyName("Custom Event (Vector4)", "Fires an event signal when Instance receives a custom event with a Vector4.")]
[NodePath("Events/Custom Events")]
public class uScript_CustomEventVector4 : uScriptEvent
{
	public class CustomEventVector4Args : EventArgs
	{
		private string m_EventName;

		private Vector4 m_EventData;

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
		public Vector4 EventData
		{
			get
			{
				return m_EventData;
			}
		}

		public CustomEventVector4Args(string eventName, Vector4 eventData, GameObject sender)
		{
			m_Sender = sender;
			m_EventData = eventData;
			m_EventName = eventName;
		}
	}

	public delegate void uScriptEventHandler(object sender, CustomEventVector4Args args);

	[FriendlyName("On Custom Event Vector4")]
	public event uScriptEventHandler OnCustomEventVector4;

	private void CustomEvent(uScriptCustomEvent.CustomEventData cEventData)
	{
		if (this.OnCustomEventVector4 != null && cEventData.EventData != null && cEventData.EventData.GetType() == typeof(Vector4))
		{
			this.OnCustomEventVector4(this, new CustomEventVector4Args(cEventData.EventName, (Vector4)cEventData.EventData, cEventData.Sender));
		}
	}
}
