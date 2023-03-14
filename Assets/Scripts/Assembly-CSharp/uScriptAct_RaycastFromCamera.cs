using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Cast a ray from the Camera out to the center of the screen.")]
[NodePath("Actions/Physics")]
[FriendlyName("Raycast From Camera", "Cast a ray from the Camera out to the center of the screen, determines if anything was hit along the way, and fires the associated output link.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Raycast_From_Camera")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_RaycastFromCamera : uScriptLogic
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

	public void In([FriendlyName("Camera", "The Camera GameObject to cast the ray from.")] Camera Camera, [SocketState(false, false)][FriendlyName("X Pixel Offset", "The number of pixels (positive or negative value) to offset the ray from the center of the screen on X (width). Capped to the screen pixel width min/max.")] int Offset_X, [FriendlyName("Y Pixel Offset", "The number of pixels (positive or negative value) to offset the ray from the center of the screen on Y (height). Capped to the screen pixel height min/max.")][SocketState(false, false)] int Offset_Y, [DefaultValue(100f)][FriendlyName("Distance", "How far out to cast the ray.")] float Distance, [FriendlyName("Layer Mask", "A Layer mask that is used to selectively ignore colliders when casting a ray.")][SocketState(false, false)] LayerMask layerMask, [DefaultValue(true)][SocketState(false, false)][FriendlyName("Include Masked Layers", "If true, the ray will test against the masked layers, otherwise it will test against all layers excluding the masked layers.")] bool include, [SocketState(false, false)][FriendlyName("Show Ray", "If true, the ray will be displayed as a line in the Scene view.")] bool showRay, [FriendlyName("Hit GameObject", "The first GameObject that was hit by the raycast (if any).")] out GameObject HitObject, [SocketState(false, false)][FriendlyName("Hit Distance", "The distance along the ray that the hit occured (if any).")] out float HitDistance, [FriendlyName("Hit Location", "The position of the hit (if any).")] out Vector3 HitLocation, [FriendlyName("Hit Normal", "The surface normal of the hit (if any).")][SocketState(false, false)] out Vector3 HitNormal)
	{
		bool obstructed = false;
		float num = 0f;
		Vector3 vector = Vector3.zero;
		Vector3 vector2 = new Vector3(0f, 1f, 0f);
		GameObject gameObject = null;
		float num2 = (float)Offset_X + (float)(Screen.width / 2);
		float num3 = (float)Offset_Y + (float)(Screen.height / 2);
		if (num2 < 0f)
		{
			num2 = 0f;
		}
		if (num2 > (float)Screen.width)
		{
			num2 = Screen.width;
		}
		if (num3 < 0f)
		{
			num3 = 0f;
		}
		if (num3 > (float)Screen.height)
		{
			num3 = Screen.height;
		}
		Ray ray = Camera.ScreenPointToRay(new Vector3(num2, num3, 0f));
		if (Distance <= 0f)
		{
			Distance = float.PositiveInfinity;
		}
		float num4 = Distance;
		if (!include)
		{
			layerMask = ~(int)layerMask;
		}
		if (showRay)
		{
			Debug.DrawLine(ray.origin, ray.origin + ray.direction * num4);
		}
		RaycastHit hitInfo;
		if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, num4, layerMask))
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
