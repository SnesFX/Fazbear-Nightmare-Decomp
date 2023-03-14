using UnityEngine;

[FriendlyName("Set Texture Scale", "Applys a texture scale to the specified Material.")]
[NodePath("Actions/GameObjects")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Main_Texture_Scale")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Sets the main texture scale of a Material.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_SetMainTextureScale : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Material", "The material to set the main texture scale.")] Material material, [FriendlyName("Texture Scale", "The texture scale to apply.")] Vector2 textureScale)
	{
		material.mainTextureScale = textureScale;
	}
}
