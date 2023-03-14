using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Rendering/Render Settings")]
[FriendlyName("Get Fog Density", "Returns the current fog density used for exponential fog modes.")]
[NodeToolTip("Returns the current fog density used for exponential fog modes.")]
public class uScriptAct_RenderSettingsGetFogDensity : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Density", "The current fog density used by the renderer.")] out float currentFogDensity)
	{
		currentFogDensity = RenderSettings.fogDensity;
	}
}
