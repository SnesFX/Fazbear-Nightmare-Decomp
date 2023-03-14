using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Loads a Texture2D")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Load_Texture2D")]
[NodePath("Actions/Assets")]
[FriendlyName("Load Texture2D", "Loads a Texture2D file from your Resources directory.")]
public class uScriptAct_LoadTexture2D : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Asset Path", "The Texture2D file to load.  The supported file formats are: \"psd\", \"tiff\", \"jpg\", \"tga\", \"png\", \"gif\", \"bmp\", \"iff\", and \"pict\"")][AssetPathField(AssetType.Texture2D)] string name, [FriendlyName("Loaded Asset", "The Texture2D loaded from the specified file path.")] out Texture2D textureFile)
	{
		textureFile = Resources.Load(name) as Texture2D;
		if (null == textureFile)
		{
			uScriptDebug.Log("Asset " + name + " couldn't be loaded, are you sure it's in a Resources folder?", uScriptDebug.Type.Warning);
		}
	}
}
