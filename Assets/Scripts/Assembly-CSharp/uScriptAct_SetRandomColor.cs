using UnityEngine;

[NodePath("Actions/Variables/Color")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Random_Color")]
[FriendlyName("Set Random Color", "Randomly sets the value of a Color variable.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Randomly sets the value of a Color variable.")]
public class uScriptAct_SetRandomColor : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Red Min", "Minimum allowable Red component value.")][SocketState(false, false)] float RedMin, [FriendlyName("Red Max", "Maximum allowable Red component value.")][DefaultValue(1f)][SocketState(false, false)] float RedMax, [SocketState(false, false)][FriendlyName("Green Min", "Minimum allowable Green component value.")] float GreenMin, [DefaultValue(1f)][SocketState(false, false)][FriendlyName("Green Max", "Maximum allowable Green component value.")] float GreenMax, [FriendlyName("Blue Min", "Minimum allowable Blue component value.")][SocketState(false, false)] float BlueMin, [FriendlyName("Blue Max", "Maximum allowable Blue component value.")][SocketState(false, false)][DefaultValue(1f)] float BlueMax, [DefaultValue(1f)][SocketState(false, false)][FriendlyName("Alpha Min", "Minimum allowable Alpha component value.")] float AlphaMin, [DefaultValue(1f)][FriendlyName("Alpha Max", "Maximum allowable Alpha component value.")][SocketState(false, false)] float AlphaMax, [FriendlyName("Target Color", "The random color that has been set.")] out Color TargetColor)
	{
		if (RedMin < 0f)
		{
			RedMin = 0f;
		}
		if (RedMax > 1f)
		{
			RedMax = 1f;
		}
		if (GreenMin < 0f)
		{
			GreenMin = 0f;
		}
		if (GreenMax > 1f)
		{
			GreenMax = 1f;
		}
		if (BlueMin < 0f)
		{
			BlueMin = 0f;
		}
		if (BlueMax > 1f)
		{
			BlueMax = 1f;
		}
		if (AlphaMin < 0f)
		{
			AlphaMin = 0f;
		}
		if (AlphaMax > 1f)
		{
			AlphaMax = 1f;
		}
		float r = ReturnRandomFloat(RedMin, RedMax);
		float g = ReturnRandomFloat(GreenMin, GreenMax);
		float b = ReturnRandomFloat(BlueMin, BlueMax);
		float a = ReturnRandomFloat(AlphaMin, AlphaMax);
		TargetColor = new Color(r, g, b, a);
	}

	private float ReturnRandomFloat(float min, float max)
	{
		if (min > max)
		{
			min = max;
		}
		return Random.Range(min, max);
	}
}
