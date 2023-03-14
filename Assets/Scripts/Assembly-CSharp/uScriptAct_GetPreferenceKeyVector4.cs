using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Application/Preferences")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Get Preference Key (Vector4)", "Returns the value of the specified Key from the preference file if it exists.")]
[NodeToolTip("Returns the value of the specified Key from the preference file if it exists.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_GetPreferenceKeyVector4 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Key Name", "The name of the preference key you wish to get the value for.")] string KeyName, [FriendlyName("Default Value", "(optional) Allows you to specify a value to return if the actual value is not found. Will return (0,0,0,0) if not specified.")] Vector4 DefaultValue, [FriendlyName("Value", "The returned key value.")] out Vector4 Value)
	{
		string @string = PlayerPrefs.GetString(KeyName);
		string[] array = @string.Split('|');
		if (array.Length != 4)
		{
			uScriptDebug.Log("The specified Preference Key was not found or not of type Vector4!", uScriptDebug.Type.Warning);
			Value = DefaultValue;
			return;
		}
		float x = 0f;
		float y = 0f;
		float z = 0f;
		float w = 0f;
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
			if (num == 3)
			{
				w = float.Parse(s);
			}
			num++;
		}
		Value = new Vector4(x, y, z, w);
	}
}
