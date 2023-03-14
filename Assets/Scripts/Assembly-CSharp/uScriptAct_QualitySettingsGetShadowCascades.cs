using UnityEngine;

[NodeToolTip("Gets the Shadow Cascades from the current Quality Settings.")]
[NodePath("Actions/Application/Quality Settings")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Get Shadow Cascades", "Gets the Shadow Cascades from the current Quality Settings.")]
public class uScriptAct_QualitySettingsGetShadowCascades : uScriptLogic
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
		Value = QualitySettings.shadowCascades;
	}
}
