using UnityEngine;

[NodePath("Actions/Math/LayerMasks")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Removes LayerMasks from a layer mask combination and returns the result.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Remove_LayerMasks")]
[FriendlyName("Remove LayerMasks", "Removes multiple LayerMasks.\n\n[ A & ~B ]")]
public class uScriptAct_RemoveLayerMasks : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Existing Masks", "The existing layer masks.")] LayerMask A, [FriendlyName("Masks to Remove", "The masks you want removed from the Existing Mask.")] LayerMask B, [FriendlyName("Result", "The LayerMask result of the operation.")] out LayerMask LayerMask)
	{
		LayerMask = (int)A & ~(int)B;
	}
}
