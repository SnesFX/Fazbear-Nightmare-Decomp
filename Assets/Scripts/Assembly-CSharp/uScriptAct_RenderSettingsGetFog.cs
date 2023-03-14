using UnityEngine;

[NodeToolTip("Gets the current state of the renderer's fog.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Get Fog", "Gets the current state of the renderer's fog.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Rendering/Render Settings")]
public class uScriptAct_RenderSettingsGetFog : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("State", "Returns the current state of the renderer's fog value (true = on, false = off).")] out bool fogState)
	{
		fogState = RenderSettings.fog;
	}
}
