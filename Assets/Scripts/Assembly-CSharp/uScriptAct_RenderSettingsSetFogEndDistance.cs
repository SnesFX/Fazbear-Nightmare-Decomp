using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Sets the fog end distance the renderer should use.")]
[NodePath("Actions/Rendering/Render Settings")]
[FriendlyName("Set Fog End Distance", "Sets the fog end distance the renderer should use when using the linear fog mode. Please note, that the fog end distance is NOT used for the exponential fog modes.")]
public class uScriptAct_RenderSettingsSetFogEndDistance : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The value to set the fog end distance to.")] float fogEndDistance)
	{
		RenderSettings.fogEndDistance = fogEndDistance;
	}
}
