using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Load_Shader")]
[FriendlyName("Load Shader", "Loads a Shader file from your Resources directory.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Loads a Shader")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Assets")]
public class uScriptAct_LoadShader : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([AssetPathField(AssetType.Shader)][FriendlyName("Asset Path", "The Shader file to load.  The supported file format is: \"shader\".")] string name, [FriendlyName("Loaded Asset", "The Shader loaded from the specified file path.")] out Shader asset)
	{
		asset = Resources.Load(name) as Shader;
		if (null == asset)
		{
			uScriptDebug.Log("Asset " + name + " couldn't be loaded, are you sure it's in a Resources folder?", uScriptDebug.Type.Warning);
		}
	}
}
