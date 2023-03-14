using UnityEngine;

[AddComponentMenu("uScript/Events/Broadcast Custom Event")]
public class uScript_BroadcastCustomEvent : MonoBehaviour
{
	private void uScript_Broadcast_Custom_Event(string eventName)
	{
		uScriptCustomEvent.BroadcastCustomEvent(eventName, null, base.gameObject);
	}
}
