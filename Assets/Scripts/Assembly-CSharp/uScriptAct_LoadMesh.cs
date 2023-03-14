using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Load_Mesh")]
[NodePath("Actions/Assets")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Loads a Mesh")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Load Mesh", "Loads a Mesh file from your Resources directory.")]
public class uScriptAct_LoadMesh : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([AssetPathField(AssetType.Mesh)][FriendlyName("Asset Path", "The Mesh file to load.  The supported file formats are: \"fbx\", \"dae\", \"3ds\", \"dxf\", and \"obj\".")] string name, [FriendlyName("Loaded Asset", "The Mesh loaded from the specified file path.")] out Mesh asset)
	{
		asset = Resources.Load(name) as Mesh;
		if (null == asset)
		{
			uScriptDebug.Log("Asset " + name + " couldn't be loaded, are you sure it's in a Resources folder?", uScriptDebug.Type.Warning);
		}
	}
}
