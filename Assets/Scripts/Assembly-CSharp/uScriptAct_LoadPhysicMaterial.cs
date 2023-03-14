using UnityEngine;

[NodeToolTip("Loads a PhysicMaterial")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Load_PhysicMaterial")]
[FriendlyName("Load PhysicMaterial", "Loads a PhysicMaterial file from your Resources directory.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Assets")]
public class uScriptAct_LoadPhysicMaterial : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([AssetPathField(AssetType.PhysicMaterial)][FriendlyName("Asset Path", "The PhysicMaterial file to load.  The supported file format is: \"physicMaterial\".")] string name, [FriendlyName("Loaded Asset", "The PhysicMaterial loaded from the specified file path.")] out PhysicMaterial asset)
	{
		asset = Resources.Load(name) as PhysicMaterial;
		if (null == asset)
		{
			uScriptDebug.Log("Asset " + name + " couldn't be loaded, are you sure it's in a Resources folder?", uScriptDebug.Type.Warning);
		}
	}
}
