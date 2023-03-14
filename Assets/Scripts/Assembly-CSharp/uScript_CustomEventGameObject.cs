using System;
using UnityEngine;

[NodeToolTip("Fires an event signal when Instance receives a custom event with a GameObject.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Custom Event (GameObject)", "Fires an event signal when Instance receives a custom event with a GameObject.")]
[NodePath("Events/Custom Events")]
[NodePropertiesPath("Properties/CustomEventGameObject")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Custom_Event_.28GameObject.29")]
public class uScript_CustomEventGameObject : uScriptEvent
{
	public class CustomEventGameObjectArgs : EventArgs
	{
		private string m_EventName;

		private GameObject m_EventData;

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
		public GameObject EventData
		{
			get
			{
				return m_EventData;
			}
		}

		public CustomEventGameObjectArgs(string eventName, GameObject eventData, GameObject sender)
		{
			m_Sender = sender;
			m_EventData = eventData;
			m_EventName = eventName;
		}
	}

	public delegate void uScriptEventHandler(object sender, CustomEventGameObjectArgs args);

	[FriendlyName("On Custom Event GameObject")]
	public event uScriptEventHandler OnCustomEventGameObject;

	private void CustomEvent(uScriptCustomEvent.CustomEventData cEventData)
	{
		if (this.OnCustomEventGameObject != null && cEventData.EventData != null && cEventData.EventData.GetType() == typeof(GameObject))
		{
			this.OnCustomEventGameObject(this, new CustomEventGameObjectArgs(cEventData.EventName, (GameObject)cEventData.EventData, cEventData.Sender));
		}
	}
}
