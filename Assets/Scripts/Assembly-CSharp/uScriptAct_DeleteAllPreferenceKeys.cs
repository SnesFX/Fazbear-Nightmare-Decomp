using UnityEngine;

[NodeToolTip("Deletes all preference keys.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Delete All Preference Keys", "Deletes all preference keys.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Application/Preferences")]
public class uScriptAct_DeleteAllPreferenceKeys : uScriptLogic
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
		PlayerPrefs.DeleteAll();
	}
}
