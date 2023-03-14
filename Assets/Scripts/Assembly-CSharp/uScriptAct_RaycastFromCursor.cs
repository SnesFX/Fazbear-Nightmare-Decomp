using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Cast a ray from the Mouse Cursor into the scene.")]
[NodePath("Actions/Physics")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Raycast_From_Mouse_Cursor")]
[FriendlyName("Raycast From Mouse Cursor", "Cast a ray from the Mouse Cursor into the scene, determines if anything was hit along the way, and fires the associated output link.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_RaycastFromCursor : uScriptLogic
{
	private bool m_NotObstructed;

	private bool m_Obstructed;

	public bool NotObstructed
	{
		get
		{
			return m_NotObstructed;
		}
	}

	public bool Obstructed
	{
		get
		{
			return m_Obstructed;
		}
	}

	public void In([FriendlyName("Camera", "The Camera GameObject to cast the ray from.")] Camera Camera, [DefaultValue(100f)][FriendlyName("Distance", "How far out to cast the ray.")] float Distance, [FriendlyName("Layer Mask", "A Layer mask that is used to selectively ignore colliders when casting a ray.")][SocketState(false, false)] LayerMask layerMask, [DefaultValue(true)][SocketState(false, false)][FriendlyName("Include Masked Layers", "If true, the ray will test against the masked layers, otherwise it will test against all layers excluding the masked layers.")] bool include, [FriendlyName("Show Ray", "If true, the ray will be displayed as a line in the Scene view.")][SocketState(false, false)] bool showRay, [FriendlyName("Hit GameObject", "The first GameObject that was hit by the raycast (if any).")] out GameObject HitObject, [SocketState(false, false)][FriendlyName("Hit Distance", "The distance along the ray that the hit occured (if any).")] out float HitDistance, [FriendlyName("Hit Location", "The position of the hit (if any).")] out Vector3 HitLocation, [FriendlyName("Hit Normal", "The surface normal of the hit (if any).")][SocketState(false, false)] out Vector3 HitNormal)
	{
		bool obstructed = false;
		float num = 0f;
		Vector3 vector = Vector3.zero;
		Vector3 vector2 = new Vector3(0f, 1f, 0f);
		GameObject gameObject = null;
		Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
		if (Distance <= 0f)
		{
			Distance = float.PositiveInfinity;
		}
		float num2 = Distance;
		if (!include)
		{
			layerMask = ~(int)layerMask;
		}
		if (showRay)
		{
			Debug.DrawLine(ray.origin, ray.origin + ray.direction * num2);
		}
		RaycastHit hitInfo;
		if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, num2, layerMask))
		{
			num = hitInfo.distance;
			vector = hitInfo.point;
			gameObject = hitInfo.collider.gameObject;
			vector2 = hitInfo.normal;
			obstructed = true;
		}
		HitDistance = num;
		HitLocation = vector;
		HitObject = gameObject;
		HitNormal = vector2;
		m_Obstructed = obstructed;
		m_NotObstructed = !m_Obstructed;
	}
}
