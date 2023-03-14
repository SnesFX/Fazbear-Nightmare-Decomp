using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class push : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private string local_12_System_String = "push";

	private Vector3 local_2_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private GameObject local_4_UnityEngine_GameObject;

	private GameObject local_4_UnityEngine_GameObject_previous;

	private GameObject owner_Connection_5;

	private uScriptAct_AddForce logic_uScriptAct_AddForce_uScriptAct_AddForce_1 = new uScriptAct_AddForce();

	private GameObject logic_uScriptAct_AddForce_Target_1;

	private Vector3 logic_uScriptAct_AddForce_Force_1 = default(Vector3);

	private float logic_uScriptAct_AddForce_Scale_1 = 10f;

	private bool logic_uScriptAct_AddForce_UseForceMode_1;

	private ForceMode logic_uScriptAct_AddForce_ForceModeType_1;

	private bool logic_uScriptAct_AddForce_Out_1 = true;

	private uScriptCon_GameObjectHasTag logic_uScriptCon_GameObjectHasTag_uScriptCon_GameObjectHasTag_11 = new uScriptCon_GameObjectHasTag();

	private GameObject logic_uScriptCon_GameObjectHasTag_GameObject_11;

	private string[] logic_uScriptCon_GameObjectHasTag_Tag_11 = new string[0];

	private bool logic_uScriptCon_GameObjectHasTag_HasAllTags_11 = true;

	private bool logic_uScriptCon_GameObjectHasTag_HasTag_11 = true;

	private bool logic_uScriptCon_GameObjectHasTag_MissingTags_11 = true;

	private GameObject event_UnityEngine_GameObject_GameObject_0;

	private CharacterController event_UnityEngine_GameObject_Controller_0;

	private Collider event_UnityEngine_GameObject_Collider_0;

	private Rigidbody event_UnityEngine_GameObject_RigidBody_0;

	private Transform event_UnityEngine_GameObject_Transform_0;

	private Vector3 event_UnityEngine_GameObject_Point_0 = new Vector3(0f, 0f, 0f);

	private Vector3 event_UnityEngine_GameObject_Normal_0 = new Vector3(0f, 0f, 0f);

	private Vector3 event_UnityEngine_GameObject_MoveDirection_0 = default(Vector3);

	private float event_UnityEngine_GameObject_MoveLength_0;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (local_4_UnityEngine_GameObject_previous != local_4_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_4_UnityEngine_GameObject_previous = local_4_UnityEngine_GameObject;
		}
		if (!(null == owner_Connection_5) && m_RegisteredForEvents)
		{
			return;
		}
		owner_Connection_5 = parentGameObject;
		if (null != owner_Connection_5)
		{
			uScript_ProxyController uScript_ProxyController2 = owner_Connection_5.GetComponent<uScript_ProxyController>();
			if (null == uScript_ProxyController2)
			{
				uScript_ProxyController2 = owner_Connection_5.AddComponent<uScript_ProxyController>();
			}
			if (null != uScript_ProxyController2)
			{
				uScript_ProxyController2.OnHit += Instance_OnHit_0;
			}
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (local_4_UnityEngine_GameObject_previous != local_4_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_4_UnityEngine_GameObject_previous = local_4_UnityEngine_GameObject;
		}
		if (!m_RegisteredForEvents && null != owner_Connection_5)
		{
			uScript_ProxyController uScript_ProxyController2 = owner_Connection_5.GetComponent<uScript_ProxyController>();
			if (null == uScript_ProxyController2)
			{
				uScript_ProxyController2 = owner_Connection_5.AddComponent<uScript_ProxyController>();
			}
			if (null != uScript_ProxyController2)
			{
				uScript_ProxyController2.OnHit += Instance_OnHit_0;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_5)
		{
			uScript_ProxyController component = owner_Connection_5.GetComponent<uScript_ProxyController>();
			if (null != component)
			{
				component.OnHit -= Instance_OnHit_0;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptAct_AddForce_uScriptAct_AddForce_1.SetParent(g);
		logic_uScriptCon_GameObjectHasTag_uScriptCon_GameObjectHasTag_11.SetParent(g);
	}

	public void Awake()
	{
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
	}

	public void OnDisable()
	{
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
	}

	public void OnDestroy()
	{
	}

	private void Instance_OnHit_0(object o, uScript_ProxyController.ProxyControllerCollisionEventArgs e)
	{
		event_UnityEngine_GameObject_GameObject_0 = e.GameObject;
		event_UnityEngine_GameObject_Controller_0 = e.Controller;
		event_UnityEngine_GameObject_Collider_0 = e.Collider;
		event_UnityEngine_GameObject_RigidBody_0 = e.RigidBody;
		event_UnityEngine_GameObject_Transform_0 = e.Transform;
		event_UnityEngine_GameObject_Point_0 = e.Point;
		event_UnityEngine_GameObject_Normal_0 = e.Normal;
		event_UnityEngine_GameObject_MoveDirection_0 = e.MoveDirection;
		event_UnityEngine_GameObject_MoveLength_0 = e.MoveLength;
		Relay_OnHit_0();
	}

	private void Relay_OnHit_0()
	{
		local_4_UnityEngine_GameObject = event_UnityEngine_GameObject_GameObject_0;
		if (local_4_UnityEngine_GameObject_previous != local_4_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_4_UnityEngine_GameObject_previous = local_4_UnityEngine_GameObject;
		}
		local_2_UnityEngine_Vector3 = event_UnityEngine_GameObject_MoveDirection_0;
		Relay_In_11();
	}

	private void Relay_In_1()
	{
		if (local_4_UnityEngine_GameObject_previous != local_4_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_4_UnityEngine_GameObject_previous = local_4_UnityEngine_GameObject;
		}
		logic_uScriptAct_AddForce_Target_1 = local_4_UnityEngine_GameObject;
		logic_uScriptAct_AddForce_Force_1 = local_2_UnityEngine_Vector3;
		logic_uScriptAct_AddForce_uScriptAct_AddForce_1.In(logic_uScriptAct_AddForce_Target_1, logic_uScriptAct_AddForce_Force_1, logic_uScriptAct_AddForce_Scale_1, logic_uScriptAct_AddForce_UseForceMode_1, logic_uScriptAct_AddForce_ForceModeType_1);
	}

	private void Relay_In_11()
	{
		if (local_4_UnityEngine_GameObject_previous != local_4_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_4_UnityEngine_GameObject_previous = local_4_UnityEngine_GameObject;
		}
		logic_uScriptCon_GameObjectHasTag_GameObject_11 = local_4_UnityEngine_GameObject;
		List<string> list = new List<string>();
		list.Add(local_12_System_String);
		logic_uScriptCon_GameObjectHasTag_Tag_11 = list.ToArray();
		logic_uScriptCon_GameObjectHasTag_uScriptCon_GameObjectHasTag_11.In(logic_uScriptCon_GameObjectHasTag_GameObject_11, logic_uScriptCon_GameObjectHasTag_Tag_11);
		if (logic_uScriptCon_GameObjectHasTag_uScriptCon_GameObjectHasTag_11.HasTag)
		{
			Relay_In_1();
		}
	}
}
