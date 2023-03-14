using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Set Fog Color", "Sets the color of the fog the renderer should use.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Rendering/Render Settings")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Sets the color of the fog the renderer should use.")]
public class uScriptAct_RenderSettingsSetFogColor : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Color", "The color to set the fog to.")] Color fogColor)
	{
		RenderSettings.fogColor = fogColor;
	}
}
