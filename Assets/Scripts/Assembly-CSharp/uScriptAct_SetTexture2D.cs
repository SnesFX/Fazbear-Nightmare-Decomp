using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Texture2D")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Sets a Texture2D to the defined value.")]
[NodePath("Actions/Variables/Texture2D")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Set Texture2D", "Sets a Texture2D to the defined value.")]
public class uScriptAct_SetTexture2D : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The source texture data")] Texture2D Value, [FriendlyName("Target", "The value that has been set for this variable.")] out Texture2D Target)
	{
		Target = Value;
	}
}
