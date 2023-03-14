using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Get Shadow Distance", "Gets the Shadow Distance from the current Quality Settings.")]
[NodeToolTip("Gets the Shadow Distance from the current Quality Settings.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Application/Quality Settings")]
public class uScriptAct_QualitySettingsGetShadowDistance : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The current value for this quality setting level.")] out float Value)
	{
		Value = QualitySettings.shadowDistance;
	}
}
