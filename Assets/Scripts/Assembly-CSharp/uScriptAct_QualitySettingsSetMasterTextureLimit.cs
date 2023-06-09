using UnityEngine;

[FriendlyName("Set Master Texture Limit", "Sets the Master Texture Limit on the current Quality Settings.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Sets the Master Texture Limit on the current Quality Settings.")]
[NodePath("Actions/Application/Quality Settings")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_QualitySettingsSetMasterTextureLimit : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The value to set on this quality setting. Setting this to one uses the first mipmap of each texture (so all textures are half size), setting this to two uses the second mipmap of each texture (so all textures are quarter size), etc.. This can be used to decrease video memory requirements on low-end computers. The default value is zero.")] int Value)
	{
		QualitySettings.masterTextureLimit = Value;
	}
}
