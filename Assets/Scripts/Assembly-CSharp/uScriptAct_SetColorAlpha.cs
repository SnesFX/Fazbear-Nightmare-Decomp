using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Color")]
[FriendlyName("Set Color Alpha", "Takes an existing Color variable and applies the specified Alpha value.  The results are returned to the Target variable.")]
[NodePath("Actions/Variables/Color")]
[NodeToolTip("Takes the red, green and blue values of the Value color variable and combines them with the specified alpha value, outputing the results to the target color variable.")]
public class uScriptAct_SetColorAlpha : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The color variable you wish to use as the source for the Target's Red, Green, and Blue color channels.")] Color Value, [FriendlyName("Alpha", "The new Alpha channel value.")] float Alpha, [SocketState(false, false)][FriendlyName("Use 8-bit Range", "If True, the Alpha channel will use a traditional 0-255 value range for specifying the alpha channel, otherwise it will use the normalized 0.0 to 1.0 value range.")] bool Use8bitRange, [FriendlyName("Target", "The Target variable you wish to set.")] out Color TargetColor)
	{
		if (Use8bitRange)
		{
			if (Alpha < 0f)
			{
				Alpha = 0f;
			}
			if (Alpha > 255f)
			{
				Alpha = 255f;
			}
			TargetColor = new Color(Value.r, Value.g, Value.b, Alpha / 255f);
		}
		else
		{
			if (Alpha < 0f)
			{
				Alpha = 0f;
			}
			if (Alpha > 1f)
			{
				Alpha = 1f;
			}
			TargetColor = new Color(Value.r, Value.g, Value.b, Alpha);
		}
	}
}
