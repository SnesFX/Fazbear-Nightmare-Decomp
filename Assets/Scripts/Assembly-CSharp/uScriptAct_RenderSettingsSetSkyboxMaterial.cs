using UnityEngine;

[NodeToolTip("Sets the material for the renderer's global skybox.")]
[NodePath("Actions/Rendering/Render Settings")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Set Skybox Material", "Sets the material for the renderer's global skybox.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_RenderSettingsSetSkyboxMaterial : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Material", "The material to set the global skybox to.")] Material newSkybox)
	{
		RenderSettings.skybox = newSkybox;
	}
}
