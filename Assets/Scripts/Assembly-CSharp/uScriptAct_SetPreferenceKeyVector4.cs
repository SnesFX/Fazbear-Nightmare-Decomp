using UnityEngine;

[FriendlyName("Set Preference Key (Vector4)", "Sets the value of the specified Key from the preference.")]
[NodePath("Actions/Application/Preferences")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeToolTip("Sets the value of the specified Key from the preference.")]
public class uScriptAct_SetPreferenceKeyVector4 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Key Name", "The name of the preference key you wish to set the value for.")] string KeyName, [FriendlyName("Value", "The new value of the key.")] Vector4 Value)
	{
		float x = Value.x;
		float y = Value.y;
		float z = Value.z;
		float w = Value.w;
		string value = x + "|" + y + "|" + z + "|" + w;
		PlayerPrefs.SetString(KeyName, value);
	}
}
