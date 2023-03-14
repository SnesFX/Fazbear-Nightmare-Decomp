using UnityEngine;

[NodeToolTip("Sends a basic custom event.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Events/Custom Events")]
[FriendlyName("Send Custom Event", "Sends a basic custom event.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Send_Custom_Event")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_SendCustomEvent : uScriptLogic
{
	private GameObject m_Parent;

	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Send Custom Event")]
	public void SendCustomEvent([FriendlyName("Event Name", "The string-based event name.")] string EventName, [SocketState(false, false)][FriendlyName("Send To", "Where to send this event. Choices are Parents (which is the default), Children, or All (broadcast).")] uScriptCustomEvent.SendGroup sendGroup, [SocketState(false, false)][FriendlyName("Event Sender", "The GameObject responsible for sending the event. If not specified, the sender will be the owner of this uScript.")] GameObject EventSender)
	{
		GameObject eventSender = m_Parent;
		if (EventSender != null)
		{
			eventSender = EventSender;
		}
		switch (sendGroup)
		{
		case uScriptCustomEvent.SendGroup.All:
			uScriptCustomEvent.BroadcastCustomEvent(EventName, null, eventSender);
			break;
		case uScriptCustomEvent.SendGroup.Children:
			uScriptCustomEvent.SendCustomEventDown(EventName, null, eventSender);
			break;
		default:
			uScriptCustomEvent.SendCustomEventUp(EventName, null, eventSender);
			break;
		}
	}

	public override void SetParent(GameObject parent)
	{
		m_Parent = parent;
	}
}
