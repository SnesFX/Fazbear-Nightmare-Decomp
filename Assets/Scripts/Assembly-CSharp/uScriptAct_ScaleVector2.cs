using UnityEngine;

[NodeToolTip("Scales a Vector2.")]
[NodePath("Actions/Math/Vectors")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Scale_Vector2")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Scale Vector2", "Scales a Vector2")]
public class uScriptAct_ScaleVector2 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Vector", "Vector to scale.")] Vector2 v, [FriendlyName("Scale", "Amount to scale Vector by.")] float s, [FriendlyName("Vector Result", "Scaled vector.")] out Vector2 result)
	{
		result = v * s;
	}
}
