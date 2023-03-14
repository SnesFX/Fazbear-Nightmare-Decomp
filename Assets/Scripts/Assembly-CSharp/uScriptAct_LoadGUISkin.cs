using UnityEngine;

[FriendlyName("Load GUISkin", "Loads a GUISkin file from your Resources directory.")]
[NodeToolTip("Loads a GUISkin")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Assets")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Load_GUISkin")]
public class uScriptAct_LoadGUISkin : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Asset Path", "The GUISkin file to load.  The supported file format is: \"guiSkin\".")][AssetPathField(AssetType.GUISkin)] string name, [FriendlyName("Loaded Asset", "The GUISkin loaded from the specified file path.")] out GUISkin asset)
	{
		asset = Resources.Load(name) as GUISkin;
		if (null == asset)
		{
			uScriptDebug.Log("Asset " + name + " couldn't be loaded, are you sure it's in a Resources folder?", uScriptDebug.Type.Warning);
		}
	}
}
