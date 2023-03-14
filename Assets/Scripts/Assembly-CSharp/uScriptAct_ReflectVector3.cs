using UnityEngine;

[FriendlyName("Reflect Vector3", "Reflects the vector along the normal. The returned value is the reflected direction from a surface with a normal.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Math/Vectors")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Reflects the vector along the normal.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
public class uScriptAct_ReflectVector3 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The Vector3 to be reflected.")] Vector3 Target, [FriendlyName("Normal", "The Vector3 normal used for the reflection.")] Vector3 ReflectedNormal, [FriendlyName("Result", "The resulting reflected Vector3 normal.")] out Vector3 Result)
	{
		Result = Vector3.Reflect(Target, ReflectedNormal);
	}
}
