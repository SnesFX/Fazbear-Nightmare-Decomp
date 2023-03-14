using UnityEngine;

[NodeToolTip("Loads a TextAsset")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Assets")]
[FriendlyName("Load TextAsset", "Loads a TextAsset file from your Resources directory. Binary files can be loaded as a TextAsset, but they must use the \"bytes\" file extension.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Load_TextAsset")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_LoadTextAsset : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Asset Path", "The TextAsset file to load.  The supported text formats are: \"txt\", \"htm\", \"html\", \"xml\", and \"bytes\".")][AssetPathField(AssetType.TextAsset)] string name, [FriendlyName("Loaded Asset", "The TextAsset loaded from the specified file path.")] out TextAsset asset)
	{
		asset = Resources.Load(name) as TextAsset;
		if (null == asset)
		{
			uScriptDebug.Log("Asset " + name + " couldn't be loaded, are you sure it's in a Resources folder?", uScriptDebug.Type.Warning);
		}
	}
}
