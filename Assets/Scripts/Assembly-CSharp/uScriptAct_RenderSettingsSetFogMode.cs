using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Sets the fog mode the renderer should use.")]
[NodePath("Actions/Rendering/Render Settings")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Set Fog Mode", "Sets the fog mode the renderer should use.")]
public class uScriptAct_RenderSettingsSetFogMode : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Mode", "The value to set the fog to.")] FogMode newFogMode)
	{
		RenderSettings.fogMode = newFogMode;
	}
}
