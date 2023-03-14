using UnityEngine;

[NodePath("Actions/Rendering/Render Settings")]
[NodeToolTip("Returns the current light flare strength used by the renderer.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Get Flare Strength", "Returns the current light flare strength used by the renderer.")]
public class uScriptAct_RenderSettingsGetFlareStrength : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The current value of the light flare strength used by the renderer.")] out float currentFlareStrength)
	{
		currentFlareStrength = RenderSettings.flareStrength;
	}
}
