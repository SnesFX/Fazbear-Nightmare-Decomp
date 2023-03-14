using UnityEngine;

[NodePath("Actions/Application/Quality Settings")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Gets the Soft Vegetation from the current Quality Settings.")]
[FriendlyName("Get Soft Vegetation", "Gets the Soft Vegetation setting from the current Quality Settings.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_QualitySettingsGetSoftVegetation : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The current value for this quality setting level.")] out bool Value)
	{
		Value = QualitySettings.softVegetation;
	}
}
