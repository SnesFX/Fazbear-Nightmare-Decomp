using UnityEngine;

[NodeToolTip("Mirrors the X and Y of a Vector2.")]
[FriendlyName("Invert Vector2", "Returns the inverse value of a Vector2 variable. Individual components can optionally be ignored by this operation.\n\nExample:\n\t[x, y] -> [-x, -y]")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Math/Vectors")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Invert_Vector2")]
public class uScriptAct_InvertVector2 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "Value to invert.")] Vector2 Target, [FriendlyName("Ignore X", "If True, the X component will be ignored.")][SocketState(false, false)] bool IgnoreX, [SocketState(false, false)][FriendlyName("Ignore Y", "If True, the Y component will be ignored.")] bool IgnoreY, [FriendlyName("Value", "The inverted value.")] out Vector2 Value)
	{
		Value = new Vector2(Target.x, Target.y);
		if (!IgnoreX)
		{
			Value.x = 0f - Value.x;
		}
		if (!IgnoreY)
		{
			Value.y = 0f - Value.y;
		}
	}
}
