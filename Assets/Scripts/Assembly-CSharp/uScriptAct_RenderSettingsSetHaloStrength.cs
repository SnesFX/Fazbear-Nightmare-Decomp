using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Set Halo Strength", "Sets the size of the light halos the renderer should use.")]
[NodeToolTip("Sets the size of the light halos the renderer should use.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Rendering/Render Settings")]
public class uScriptAct_RenderSettingsSetHaloStrength : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The value to set the light halo strength to.")] float haloStrength)
	{
		RenderSettings.haloStrength = haloStrength;
	}
}
