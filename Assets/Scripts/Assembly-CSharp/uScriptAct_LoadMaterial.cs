using UnityEngine;

[NodeToolTip("Loads a Material")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Load_Material")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Assets")]
[FriendlyName("Load Material", "Loads a Material file from your Resources directory.")]
public class uScriptAct_LoadMaterial : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Asset Path", "The Material file to load.  The supported file format is: \"mat\".")][AssetPathField(AssetType.Material)] string name, [FriendlyName("Loaded Asset", "The Material loaded from the specified file path.")] out Material materialFile)
	{
		materialFile = Resources.Load(name) as Material;
		if (null == materialFile)
		{
			uScriptDebug.Log("Asset " + name + " couldn't be loaded, are you sure it's in a Resources folder?", uScriptDebug.Type.Warning);
		}
	}
}
