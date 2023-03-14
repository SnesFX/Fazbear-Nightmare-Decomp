using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Application/Preferences")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Returns the value of the specified Key from the preference file if it exists.")]
[FriendlyName("Get Preference Key (Bool)", "Returns the value of the specified Key from the preference file if it exists.")]
public class uScriptAct_GetPreferenceKeyBool : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Key Name", "The name of the preference key you wish to get the value for.")] string KeyName, [DefaultValue(false)][FriendlyName("Default Value", "(optional) Allows you to specify a value to return if the actual value is not found. Will return FALSE if not specified.")] bool DefaultValue, [FriendlyName("Value", "The returned key value.")] out bool Value)
	{
		int defaultValue = (DefaultValue ? 1 : 0);
		if (PlayerPrefs.GetInt(KeyName, defaultValue) == 0)
		{
			Value = false;
		}
		else
		{
			Value = true;
		}
	}
}
