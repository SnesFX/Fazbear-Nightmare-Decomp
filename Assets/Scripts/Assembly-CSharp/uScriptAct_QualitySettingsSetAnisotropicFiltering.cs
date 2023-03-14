using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeToolTip("Sets the Anisotropic Filtering on the current Quality Settings.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Application/Quality Settings")]
[FriendlyName("Set Anisotropic Filtering", "Sets the Anisotropic Filtering on the current Quality Settings.")]
public class uScriptAct_QualitySettingsSetAnisotropicFiltering : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The value to set on this quality setting.")] AnisotropicFiltering Value)
	{
		QualitySettings.anisotropicFiltering = Value;
	}
}
