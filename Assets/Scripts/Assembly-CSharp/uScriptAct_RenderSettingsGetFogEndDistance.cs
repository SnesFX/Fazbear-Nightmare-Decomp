using UnityEngine;

[NodeToolTip("Returns the current fog end distance used for the linear fog mode.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Rendering/Render Settings")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Get Fog End Distance", "Returns the current fog end distance used for the linear fog mode.")]
public class uScriptAct_RenderSettingsGetFogEndDistance : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The current fog end distance used by the renderer.")] out float currentFogEndDistance)
	{
		currentFogEndDistance = RenderSettings.fogEndDistance;
	}
}
