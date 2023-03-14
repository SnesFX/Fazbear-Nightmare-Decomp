using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Loads a Font")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Load Font", "Loads a Font file from your Resources directory.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Load_Font")]
[NodePath("Actions/Assets")]
public class uScriptAct_LoadFont : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Asset Path", "The Font file to load.  The supported file formats are: \"otf\" and \"ttf\".")][AssetPathField(AssetType.Font)] string name, [FriendlyName("Loaded Asset", "The Font loaded from the specified file path.")] out Font asset)
	{
		asset = Resources.Load(name) as Font;
		if (null == asset)
		{
			uScriptDebug.Log("Asset " + name + " couldn't be loaded, are you sure it's in a Resources folder?", uScriptDebug.Type.Warning);
		}
	}
}
