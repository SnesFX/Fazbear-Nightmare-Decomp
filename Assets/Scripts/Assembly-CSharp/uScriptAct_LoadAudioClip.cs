using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Load_AudioClip")]
[FriendlyName("Load AudioClip", "Loads an AudioClip file from your Resources directory.")]
[NodeToolTip("Loads an AudioClip")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Assets")]
public class uScriptAct_LoadAudioClip : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([AssetPathField(AssetType.AudioClip)][FriendlyName("Asset Path", "The AudioClip file to load.  The supported file formats are: \"aif\", \"wav\", \"mp3\", \"ogg\", \"xm\", \"mod\", \"it\", and \"s3m\".")] string name, [FriendlyName("Loaded Asset", "The AudioClip loaded from the specified file path.")] out AudioClip audioClip)
	{
		audioClip = Resources.Load(name) as AudioClip;
		if (null == audioClip)
		{
			uScriptDebug.Log("Asset " + name + " couldn't be loaded, are you sure it's in a Resources folder?", uScriptDebug.Type.Warning);
		}
	}
}
