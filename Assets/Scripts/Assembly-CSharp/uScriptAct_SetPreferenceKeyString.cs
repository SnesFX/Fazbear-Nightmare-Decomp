using UnityEngine;

[NodeToolTip("Sets the value of the specified Key from the preference.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Application/Preferences")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Set Preference Key (String)", "Sets the value of the specified Key from the preference.")]
public class uScriptAct_SetPreferenceKeyString : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Key Name", "The name of the preference key you wish to set the value for.")] string KeyName, [FriendlyName("Value", "The new value of the key.")] string Value)
	{
		PlayerPrefs.SetString(KeyName, Value);
	}
}
