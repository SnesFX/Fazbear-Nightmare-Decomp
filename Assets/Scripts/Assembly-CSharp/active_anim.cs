using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class active_anim : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private string local_11_System_String = "close";

	private string local_15_System_String = "open";

	private float local_16_System_Single;

	private float local_17_System_Single;

	private string local_2_System_String = "open";

	private float local_20_System_Single;

	private float local_22_System_Single;

	private string local_3_System_String = "close";

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_18;

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

	private float logic_uScriptAct_PlayAnimation_SpeedFactor_5 = 1f;

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

	private float logic_uScriptAct_PlayAnimation_SpeedFactor_14 = 1f;

	private WrapMode logic_uScriptAct_PlayAnimation_AnimWrapMode_14;

	private bool logic_uScriptAct_PlayAnimation_StopOtherAnimations_14 = true;

	private bool logic_uScriptAct_PlayAnimation_Out_14 = true;

	private uScriptAct_SubtractFloat logic_uScriptAct_SubtractFloat_uScriptAct_SubtractFloat_19 = new uScriptAct_SubtractFloat();

	private float logic_uScriptAct_SubtractFloat_A_19 = 1f;

	private float logic_uScriptAct_SubtractFloat_B_19;

	private float logic_uScriptAct_SubtractFloat_FloatResult_19;

	private int logic_uScriptAct_SubtractFloat_IntResult_19;

	private bool logic_uScriptAct_SubtractFloat_Out_19 = true;

	private uScriptAct_SubtractFloat logic_uScriptAct_SubtractFloat_uScriptAct_SubtractFloat_21 = new uScriptAct_SubtractFloat();

	private float logic_uScriptAct_SubtractFloat_A_21 = 1f;

	private float logic_uScriptAct_SubtractFloat_B_21;

	private float logic_uScriptAct_SubtractFloat_FloatResult_21;

	private int logic_uScriptAct_SubtractFloat_IntResult_21;

	private bool logic_uScriptAct_SubtractFloat_Out_21 = true;

	private int event_UnityEngine_GameObject_TimesToTrigger_1;

	private GameObject event_UnityEngine_GameObject_GameObject_1;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
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
		if (null == owner_Connection_18 || !m_RegisteredForEvents)
		{
			owner_Connection_18 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
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
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_0)
		{
			uScript_Triggers component = owner_Connection_0.GetComponent<uScript_Triggers>();
			if (null != component)
			{
				component.OnEnterTrigger -= Instance_OnEnterTrigger_1;
				component.OnExitTrigger -= Instance_OnExitTrigger_1;
				component.WhileInsideTrigger -= Instance_WhileInsideTrigger_1;
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
		logic_uScriptAct_SubtractFloat_uScriptAct_SubtractFloat_19.SetParent(g);
		logic_uScriptAct_SubtractFloat_uScriptAct_SubtractFloat_21.SetParent(g);
	}

	public void Awake()
	{
		logic_uScriptAct_PlayAnimation_uScriptAct_PlayAnimation_5.Finished += uScriptAct_PlayAnimation_Finished_5;
		logic_uScriptAct_SetAnimationPosition_uScriptAct_SetAnimationPosition_7.Out += uScriptAct_SetAnimationPosition_Out_7;
		logic_uScriptAct_SetAnimationPosition_uScriptAct_SetAnimationPosition_9.Out += uScriptAct_SetAnimationPosition_Out_9;
		logic_uScriptAct_PlayAnimation_uScriptAct_PlayAnimation_14.Finished += uScriptAct_PlayAnimation_Finished_14;
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

	private void Relay_OnEnterTrigger_1()
	{
		Relay_In_6();
	}

	private void Relay_OnExitTrigger_1()
	{
		Relay_In_8();
	}

	private void Relay_WhileInsideTrigger_1()
	{
	}

	private void Relay_In_4()
	{
		logic_uScriptAct_GetAnimationState_target_4 = owner_Connection_18;
		logic_uScriptAct_GetAnimationState_animationName_4 = local_3_System_String;
		logic_uScriptAct_GetAnimationState_uScriptAct_GetAnimationState_4.In(logic_uScriptAct_GetAnimationState_target_4, logic_uScriptAct_GetAnimationState_animationName_4, out logic_uScriptAct_GetAnimationState_weight_4, out logic_uScriptAct_GetAnimationState_normalizedPosition_4, out logic_uScriptAct_GetAnimationState_animLength_4, out logic_uScriptAct_GetAnimationState_speed_4, out logic_uScriptAct_GetAnimationState_layer_4, out logic_uScriptAct_GetAnimationState_wrapMode_4);
		local_20_System_Single = logic_uScriptAct_GetAnimationState_normalizedPosition_4;
		if (logic_uScriptAct_GetAnimationState_uScriptAct_GetAnimationState_4.Out)
		{
			Relay_In_19();
		}
	}

	private void Relay_Finished_5()
	{
	}

	private void Relay_In_5()
	{
		List<GameObject> list = new List<GameObject>();
		list.Add(owner_Connection_18);
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
		logic_uScriptAct_SetAnimationPosition_target_7 = owner_Connection_18;
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
		logic_uScriptAct_SetAnimationPosition_target_9 = owner_Connection_18;
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
		logic_uScriptAct_GetAnimationState_target_12 = owner_Connection_18;
		logic_uScriptAct_GetAnimationState_animationName_12 = local_15_System_String;
		logic_uScriptAct_GetAnimationState_uScriptAct_GetAnimationState_12.In(logic_uScriptAct_GetAnimationState_target_12, logic_uScriptAct_GetAnimationState_animationName_12, out logic_uScriptAct_GetAnimationState_weight_12, out logic_uScriptAct_GetAnimationState_normalizedPosition_12, out logic_uScriptAct_GetAnimationState_animLength_12, out logic_uScriptAct_GetAnimationState_speed_12, out logic_uScriptAct_GetAnimationState_layer_12, out logic_uScriptAct_GetAnimationState_wrapMode_12);
		local_22_System_Single = logic_uScriptAct_GetAnimationState_normalizedPosition_12;
		if (logic_uScriptAct_GetAnimationState_uScriptAct_GetAnimationState_12.Out)
		{
			Relay_In_21();
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
		list.Add(owner_Connection_18);
		logic_uScriptAct_PlayAnimation_Target_14 = list.ToArray();
		logic_uScriptAct_PlayAnimation_Animation_14 = local_11_System_String;
		logic_uScriptAct_PlayAnimation_uScriptAct_PlayAnimation_14.In(logic_uScriptAct_PlayAnimation_Target_14, logic_uScriptAct_PlayAnimation_Animation_14, logic_uScriptAct_PlayAnimation_SpeedFactor_14, logic_uScriptAct_PlayAnimation_AnimWrapMode_14, logic_uScriptAct_PlayAnimation_StopOtherAnimations_14);
	}

	private void Relay_In_19()
	{
		logic_uScriptAct_SubtractFloat_B_19 = local_20_System_Single;
		logic_uScriptAct_SubtractFloat_uScriptAct_SubtractFloat_19.In(logic_uScriptAct_SubtractFloat_A_19, logic_uScriptAct_SubtractFloat_B_19, out logic_uScriptAct_SubtractFloat_FloatResult_19, out logic_uScriptAct_SubtractFloat_IntResult_19);
		local_17_System_Single = logic_uScriptAct_SubtractFloat_FloatResult_19;
		if (logic_uScriptAct_SubtractFloat_uScriptAct_SubtractFloat_19.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_21()
	{
		logic_uScriptAct_SubtractFloat_B_21 = local_22_System_Single;
		logic_uScriptAct_SubtractFloat_uScriptAct_SubtractFloat_21.In(logic_uScriptAct_SubtractFloat_A_21, logic_uScriptAct_SubtractFloat_B_21, out logic_uScriptAct_SubtractFloat_FloatResult_21, out logic_uScriptAct_SubtractFloat_IntResult_21);
		local_16_System_Single = logic_uScriptAct_SubtractFloat_FloatResult_21;
		if (logic_uScriptAct_SubtractFloat_uScriptAct_SubtractFloat_21.Out)
		{
			Relay_In_13();
		}
	}
}
