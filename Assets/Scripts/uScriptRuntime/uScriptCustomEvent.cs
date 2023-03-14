using UnityEngine;

public class uScriptCustomEvent
{
	public enum SendGroup
	{
		Parents = 0,
		Children = 1,
		All = 2
	}

	public class CustomEventData
	{
		public string EventName = "";

		public object EventData;

		public GameObject Sender;

		public CustomEventData()
		{
		}

		public CustomEventData(string eventName, object eventData, GameObject sender)
		{
			EventName = eventName;
			EventData = eventData;
			Sender = sender;
		}
	}

	public static void BroadcastCustomEvent(string eventName, object eventData, GameObject eventSender)
	{
		GameObject[] array = (GameObject[])Object.FindObjectsOfType(typeof(GameObject));
		CustomEventData parameter = new CustomEventData(eventName, eventData, eventSender);
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if ((bool)gameObject && gameObject.transform.parent == null)
			{
				gameObject.gameObject.BroadcastMessage("CustomEvent", parameter, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public static void SendCustomEventUp(string eventName, object eventData, GameObject eventSender)
	{
		CustomEventData value = new CustomEventData(eventName, eventData, eventSender);
		eventSender.SendMessageUpwards("CustomEvent", value, SendMessageOptions.DontRequireReceiver);
	}

	public static void SendCustomEventDown(string eventName, object eventData, GameObject eventSender)
	{
		CustomEventData parameter = new CustomEventData(eventName, eventData, eventSender);
		eventSender.BroadcastMessage("CustomEvent", parameter, SendMessageOptions.DontRequireReceiver);
	}
}
