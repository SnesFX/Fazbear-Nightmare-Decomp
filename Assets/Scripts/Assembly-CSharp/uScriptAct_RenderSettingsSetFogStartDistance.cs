using UnityEngine;

[NodePath("Actions/Rendering/Render Settings")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Set Fog Start Distance", "Sets the fog start distance the renderer should use when using the linear fog mode. Please note, that the fog start distance is NOT used for the exponential fog modes.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Sets the fog start distance the renderer should use.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_RenderSettingsSetFogStartDistance : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The value to set the fog start distance to.")] float fogStartDistance)
	{
		RenderSettings.fogStartDistance = fogStartDistance;
	}
}
