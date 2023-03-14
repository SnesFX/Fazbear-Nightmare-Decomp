using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Sets the ambient light color the renderer should use.")]
[NodePath("Actions/Rendering/Render Settings")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Set Ambient Light Color", "Sets the ambient light color the renderer should use.")]
public class uScriptAct_RenderSettingsSetAmbientLightColor : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Color", "The color to set the ambient light to.")] Color ambientLightColor)
	{
		RenderSettings.ambientLight = ambientLightColor;
	}
}
