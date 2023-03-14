using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Send_Custom_Event_.28Int.29")]
[FriendlyName("Send Custom Event (Int)", "Sends a custom event with an int variable.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Sends a custom event with an int variable.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Events/Custom Events")]
public class uScriptAct_SendCustomEventInt : uScriptLogic
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
	public void SendCustomEvent([FriendlyName("Event Name", "The string-based event name.")] string EventName, [FriendlyName("Event Value", "The value to pass in the event.")] int EventValue, [FriendlyName("Send To", "Where to send this event. Choices are Parents (which is the default), Children, or All (broadcast).")][SocketState(false, false)] uScriptCustomEvent.SendGroup sendGroup, [SocketState(false, false)][FriendlyName("Event Sender", "The GameObject responsible for sending the event. If not specified, the sender will be the owner of this uScript.")] GameObject EventSender)
	{
		GameObject eventSender = m_Parent;
		if (EventSender != null)
		{
			eventSender = EventSender;
		}
		switch (sendGroup)
		{
		case uScriptCustomEvent.SendGroup.All:
			uScriptCustomEvent.BroadcastCustomEvent(EventName, EventValue, eventSender);
			break;
		case uScriptCustomEvent.SendGroup.Children:
			uScriptCustomEvent.SendCustomEventDown(EventName, EventValue, eventSender);
			break;
		default:
			uScriptCustomEvent.SendCustomEventUp(EventName, EventValue, eventSender);
			break;
		}
	}

	public override void SetParent(GameObject parent)
	{
		m_Parent = parent;
	}
}
