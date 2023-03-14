using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Load_Cubemap")]
[FriendlyName("Load Cubemap", "Loads a Cubemap file from your Resources directory.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Assets")]
[NodeToolTip("Loads a Cubemap")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_LoadCubemap : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([AssetPathField(AssetType.Cubemap)][FriendlyName("Asset Path", "The Cubemap file to load.  The supported file format is: \"cubemap\".")] string name, [FriendlyName("Loaded Asset", "The Cubemap loaded from the specified file path.")] out Cubemap asset)
	{
		asset = Resources.Load(name) as Cubemap;
		if (null == asset)
		{
			uScriptDebug.Log("Asset " + name + " couldn't be loaded, are you sure it's in a Resources folder?", uScriptDebug.Type.Warning);
		}
	}
}
