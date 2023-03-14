using UnityEngine;

[FriendlyName("Decrease Quality Level", "Decreases the current quality to the next lower level.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Decreases the current quality to the next lower level.")]
[NodePath("Actions/Application/Quality Settings")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_QualitySettingsDecreaseLevel : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In()
	{
		QualitySettings.DecreaseLevel();
	}
}
