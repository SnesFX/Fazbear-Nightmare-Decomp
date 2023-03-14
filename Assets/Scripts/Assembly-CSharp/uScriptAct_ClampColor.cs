using UnityEngine;

[FriendlyName("Clamp Color", "Clamps Color variable components between minimun and maximum values.\n\nValues must be within the normalized color range of 0 and 1. Values outside this range are themselves clamped.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Math/Color")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Clamps a Color variable between a min and a max value for the desired components and returns the resulting Color.")]
public class uScriptAct_ClampColor : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The Color to be clamped.")] Color Target, [FriendlyName("Clamp Red", "If True, the Red component will be clamped.")][SocketState(false, false)] bool ClampX, [FriendlyName("Red Min", "The minimun value allowed for the Red component.")][SocketState(false, false)] float XMin, [FriendlyName("Red Max", "The maximum value allowed for the Red component.")][SocketState(false, false)] float XMax, [FriendlyName("Clamp Green", "If True, the Green component will be clamped.")][SocketState(false, false)] bool ClampY, [SocketState(false, false)][FriendlyName("Green Min", "The minimun value allowed for the Green component.")] float YMin, [SocketState(false, false)][FriendlyName("Green Max", "The maximum value allowed for the Green component.")] float YMax, [FriendlyName("Clamp Blue", "If True, the Blue component will be clamped.")][SocketState(false, false)] bool ClampZ, [FriendlyName("Blue Min", "The minimun value allowed for the Blue component.")][SocketState(false, false)] float ZMin, [FriendlyName("Blue Max", "The maximum value allowed for the Blue component.")][SocketState(false, false)] float ZMax, [FriendlyName("Clamp Alpha", "If True, the Alpha component will be clamped.")][SocketState(false, false)] bool ClampW, [FriendlyName("Alpha Min", "The minimun value allowed for the Alpha component.")][SocketState(false, false)] float WMin, [SocketState(false, false)][FriendlyName("Alpha Max", "The maximum value allowed for the Alpha component.")] float WMax, [FriendlyName("Result", "The clamped Color variable.")] out Color Result)
	{
		if (ClampX)
		{
			if (XMin < 0f)
			{
				XMin = 0f;
			}
			if (XMin > 1f)
			{
				XMin = 1f;
			}
			if (XMax < 0f)
			{
				XMax = 0f;
			}
			if (XMax > 1f)
			{
				XMax = 1f;
			}
			Target.r = Mathf.Clamp(Target.r, XMin, XMax);
		}
		if (ClampY)
		{
			if (YMin < 0f)
			{
				YMin = 0f;
			}
			if (YMin > 1f)
			{
				YMin = 1f;
			}
			if (YMax < 0f)
			{
				YMax = 0f;
			}
			if (YMax > 1f)
			{
				YMax = 1f;
			}
			Target.g = Mathf.Clamp(Target.g, YMin, YMax);
		}
		if (ClampZ)
		{
			if (ZMin < 0f)
			{
				ZMin = 0f;
			}
			if (ZMin > 1f)
			{
				ZMin = 1f;
			}
			if (ZMax < 0f)
			{
				ZMax = 0f;
			}
			if (ZMax > 1f)
			{
				ZMax = 1f;
			}
			Target.b = Mathf.Clamp(Target.b, ZMin, ZMax);
		}
		if (ClampW)
		{
			if (WMin < 0f)
			{
				WMin = 0f;
			}
			if (WMin > 1f)
			{
				WMin = 1f;
			}
			if (WMax < 0f)
			{
				WMax = 0f;
			}
			if (WMax > 1f)
			{
				WMax = 1f;
			}
			Target.a = Mathf.Clamp(Target.a, WMin, WMax);
		}
		Result = Target;
	}
}
