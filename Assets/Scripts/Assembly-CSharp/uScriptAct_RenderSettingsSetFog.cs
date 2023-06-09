using UnityEngine;

[NodeToolTip("Sets the state of the renderer's fog to on or off.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Set Fog", "Sets the state of the renderer's fog to true (on) or false (off).")]
[NodePath("Actions/Rendering/Render Settings")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_RenderSettingsSetFog : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The value to set the fog to.")] bool fogState)
	{
		RenderSettings.fog = fogState;
	}
}
