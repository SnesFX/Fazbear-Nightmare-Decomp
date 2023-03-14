using UnityEngine;

[FriendlyName("Load Flare", "Loads a Flare file from your Resources directory.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Assets")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Load_Flare")]
[NodeToolTip("Loads a Flare")]
public class uScriptAct_LoadFlare : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Asset Path", "The Flare file to load.  The supported file format is: \"flare\".")][AssetPathField(AssetType.Flare)] string name, [FriendlyName("Loaded Asset", "The Flare loaded from the specified file path.")] out Flare asset)
	{
		asset = Resources.Load(name) as Flare;
		if (null == asset)
		{
			uScriptDebug.Log("Asset " + name + " couldn't be loaded, are you sure it's in a Resources folder?", uScriptDebug.Type.Warning);
		}
	}
}
