using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Load_Prefab")]
[FriendlyName("Load Prefab", "Loads a Prefab file from your Resources directory.  Prefabs are loaded as GameObjects.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Assets")]
[NodeToolTip("Loads a Prefab")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_LoadPrefab : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([AssetPathField(AssetType.Prefab)][FriendlyName("Asset Path", "The Prefab file to load.  The supported file format is: \"prefab\".")] string name, [FriendlyName("Loaded Asset", "The Prefab GameObject loaded from the specified file path.")] out GameObject asset)
	{
		asset = Resources.Load(name) as GameObject;
		if (null == asset)
		{
			uScriptDebug.Log("Asset " + name + " couldn't be loaded, are you sure it's in a Resources folder?", uScriptDebug.Type.Warning);
		}
	}
}
