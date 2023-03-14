using UnityEngine;

[NodeToolTip("Gets the Anisotropic Filtering Mode from the current Quality Settings.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Application/Quality Settings")]
[FriendlyName("Get Anisotropic Filtering Mode", "Gets the Anisotropic Filtering Mode from the current Quality Settings.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_QualitySettingsGetAnisotropicFiltering : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The current value for this quality setting level.")] out AnisotropicFiltering Value)
	{
		Value = QualitySettings.anisotropicFiltering;
	}
}
