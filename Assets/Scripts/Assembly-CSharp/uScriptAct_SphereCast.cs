using UnityEngine;

[NodePath("Actions/Physics")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Raycast")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Sphere Cast", "Casts a sphere against all colliders in the scene and returns information on what was hit. Please note that the sphere cast does not work against colliders configured as triggers.")]
[NodeToolTip("Casts a sphere against all colliders in the scene and returns information on what was hit.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_SphereCast : uScriptLogic
{
	private Vector3 m_StartVector = Vector3.zero;

	private Vector3 m_EndVector = Vector3.zero;

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

	public void In([FriendlyName("Start", "The center of the sphere at the start of the sweep. Must be a GameObject or Vector3.")] object Start, [FriendlyName("End", "The center of the sphere at the end of the sweep. Must be a GameObject or Vector3.")] object End, [FriendlyName("Radius", "The radius of the sphere.")] float Radius, [FriendlyName("Use Layer Mask", "If true, the ray will test against the selected layer mask, otherwise it will test against all GameObjects in the scene.")][DefaultValue(true)][SocketState(false, false)] bool useLayers, [SocketState(false, false)][FriendlyName("Layer Mask", "A Layer mask that is used to selectively ignore colliders when casting a ray.")] LayerMask layerMask, [FriendlyName("Show Ray", "If true, the ray will be displayed as a line in the Scene view.")][SocketState(false, false)] bool showRay, [FriendlyName("Hit GameObject", "The first GameObject that was hit by the raycast (if any).")] out GameObject HitObject, [FriendlyName("Hit Distance", "The distance along the ray that the hit occured (if any).")] out float HitDistance, [FriendlyName("Hit Location", "The position of the hit (if any).")] out Vector3 HitLocation, [FriendlyName("Hit Normal", "The surface normal of the hit (if any).")] out Vector3 HitNormal)
	{
		bool obstructed = false;
		bool flag = true;
		float num = 0f;
		Vector3 vector = Vector3.zero;
		Vector3 vector2 = new Vector3(0f, 1f, 0f);
		GameObject gameObject = null;
		if (typeof(GameObject) == Start.GetType() || typeof(Vector3) == Start.GetType())
		{
			if (typeof(GameObject) == Start.GetType())
			{
				GameObject gameObject2 = (GameObject)Start;
				m_StartVector = gameObject2.transform.position;
			}
			if (typeof(Vector3) == Start.GetType())
			{
				Vector3 startVector = (Vector3)Start;
				m_StartVector = startVector;
			}
		}
		else
		{
			uScriptDebug.Log("[Sphere Cast] The Sphere Cast node can only take a GameObject or Vector3 for the 'Start' input socket.", uScriptDebug.Type.Error);
			flag = false;
		}
		if (typeof(GameObject) == End.GetType() || typeof(Vector3) == End.GetType())
		{
			if (typeof(GameObject) == End.GetType())
			{
				GameObject gameObject3 = (GameObject)End;
				m_EndVector = gameObject3.transform.position;
			}
			if (typeof(Vector3) == End.GetType())
			{
				Vector3 endVector = (Vector3)End;
				m_EndVector = endVector;
			}
		}
		else
		{
			uScriptDebug.Log("[Sphere Cast] The Sphere Cast node can only take a GameObject or Vector3 for the 'End' input socket.", uScriptDebug.Type.Error);
			flag = false;
		}
		if (flag)
		{
			Vector3 normalized = (m_EndVector - m_StartVector).normalized;
			float num2 = Vector3.Distance(m_StartVector, m_EndVector);
			if (showRay)
			{
				Debug.DrawLine(m_StartVector, m_StartVector + normalized * num2);
			}
			RaycastHit hitInfo;
			if (useLayers)
			{
				if (Physics.SphereCast(m_StartVector, Radius, normalized, out hitInfo, num2, layerMask.value))
				{
					num = hitInfo.distance;
					vector = hitInfo.point;
					gameObject = hitInfo.collider.gameObject;
					vector2 = hitInfo.normal;
					obstructed = true;
				}
			}
			else if (Physics.SphereCast(m_StartVector, Radius, normalized, out hitInfo, num2))
			{
				num = hitInfo.distance;
				vector = hitInfo.point;
				gameObject = hitInfo.collider.gameObject;
				vector2 = hitInfo.normal;
				obstructed = true;
			}
		}
		HitDistance = num;
		HitLocation = vector;
		HitObject = gameObject;
		HitNormal = vector2;
		m_Obstructed = obstructed;
		m_NotObstructed = !m_Obstructed;
	}
}
