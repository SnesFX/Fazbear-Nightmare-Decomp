using UnityEngine;

[FriendlyName("Set Joint Drive", "Sets the components of a JointDrive structure.")]
[NodePath("Actions/Physics")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Sets the components of a JointDrive structure.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Joint_Drive")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_SetJointDrive : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Mode", "The JointDriveMode.")] JointDriveMode mode, [FriendlyName("Position Spring", "The JointDrive PositionSpring.")] float positionSpring, [FriendlyName("Position Damper", "The PositionDamper of the JointDrive.")] float positionDamper, [FriendlyName("Maximum Force", "The MaximumForce of the JointDrive.")] float maximumForce, [FriendlyName("Joint Drive", "The newly created JointDrive.")] out JointDrive jointDrive)
	{
		JointDrive jointDrive2 = default(JointDrive);
		jointDrive2.mode = mode;
		jointDrive2.positionSpring = positionSpring;
		jointDrive2.positionDamper = positionDamper;
		jointDrive2.maximumForce = maximumForce;
		jointDrive = jointDrive2;
	}
}
