using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/GameObjects")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_Scale")]
[NodeToolTip("Gets the scale of a GameObject as both a Vector3 and X, Y and Z float variables.")]
[FriendlyName("Get Scale", "Gets the scale of a GameObject as both a Vector3 and X, Y and Z float variables.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_GetGameObjectScale : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The Target GameObject you wish to get the scale of.")] GameObject Target, [FriendlyName("Scale", "Returns the scale as a Vector3(X, Y, Z).")] out Vector3 Scale, [FriendlyName("X", "Returns the X axis scale as a float.")] out float X, [FriendlyName("Y", "Returns the Y axis scale as a float.")] out float Y, [FriendlyName("Z", "Returns the Z axis scale as a float.")] out float Z)
	{
		if (Target != null)
		{
			Scale = Target.transform.localScale;
			X = Target.transform.localScale.x;
			Y = Target.transform.localScale.y;
			Z = Target.transform.localScale.z;
		}
		else
		{
			Scale = Vector3.zero;
			X = 0f;
			Y = 0f;
			Z = 0f;
		}
	}
}
