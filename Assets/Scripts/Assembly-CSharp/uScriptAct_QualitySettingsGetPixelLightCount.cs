using UnityEngine;

[FriendlyName("Get Pixel Light Count", "Gets the Pixel Light Count from the current Quality Settings.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Application/Quality Settings")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Gets the Pixel Light Count from the current Quality Settings.")]
public class uScriptAct_QualitySettingsGetPixelLightCount : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The current value for this quality setting level.")] out int Value)
	{
		Value = QualitySettings.pixelLightCount;
	}
}
