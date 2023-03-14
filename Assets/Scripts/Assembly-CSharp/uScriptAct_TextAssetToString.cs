using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Converts a TextAsset into a string.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("TextAsset To String", "Converts a TextAsset into a string. It will also return the name of the TextAsset.")]
[NodePath("Actions/Variables/TextAsset")]
public class uScriptAct_TextAssetToString : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The variable you wish to use to set the target's value.")] TextAsset Value, [FriendlyName("Target", "The Target variable you wish to set.")] out string Target, [SocketState(false, false)][FriendlyName("Name", "The name of the TextAsset.")] out string FileName)
	{
		Target = Value.text;
		FileName = Value.name;
	}
}
