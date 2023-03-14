using UnityEngine;

[FriendlyName("Set Pixel Light Count", "Sets the Pixel Light Count on the current Quality Settings.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Application/Quality Settings")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Sets the Pixel Light Count on the current Quality Settings.")]
public class uScriptAct_QualitySettingsSetPixelLightCount : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The value to set on this quality setting.")] int Value)
	{
		QualitySettings.pixelLightCount = Value;
	}
}
