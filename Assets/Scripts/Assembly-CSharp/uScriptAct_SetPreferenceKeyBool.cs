using UnityEngine;

[FriendlyName("Set Preference Key (Bool)", "Sets the value of the specified Key from the preference.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Sets the value of the specified Key from the preference.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Application/Preferences")]
public class uScriptAct_SetPreferenceKeyBool : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Key Name", "The name of the preference key you wish to set the value for.")] string KeyName, [FriendlyName("Value", "The new value of the key.")] bool Value)
	{
		if (Value)
		{
			PlayerPrefs.SetInt(KeyName, 1);
		}
		else
		{
			PlayerPrefs.SetInt(KeyName, 0);
		}
	}
}
