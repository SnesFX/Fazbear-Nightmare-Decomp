[NodeToolTip("Converts a list variable into a delimited string.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Convert List to String", "Converts a list variable into a delimited string.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Variables/Lists")]
[NodeAuthor("Detox Studios LLC. Original node by John on the uScript Community Forum", "http://www.detoxstudios.com")]
public class uScriptAct_ConvertListToString : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The target list variable to convert into a string.")] object[] Target, [FriendlyName("Delimiter", "The character(s) you wish to use to seperate the elements of the list variable.")][DefaultValue(",")] string Delimiter, [DefaultValue(true)][FriendlyName("Clean Names", "Should the extra Unity text be stripped from the strings.")] bool CleanNames, [FriendlyName("Result", "The resulting string variable that contains all the list strings")] out string Result)
	{
		string text = string.Empty;
		if (Target.Length > 0)
		{
			if (Target[0].GetType() == typeof(string))
			{
				int num = 0;
				for (int i = 0; i < Target.Length; i++)
				{
					string text2 = (string)Target[i];
					text = ((num != Target.Length - 1) ? ((!CleanNames) ? (text + text2 + Delimiter) : (text + CleanString(text2) + Delimiter)) : ((!CleanNames) ? (text + text2) : (text + CleanString(text2))));
					num++;
				}
			}
			else
			{
				int num2 = 0;
				foreach (object obj in Target)
				{
					text = ((num2 != Target.Length - 1) ? ((!CleanNames) ? string.Concat(text, obj, Delimiter) : (text + CleanString(obj.ToString()) + Delimiter)) : ((!CleanNames) ? (text + obj) : (text + CleanString(obj.ToString()))));
					num2++;
				}
			}
			Result = text;
		}
		else
		{
			Result = string.Empty;
		}
	}

	private string CleanString(string stringToClean)
	{
		string text = stringToClean;
		text = text.Replace(" (UnityEngine.GameObject)", string.Empty);
		text = text.Replace(" (UnityEngine.Camera)", string.Empty);
		text = text.Replace(" (UnityEngine.Color)", string.Empty);
		return text.Replace(" (UnityEngine.AudioClip)", string.Empty);
	}
}
