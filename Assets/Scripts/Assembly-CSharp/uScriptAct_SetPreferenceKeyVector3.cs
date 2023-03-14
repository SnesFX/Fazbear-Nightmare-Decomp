using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Application/Preferences")]
[FriendlyName("Set Preference Key (Vector3)", "Sets the value of the specified Key from the preference.")]
[NodeToolTip("Sets the value of the specified Key from the preference.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
public class uScriptAct_SetPreferenceKeyVector3 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Key Name", "The name of the preference key you wish to set the value for.")] string KeyName, [FriendlyName("Value", "The new value of the key.")] Vector3 Value)
	{
		float x = Value.x;
		float y = Value.y;
		float z = Value.z;
		string value = x + "|" + y + "|" + z;
		PlayerPrefs.SetString(KeyName, value);
	}
}
