using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class lift_doors : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private string local_11_System_String = "lift_close";

	private string local_15_System_String = "lift_open";

	private float local_16_System_Single;

	private float local_17_System_Single;

	private float local_19_System_Single;

	private string local_2_System_String = "lift_open";

	private float local_21_System_Single;

	private string local_24_System_String = string.Empty;

	private string local_3_System_String = "lift_close";

	private string local_30_System_String = string.Empty;

	private GameObject local_34_UnityEngine_GameObject;

	private GameObject local_34_UnityEngine_GameObject_previous;

	private GameObject local_35_UnityEngine_GameObject;

	private GameObject local_35_UnityEngine_GameObject_previous;

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_27;

	private uScriptAct_GetAnimationState logic_uScriptAct_GetAnimationState_uScriptAct_GetAnimationState_4 = new uScriptAct_GetAnimationState();

	private GameObject logic_uScriptAct_GetAnimationState_target_4;

	private string logic_uScriptAct_GetAnimationState_animationName_4 = string.Empty;

	private float logic_uScriptAct_GetAnimationState_weight_4;

	private float logic_uScriptAct_GetAnimationState_normalizedPosition_4;

	private float logic_uScriptAct_GetAnimationState_animLength_4;

	private float logic_uScriptAct_GetAnimationState_speed_4;

	private int logic_uScriptAct_GetAnimationState_layer_4;

	private WrapMode logic_uScriptAct_GetAnimationState_wrapMode_4;

	private bool logic_uScriptAct_GetAnimationState_Out_4 = true;

	private uScriptAct_PlayAnimation logic_uScriptAct_PlayAnimation_uScriptAct_PlayAnimation_5 = new uScriptAct_PlayAnimation();

	private GameObject[] logic_uScriptAct_PlayAnimation_Target_5 = new GameObject[0];

	private string logic_uScriptAct_PlayAnimation_Animation_5 = string.Empty;

	private float logic_uScriptAct_PlayAnimation_SpeedFactor_5 = 2f;

	private WrapMode logic_uScriptAct_PlayAnimation_AnimWrapMode_5;

	private bool logic_uScriptAct_PlayAnimation_StopOtherAnimations_5 = true;

	private bool logic_uScriptAct_PlayAnimation_Out_5 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_6 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_6 = true;

	private uScriptAct_SetAnimationPosition logic_uScriptAct_SetAnimationPosition_uScriptAct_SetAnimationPosition_7 = new uScriptAct_SetAnimationPosition();

	private GameObject logic_uScriptAct_SetAnimationPosition_target_7;

	private string logic_uScriptAct_SetAnimationPosition_animationName_7 = string.Empty;

	private float logic_uScriptAct_SetAnimationPosition_normalizedPosition_7;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_8 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_8 = true;

	private uScriptAct_SetAnimationPosition logic_uScriptAct_SetAnimationPosition_uScriptAct_SetAnimationPosition_9 = new uScriptAct_SetAnimationPosition();

	private GameObject logic_uScriptAct_SetAnimationPosition_target_9;

	private string logic_uScriptAct_SetAnimationPosition_animationName_9 = string.Empty;

	private float logic_uScriptAct_SetAnimationPosition_normalizedPosition_9;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_10 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_10;

	private float logic_uScriptCon_CompareFloat_B_10 = 1f;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_10 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_10 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_10 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_10 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_10 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_10 = true;

	private uScriptAct_GetAnimationState logic_uScriptAct_GetAnimationState_uScriptAct_GetAnimationState_12 = new uScriptAct_GetAnimationState();

	private GameObject logic_uScriptAct_GetAnimationState_target_12;

	private string logic_uScriptAct_GetAnimationState_animationName_12 = string.Empty;

	private float logic_uScriptAct_GetAnimationState_weight_12;

	private float logic_uScriptAct_GetAnimationState_normalizedPosition_12;

	private float logic_uScriptAct_GetAnimationState_animLength_12;

	private float logic_uScriptAct_GetAnimationState_speed_12;

	private int logic_uScriptAct_GetAnimationState_layer_12;

	private WrapMode logic_uScriptAct_GetAnimationState_wrapMode_12;

	private bool logic_uScriptAct_GetAnimationState_Out_12 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_13 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_13;

	private float logic_uScriptCon_CompareFloat_B_13 = 1f;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_13 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_13 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_13 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_13 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_13 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_13 = true;

	private uScriptAct_PlayAnimation logic_uScriptAct_PlayAnimation_uScriptAct_PlayAnimation_14 = new uScriptAct_PlayAnimation();

	private GameObject[] logic_uScriptAct_PlayAnimation_Target_14 = new GameObject[0];

	private string logic_uScriptAct_PlayAnimation_Animation_14 = string.Empty;

	private float logic_uScriptAct_PlayAnimation_SpeedFactor_14 = 2f;

	private WrapMode logic_uScriptAct_PlayAnimation_AnimWrapMode_14;

	private bool logic_uScriptAct_PlayAnimation_StopOtherAnimations_14 = true;

	private bool logic_uScriptAct_PlayAnimation_Out_14 = true;

	private uScriptAct_SubtractFloat logic_uScriptAct_SubtractFloat_uScriptAct_SubtractFloat_18 = new uScriptAct_SubtractFloat();

	private float logic_uScriptAct_SubtractFloat_A_18 = 1f;

	private float logic_uScriptAct_SubtractFloat_B_18;

	private float logic_uScriptAct_SubtractFloat_FloatResult_18;

	private int logic_uScriptAct_SubtractFloat_IntResult_18;

	private bool logic_uScriptAct_SubtractFloat_Out_18 = true;

	private uScriptAct_SubtractFloat logic_uScriptAct_SubtractFloat_uScriptAct_SubtractFloat_20 = new uScriptAct_SubtractFloat();

	private float logic_uScriptAct_SubtractFloat_A_20 = 1f;

	private float logic_uScriptAct_SubtractFloat_B_20;

	private float logic_uScriptAct_SubtractFloat_FloatResult_20;

	private int logic_uScriptAct_SubtractFloat_IntResult_20;

	private bool logic_uScriptAct_SubtractFloat_Out_20 = true;

	private uScriptCon_CompareString logic_uScriptCon_CompareString_uScriptCon_CompareString_23 = new uScriptCon_CompareString();

	private string logic_uScriptCon_CompareString_A_23 = string.Empty;

	private string logic_uScriptCon_CompareString_B_23 = "stop";

	private bool logic_uScriptCon_CompareString_Same_23 = true;

	private bool logic_uScriptCon_CompareString_Different_23 = true;

	private uScriptCon_Gate logic_uScriptCon_Gate_uScriptCon_Gate_25 = new uScriptCon_Gate();

	private bool logic_uScriptCon_Gate_StartOpen_25 = true;

	private int logic_uScriptCon_Gate_AutoCloseCount_25;

	private bool logic_uScriptCon_Gate_IsOpen_25;

	private uScriptCon_Gate logic_uScriptCon_Gate_uScriptCon_Gate_26 = new uScriptCon_Gate();

	private bool logic_uScriptCon_Gate_StartOpen_26 = true;

	private int logic_uScriptCon_Gate_AutoCloseCount_26;

	private bool logic_uScriptCon_Gate_IsOpen_26;

	private uScriptCon_CompareString logic_uScriptCon_CompareString_uScriptCon_CompareString_29 = new uScriptCon_CompareString();

	private string logic_uScriptCon_CompareString_A_29 = string.Empty;

	private string logic_uScriptCon_CompareString_B_29 = "go";

	private bool logic_uScriptCon_CompareString_Same_29 = true;

	private bool logic_uScriptCon_CompareString_Different_29 = true;

	private uScriptAct_GetParent logic_uScriptAct_GetParent_uScriptAct_GetParent_32 = new uScriptAct_GetParent();

	private GameObject logic_uScriptAct_GetParent_Target_32;

	private GameObject logic_uScriptAct_GetParent_Parent_32;

	private bool logic_uScriptAct_GetParent_Out_32 = true;

	private uScriptAct_GetParent logic_uScriptAct_GetParent_uScriptAct_GetParent_33 = new uScriptAct_GetParent();

	private GameObject logic_uScriptAct_GetParent_Target_33;

	private GameObject logic_uScriptAct_GetParent_Parent_33;

	private bool logic_uScriptAct_GetParent_Out_33 = true;

	private int event_UnityEngine_GameObject_TimesToTrigger_1;

	private GameObject event_UnityEngine_GameObject_GameObject_1;

	private GameObject event_UnityEngine_GameObject_Sender_22;

	private string event_UnityEngine_GameObject_EventName_22 = string.Empty;

	private GameObject event_UnityEngine_GameObject_Sender_28;

	private string event_UnityEngine_GameObject_EventName_28 = string.Empty;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (local_34_UnityEngine_GameObject_previous != local_34_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_34_UnityEngine_GameObject_previous = local_34_UnityEngine_GameObject;
		}
		if (local_35_UnityEngine_GameObject_previous != local_35_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			if (null != local_35_UnityEngine_GameObject_previous)
			{
				uScript_CustomEvent component = local_35_UnityEngine_GameObject_previous.GetComponent<uScript_CustomEvent>();
				if (null != component)
				{
					component.OnCustomEvent -= Instance_OnCustomEvent_22;
				}
				uScript_CustomEvent component2 = local_35_UnityEngine_GameObject_previous.GetComponent<uScript_CustomEvent>();
				if (null != component2)
				{
					component2.OnCustomEvent -= Instance_OnCustomEvent_28;
				}
			}
			local_35_UnityEngine_GameObject_previous = local_35_UnityEngine_GameObject;
			if (null != local_35_UnityEngine_GameObject)
			{
				uScript_CustomEvent uScript_CustomEvent2 = local_35_UnityEngine_GameObject.GetComponent<uScript_CustomEvent>();
				if (null == uScript_CustomEvent2)
				{
					uScript_CustomEvent2 = local_35_UnityEngine_GameObject.AddComponent<uScript_CustomEvent>();
				}
				if (null != uScript_CustomEvent2)
				{
					uScript_CustomEvent2.OnCustomEvent += Instance_OnCustomEvent_22;
				}
				uScript_CustomEvent uScript_CustomEvent3 = local_35_UnityEngine_GameObject.GetComponent<uScript_CustomEvent>();
				if (null == uScript_CustomEvent3)
				{
					uScript_CustomEvent3 = local_35_UnityEngine_GameObject.AddComponent<uScript_CustomEvent>();
				}
				if (null != uScript_CustomEvent3)
				{
					uScript_CustomEvent3.OnCustomEvent += Instance_OnCustomEvent_28;
				}
			}
		}
		if (null == owner_Connection_0 || !m_RegisteredForEvents)
		{
			owner_Connection_0 = parentGameObject;
			if (null != owner_Connection_0)
			{
				uScript_Triggers uScript_Triggers2 = owner_Connection_0.GetComponent<uScript_Triggers>();
				if (null == uScript_Triggers2)
				{
					uScript_Triggers2 = owner_Connection_0.AddComponent<uScript_Triggers>();
				}
				if (null != uScript_Triggers2)
				{
					uScript_Triggers2.TimesToTrigger = event_UnityEngine_GameObject_TimesToTrigger_1;
				}
				uScript_Triggers uScript_Triggers3 = owner_Connection_0.GetComponent<uScript_Triggers>();
				if (null == uScript_Triggers3)
				{
					uScript_Triggers3 = owner_Connection_0.AddComponent<uScript_Triggers>();
				}
				if (null != uScript_Triggers3)
				{
					uScript_Triggers3.OnEnterTrigger += Instance_OnEnterTrigger_1;
					uScript_Triggers3.OnExitTrigger += Instance_OnExitTrigger_1;
					uScript_Triggers3.WhileInsideTrigger += Instance_WhileInsideTrigger_1;
				}
			}
		}
		if (!(null == owner_Connection_27) && m_RegisteredForEvents)
		{
			return;
		}
		owner_Connection_27 = parentGameObject;
		if (null != owner_Connection_27)
		{
			uScript_Global uScript_Global2 = owner_Connection_27.GetComponent<uScript_Global>();
			if (null == uScript_Global2)
			{
				uScript_Global2 = owner_Connection_27.AddComponent<uScript_Global>();
			}
			if (null != uScript_Global2)
			{
				uScript_Global2.uScriptStart += Instance_uScriptStart_31;
				uScript_Global2.uScriptLateStart += Instance_uScriptLateStart_31;
			}
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (local_34_UnityEngine_GameObject_previous != local_34_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_34_UnityEngine_GameObject_previous = local_34_UnityEngine_GameObject;
		}
		if (local_35_UnityEngine_GameObject_previous != local_35_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			if (null != local_35_UnityEngine_GameObject_previous)
			{
				uScript_CustomEvent component = local_35_UnityEngine_GameObject_previous.GetComponent<uScript_CustomEvent>();
				if (null != component)
				{
					component.OnCustomEvent -= Instance_OnCustomEvent_22;
				}
				uScript_CustomEvent component2 = local_35_UnityEngine_GameObject_previous.GetComponent<uScript_CustomEvent>();
				if (null != component2)
				{
					component2.OnCustomEvent -= Instance_OnCustomEvent_28;
				}
			}
			local_35_UnityEngine_GameObject_previous = local_35_UnityEngine_GameObject;
			if (null != local_35_UnityEngine_GameObject)
			{
				uScript_CustomEvent uScript_CustomEvent2 = local_35_UnityEngine_GameObject.GetComponent<uScript_CustomEvent>();
				if (null == uScript_CustomEvent2)
				{
					uScript_CustomEvent2 = local_35_UnityEngine_GameObject.AddComponent<uScript_CustomEvent>();
				}
				if (null != uScript_CustomEvent2)
				{
					uScript_CustomEvent2.OnCustomEvent += Instance_OnCustomEvent_22;
				}
				uScript_CustomEvent uScript_CustomEvent3 = local_35_UnityEngine_GameObject.GetComponent<uScript_CustomEvent>();
				if (null == uScript_CustomEvent3)
				{
					uScript_CustomEvent3 = local_35_UnityEngine_GameObject.AddComponent<uScript_CustomEvent>();
				}
				if (null != uScript_CustomEvent3)
				{
					uScript_CustomEvent3.OnCustomEvent += Instance_OnCustomEvent_28;
				}
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_0)
		{
			uScript_Triggers uScript_Triggers2 = owner_Connection_0.GetComponent<uScript_Triggers>();
			if (null == uScript_Triggers2)
			{
				uScript_Triggers2 = owner_Connection_0.AddComponent<uScript_Triggers>();
			}
			if (null != uScript_Triggers2)
			{
				uScript_Triggers2.TimesToTrigger = event_UnityEngine_GameObject_TimesToTrigger_1;
			}
			uScript_Triggers uScript_Triggers3 = owner_Connection_0.GetComponent<uScript_Triggers>();
			if (null == uScript_Triggers3)
			{
				uScript_Triggers3 = owner_Connection_0.AddComponent<uScript_Triggers>();
			}
			if (null != uScript_Triggers3)
			{
				uScript_Triggers3.OnEnterTrigger += Instance_OnEnterTrigger_1;
				uScript_Triggers3.OnExitTrigger += Instance_OnExitTrigger_1;
				uScript_Triggers3.WhileInsideTrigger += Instance_WhileInsideTrigger_1;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_27)
		{
			uScript_Global uScript_Global2 = owner_Connection_27.GetComponent<uScript_Global>();
			if (null == uScript_Global2)
			{
				uScript_Global2 = owner_Connection_27.AddComponent<uScript_Global>();
			}
			if (null != uScript_Global2)
			{
				uScript_Global2.uScriptStart += Instance_uScriptStart_31;
				uScript_Global2.uScriptLateStart += Instance_uScriptLateStart_31;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != local_35_UnityEngine_GameObject)
		{
			uScript_CustomEvent component = local_35_UnityEngine_GameObject.GetComponent<uScript_CustomEvent>();
			if (null != component)
			{
				component.OnCustomEvent -= Instance_OnCustomEvent_22;
			}
			uScript_CustomEvent component2 = local_35_UnityEngine_GameObject.GetComponent<uScript_CustomEvent>();
			if (null != component2)
			{
				component2.OnCustomEvent -= Instance_OnCustomEvent_28;
			}
		}
		if (null != owner_Connection_0)
		{
			uScript_Triggers component3 = owner_Connection_0.GetComponent<uScript_Triggers>();
			if (null != component3)
			{
				component3.OnEnterTrigger -= Instance_OnEnterTrigger_1;
				component3.OnExitTrigger -= Instance_OnExitTrigger_1;
				component3.WhileInsideTrigger -= Instance_WhileInsideTrigger_1;
			}
		}
		if (null != owner_Connection_27)
		{
			uScript_Global component4 = owner_Connection_27.GetComponent<uScript_Global>();
			if (null != component4)
			{
				component4.uScriptStart -= Instance_uScriptStart_31;
				component4.uScriptLateStart -= Instance_uScriptLateStart_31;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptAct_GetAnimationState_uScriptAct_GetAnimationState_4.SetParent(g);
		logic_uScriptAct_PlayAnimation_uScriptAct_PlayAnimation_5.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_6.SetParent(g);
		logic_uScriptAct_SetAnimationPosition_uScriptAct_SetAnimationPosition_7.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_8.SetParent(g);
		logic_uScriptAct_SetAnimationPosition_uScriptAct_SetAnimationPosition_9.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_10.SetParent(g);
		logic_uScriptAct_GetAnimationState_uScriptAct_GetAnimationState_12.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_13.SetParent(g);
		logic_uScriptAct_PlayAnimation_uScriptAct_PlayAnimation_14.SetParent(g);
		logic_uScriptAct_SubtractFloat_uScriptAct_SubtractFloat_18.SetParent(g);
		logic_uScriptAct_SubtractFloat_uScriptAct_SubtractFloat_20.SetParent(g);
		logic_uScriptCon_CompareString_uScriptCon_CompareString_23.SetParent(g);
		logic_uScriptCon_Gate_uScriptCon_Gate_25.SetParent(g);
		logic_uScriptCon_Gate_uScriptCon_Gate_26.SetParent(g);
		logic_uScriptCon_CompareString_uScriptCon_CompareString_29.SetParent(g);
		logic_uScriptAct_GetParent_uScriptAct_GetParent_32.SetParent(g);
		logic_uScriptAct_GetParent_uScriptAct_GetParent_33.SetParent(g);
	}

	public void Awake()
	{
		logic_uScriptAct_PlayAnimation_uScriptAct_PlayAnimation_5.Finished += uScriptAct_PlayAnimation_Finished_5;
		logic_uScriptAct_SetAnimationPosition_uScriptAct_SetAnimationPosition_7.Out += uScriptAct_SetAnimationPosition_Out_7;
		logic_uScriptAct_SetAnimationPosition_uScriptAct_SetAnimationPosition_9.Out += uScriptAct_SetAnimationPosition_Out_9;
		logic_uScriptAct_PlayAnimation_uScriptAct_PlayAnimation_14.Finished += uScriptAct_PlayAnimation_Finished_14;
		logic_uScriptCon_Gate_uScriptCon_Gate_25.Out += uScriptCon_Gate_Out_25;
		logic_uScriptCon_Gate_uScriptCon_Gate_26.Out += uScriptCon_Gate_Out_26;
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
		logic_uScriptAct_PlayAnimation_uScriptAct_PlayAnimation_5.Update();
		logic_uScriptAct_PlayAnimation_uScriptAct_PlayAnimation_14.Update();
	}

	public void OnDestroy()
	{
		logic_uScriptAct_PlayAnimation_uScriptAct_PlayAnimation_5.Finished -= uScriptAct_PlayAnimation_Finished_5;
		logic_uScriptAct_SetAnimationPosition_uScriptAct_SetAnimationPosition_7.Out -= uScriptAct_SetAnimationPosition_Out_7;
		logic_uScriptAct_SetAnimationPosition_uScriptAct_SetAnimationPosition_9.Out -= uScriptAct_SetAnimationPosition_Out_9;
		logic_uScriptAct_PlayAnimation_uScriptAct_PlayAnimation_14.Finished -= uScriptAct_PlayAnimation_Finished_14;
		logic_uScriptCon_Gate_uScriptCon_Gate_25.Out -= uScriptCon_Gate_Out_25;
		logic_uScriptCon_Gate_uScriptCon_Gate_26.Out -= uScriptCon_Gate_Out_26;
	}

	private void Instance_OnEnterTrigger_1(object o, uScript_Triggers.TriggerEventArgs e)
	{
		event_UnityEngine_GameObject_GameObject_1 = e.GameObject;
		Relay_OnEnterTrigger_1();
	}

	private void Instance_OnExitTrigger_1(object o, uScript_Triggers.TriggerEventArgs e)
	{
		event_UnityEngine_GameObject_GameObject_1 = e.GameObject;
		Relay_OnExitTrigger_1();
	}

	private void Instance_WhileInsideTrigger_1(object o, uScript_Triggers.TriggerEventArgs e)
	{
		event_UnityEngine_GameObject_GameObject_1 = e.GameObject;
		Relay_WhileInsideTrigger_1();
	}

	private void Instance_OnCustomEvent_22(object o, uScript_CustomEvent.CustomEventBoolArgs e)
	{
		event_UnityEngine_GameObject_Sender_22 = e.Sender;
		event_UnityEngine_GameObject_EventName_22 = e.EventName;
		Relay_OnCustomEvent_22();
	}

	private void Instance_OnCustomEvent_28(object o, uScript_CustomEvent.CustomEventBoolArgs e)
	{
		event_UnityEngine_GameObject_Sender_28 = e.Sender;
		event_UnityEngine_GameObject_EventName_28 = e.EventName;
		Relay_OnCustomEvent_28();
	}

	private void Instance_uScriptStart_31(object o, EventArgs e)
	{
		Relay_uScriptStart_31();
	}

	private void Instance_uScriptLateStart_31(object o, EventArgs e)
	{
		Relay_uScriptLateStart_31();
	}

	private void uScriptAct_PlayAnimation_Finished_5(object o, EventArgs e)
	{
		Relay_Finished_5();
	}

	private void uScriptAct_SetAnimationPosition_Out_7(object o, EventArgs e)
	{
		Relay_Out_7();
	}

	private void uScriptAct_SetAnimationPosition_Out_9(object o, EventArgs e)
	{
		Relay_Out_9();
	}

	private void uScriptAct_PlayAnimation_Finished_14(object o, EventArgs e)
	{
		Relay_Finished_14();
	}

	private void uScriptCon_Gate_Out_25(object o, EventArgs e)
	{
		Relay_Out_25();
	}

	private void uScriptCon_Gate_Out_26(object o, EventArgs e)
	{
		Relay_Out_26();
	}

	private void Relay_OnEnterTrigger_1()
	{
		Relay_In_25();
	}

	private void Relay_OnExitTrigger_1()
	{
		Relay_In_26();
	}

	private void Relay_WhileInsideTrigger_1()
	{
	}

	private void Relay_In_4()
	{
		logic_uScriptAct_GetAnimationState_target_4 = owner_Connection_0;
		logic_uScriptAct_GetAnimationState_animationName_4 = local_3_System_String;
		logic_uScriptAct_GetAnimationState_uScriptAct_GetAnimationState_4.In(logic_uScriptAct_GetAnimationState_target_4, logic_uScriptAct_GetAnimationState_animationName_4, out logic_uScriptAct_GetAnimationState_weight_4, out logic_uScriptAct_GetAnimationState_normalizedPosition_4, out logic_uScriptAct_GetAnimationState_animLength_4, out logic_uScriptAct_GetAnimationState_speed_4, out logic_uScriptAct_GetAnimationState_layer_4, out logic_uScriptAct_GetAnimationState_wrapMode_4);
		local_19_System_Single = logic_uScriptAct_GetAnimationState_normalizedPosition_4;
		if (logic_uScriptAct_GetAnimationState_uScriptAct_GetAnimationState_4.Out)
		{
			Relay_In_18();
		}
	}

	private void Relay_Finished_5()
	{
		Relay_Open_26();
	}

	private void Relay_In_5()
	{
		List<GameObject> list = new List<GameObject>();
		list.Add(owner_Connection_0);
		logic_uScriptAct_PlayAnimation_Target_5 = list.ToArray();
		logic_uScriptAct_PlayAnimation_Animation_5 = local_2_System_String;
		logic_uScriptAct_PlayAnimation_uScriptAct_PlayAnimation_5.In(logic_uScriptAct_PlayAnimation_Target_5, logic_uScriptAct_PlayAnimation_Animation_5, logic_uScriptAct_PlayAnimation_SpeedFactor_5, logic_uScriptAct_PlayAnimation_AnimWrapMode_5, logic_uScriptAct_PlayAnimation_StopOtherAnimations_5);
	}

	private void Relay_In_6()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_6.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_6.Out)
		{
			Relay_In_4();
		}
	}

	private void Relay_Out_7()
	{
		Relay_In_5();
	}

	private void Relay_In_7()
	{
		logic_uScriptAct_SetAnimationPosition_target_7 = owner_Connection_0;
		logic_uScriptAct_SetAnimationPosition_animationName_7 = local_2_System_String;
		logic_uScriptAct_SetAnimationPosition_normalizedPosition_7 = local_17_System_Single;
		logic_uScriptAct_SetAnimationPosition_uScriptAct_SetAnimationPosition_7.In(logic_uScriptAct_SetAnimationPosition_target_7, logic_uScriptAct_SetAnimationPosition_animationName_7, logic_uScriptAct_SetAnimationPosition_normalizedPosition_7);
	}

	private void Relay_In_8()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_8.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_8.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_Out_9()
	{
		Relay_In_14();
	}

	private void Relay_In_9()
	{
		logic_uScriptAct_SetAnimationPosition_target_9 = owner_Connection_0;
		logic_uScriptAct_SetAnimationPosition_animationName_9 = local_11_System_String;
		logic_uScriptAct_SetAnimationPosition_normalizedPosition_9 = local_16_System_Single;
		logic_uScriptAct_SetAnimationPosition_uScriptAct_SetAnimationPosition_9.In(logic_uScriptAct_SetAnimationPosition_target_9, logic_uScriptAct_SetAnimationPosition_animationName_9, logic_uScriptAct_SetAnimationPosition_normalizedPosition_9);
	}

	private void Relay_In_10()
	{
		logic_uScriptCon_CompareFloat_A_10 = local_17_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_10.In(logic_uScriptCon_CompareFloat_A_10, logic_uScriptCon_CompareFloat_B_10);
		bool equalTo = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_10.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_10.NotEqualTo;
		if (equalTo)
		{
			Relay_In_5();
		}
		if (notEqualTo)
		{
			Relay_In_7();
		}
	}

	private void Relay_In_12()
	{
		logic_uScriptAct_GetAnimationState_target_12 = owner_Connection_0;
		logic_uScriptAct_GetAnimationState_animationName_12 = local_15_System_String;
		logic_uScriptAct_GetAnimationState_uScriptAct_GetAnimationState_12.In(logic_uScriptAct_GetAnimationState_target_12, logic_uScriptAct_GetAnimationState_animationName_12, out logic_uScriptAct_GetAnimationState_weight_12, out logic_uScriptAct_GetAnimationState_normalizedPosition_12, out logic_uScriptAct_GetAnimationState_animLength_12, out logic_uScriptAct_GetAnimationState_speed_12, out logic_uScriptAct_GetAnimationState_layer_12, out logic_uScriptAct_GetAnimationState_wrapMode_12);
		local_21_System_Single = logic_uScriptAct_GetAnimationState_normalizedPosition_12;
		if (logic_uScriptAct_GetAnimationState_uScriptAct_GetAnimationState_12.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_In_13()
	{
		logic_uScriptCon_CompareFloat_A_13 = local_16_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_13.In(logic_uScriptCon_CompareFloat_A_13, logic_uScriptCon_CompareFloat_B_13);
		bool equalTo = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_13.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_13.NotEqualTo;
		if (equalTo)
		{
			Relay_In_14();
		}
		if (notEqualTo)
		{
			Relay_In_9();
		}
	}

	private void Relay_Finished_14()
	{
	}

	private void Relay_In_14()
	{
		List<GameObject> list = new List<GameObject>();
		list.Add(owner_Connection_0);
		logic_uScriptAct_PlayAnimation_Target_14 = list.ToArray();
		logic_uScriptAct_PlayAnimation_Animation_14 = local_11_System_String;
		logic_uScriptAct_PlayAnimation_uScriptAct_PlayAnimation_14.In(logic_uScriptAct_PlayAnimation_Target_14, logic_uScriptAct_PlayAnimation_Animation_14, logic_uScriptAct_PlayAnimation_SpeedFactor_14, logic_uScriptAct_PlayAnimation_AnimWrapMode_14, logic_uScriptAct_PlayAnimation_StopOtherAnimations_14);
	}

	private void Relay_In_18()
	{
		logic_uScriptAct_SubtractFloat_B_18 = local_19_System_Single;
		logic_uScriptAct_SubtractFloat_uScriptAct_SubtractFloat_18.In(logic_uScriptAct_SubtractFloat_A_18, logic_uScriptAct_SubtractFloat_B_18, out logic_uScriptAct_SubtractFloat_FloatResult_18, out logic_uScriptAct_SubtractFloat_IntResult_18);
		local_17_System_Single = logic_uScriptAct_SubtractFloat_FloatResult_18;
		if (logic_uScriptAct_SubtractFloat_uScriptAct_SubtractFloat_18.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_20()
	{
		logic_uScriptAct_SubtractFloat_B_20 = local_21_System_Single;
		logic_uScriptAct_SubtractFloat_uScriptAct_SubtractFloat_20.In(logic_uScriptAct_SubtractFloat_A_20, logic_uScriptAct_SubtractFloat_B_20, out logic_uScriptAct_SubtractFloat_FloatResult_20, out logic_uScriptAct_SubtractFloat_IntResult_20);
		local_16_System_Single = logic_uScriptAct_SubtractFloat_FloatResult_20;
		if (logic_uScriptAct_SubtractFloat_uScriptAct_SubtractFloat_20.Out)
		{
			Relay_In_13();
		}
	}

	private void Relay_OnCustomEvent_22()
	{
		local_24_System_String = event_UnityEngine_GameObject_EventName_22;
		Relay_In_23();
	}

	private void Relay_In_23()
	{
		logic_uScriptCon_CompareString_A_23 = local_24_System_String;
		logic_uScriptCon_CompareString_uScriptCon_CompareString_23.In(logic_uScriptCon_CompareString_A_23, logic_uScriptCon_CompareString_B_23);
		if (logic_uScriptCon_CompareString_uScriptCon_CompareString_23.Same)
		{
			Relay_Close_25();
			Relay_Close_26();
		}
	}

	private void Relay_Out_25()
	{
		Relay_In_6();
	}

	private void Relay_In_25()
	{
		logic_uScriptCon_Gate_uScriptCon_Gate_25.In(logic_uScriptCon_Gate_StartOpen_25, logic_uScriptCon_Gate_AutoCloseCount_25, out logic_uScriptCon_Gate_IsOpen_25);
	}

	private void Relay_Open_25()
	{
		logic_uScriptCon_Gate_uScriptCon_Gate_25.Open(logic_uScriptCon_Gate_StartOpen_25, logic_uScriptCon_Gate_AutoCloseCount_25, out logic_uScriptCon_Gate_IsOpen_25);
	}

	private void Relay_Close_25()
	{
		logic_uScriptCon_Gate_uScriptCon_Gate_25.Close(logic_uScriptCon_Gate_StartOpen_25, logic_uScriptCon_Gate_AutoCloseCount_25, out logic_uScriptCon_Gate_IsOpen_25);
	}

	private void Relay_Toggle_25()
	{
		logic_uScriptCon_Gate_uScriptCon_Gate_25.Toggle(logic_uScriptCon_Gate_StartOpen_25, logic_uScriptCon_Gate_AutoCloseCount_25, out logic_uScriptCon_Gate_IsOpen_25);
	}

	private void Relay_Out_26()
	{
		Relay_In_8();
	}

	private void Relay_In_26()
	{
		logic_uScriptCon_Gate_uScriptCon_Gate_26.In(logic_uScriptCon_Gate_StartOpen_26, logic_uScriptCon_Gate_AutoCloseCount_26, out logic_uScriptCon_Gate_IsOpen_26);
	}

	private void Relay_Open_26()
	{
		logic_uScriptCon_Gate_uScriptCon_Gate_26.Open(logic_uScriptCon_Gate_StartOpen_26, logic_uScriptCon_Gate_AutoCloseCount_26, out logic_uScriptCon_Gate_IsOpen_26);
	}

	private void Relay_Close_26()
	{
		logic_uScriptCon_Gate_uScriptCon_Gate_26.Close(logic_uScriptCon_Gate_StartOpen_26, logic_uScriptCon_Gate_AutoCloseCount_26, out logic_uScriptCon_Gate_IsOpen_26);
	}

	private void Relay_Toggle_26()
	{
		logic_uScriptCon_Gate_uScriptCon_Gate_26.Toggle(logic_uScriptCon_Gate_StartOpen_26, logic_uScriptCon_Gate_AutoCloseCount_26, out logic_uScriptCon_Gate_IsOpen_26);
	}

	private void Relay_OnCustomEvent_28()
	{
		local_30_System_String = event_UnityEngine_GameObject_EventName_28;
		Relay_In_29();
	}

	private void Relay_In_29()
	{
		logic_uScriptCon_CompareString_A_29 = local_30_System_String;
		logic_uScriptCon_CompareString_uScriptCon_CompareString_29.In(logic_uScriptCon_CompareString_A_29, logic_uScriptCon_CompareString_B_29);
		if (logic_uScriptCon_CompareString_uScriptCon_CompareString_29.Same)
		{
			Relay_Open_25();
		}
	}

	private void Relay_uScriptStart_31()
	{
		Relay_In_32();
	}

	private void Relay_uScriptLateStart_31()
	{
	}

	private void Relay_In_32()
	{
		logic_uScriptAct_GetParent_Target_32 = owner_Connection_27;
		logic_uScriptAct_GetParent_uScriptAct_GetParent_32.In(logic_uScriptAct_GetParent_Target_32, out logic_uScriptAct_GetParent_Parent_32);
		local_34_UnityEngine_GameObject = logic_uScriptAct_GetParent_Parent_32;
		if (local_34_UnityEngine_GameObject_previous != local_34_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_34_UnityEngine_GameObject_previous = local_34_UnityEngine_GameObject;
		}
		if (logic_uScriptAct_GetParent_uScriptAct_GetParent_32.Out)
		{
			Relay_In_33();
		}
	}

	private void Relay_In_33()
	{
		if (local_34_UnityEngine_GameObject_previous != local_34_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_34_UnityEngine_GameObject_previous = local_34_UnityEngine_GameObject;
		}
		logic_uScriptAct_GetParent_Target_33 = local_34_UnityEngine_GameObject;
		logic_uScriptAct_GetParent_uScriptAct_GetParent_33.In(logic_uScriptAct_GetParent_Target_33, out logic_uScriptAct_GetParent_Parent_33);
		local_35_UnityEngine_GameObject = logic_uScriptAct_GetParent_Parent_33;
		if (!(local_35_UnityEngine_GameObject_previous != local_35_UnityEngine_GameObject) && m_RegisteredForEvents)
		{
			return;
		}
		if (null != local_35_UnityEngine_GameObject_previous)
		{
			uScript_CustomEvent component = local_35_UnityEngine_GameObject_previous.GetComponent<uScript_CustomEvent>();
			if (null != component)
			{
				component.OnCustomEvent -= Instance_OnCustomEvent_22;
			}
			uScript_CustomEvent component2 = local_35_UnityEngine_GameObject_previous.GetComponent<uScript_CustomEvent>();
			if (null != component2)
			{
				component2.OnCustomEvent -= Instance_OnCustomEvent_28;
			}
		}
		local_35_UnityEngine_GameObject_previous = local_35_UnityEngine_GameObject;
		if (null != local_35_UnityEngine_GameObject)
		{
			uScript_CustomEvent uScript_CustomEvent2 = local_35_UnityEngine_GameObject.GetComponent<uScript_CustomEvent>();
			if (null == uScript_CustomEvent2)
			{
				uScript_CustomEvent2 = local_35_UnityEngine_GameObject.AddComponent<uScript_CustomEvent>();
			}
			if (null != uScript_CustomEvent2)
			{
				uScript_CustomEvent2.OnCustomEvent += Instance_OnCustomEvent_22;
			}
			uScript_CustomEvent uScript_CustomEvent3 = local_35_UnityEngine_GameObject.GetComponent<uScript_CustomEvent>();
			if (null == uScript_CustomEvent3)
			{
				uScript_CustomEvent3 = local_35_UnityEngine_GameObject.AddComponent<uScript_CustomEvent>();
			}
			if (null != uScript_CustomEvent3)
			{
				uScript_CustomEvent3.OnCustomEvent += Instance_OnCustomEvent_28;
			}
		}
	}
}
