using UnityEngine;

[NodeToolTip("Returns the material used for the renderer's global skybox.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Rendering/Render Settings")]
[FriendlyName("Get Skybox Material", "Returns the material used for the renderer's global skybox.")]
public class uScriptAct_RenderSettingsGetSkyboxMaterial : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Material", "The current material of the global skybox.")] out Material currentSkybox)
	{
		currentSkybox = RenderSettings.skybox;
	}
}
