using UnityEngine;

[NodePath("Actions/Rendering/Render Settings")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Returns the current ambient light color used by the renderer.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Get Ambient Light Color", "Returns the current ambient light color used by the renderer.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
public class uScriptAct_RenderSettingsGetAmbientLightColor : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Color", "The current color of the ambient light used by the renderer.")] out Color currentAmbientLightColor)
	{
		currentAmbientLightColor = RenderSettings.ambientLight;
	}
}
