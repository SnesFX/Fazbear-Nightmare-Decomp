using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Add_Torque")]
[NodeToolTip("Applies an Add Torque to the specified GameObject.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Physics")]
[FriendlyName("Add Torque", "Applies an Add Torque to the specified GameObject. Target must have a Rigid Body Component in order to recieve a force.")]
public class uScriptAct_AddTorque : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "GameObject to apply the force to.")] GameObject Target, [FriendlyName("Force", "The force to apply to the Target. The force is a Vector3, so it defines both the direction and magnitude of the force.")] Vector3 Force, [SocketState(false, false)][FriendlyName("Scale", "A scale to multiply to the force (force x scale).")][DefaultValue(0f)] float Scale, [SocketState(false, false)][FriendlyName("Use ForceMode", "The force being applied will use the object's mass.")] bool UseForceMode, [FriendlyName("ForceMode Type", "Specifies the ForceMode to use if Use ForceMode is set to true.")][SocketState(false, false)] ForceMode ForceModeType)
	{
		if (null != Target.GetComponent<Rigidbody>())
		{
			if (Scale != 0f)
			{
				Force *= Scale;
			}
			if (UseForceMode)
			{
				Target.GetComponent<Rigidbody>().AddTorque(Force, ForceModeType);
			}
			else
			{
				Target.GetComponent<Rigidbody>().AddTorque(Force);
			}
		}
		else
		{
			uScriptDebug.Log("(Node - Add Torque) The specified Target GameObject does not have a Rigid Body Component, so no force could be added.", uScriptDebug.Type.Warning);
		}
	}
}
