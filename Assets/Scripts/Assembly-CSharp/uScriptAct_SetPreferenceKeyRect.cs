using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Sets the value of the specified Key from the preference.")]
[NodePath("Actions/Application/Preferences")]
[FriendlyName("Set Preference Key (Rect)", "Sets the value of the specified Key from the preference.")]
public class uScriptAct_SetPreferenceKeyRect : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Key Name", "The name of the preference key you wish to set the value for.")] string KeyName, [FriendlyName("Value", "The new value of the key.")] Rect Value)
	{
		float xMin = Value.xMin;
		float yMin = Value.yMin;
		float width = Value.width;
		float height = Value.height;
		string value = xMin + "|" + yMin + "|" + width + "|" + height;
		PlayerPrefs.SetString(KeyName, value);
	}
}
