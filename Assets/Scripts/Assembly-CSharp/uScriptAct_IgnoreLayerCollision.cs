using UnityEngine;

[NodePath("Actions/Physics")]
[FriendlyName("Ignore Layer Collision", "Tells the collision detection system ignore all collisions between any GameObject in Layer A and any GameObject in Layer B.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Ignore_Layer_Collision")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Tells the collision detection system ignore all collisions between any GameObject in Layer A and any GameObject in Layer B.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_IgnoreLayerCollision : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Layer A", "The first layer.")] LayerMask LayerA, [FriendlyName("Layer B", "The second layer.")] LayerMask LayerB, [SocketState(false, false)][DefaultValue(true)][FriendlyName("Ignore", "True = Ignore collisions between the layers, False = Enable collisions between the layers.")] bool Ignore)
	{
		if ((int)LayerA != (int)LayerB)
		{
			Physics.IgnoreLayerCollision(1 << (int)LayerA, 1 << (int)LayerB, Ignore);
		}
	}
}
