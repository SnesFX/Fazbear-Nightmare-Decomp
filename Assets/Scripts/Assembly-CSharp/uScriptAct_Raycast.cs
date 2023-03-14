using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Physics")]
[FriendlyName("Raycast", "Performs a ray trace from the starting point to the end point, determines if anything was hit along the way, and fires the associated output link.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Raycast")]
[NodeToolTip("Performs a ray trace from the starting point to the end point. Returns any hit data.")]
public class uScriptAct_Raycast : uScriptLogic
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

	public void In([FriendlyName("Start", "The start point of the ray cast. Must be a GameObject or Vector3.")] object Start, [FriendlyName("End", "The end point of the ray cast. Must be a GameObject or Vector3.")] object End, [FriendlyName("Layer Mask", "A Layer mask that is used to selectively ignore colliders when casting a ray.")][SocketState(false, false)] LayerMask layerMask, [FriendlyName("Include Masked Layers", "If true, the ray will test against the masked layers, otherwise it will test against all layers excluding the masked layers.")][SocketState(false, false)][DefaultValue(true)] bool include, [FriendlyName("Show Ray", "If true, the ray will be displayed as a line in the Scene view.")][SocketState(false, false)] bool showRay, [FriendlyName("Hit GameObject", "The first GameObject that was hit by the raycast (if any).")] out GameObject HitObject, [FriendlyName("Hit Distance", "The distance along the ray that the hit occured (if any).")] out float HitDistance, [FriendlyName("Hit Location", "The position of the hit (if any).")] out Vector3 HitLocation, [FriendlyName("Hit Normal", "The surface normal of the hit (if any).")] out Vector3 HitNormal)
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
			uScriptDebug.Log("The Raycast node can only take a GameObject or Vector3 for the 'Start' input nub!", uScriptDebug.Type.Error);
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
			uScriptDebug.Log("The Raycast node can only take a GameObject or Vector3 for the 'End' input nub!", uScriptDebug.Type.Error);
			flag = false;
		}
		if (flag)
		{
			Vector3 normalized = (m_EndVector - m_StartVector).normalized;
			float num2 = Vector3.Distance(m_StartVector, m_EndVector);
			if (!include)
			{
				layerMask = ~(int)layerMask;
			}
			if (showRay)
			{
				Debug.DrawLine(m_StartVector, m_StartVector + normalized * num2);
			}
			RaycastHit hitInfo;
			if (Physics.Raycast(m_StartVector, normalized, out hitInfo, num2, layerMask))
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
