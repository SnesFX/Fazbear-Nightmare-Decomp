using UnityEngine;

[FriendlyName("Get Fog Color", "Returns the renderer's current fog color.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Returns the renderer's current fog color.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Rendering/Render Settings")]
public class uScriptAct_RenderSettingsGetFogColor : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Color", "The current fog color.")] out Color FogColor)
	{
		FogColor = RenderSettings.fogColor;
	}
}
