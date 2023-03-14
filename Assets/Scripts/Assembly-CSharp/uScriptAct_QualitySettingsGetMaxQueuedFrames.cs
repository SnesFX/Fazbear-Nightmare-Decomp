using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Gets the Maximum Queued Frames from the current Quality Settings.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Application/Quality Settings")]
[FriendlyName("Get Max Queued Frames", "Gets the Maximum Queued Frames setting from the current Quality Settings.")]
public class uScriptAct_QualitySettingsGetMaxQueuedFrames : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Value", "The current value for this quality setting level.")] out int Value)
	{
		Value = QualitySettings.maxQueuedFrames;
	}
}
