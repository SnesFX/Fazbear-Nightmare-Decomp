using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Rendering/Render Settings")]
[FriendlyName("Set Fog Density", "Sets the fog density the renderer should use when using an exponential fog mode (0.0 - 1.0). Please note, that fog density is NOT used for the linear fog mode.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Sets the fog density the renderer should use.")]
public class uScriptAct_RenderSettingsSetFogDensity : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Density", "The value to set the fog density to.")] float fogDensity)
	{
		RenderSettings.fogDensity = fogDensity;
	}
}
