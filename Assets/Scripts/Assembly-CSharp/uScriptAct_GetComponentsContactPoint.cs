using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_ContactPoint_Components")]
[FriendlyName("Get Components (ContactPoint)", "Gets the components of a ContactPoint as floats.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Gets the components of a ContactPoint.")]
[NodePath("Actions/Variables/ContactPoint")]
public class uScriptAct_GetComponentsContactPoint : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Input ContactPoint", "The input vector to get components of.")] ContactPoint ContactPoint, [FriendlyName("Point", "The point of contact.")] out Vector3 point, [FriendlyName("Normal", "Normal of the contact point.")] out Vector3 normal, [SocketState(false, false)][FriendlyName("This Collider", "The first collider in contact.")] out Collider ThisCollider, [FriendlyName("Other Collider", "The other collider in contact.")][SocketState(false, false)] out Collider OtherCollider)
	{
		normal = ContactPoint.normal;
		point = ContactPoint.point;
		ThisCollider = ContactPoint.thisCollider;
		OtherCollider = ContactPoint.otherCollider;
	}
}
