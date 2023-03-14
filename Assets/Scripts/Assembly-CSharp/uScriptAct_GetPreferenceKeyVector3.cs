using UnityEngine;

[NodeToolTip("Returns the value of the specified Key from the preference file if it exists.")]
[NodePath("Actions/Application/Preferences")]
[FriendlyName("Get Preference Key (Vector3)", "Returns the value of the specified Key from the preference file if it exists.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
public class uScriptAct_GetPreferenceKeyVector3 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Key Name", "The name of the preference key you wish to get the value for.")] string KeyName, [FriendlyName("Default Value", "(optional) Allows you to specify a value to return if the actual value is not found. Will return (0,0,0) if not specified.")] Vector3 DefaultValue, [FriendlyName("Value", "The returned key value.")] out Vector3 Value)
	{
		string @string = PlayerPrefs.GetString(KeyName);
		string[] array = @string.Split('|');
		if (array.Length != 3)
		{
			uScriptDebug.Log("The specified Preference Key was not found or not of type Vector3!", uScriptDebug.Type.Warning);
			Value = DefaultValue;
			return;
		}
		float x = 0f;
		float y = 0f;
		float z = 0f;
		int num = 0;
		string[] array2 = array;
		foreach (string s in array2)
		{
			if (num == 0)
			{
				x = float.Parse(s);
			}
			if (num == 1)
			{
				y = float.Parse(s);
			}
			if (num == 2)
			{
				z = float.Parse(s);
			}
			num++;
		}
		Value = new Vector3(x, y, z);
	}
}
