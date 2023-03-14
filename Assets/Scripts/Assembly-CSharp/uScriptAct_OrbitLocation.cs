using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Orbits GameObjects around a world location.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Orbit Around Location", "Orbits GameObjects around a world location.")]
[NodePath("Actions/GameObjects/Movement")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
public class uScriptAct_OrbitLocation : uScriptLogic
{
	private bool m_IsOrbitting;

	private float m_CurrentSpeed;

	private GameObject[] m_Targets;

	private Vector2 m_Location;

	private Vector3 m_Axis;

	[FriendlyName("Orbitting")]
	public bool Orbitting
	{
		get
		{
			return m_IsOrbitting;
		}
	}

	[FriendlyName("Not Orbitting")]
	public bool NotOrbitting
	{
		get
		{
			return !m_IsOrbitting;
		}
	}

	[FriendlyName("Start Orbit")]
	public void StartOrbit(GameObject[] Target, Vector3 Location, Vector3 Axis, float OrbitSpeed, out bool OrbitState)
	{
		if (!m_IsOrbitting)
		{
			m_CurrentSpeed = OrbitSpeed;
			m_Targets = Target;
			m_Location = Location;
			m_Axis = Axis;
			m_IsOrbitting = true;
			OrbitState = true;
		}
		else
		{
			OrbitState = m_IsOrbitting;
		}
	}

	[FriendlyName("Stop Orbit")]
	public void StopOrbit(GameObject[] Target, Vector3 Location, Vector3 Axis, float OrbitSpeed, out bool OrbitState)
	{
		if (m_IsOrbitting)
		{
			OrbitState = false;
			m_IsOrbitting = false;
		}
		else
		{
			OrbitState = m_IsOrbitting;
		}
	}

	[FriendlyName("Update Orbit Data")]
	public void UpdateSpeed([FriendlyName("Target", "The GameObject(s) you wish to orbit.")] GameObject[] Target, [FriendlyName("Location", "The location you wish to have the Target orbit around.")] Vector3 Location, [FriendlyName("Axis", "The axis you wish to orbit on.")] Vector3 Axis, [FriendlyName("Orbit Speed", "How fast the Target rotates around the Location.")] float OrbitSpeed, [FriendlyName("Orbit State", "Reflects the current orbit state as a bool variable (True=On/False=Off).")] out bool OrbitState)
	{
		m_CurrentSpeed = OrbitSpeed;
		m_Targets = Target;
		m_Location = Location;
		m_Axis = Axis;
		m_CurrentSpeed = OrbitSpeed;
		OrbitState = m_IsOrbitting;
	}

	public void Update()
	{
		if (!m_IsOrbitting || m_CurrentSpeed == 0f)
		{
			return;
		}
		GameObject[] targets = m_Targets;
		foreach (GameObject gameObject in targets)
		{
			if (gameObject != null)
			{
				gameObject.transform.RotateAround(m_Location, m_Axis, m_CurrentSpeed * Time.deltaTime);
			}
		}
	}
}
