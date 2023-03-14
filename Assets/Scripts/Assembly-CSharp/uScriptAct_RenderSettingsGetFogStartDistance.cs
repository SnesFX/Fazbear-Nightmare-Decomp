using UnityEngine;

[NodeToolTip("Returns the current fog start distance used for the linear fog mode.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Get Fog Start Distance", "Returns the current fog start distance used for the linear fog mode.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Rendering/Render Settings")]
public class uScriptAct_RenderSettingsGetFogStartDistance : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The current fog start distance used by the renderer.")] out float currentFogStartDistance)
	{
		currentFogStartDistance = RenderSettings.fogStartDistance;
	}
}
