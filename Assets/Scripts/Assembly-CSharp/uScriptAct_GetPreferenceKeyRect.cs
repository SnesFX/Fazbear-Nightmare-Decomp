using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Get Preference Key (Rect)", "Returns the value of the specified Key from the preference file if it exists.")]
[NodeToolTip("Returns the value of the specified Key from the preference file if it exists.")]
[NodePath("Actions/Application/Preferences")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_GetPreferenceKeyRect : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Key Name", "The name of the preference key you wish to get the value for.")] string KeyName, [FriendlyName("Default Value", "Optional. Allows you to specify a value to return if the actual value is not found. Will return (0,0,0,0) if not specified.")] Rect DefaultValue, [FriendlyName("Value", "The returned key value.")] out Rect Value)
	{
		string @string = PlayerPrefs.GetString(KeyName);
		string[] array = @string.Split('|');
		if (array.Length != 4)
		{
			uScriptDebug.Log("The specified Preference Key was not found or not of type Rect!", uScriptDebug.Type.Warning);
			Value = DefaultValue;
			return;
		}
		float left = 0f;
		float top = 0f;
		float width = 0f;
		float height = 0f;
		int num = 0;
		string[] array2 = array;
		foreach (string s in array2)
		{
			if (num == 0)
			{
				left = float.Parse(s);
			}
			if (num == 1)
			{
				top = float.Parse(s);
			}
			if (num == 2)
			{
				width = float.Parse(s);
			}
			if (num == 3)
			{
				height = float.Parse(s);
			}
			num++;
		}
		Value = new Rect(left, top, width, height);
	}
}
