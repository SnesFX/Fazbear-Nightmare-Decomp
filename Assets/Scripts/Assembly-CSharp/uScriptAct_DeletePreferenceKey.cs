using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Deletes the specified preference key if found.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Delete Preference Key", "Deletes the specified preference key if found.")]
[NodePath("Actions/Application/Preferences")]
public class uScriptAct_DeletePreferenceKey : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Key Name", "The name of the preference key you wish to delete.")] string KeyName)
	{
		PlayerPrefs.DeleteKey(KeyName);
	}
}
