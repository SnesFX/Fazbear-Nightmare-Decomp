using UnityEngine;

[NodeToolTip("Combines LayerMask variables together and returns the result.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Combine_LayerMasks")]
[FriendlyName("Combine LayerMasks", "Combines multiple LayerMasks.\n\n[ A | B ]")]
[NodePath("Actions/Math/LayerMasks")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_CombineLayerMasks : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "The first layer mask.")] LayerMask A, [FriendlyName("B", "The second layer mask.")] LayerMask B, [FriendlyName("Result", "The LayerMask result of the operation.")] out LayerMask LayerMask)
	{
		LayerMask = (int)A | (int)B;
	}
}
