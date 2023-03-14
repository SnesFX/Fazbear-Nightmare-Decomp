using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[FriendlyName("", "")]
[NodePath("Graphs")]
public class lift_make_your_lift : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public GameObject _Person_Controller;

	private GameObject _Person_Controller_previous;

	public KeyCode BOTTOM;

	private GameObject local_11_UnityEngine_GameObject;

	private GameObject local_11_UnityEngine_GameObject_previous;

	private GameObject local_13_UnityEngine_GameObject;

	private GameObject local_13_UnityEngine_GameObject_previous;

	private GameObject local_40_UnityEngine_GameObject;

	private GameObject local_40_UnityEngine_GameObject_previous;

	private GameObject local_42_UnityEngine_GameObject;

	private GameObject local_42_UnityEngine_GameObject_previous;

	private GameObject local_44_UnityEngine_GameObject;

	private GameObject local_44_UnityEngine_GameObject_previous;

	private string local_47_System_String = string.Empty;

	private string local_49_System_String = string.Empty;

	private string local_51_System_String = string.Empty;

	private GameObject local_etage_UnityEngine_GameObject;

	private GameObject local_etage_UnityEngine_GameObject_previous;

	private GameObject local_lift_active_UnityEngine_GameObject;

	private GameObject local_lift_active_UnityEngine_GameObject_previous;

	private GameObject local_lift_make_UnityEngine_GameObject;

	private GameObject local_lift_make_UnityEngine_GameObject_previous;

	private GameObject local_lift_make_your_lift_UnityEngine_GameObject;

	private GameObject local_lift_make_your_lift_UnityEngine_GameObject_previous;

	public KeyCode TOP;

	private uScriptCon_CompareGameObjects logic_uScriptCon_CompareGameObjects_uScriptCon_CompareGameObjects_1 = new uScriptCon_CompareGameObjects();

	private GameObject logic_uScriptCon_CompareGameObjects_A_1;

	private GameObject logic_uScriptCon_CompareGameObjects_B_1;

	private bool logic_uScriptCon_CompareGameObjects_CompareByTag_1;

	private bool logic_uScriptCon_CompareGameObjects_CompareByName_1 = true;

	private bool logic_uScriptCon_CompareGameObjects_ReportNull_1 = true;

	private bool logic_uScriptCon_CompareGameObjects_Same_1 = true;

	private bool logic_uScriptCon_CompareGameObjects_Different_1 = true;

	private uScriptAct_GetChildrenByName logic_uScriptAct_GetChildrenByName_uScriptAct_GetChildrenByName_3 = new uScriptAct_GetChildrenByName();

	private GameObject logic_uScriptAct_GetChildrenByName_Target_3;

	private string logic_uScriptAct_GetChildrenByName_Name_3 = "lift_active";

	private uScriptAct_GetChildrenByName.SearchType logic_uScriptAct_GetChildrenByName_SearchMethod_3;

	private bool logic_uScriptAct_GetChildrenByName_recursive_3;

	private GameObject logic_uScriptAct_GetChildrenByName_FirstChild_3;

	private GameObject[] logic_uScriptAct_GetChildrenByName_Children_3;

	private int logic_uScriptAct_GetChildrenByName_ChildrenCount_3;

	private bool logic_uScriptAct_GetChildrenByName_Out_3 = true;

	private bool logic_uScriptAct_GetChildrenByName_ChildrenFound_3 = true;

	private bool logic_uScriptAct_GetChildrenByName_ChildrenNotFound_3 = true;

	private uScriptAct_GetParent logic_uScriptAct_GetParent_uScriptAct_GetParent_4 = new uScriptAct_GetParent();

	private GameObject logic_uScriptAct_GetParent_Target_4;

	private GameObject logic_uScriptAct_GetParent_Parent_4;

	private bool logic_uScriptAct_GetParent_Out_4 = true;

	private uScriptAct_Teleport logic_uScriptAct_Teleport_uScriptAct_Teleport_5 = new uScriptAct_Teleport();

	private GameObject[] logic_uScriptAct_Teleport_Target_5 = new GameObject[0];

	private GameObject logic_uScriptAct_Teleport_Destination_5;

	private bool logic_uScriptAct_Teleport_UpdateRotation_5;

	private bool logic_uScriptAct_Teleport_Out_5 = true;

	private uScriptAct_MoveToLocationRelative logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_6 = new uScriptAct_MoveToLocationRelative();

	private GameObject[] logic_uScriptAct_MoveToLocationRelative_targetArray_6 = new GameObject[0];

	private Vector3 logic_uScriptAct_MoveToLocationRelative_location_6 = new Vector3(0f, 3.2f, 0f);

	private GameObject logic_uScriptAct_MoveToLocationRelative_source_6;

	private float logic_uScriptAct_MoveToLocationRelative_totalTime_6 = 2f;

	private bool logic_uScriptAct_MoveToLocationRelative_Out_6 = true;

	private bool logic_uScriptAct_MoveToLocationRelative_Cancelled_6 = true;

	private uScriptAct_GetParent logic_uScriptAct_GetParent_uScriptAct_GetParent_8 = new uScriptAct_GetParent();

	private GameObject logic_uScriptAct_GetParent_Target_8;

	private GameObject logic_uScriptAct_GetParent_Parent_8;

	private bool logic_uScriptAct_GetParent_Out_8 = true;

	private uScriptAct_GetChildrenByName logic_uScriptAct_GetChildrenByName_uScriptAct_GetChildrenByName_9 = new uScriptAct_GetChildrenByName();

	private GameObject logic_uScriptAct_GetChildrenByName_Target_9;

	private string logic_uScriptAct_GetChildrenByName_Name_9 = "lift_active";

	private uScriptAct_GetChildrenByName.SearchType logic_uScriptAct_GetChildrenByName_SearchMethod_9;

	private bool logic_uScriptAct_GetChildrenByName_recursive_9;

	private GameObject logic_uScriptAct_GetChildrenByName_FirstChild_9;

	private GameObject[] logic_uScriptAct_GetChildrenByName_Children_9;

	private int logic_uScriptAct_GetChildrenByName_ChildrenCount_9;

	private bool logic_uScriptAct_GetChildrenByName_Out_9 = true;

	private bool logic_uScriptAct_GetChildrenByName_ChildrenFound_9 = true;

	private bool logic_uScriptAct_GetChildrenByName_ChildrenNotFound_9 = true;

	private uScriptAct_GetParent logic_uScriptAct_GetParent_uScriptAct_GetParent_15 = new uScriptAct_GetParent();

	private GameObject logic_uScriptAct_GetParent_Target_15;

	private GameObject logic_uScriptAct_GetParent_Parent_15;

	private bool logic_uScriptAct_GetParent_Out_15 = true;

	private uScriptAct_OnInputEventFilter logic_uScriptAct_OnInputEventFilter_uScriptAct_OnInputEventFilter_17 = new uScriptAct_OnInputEventFilter();

	private KeyCode logic_uScriptAct_OnInputEventFilter_KeyCode_17;

	private bool logic_uScriptAct_OnInputEventFilter_KeyDown_17 = true;

	private bool logic_uScriptAct_OnInputEventFilter_KeyHeld_17 = true;

	private bool logic_uScriptAct_OnInputEventFilter_KeyUp_17 = true;

	private uScriptAct_GetChildrenByName logic_uScriptAct_GetChildrenByName_uScriptAct_GetChildrenByName_19 = new uScriptAct_GetChildrenByName();

	private GameObject logic_uScriptAct_GetChildrenByName_Target_19;

	private string logic_uScriptAct_GetChildrenByName_Name_19 = "lift_active";

	private uScriptAct_GetChildrenByName.SearchType logic_uScriptAct_GetChildrenByName_SearchMethod_19;

	private bool logic_uScriptAct_GetChildrenByName_recursive_19;

	private GameObject logic_uScriptAct_GetChildrenByName_FirstChild_19;

	private GameObject[] logic_uScriptAct_GetChildrenByName_Children_19;

	private int logic_uScriptAct_GetChildrenByName_ChildrenCount_19;

	private bool logic_uScriptAct_GetChildrenByName_Out_19 = true;

	private bool logic_uScriptAct_GetChildrenByName_ChildrenFound_19 = true;

	private bool logic_uScriptAct_GetChildrenByName_ChildrenNotFound_19 = true;

	private uScriptAct_MoveToLocationRelative logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_20 = new uScriptAct_MoveToLocationRelative();

	private GameObject[] logic_uScriptAct_MoveToLocationRelative_targetArray_20 = new GameObject[0];

	private Vector3 logic_uScriptAct_MoveToLocationRelative_location_20 = new Vector3(0f, -3.2f, 0f);

	private GameObject logic_uScriptAct_MoveToLocationRelative_source_20;

	private float logic_uScriptAct_MoveToLocationRelative_totalTime_20 = 2f;

	private bool logic_uScriptAct_MoveToLocationRelative_Out_20 = true;

	private bool logic_uScriptAct_MoveToLocationRelative_Cancelled_20 = true;

	private uScriptAct_OnInputEventFilter logic_uScriptAct_OnInputEventFilter_uScriptAct_OnInputEventFilter_22 = new uScriptAct_OnInputEventFilter();

	private KeyCode logic_uScriptAct_OnInputEventFilter_KeyCode_22;

	private bool logic_uScriptAct_OnInputEventFilter_KeyDown_22 = true;

	private bool logic_uScriptAct_OnInputEventFilter_KeyHeld_22 = true;

	private bool logic_uScriptAct_OnInputEventFilter_KeyUp_22 = true;

	private uScriptAct_GetChildrenByName logic_uScriptAct_GetChildrenByName_uScriptAct_GetChildrenByName_25 = new uScriptAct_GetChildrenByName();

	private GameObject logic_uScriptAct_GetChildrenByName_Target_25;

	private string logic_uScriptAct_GetChildrenByName_Name_25 = "lift_active";

	private uScriptAct_GetChildrenByName.SearchType logic_uScriptAct_GetChildrenByName_SearchMethod_25;

	private bool logic_uScriptAct_GetChildrenByName_recursive_25;

	private GameObject logic_uScriptAct_GetChildrenByName_FirstChild_25;

	private GameObject[] logic_uScriptAct_GetChildrenByName_Children_25;

	private int logic_uScriptAct_GetChildrenByName_ChildrenCount_25;

	private bool logic_uScriptAct_GetChildrenByName_Out_25 = true;

	private bool logic_uScriptAct_GetChildrenByName_ChildrenFound_25 = true;

	private bool logic_uScriptAct_GetChildrenByName_ChildrenNotFound_25 = true;

	private uScriptAct_GetParent logic_uScriptAct_GetParent_uScriptAct_GetParent_26 = new uScriptAct_GetParent();

	private GameObject logic_uScriptAct_GetParent_Target_26;

	private GameObject logic_uScriptAct_GetParent_Parent_26;

	private bool logic_uScriptAct_GetParent_Out_26 = true;

	private uScriptAct_MoveToLocationRelative logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_27 = new uScriptAct_MoveToLocationRelative();

	private GameObject[] logic_uScriptAct_MoveToLocationRelative_targetArray_27 = new GameObject[0];

	private Vector3 logic_uScriptAct_MoveToLocationRelative_location_27 = new Vector3(0f, 3.2f, 0f);

	private GameObject logic_uScriptAct_MoveToLocationRelative_source_27;

	private float logic_uScriptAct_MoveToLocationRelative_totalTime_27 = 2f;

	private bool logic_uScriptAct_MoveToLocationRelative_Out_27 = true;

	private bool logic_uScriptAct_MoveToLocationRelative_Cancelled_27 = true;

	private uScriptAct_OnInputEventFilter logic_uScriptAct_OnInputEventFilter_uScriptAct_OnInputEventFilter_29 = new uScriptAct_OnInputEventFilter();

	private KeyCode logic_uScriptAct_OnInputEventFilter_KeyCode_29;

	private bool logic_uScriptAct_OnInputEventFilter_KeyDown_29 = true;

	private bool logic_uScriptAct_OnInputEventFilter_KeyHeld_29 = true;

	private bool logic_uScriptAct_OnInputEventFilter_KeyUp_29 = true;

	private uScriptAct_MoveToLocationRelative logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_30 = new uScriptAct_MoveToLocationRelative();

	private GameObject[] logic_uScriptAct_MoveToLocationRelative_targetArray_30 = new GameObject[0];

	private Vector3 logic_uScriptAct_MoveToLocationRelative_location_30 = new Vector3(0f, -3.2f, 0f);

	private GameObject logic_uScriptAct_MoveToLocationRelative_source_30;

	private float logic_uScriptAct_MoveToLocationRelative_totalTime_30 = 2f;

	private bool logic_uScriptAct_MoveToLocationRelative_Out_30 = true;

	private bool logic_uScriptAct_MoveToLocationRelative_Cancelled_30 = true;

	private uScriptAct_GetChildrenByName logic_uScriptAct_GetChildrenByName_uScriptAct_GetChildrenByName_31 = new uScriptAct_GetChildrenByName();

	private GameObject logic_uScriptAct_GetChildrenByName_Target_31;

	private string logic_uScriptAct_GetChildrenByName_Name_31 = "lift_active";

	private uScriptAct_GetChildrenByName.SearchType logic_uScriptAct_GetChildrenByName_SearchMethod_31;

	private bool logic_uScriptAct_GetChildrenByName_recursive_31;

	private GameObject logic_uScriptAct_GetChildrenByName_FirstChild_31;

	private GameObject[] logic_uScriptAct_GetChildrenByName_Children_31;

	private int logic_uScriptAct_GetChildrenByName_ChildrenCount_31;

	private bool logic_uScriptAct_GetChildrenByName_Out_31 = true;

	private bool logic_uScriptAct_GetChildrenByName_ChildrenFound_31 = true;

	private bool logic_uScriptAct_GetChildrenByName_ChildrenNotFound_31 = true;

	private uScriptAct_GetParent logic_uScriptAct_GetParent_uScriptAct_GetParent_35 = new uScriptAct_GetParent();

	private GameObject logic_uScriptAct_GetParent_Target_35;

	private GameObject logic_uScriptAct_GetParent_Parent_35;

	private bool logic_uScriptAct_GetParent_Out_35 = true;

	private uScriptAct_OnInputEventFilter logic_uScriptAct_OnInputEventFilter_uScriptAct_OnInputEventFilter_38 = new uScriptAct_OnInputEventFilter();

	private KeyCode logic_uScriptAct_OnInputEventFilter_KeyCode_38;

	private bool logic_uScriptAct_OnInputEventFilter_KeyDown_38 = true;

	private bool logic_uScriptAct_OnInputEventFilter_KeyHeld_38 = true;

	private bool logic_uScriptAct_OnInputEventFilter_KeyUp_38 = true;

	private uScriptAct_GetParent logic_uScriptAct_GetParent_uScriptAct_GetParent_39 = new uScriptAct_GetParent();

	private GameObject logic_uScriptAct_GetParent_Target_39;

	private GameObject logic_uScriptAct_GetParent_Parent_39;

	private bool logic_uScriptAct_GetParent_Out_39 = true;

	private uScriptAct_GetParent logic_uScriptAct_GetParent_uScriptAct_GetParent_41 = new uScriptAct_GetParent();

	private GameObject logic_uScriptAct_GetParent_Target_41;

	private GameObject logic_uScriptAct_GetParent_Parent_41;

	private bool logic_uScriptAct_GetParent_Out_41 = true;

	private uScriptAct_GetParent logic_uScriptAct_GetParent_uScriptAct_GetParent_43 = new uScriptAct_GetParent();

	private GameObject logic_uScriptAct_GetParent_Target_43;

	private GameObject logic_uScriptAct_GetParent_Parent_43;

	private bool logic_uScriptAct_GetParent_Out_43 = true;

	private uScriptAct_GetGameObjectName logic_uScriptAct_GetGameObjectName_uScriptAct_GetGameObjectName_45 = new uScriptAct_GetGameObjectName();

	private GameObject logic_uScriptAct_GetGameObjectName_gameObject_45;

	private string logic_uScriptAct_GetGameObjectName_name_45;

	private bool logic_uScriptAct_GetGameObjectName_Out_45 = true;

	private uScriptCon_CompareString logic_uScriptCon_CompareString_uScriptCon_CompareString_46 = new uScriptCon_CompareString();

	private string logic_uScriptCon_CompareString_A_46 = string.Empty;

	private string logic_uScriptCon_CompareString_B_46 = "detect_lift_last";

	private bool logic_uScriptCon_CompareString_Same_46 = true;

	private bool logic_uScriptCon_CompareString_Different_46 = true;

	private uScriptCon_CompareString logic_uScriptCon_CompareString_uScriptCon_CompareString_48 = new uScriptCon_CompareString();

	private string logic_uScriptCon_CompareString_A_48 = string.Empty;

	private string logic_uScriptCon_CompareString_B_48 = "detect_lift_1st";

	private bool logic_uScriptCon_CompareString_Same_48 = true;

	private bool logic_uScriptCon_CompareString_Different_48 = true;

	private uScriptAct_GetGameObjectName logic_uScriptAct_GetGameObjectName_uScriptAct_GetGameObjectName_50 = new uScriptAct_GetGameObjectName();

	private GameObject logic_uScriptAct_GetGameObjectName_gameObject_50;

	private string logic_uScriptAct_GetGameObjectName_name_50;

	private bool logic_uScriptAct_GetGameObjectName_Out_50 = true;

	private uScriptCon_CompareString logic_uScriptCon_CompareString_uScriptCon_CompareString_52 = new uScriptCon_CompareString();

	private string logic_uScriptCon_CompareString_A_52 = string.Empty;

	private string logic_uScriptCon_CompareString_B_52 = "detect_lift_middle";

	private bool logic_uScriptCon_CompareString_Same_52 = true;

	private bool logic_uScriptCon_CompareString_Different_52 = true;

	private uScriptAct_GetGameObjectName logic_uScriptAct_GetGameObjectName_uScriptAct_GetGameObjectName_53 = new uScriptAct_GetGameObjectName();

	private GameObject logic_uScriptAct_GetGameObjectName_gameObject_53;

	private string logic_uScriptAct_GetGameObjectName_name_53;

	private bool logic_uScriptAct_GetGameObjectName_Out_53 = true;

	private uScriptAct_SendCustomEvent logic_uScriptAct_SendCustomEvent_uScriptAct_SendCustomEvent_54 = new uScriptAct_SendCustomEvent();

	private string logic_uScriptAct_SendCustomEvent_EventName_54 = "stop";

	private uScriptCustomEvent.SendGroup logic_uScriptAct_SendCustomEvent_sendGroup_54 = uScriptCustomEvent.SendGroup.Children;

	private GameObject logic_uScriptAct_SendCustomEvent_EventSender_54;

	private bool logic_uScriptAct_SendCustomEvent_Out_54 = true;

	private uScriptAct_SendCustomEvent logic_uScriptAct_SendCustomEvent_uScriptAct_SendCustomEvent_55 = new uScriptAct_SendCustomEvent();

	private string logic_uScriptAct_SendCustomEvent_EventName_55 = "go";

	private uScriptCustomEvent.SendGroup logic_uScriptAct_SendCustomEvent_sendGroup_55 = uScriptCustomEvent.SendGroup.Children;

	private GameObject logic_uScriptAct_SendCustomEvent_EventSender_55;

	private bool logic_uScriptAct_SendCustomEvent_Out_55 = true;

	private int event_UnityEngine_GameObject_TimesToTrigger_0;

	private GameObject event_UnityEngine_GameObject_GameObject_0;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == logic_uScriptCon_CompareGameObjects_B_1 || !m_RegisteredForEvents)
		{
			logic_uScriptCon_CompareGameObjects_B_1 = GameObject.Find("detect_floor");
		}
		if (local_etage_UnityEngine_GameObject_previous != local_etage_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_etage_UnityEngine_GameObject_previous = local_etage_UnityEngine_GameObject;
		}
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		if (null == _Person_Controller || !m_RegisteredForEvents)
		{
			_Person_Controller = GameObject.Find("First Person Controller");
		}
		if (_Person_Controller_previous != _Person_Controller || !m_RegisteredForEvents)
		{
			if (null != _Person_Controller_previous)
			{
				uScript_Triggers component = _Person_Controller_previous.GetComponent<uScript_Triggers>();
				if (null != component)
				{
					component.OnEnterTrigger -= Instance_OnEnterTrigger_0;
					component.OnExitTrigger -= Instance_OnExitTrigger_0;
					component.WhileInsideTrigger -= Instance_WhileInsideTrigger_0;
				}
			}
			_Person_Controller_previous = _Person_Controller;
			if (null != _Person_Controller)
			{
				uScript_Triggers uScript_Triggers2 = _Person_Controller.GetComponent<uScript_Triggers>();
				if (null == uScript_Triggers2)
				{
					uScript_Triggers2 = _Person_Controller.AddComponent<uScript_Triggers>();
				}
				if (null != uScript_Triggers2)
				{
					uScript_Triggers2.TimesToTrigger = event_UnityEngine_GameObject_TimesToTrigger_0;
				}
				uScript_Triggers uScript_Triggers3 = _Person_Controller.GetComponent<uScript_Triggers>();
				if (null == uScript_Triggers3)
				{
					uScript_Triggers3 = _Person_Controller.AddComponent<uScript_Triggers>();
				}
				if (null != uScript_Triggers3)
				{
					uScript_Triggers3.OnEnterTrigger += Instance_OnEnterTrigger_0;
					uScript_Triggers3.OnExitTrigger += Instance_OnExitTrigger_0;
					uScript_Triggers3.WhileInsideTrigger += Instance_WhileInsideTrigger_0;
				}
			}
		}
		if (local_13_UnityEngine_GameObject_previous != local_13_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_13_UnityEngine_GameObject_previous = local_13_UnityEngine_GameObject;
		}
		if (local_lift_make_UnityEngine_GameObject_previous != local_lift_make_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_make_UnityEngine_GameObject_previous = local_lift_make_UnityEngine_GameObject;
		}
		if (local_lift_active_UnityEngine_GameObject_previous != local_lift_active_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_active_UnityEngine_GameObject_previous = local_lift_active_UnityEngine_GameObject;
		}
		if (local_lift_make_your_lift_UnityEngine_GameObject_previous != local_lift_make_your_lift_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_make_your_lift_UnityEngine_GameObject_previous = local_lift_make_your_lift_UnityEngine_GameObject;
		}
		if (local_40_UnityEngine_GameObject_previous != local_40_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_40_UnityEngine_GameObject_previous = local_40_UnityEngine_GameObject;
		}
		if (local_42_UnityEngine_GameObject_previous != local_42_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_42_UnityEngine_GameObject_previous = local_42_UnityEngine_GameObject;
		}
		if (local_44_UnityEngine_GameObject_previous != local_44_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_44_UnityEngine_GameObject_previous = local_44_UnityEngine_GameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (local_etage_UnityEngine_GameObject_previous != local_etage_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_etage_UnityEngine_GameObject_previous = local_etage_UnityEngine_GameObject;
		}
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		if (_Person_Controller_previous != _Person_Controller || !m_RegisteredForEvents)
		{
			if (null != _Person_Controller_previous)
			{
				uScript_Triggers component = _Person_Controller_previous.GetComponent<uScript_Triggers>();
				if (null != component)
				{
					component.OnEnterTrigger -= Instance_OnEnterTrigger_0;
					component.OnExitTrigger -= Instance_OnExitTrigger_0;
					component.WhileInsideTrigger -= Instance_WhileInsideTrigger_0;
				}
			}
			_Person_Controller_previous = _Person_Controller;
			if (null != _Person_Controller)
			{
				uScript_Triggers uScript_Triggers2 = _Person_Controller.GetComponent<uScript_Triggers>();
				if (null == uScript_Triggers2)
				{
					uScript_Triggers2 = _Person_Controller.AddComponent<uScript_Triggers>();
				}
				if (null != uScript_Triggers2)
				{
					uScript_Triggers2.TimesToTrigger = event_UnityEngine_GameObject_TimesToTrigger_0;
				}
				uScript_Triggers uScript_Triggers3 = _Person_Controller.GetComponent<uScript_Triggers>();
				if (null == uScript_Triggers3)
				{
					uScript_Triggers3 = _Person_Controller.AddComponent<uScript_Triggers>();
				}
				if (null != uScript_Triggers3)
				{
					uScript_Triggers3.OnEnterTrigger += Instance_OnEnterTrigger_0;
					uScript_Triggers3.OnExitTrigger += Instance_OnExitTrigger_0;
					uScript_Triggers3.WhileInsideTrigger += Instance_WhileInsideTrigger_0;
				}
			}
		}
		if (local_13_UnityEngine_GameObject_previous != local_13_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_13_UnityEngine_GameObject_previous = local_13_UnityEngine_GameObject;
		}
		if (local_lift_make_UnityEngine_GameObject_previous != local_lift_make_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_make_UnityEngine_GameObject_previous = local_lift_make_UnityEngine_GameObject;
		}
		if (local_lift_active_UnityEngine_GameObject_previous != local_lift_active_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_active_UnityEngine_GameObject_previous = local_lift_active_UnityEngine_GameObject;
		}
		if (local_lift_make_your_lift_UnityEngine_GameObject_previous != local_lift_make_your_lift_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_make_your_lift_UnityEngine_GameObject_previous = local_lift_make_your_lift_UnityEngine_GameObject;
		}
		if (local_40_UnityEngine_GameObject_previous != local_40_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_40_UnityEngine_GameObject_previous = local_40_UnityEngine_GameObject;
		}
		if (local_42_UnityEngine_GameObject_previous != local_42_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_42_UnityEngine_GameObject_previous = local_42_UnityEngine_GameObject;
		}
		if (local_44_UnityEngine_GameObject_previous != local_44_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_44_UnityEngine_GameObject_previous = local_44_UnityEngine_GameObject;
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != _Person_Controller)
		{
			uScript_Triggers component = _Person_Controller.GetComponent<uScript_Triggers>();
			if (null != component)
			{
				component.OnEnterTrigger -= Instance_OnEnterTrigger_0;
				component.OnExitTrigger -= Instance_OnExitTrigger_0;
				component.WhileInsideTrigger -= Instance_WhileInsideTrigger_0;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_CompareGameObjects_uScriptCon_CompareGameObjects_1.SetParent(g);
		logic_uScriptAct_GetChildrenByName_uScriptAct_GetChildrenByName_3.SetParent(g);
		logic_uScriptAct_GetParent_uScriptAct_GetParent_4.SetParent(g);
		logic_uScriptAct_Teleport_uScriptAct_Teleport_5.SetParent(g);
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_6.SetParent(g);
		logic_uScriptAct_GetParent_uScriptAct_GetParent_8.SetParent(g);
		logic_uScriptAct_GetChildrenByName_uScriptAct_GetChildrenByName_9.SetParent(g);
		logic_uScriptAct_GetParent_uScriptAct_GetParent_15.SetParent(g);
		logic_uScriptAct_OnInputEventFilter_uScriptAct_OnInputEventFilter_17.SetParent(g);
		logic_uScriptAct_GetChildrenByName_uScriptAct_GetChildrenByName_19.SetParent(g);
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_20.SetParent(g);
		logic_uScriptAct_OnInputEventFilter_uScriptAct_OnInputEventFilter_22.SetParent(g);
		logic_uScriptAct_GetChildrenByName_uScriptAct_GetChildrenByName_25.SetParent(g);
		logic_uScriptAct_GetParent_uScriptAct_GetParent_26.SetParent(g);
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_27.SetParent(g);
		logic_uScriptAct_OnInputEventFilter_uScriptAct_OnInputEventFilter_29.SetParent(g);
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_30.SetParent(g);
		logic_uScriptAct_GetChildrenByName_uScriptAct_GetChildrenByName_31.SetParent(g);
		logic_uScriptAct_GetParent_uScriptAct_GetParent_35.SetParent(g);
		logic_uScriptAct_OnInputEventFilter_uScriptAct_OnInputEventFilter_38.SetParent(g);
		logic_uScriptAct_GetParent_uScriptAct_GetParent_39.SetParent(g);
		logic_uScriptAct_GetParent_uScriptAct_GetParent_41.SetParent(g);
		logic_uScriptAct_GetParent_uScriptAct_GetParent_43.SetParent(g);
		logic_uScriptAct_GetGameObjectName_uScriptAct_GetGameObjectName_45.SetParent(g);
		logic_uScriptCon_CompareString_uScriptCon_CompareString_46.SetParent(g);
		logic_uScriptCon_CompareString_uScriptCon_CompareString_48.SetParent(g);
		logic_uScriptAct_GetGameObjectName_uScriptAct_GetGameObjectName_50.SetParent(g);
		logic_uScriptCon_CompareString_uScriptCon_CompareString_52.SetParent(g);
		logic_uScriptAct_GetGameObjectName_uScriptAct_GetGameObjectName_53.SetParent(g);
		logic_uScriptAct_SendCustomEvent_uScriptAct_SendCustomEvent_54.SetParent(g);
		logic_uScriptAct_SendCustomEvent_uScriptAct_SendCustomEvent_55.SetParent(g);
	}

	public void Awake()
	{
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_6.Finished += uScriptAct_MoveToLocationRelative_Finished_6;
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_20.Finished += uScriptAct_MoveToLocationRelative_Finished_20;
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_27.Finished += uScriptAct_MoveToLocationRelative_Finished_27;
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_30.Finished += uScriptAct_MoveToLocationRelative_Finished_30;
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
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_6.Update();
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_20.Update();
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_27.Update();
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_30.Update();
	}

	public void OnDestroy()
	{
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_6.Finished -= uScriptAct_MoveToLocationRelative_Finished_6;
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_20.Finished -= uScriptAct_MoveToLocationRelative_Finished_20;
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_27.Finished -= uScriptAct_MoveToLocationRelative_Finished_27;
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_30.Finished -= uScriptAct_MoveToLocationRelative_Finished_30;
	}

	private void Instance_OnEnterTrigger_0(object o, uScript_Triggers.TriggerEventArgs e)
	{
		event_UnityEngine_GameObject_GameObject_0 = e.GameObject;
		Relay_OnEnterTrigger_0();
	}

	private void Instance_OnExitTrigger_0(object o, uScript_Triggers.TriggerEventArgs e)
	{
		event_UnityEngine_GameObject_GameObject_0 = e.GameObject;
		Relay_OnExitTrigger_0();
	}

	private void Instance_WhileInsideTrigger_0(object o, uScript_Triggers.TriggerEventArgs e)
	{
		event_UnityEngine_GameObject_GameObject_0 = e.GameObject;
		Relay_WhileInsideTrigger_0();
	}

	private void uScriptAct_MoveToLocationRelative_Finished_6(object o, EventArgs e)
	{
		Relay_Finished_6();
	}

	private void uScriptAct_MoveToLocationRelative_Finished_20(object o, EventArgs e)
	{
		Relay_Finished_20();
	}

	private void uScriptAct_MoveToLocationRelative_Finished_27(object o, EventArgs e)
	{
		Relay_Finished_27();
	}

	private void uScriptAct_MoveToLocationRelative_Finished_30(object o, EventArgs e)
	{
		Relay_Finished_30();
	}

	private void Relay_OnEnterTrigger_0()
	{
		local_11_UnityEngine_GameObject = event_UnityEngine_GameObject_GameObject_0;
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		Relay_In_1();
	}

	private void Relay_OnExitTrigger_0()
	{
		local_11_UnityEngine_GameObject = event_UnityEngine_GameObject_GameObject_0;
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
	}

	private void Relay_WhileInsideTrigger_0()
	{
		local_11_UnityEngine_GameObject = event_UnityEngine_GameObject_GameObject_0;
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		Relay_In_53();
		Relay_In_50();
		Relay_In_45();
	}

	private void Relay_In_1()
	{
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		logic_uScriptCon_CompareGameObjects_A_1 = local_11_UnityEngine_GameObject;
		logic_uScriptCon_CompareGameObjects_uScriptCon_CompareGameObjects_1.In(logic_uScriptCon_CompareGameObjects_A_1, logic_uScriptCon_CompareGameObjects_B_1, logic_uScriptCon_CompareGameObjects_CompareByTag_1, logic_uScriptCon_CompareGameObjects_CompareByName_1, logic_uScriptCon_CompareGameObjects_ReportNull_1);
		if (logic_uScriptCon_CompareGameObjects_uScriptCon_CompareGameObjects_1.Same)
		{
			Relay_In_4();
		}
	}

	private void Relay_In_3()
	{
		if (local_lift_make_UnityEngine_GameObject_previous != local_lift_make_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_make_UnityEngine_GameObject_previous = local_lift_make_UnityEngine_GameObject;
		}
		logic_uScriptAct_GetChildrenByName_Target_3 = local_lift_make_UnityEngine_GameObject;
		logic_uScriptAct_GetChildrenByName_uScriptAct_GetChildrenByName_3.In(logic_uScriptAct_GetChildrenByName_Target_3, logic_uScriptAct_GetChildrenByName_Name_3, logic_uScriptAct_GetChildrenByName_SearchMethod_3, logic_uScriptAct_GetChildrenByName_recursive_3, out logic_uScriptAct_GetChildrenByName_FirstChild_3, out logic_uScriptAct_GetChildrenByName_Children_3, out logic_uScriptAct_GetChildrenByName_ChildrenCount_3);
		local_13_UnityEngine_GameObject = logic_uScriptAct_GetChildrenByName_FirstChild_3;
		if (local_13_UnityEngine_GameObject_previous != local_13_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_13_UnityEngine_GameObject_previous = local_13_UnityEngine_GameObject;
		}
		if (logic_uScriptAct_GetChildrenByName_uScriptAct_GetChildrenByName_3.ChildrenFound)
		{
			Relay_In_5();
		}
	}

	private void Relay_In_4()
	{
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		logic_uScriptAct_GetParent_Target_4 = local_11_UnityEngine_GameObject;
		logic_uScriptAct_GetParent_uScriptAct_GetParent_4.In(logic_uScriptAct_GetParent_Target_4, out logic_uScriptAct_GetParent_Parent_4);
		local_etage_UnityEngine_GameObject = logic_uScriptAct_GetParent_Parent_4;
		if (local_etage_UnityEngine_GameObject_previous != local_etage_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_etage_UnityEngine_GameObject_previous = local_etage_UnityEngine_GameObject;
		}
		if (logic_uScriptAct_GetParent_uScriptAct_GetParent_4.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_In_5()
	{
		List<GameObject> list = new List<GameObject>();
		if (local_13_UnityEngine_GameObject_previous != local_13_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_13_UnityEngine_GameObject_previous = local_13_UnityEngine_GameObject;
		}
		list.Add(local_13_UnityEngine_GameObject);
		logic_uScriptAct_Teleport_Target_5 = list.ToArray();
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		logic_uScriptAct_Teleport_Destination_5 = local_11_UnityEngine_GameObject;
		logic_uScriptAct_Teleport_uScriptAct_Teleport_5.In(logic_uScriptAct_Teleport_Target_5, logic_uScriptAct_Teleport_Destination_5, logic_uScriptAct_Teleport_UpdateRotation_5);
	}

	private void Relay_Finished_6()
	{
		Relay_SendCustomEvent_55();
	}

	private void Relay_In_6()
	{
		List<GameObject> list = new List<GameObject>();
		if (local_lift_active_UnityEngine_GameObject_previous != local_lift_active_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_active_UnityEngine_GameObject_previous = local_lift_active_UnityEngine_GameObject;
		}
		list.Add(local_lift_active_UnityEngine_GameObject);
		logic_uScriptAct_MoveToLocationRelative_targetArray_6 = list.ToArray();
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		logic_uScriptAct_MoveToLocationRelative_source_6 = local_11_UnityEngine_GameObject;
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_6.In(logic_uScriptAct_MoveToLocationRelative_targetArray_6, logic_uScriptAct_MoveToLocationRelative_location_6, logic_uScriptAct_MoveToLocationRelative_source_6, logic_uScriptAct_MoveToLocationRelative_totalTime_6);
	}

	private void Relay_Cancel_6()
	{
		List<GameObject> list = new List<GameObject>();
		if (local_lift_active_UnityEngine_GameObject_previous != local_lift_active_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_active_UnityEngine_GameObject_previous = local_lift_active_UnityEngine_GameObject;
		}
		list.Add(local_lift_active_UnityEngine_GameObject);
		logic_uScriptAct_MoveToLocationRelative_targetArray_6 = list.ToArray();
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		logic_uScriptAct_MoveToLocationRelative_source_6 = local_11_UnityEngine_GameObject;
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_6.Cancel(logic_uScriptAct_MoveToLocationRelative_targetArray_6, logic_uScriptAct_MoveToLocationRelative_location_6, logic_uScriptAct_MoveToLocationRelative_source_6, logic_uScriptAct_MoveToLocationRelative_totalTime_6);
	}

	private void Relay_In_8()
	{
		if (local_40_UnityEngine_GameObject_previous != local_40_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_40_UnityEngine_GameObject_previous = local_40_UnityEngine_GameObject;
		}
		logic_uScriptAct_GetParent_Target_8 = local_40_UnityEngine_GameObject;
		logic_uScriptAct_GetParent_uScriptAct_GetParent_8.In(logic_uScriptAct_GetParent_Target_8, out logic_uScriptAct_GetParent_Parent_8);
		local_lift_make_your_lift_UnityEngine_GameObject = logic_uScriptAct_GetParent_Parent_8;
		if (local_lift_make_your_lift_UnityEngine_GameObject_previous != local_lift_make_your_lift_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_make_your_lift_UnityEngine_GameObject_previous = local_lift_make_your_lift_UnityEngine_GameObject;
		}
		if (logic_uScriptAct_GetParent_uScriptAct_GetParent_8.Out)
		{
			Relay_In_17();
			Relay_In_38();
		}
	}

	private void Relay_In_9()
	{
		if (local_lift_make_your_lift_UnityEngine_GameObject_previous != local_lift_make_your_lift_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_make_your_lift_UnityEngine_GameObject_previous = local_lift_make_your_lift_UnityEngine_GameObject;
		}
		logic_uScriptAct_GetChildrenByName_Target_9 = local_lift_make_your_lift_UnityEngine_GameObject;
		logic_uScriptAct_GetChildrenByName_uScriptAct_GetChildrenByName_9.In(logic_uScriptAct_GetChildrenByName_Target_9, logic_uScriptAct_GetChildrenByName_Name_9, logic_uScriptAct_GetChildrenByName_SearchMethod_9, logic_uScriptAct_GetChildrenByName_recursive_9, out logic_uScriptAct_GetChildrenByName_FirstChild_9, out logic_uScriptAct_GetChildrenByName_Children_9, out logic_uScriptAct_GetChildrenByName_ChildrenCount_9);
		local_lift_active_UnityEngine_GameObject = logic_uScriptAct_GetChildrenByName_FirstChild_9;
		if (local_lift_active_UnityEngine_GameObject_previous != local_lift_active_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_active_UnityEngine_GameObject_previous = local_lift_active_UnityEngine_GameObject;
		}
		if (logic_uScriptAct_GetChildrenByName_uScriptAct_GetChildrenByName_9.ChildrenFound)
		{
			Relay_In_6();
		}
	}

	private void Relay_In_15()
	{
		if (local_etage_UnityEngine_GameObject_previous != local_etage_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_etage_UnityEngine_GameObject_previous = local_etage_UnityEngine_GameObject;
		}
		logic_uScriptAct_GetParent_Target_15 = local_etage_UnityEngine_GameObject;
		logic_uScriptAct_GetParent_uScriptAct_GetParent_15.In(logic_uScriptAct_GetParent_Target_15, out logic_uScriptAct_GetParent_Parent_15);
		local_lift_make_UnityEngine_GameObject = logic_uScriptAct_GetParent_Parent_15;
		if (local_lift_make_UnityEngine_GameObject_previous != local_lift_make_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_make_UnityEngine_GameObject_previous = local_lift_make_UnityEngine_GameObject;
		}
		if (logic_uScriptAct_GetParent_uScriptAct_GetParent_15.Out)
		{
			Relay_In_3();
		}
	}

	private void Relay_In_17()
	{
		logic_uScriptAct_OnInputEventFilter_KeyCode_17 = BOTTOM;
		logic_uScriptAct_OnInputEventFilter_uScriptAct_OnInputEventFilter_17.In(logic_uScriptAct_OnInputEventFilter_KeyCode_17);
		if (logic_uScriptAct_OnInputEventFilter_uScriptAct_OnInputEventFilter_17.KeyDown)
		{
			Relay_In_19();
			Relay_SendCustomEvent_54();
		}
	}

	private void Relay_In_19()
	{
		if (local_lift_make_your_lift_UnityEngine_GameObject_previous != local_lift_make_your_lift_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_make_your_lift_UnityEngine_GameObject_previous = local_lift_make_your_lift_UnityEngine_GameObject;
		}
		logic_uScriptAct_GetChildrenByName_Target_19 = local_lift_make_your_lift_UnityEngine_GameObject;
		logic_uScriptAct_GetChildrenByName_uScriptAct_GetChildrenByName_19.In(logic_uScriptAct_GetChildrenByName_Target_19, logic_uScriptAct_GetChildrenByName_Name_19, logic_uScriptAct_GetChildrenByName_SearchMethod_19, logic_uScriptAct_GetChildrenByName_recursive_19, out logic_uScriptAct_GetChildrenByName_FirstChild_19, out logic_uScriptAct_GetChildrenByName_Children_19, out logic_uScriptAct_GetChildrenByName_ChildrenCount_19);
		local_lift_active_UnityEngine_GameObject = logic_uScriptAct_GetChildrenByName_FirstChild_19;
		if (local_lift_active_UnityEngine_GameObject_previous != local_lift_active_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_active_UnityEngine_GameObject_previous = local_lift_active_UnityEngine_GameObject;
		}
		if (logic_uScriptAct_GetChildrenByName_uScriptAct_GetChildrenByName_19.ChildrenFound)
		{
			Relay_In_20();
		}
	}

	private void Relay_Finished_20()
	{
		Relay_SendCustomEvent_55();
	}

	private void Relay_In_20()
	{
		List<GameObject> list = new List<GameObject>();
		if (local_lift_active_UnityEngine_GameObject_previous != local_lift_active_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_active_UnityEngine_GameObject_previous = local_lift_active_UnityEngine_GameObject;
		}
		list.Add(local_lift_active_UnityEngine_GameObject);
		logic_uScriptAct_MoveToLocationRelative_targetArray_20 = list.ToArray();
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		logic_uScriptAct_MoveToLocationRelative_source_20 = local_11_UnityEngine_GameObject;
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_20.In(logic_uScriptAct_MoveToLocationRelative_targetArray_20, logic_uScriptAct_MoveToLocationRelative_location_20, logic_uScriptAct_MoveToLocationRelative_source_20, logic_uScriptAct_MoveToLocationRelative_totalTime_20);
	}

	private void Relay_Cancel_20()
	{
		List<GameObject> list = new List<GameObject>();
		if (local_lift_active_UnityEngine_GameObject_previous != local_lift_active_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_active_UnityEngine_GameObject_previous = local_lift_active_UnityEngine_GameObject;
		}
		list.Add(local_lift_active_UnityEngine_GameObject);
		logic_uScriptAct_MoveToLocationRelative_targetArray_20 = list.ToArray();
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		logic_uScriptAct_MoveToLocationRelative_source_20 = local_11_UnityEngine_GameObject;
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_20.Cancel(logic_uScriptAct_MoveToLocationRelative_targetArray_20, logic_uScriptAct_MoveToLocationRelative_location_20, logic_uScriptAct_MoveToLocationRelative_source_20, logic_uScriptAct_MoveToLocationRelative_totalTime_20);
	}

	private void Relay_In_22()
	{
		logic_uScriptAct_OnInputEventFilter_KeyCode_22 = TOP;
		logic_uScriptAct_OnInputEventFilter_uScriptAct_OnInputEventFilter_22.In(logic_uScriptAct_OnInputEventFilter_KeyCode_22);
		if (logic_uScriptAct_OnInputEventFilter_uScriptAct_OnInputEventFilter_22.KeyDown)
		{
			Relay_In_25();
			Relay_SendCustomEvent_54();
		}
	}

	private void Relay_In_25()
	{
		if (local_lift_make_your_lift_UnityEngine_GameObject_previous != local_lift_make_your_lift_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_make_your_lift_UnityEngine_GameObject_previous = local_lift_make_your_lift_UnityEngine_GameObject;
		}
		logic_uScriptAct_GetChildrenByName_Target_25 = local_lift_make_your_lift_UnityEngine_GameObject;
		logic_uScriptAct_GetChildrenByName_uScriptAct_GetChildrenByName_25.In(logic_uScriptAct_GetChildrenByName_Target_25, logic_uScriptAct_GetChildrenByName_Name_25, logic_uScriptAct_GetChildrenByName_SearchMethod_25, logic_uScriptAct_GetChildrenByName_recursive_25, out logic_uScriptAct_GetChildrenByName_FirstChild_25, out logic_uScriptAct_GetChildrenByName_Children_25, out logic_uScriptAct_GetChildrenByName_ChildrenCount_25);
		local_lift_active_UnityEngine_GameObject = logic_uScriptAct_GetChildrenByName_FirstChild_25;
		if (local_lift_active_UnityEngine_GameObject_previous != local_lift_active_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_active_UnityEngine_GameObject_previous = local_lift_active_UnityEngine_GameObject;
		}
		if (logic_uScriptAct_GetChildrenByName_uScriptAct_GetChildrenByName_25.ChildrenFound)
		{
			Relay_In_27();
		}
	}

	private void Relay_In_26()
	{
		if (local_42_UnityEngine_GameObject_previous != local_42_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_42_UnityEngine_GameObject_previous = local_42_UnityEngine_GameObject;
		}
		logic_uScriptAct_GetParent_Target_26 = local_42_UnityEngine_GameObject;
		logic_uScriptAct_GetParent_uScriptAct_GetParent_26.In(logic_uScriptAct_GetParent_Target_26, out logic_uScriptAct_GetParent_Parent_26);
		local_lift_make_your_lift_UnityEngine_GameObject = logic_uScriptAct_GetParent_Parent_26;
		if (local_lift_make_your_lift_UnityEngine_GameObject_previous != local_lift_make_your_lift_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_make_your_lift_UnityEngine_GameObject_previous = local_lift_make_your_lift_UnityEngine_GameObject;
		}
		if (logic_uScriptAct_GetParent_uScriptAct_GetParent_26.Out)
		{
			Relay_In_22();
		}
	}

	private void Relay_Finished_27()
	{
		Relay_SendCustomEvent_55();
	}

	private void Relay_In_27()
	{
		List<GameObject> list = new List<GameObject>();
		if (local_lift_active_UnityEngine_GameObject_previous != local_lift_active_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_active_UnityEngine_GameObject_previous = local_lift_active_UnityEngine_GameObject;
		}
		list.Add(local_lift_active_UnityEngine_GameObject);
		logic_uScriptAct_MoveToLocationRelative_targetArray_27 = list.ToArray();
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		logic_uScriptAct_MoveToLocationRelative_source_27 = local_11_UnityEngine_GameObject;
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_27.In(logic_uScriptAct_MoveToLocationRelative_targetArray_27, logic_uScriptAct_MoveToLocationRelative_location_27, logic_uScriptAct_MoveToLocationRelative_source_27, logic_uScriptAct_MoveToLocationRelative_totalTime_27);
	}

	private void Relay_Cancel_27()
	{
		List<GameObject> list = new List<GameObject>();
		if (local_lift_active_UnityEngine_GameObject_previous != local_lift_active_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_active_UnityEngine_GameObject_previous = local_lift_active_UnityEngine_GameObject;
		}
		list.Add(local_lift_active_UnityEngine_GameObject);
		logic_uScriptAct_MoveToLocationRelative_targetArray_27 = list.ToArray();
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		logic_uScriptAct_MoveToLocationRelative_source_27 = local_11_UnityEngine_GameObject;
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_27.Cancel(logic_uScriptAct_MoveToLocationRelative_targetArray_27, logic_uScriptAct_MoveToLocationRelative_location_27, logic_uScriptAct_MoveToLocationRelative_source_27, logic_uScriptAct_MoveToLocationRelative_totalTime_27);
	}

	private void Relay_In_29()
	{
		logic_uScriptAct_OnInputEventFilter_KeyCode_29 = BOTTOM;
		logic_uScriptAct_OnInputEventFilter_uScriptAct_OnInputEventFilter_29.In(logic_uScriptAct_OnInputEventFilter_KeyCode_29);
		if (logic_uScriptAct_OnInputEventFilter_uScriptAct_OnInputEventFilter_29.KeyDown)
		{
			Relay_In_31();
			Relay_SendCustomEvent_54();
		}
	}

	private void Relay_Finished_30()
	{
		Relay_SendCustomEvent_55();
	}

	private void Relay_In_30()
	{
		List<GameObject> list = new List<GameObject>();
		if (local_lift_active_UnityEngine_GameObject_previous != local_lift_active_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_active_UnityEngine_GameObject_previous = local_lift_active_UnityEngine_GameObject;
		}
		list.Add(local_lift_active_UnityEngine_GameObject);
		logic_uScriptAct_MoveToLocationRelative_targetArray_30 = list.ToArray();
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		logic_uScriptAct_MoveToLocationRelative_source_30 = local_11_UnityEngine_GameObject;
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_30.In(logic_uScriptAct_MoveToLocationRelative_targetArray_30, logic_uScriptAct_MoveToLocationRelative_location_30, logic_uScriptAct_MoveToLocationRelative_source_30, logic_uScriptAct_MoveToLocationRelative_totalTime_30);
	}

	private void Relay_Cancel_30()
	{
		List<GameObject> list = new List<GameObject>();
		if (local_lift_active_UnityEngine_GameObject_previous != local_lift_active_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_active_UnityEngine_GameObject_previous = local_lift_active_UnityEngine_GameObject;
		}
		list.Add(local_lift_active_UnityEngine_GameObject);
		logic_uScriptAct_MoveToLocationRelative_targetArray_30 = list.ToArray();
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		logic_uScriptAct_MoveToLocationRelative_source_30 = local_11_UnityEngine_GameObject;
		logic_uScriptAct_MoveToLocationRelative_uScriptAct_MoveToLocationRelative_30.Cancel(logic_uScriptAct_MoveToLocationRelative_targetArray_30, logic_uScriptAct_MoveToLocationRelative_location_30, logic_uScriptAct_MoveToLocationRelative_source_30, logic_uScriptAct_MoveToLocationRelative_totalTime_30);
	}

	private void Relay_In_31()
	{
		if (local_lift_make_your_lift_UnityEngine_GameObject_previous != local_lift_make_your_lift_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_make_your_lift_UnityEngine_GameObject_previous = local_lift_make_your_lift_UnityEngine_GameObject;
		}
		logic_uScriptAct_GetChildrenByName_Target_31 = local_lift_make_your_lift_UnityEngine_GameObject;
		logic_uScriptAct_GetChildrenByName_uScriptAct_GetChildrenByName_31.In(logic_uScriptAct_GetChildrenByName_Target_31, logic_uScriptAct_GetChildrenByName_Name_31, logic_uScriptAct_GetChildrenByName_SearchMethod_31, logic_uScriptAct_GetChildrenByName_recursive_31, out logic_uScriptAct_GetChildrenByName_FirstChild_31, out logic_uScriptAct_GetChildrenByName_Children_31, out logic_uScriptAct_GetChildrenByName_ChildrenCount_31);
		local_lift_active_UnityEngine_GameObject = logic_uScriptAct_GetChildrenByName_FirstChild_31;
		if (local_lift_active_UnityEngine_GameObject_previous != local_lift_active_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_active_UnityEngine_GameObject_previous = local_lift_active_UnityEngine_GameObject;
		}
		if (logic_uScriptAct_GetChildrenByName_uScriptAct_GetChildrenByName_31.ChildrenFound)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_35()
	{
		if (local_44_UnityEngine_GameObject_previous != local_44_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_44_UnityEngine_GameObject_previous = local_44_UnityEngine_GameObject;
		}
		logic_uScriptAct_GetParent_Target_35 = local_44_UnityEngine_GameObject;
		logic_uScriptAct_GetParent_uScriptAct_GetParent_35.In(logic_uScriptAct_GetParent_Target_35, out logic_uScriptAct_GetParent_Parent_35);
		local_lift_make_your_lift_UnityEngine_GameObject = logic_uScriptAct_GetParent_Parent_35;
		if (local_lift_make_your_lift_UnityEngine_GameObject_previous != local_lift_make_your_lift_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_lift_make_your_lift_UnityEngine_GameObject_previous = local_lift_make_your_lift_UnityEngine_GameObject;
		}
		if (logic_uScriptAct_GetParent_uScriptAct_GetParent_35.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_In_38()
	{
		logic_uScriptAct_OnInputEventFilter_KeyCode_38 = TOP;
		logic_uScriptAct_OnInputEventFilter_uScriptAct_OnInputEventFilter_38.In(logic_uScriptAct_OnInputEventFilter_KeyCode_38);
		if (logic_uScriptAct_OnInputEventFilter_uScriptAct_OnInputEventFilter_38.KeyDown)
		{
			Relay_In_9();
			Relay_SendCustomEvent_54();
		}
	}

	private void Relay_In_39()
	{
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		logic_uScriptAct_GetParent_Target_39 = local_11_UnityEngine_GameObject;
		logic_uScriptAct_GetParent_uScriptAct_GetParent_39.In(logic_uScriptAct_GetParent_Target_39, out logic_uScriptAct_GetParent_Parent_39);
		local_40_UnityEngine_GameObject = logic_uScriptAct_GetParent_Parent_39;
		if (local_40_UnityEngine_GameObject_previous != local_40_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_40_UnityEngine_GameObject_previous = local_40_UnityEngine_GameObject;
		}
		if (logic_uScriptAct_GetParent_uScriptAct_GetParent_39.Out)
		{
			Relay_In_8();
		}
	}

	private void Relay_In_41()
	{
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		logic_uScriptAct_GetParent_Target_41 = local_11_UnityEngine_GameObject;
		logic_uScriptAct_GetParent_uScriptAct_GetParent_41.In(logic_uScriptAct_GetParent_Target_41, out logic_uScriptAct_GetParent_Parent_41);
		local_42_UnityEngine_GameObject = logic_uScriptAct_GetParent_Parent_41;
		if (local_42_UnityEngine_GameObject_previous != local_42_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_42_UnityEngine_GameObject_previous = local_42_UnityEngine_GameObject;
		}
		if (logic_uScriptAct_GetParent_uScriptAct_GetParent_41.Out)
		{
			Relay_In_26();
		}
	}

	private void Relay_In_43()
	{
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		logic_uScriptAct_GetParent_Target_43 = local_11_UnityEngine_GameObject;
		logic_uScriptAct_GetParent_uScriptAct_GetParent_43.In(logic_uScriptAct_GetParent_Target_43, out logic_uScriptAct_GetParent_Parent_43);
		local_44_UnityEngine_GameObject = logic_uScriptAct_GetParent_Parent_43;
		if (local_44_UnityEngine_GameObject_previous != local_44_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_44_UnityEngine_GameObject_previous = local_44_UnityEngine_GameObject;
		}
		if (logic_uScriptAct_GetParent_uScriptAct_GetParent_43.Out)
		{
			Relay_In_35();
		}
	}

	private void Relay_In_45()
	{
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		logic_uScriptAct_GetGameObjectName_gameObject_45 = local_11_UnityEngine_GameObject;
		logic_uScriptAct_GetGameObjectName_uScriptAct_GetGameObjectName_45.In(logic_uScriptAct_GetGameObjectName_gameObject_45, out logic_uScriptAct_GetGameObjectName_name_45);
		local_47_System_String = logic_uScriptAct_GetGameObjectName_name_45;
		if (logic_uScriptAct_GetGameObjectName_uScriptAct_GetGameObjectName_45.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_In_46()
	{
		logic_uScriptCon_CompareString_A_46 = local_47_System_String;
		logic_uScriptCon_CompareString_uScriptCon_CompareString_46.In(logic_uScriptCon_CompareString_A_46, logic_uScriptCon_CompareString_B_46);
		if (logic_uScriptCon_CompareString_uScriptCon_CompareString_46.Same)
		{
			Relay_In_43();
		}
	}

	private void Relay_In_48()
	{
		logic_uScriptCon_CompareString_A_48 = local_49_System_String;
		logic_uScriptCon_CompareString_uScriptCon_CompareString_48.In(logic_uScriptCon_CompareString_A_48, logic_uScriptCon_CompareString_B_48);
		if (logic_uScriptCon_CompareString_uScriptCon_CompareString_48.Same)
		{
			Relay_In_41();
		}
	}

	private void Relay_In_50()
	{
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		logic_uScriptAct_GetGameObjectName_gameObject_50 = local_11_UnityEngine_GameObject;
		logic_uScriptAct_GetGameObjectName_uScriptAct_GetGameObjectName_50.In(logic_uScriptAct_GetGameObjectName_gameObject_50, out logic_uScriptAct_GetGameObjectName_name_50);
		local_49_System_String = logic_uScriptAct_GetGameObjectName_name_50;
		if (logic_uScriptAct_GetGameObjectName_uScriptAct_GetGameObjectName_50.Out)
		{
			Relay_In_48();
		}
	}

	private void Relay_In_52()
	{
		logic_uScriptCon_CompareString_A_52 = local_51_System_String;
		logic_uScriptCon_CompareString_uScriptCon_CompareString_52.In(logic_uScriptCon_CompareString_A_52, logic_uScriptCon_CompareString_B_52);
		if (logic_uScriptCon_CompareString_uScriptCon_CompareString_52.Same)
		{
			Relay_In_39();
		}
	}

	private void Relay_In_53()
	{
		if (local_11_UnityEngine_GameObject_previous != local_11_UnityEngine_GameObject || !m_RegisteredForEvents)
		{
			local_11_UnityEngine_GameObject_previous = local_11_UnityEngine_GameObject;
		}
		logic_uScriptAct_GetGameObjectName_gameObject_53 = local_11_UnityEngine_GameObject;
		logic_uScriptAct_GetGameObjectName_uScriptAct_GetGameObjectName_53.In(logic_uScriptAct_GetGameObjectName_gameObject_53, out logic_uScriptAct_GetGameObjectName_name_53);
		local_51_System_String = logic_uScriptAct_GetGameObjectName_name_53;
		if (logic_uScriptAct_GetGameObjectName_uScriptAct_GetGameObjectName_53.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_SendCustomEvent_54()
	{
		logic_uScriptAct_SendCustomEvent_uScriptAct_SendCustomEvent_54.SendCustomEvent(logic_uScriptAct_SendCustomEvent_EventName_54, logic_uScriptAct_SendCustomEvent_sendGroup_54, logic_uScriptAct_SendCustomEvent_EventSender_54);
	}

	private void Relay_SendCustomEvent_55()
	{
		logic_uScriptAct_SendCustomEvent_uScriptAct_SendCustomEvent_55.SendCustomEvent(logic_uScriptAct_SendCustomEvent_EventName_55, logic_uScriptAct_SendCustomEvent_sendGroup_55, logic_uScriptAct_SendCustomEvent_EventSender_55);
	}
}
