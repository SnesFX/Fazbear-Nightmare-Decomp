using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Get Preference Key (Float)", "Returns the value of the specified Key from the preference file if it exists.")]
[NodePath("Actions/Application/Preferences")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Returns the value of the specified Key from the preference file if it exists.")]
public class uScriptAct_GetPreferenceKeyFloat : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Key Name", "The name of the preference key you wish to get the value for.")] string KeyName, [DefaultValue(0f)][FriendlyName("Default Value", "(optional) Allows you to specify a value to return if the actual value is not found. Will return 0 if not specified.")] float DefaultValue, [FriendlyName("Value", "The returned key value.")] out float Value)
	{
		Value = PlayerPrefs.GetFloat(KeyName, DefaultValue);
	}
}
