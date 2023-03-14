using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Application/Quality Settings")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Get Master Texture Limit", "Gets the Master Texture Limit from the current Quality Settings.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeToolTip("Gets the Master Texture Limit from the current Quality Settings.")]
public class uScriptAct_QualitySettingsGetMasterTextureLimit : uScriptLogic
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
		Value = QualitySettings.masterTextureLimit;
	}
}
