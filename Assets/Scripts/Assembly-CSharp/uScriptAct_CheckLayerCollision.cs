using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Check Layer Collision", "Returns True or False if collisions are being ignored between Layer A and Layer B.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Check_Layer_Collision")]
[NodeToolTip("Returns True or False if collisions are being ignored between Layer A and Layer B.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Physics")]
public class uScriptAct_CheckLayerCollision : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Layer A", "The first layer.")] LayerMask LayerA, [FriendlyName("Layer B", "The second layer.")] LayerMask LayerB, [FriendlyName("Result", "True = collisions are being ignored between the layers, False = collisions are NOT being ignored between the layers.")] out bool Result)
	{
		if ((int)LayerA != (int)LayerB)
		{
			Result = Physics.GetIgnoreLayerCollision(1 >> (int)LayerA, 1 >> (int)LayerB);
		}
		else
		{
			Result = false;
		}
	}
}
