using System.Collections.Generic;

[FriendlyName("Convert String to List", "Converts a string into int, float and string lists dependent on the content of the input string.\n\nFor example, the input string 'apple,orange,27,1.66' will output a string list containing all four items, a float list containing two items (27, 1.66) and an int list with a single item (27).\n\nYou can opt to preserve the length of the lists and a padding value (0 unless you specify otherwise) will be insertedwhere an appropriate value isn't in the original string.\n\nIn the example above this would mean that the float list would be (0,0,27,1.66) and the int list (0,0,27,0)\n\nInput: A string with each item separated by a comma.")]
[NodeToolTip("Converts a string into int, float and string lists.")]
[NodePath("Actions/Variables/Lists")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC. Original node by John on the uScript Community Forum", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_ConvertStringToList : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "A string variable containing the comma delimited text to create the lists from (each item is separated with a comma character).")] string Target, [FriendlyName("Preserve Length", "Used for the float and int output lists. If set to true, a padding value (0 unless you specify otherwise) will be inserted into lists where an appropriate value isn't in the original string.")][DefaultValue(true)][SocketState(false, false)] ref bool preserveLength, [SocketState(false, false)][DefaultValue(0)][FriendlyName("Padding Value", "Used with Preserve Length. If a float value is used, it will be truncated for the int list.")] ref float paddingValue, [SocketState(false, false)][FriendlyName("String Count", "Number of items in the output string list.")] out int stringCount, [SocketState(false, false)][FriendlyName("Int Count", "Number of items in the output int list.")] out int intCount, [SocketState(false, false)][FriendlyName("Float Count", "Number of items in the output float list.")] out int floatCount, [FriendlyName("String List", "A string list variable containing each item from the Target string.")] out string[] theString, [SocketState(false, false)][FriendlyName("Int List", "A int list variable containing any ints from the Target string.")] out int[] theIntList, [FriendlyName("Float List", "A float list variable containing any floats from the Target string.")] out float[] theFloatList)
	{
		theString = Target.Split(',');
		string[] array = theString;
		stringCount = theString.Length;
		int item = (int)paddingValue;
		List<int> list = new List<int>();
		List<float> list2 = new List<float>();
		int result;
		float result2;
		if (preserveLength)
		{
			string[] array2 = array;
			foreach (string s in array2)
			{
				if (int.TryParse(s, out result))
				{
					list.Add(result);
				}
				else
				{
					list.Add(item);
				}
				if (float.TryParse(s, out result2))
				{
					list2.Add(result2);
				}
				else
				{
					list2.Add(paddingValue);
				}
			}
			theIntList = list.ToArray();
			theFloatList = list2.ToArray();
			intCount = theIntList.Length;
			floatCount = theFloatList.Length;
			return;
		}
		string[] array3 = array;
		foreach (string s2 in array3)
		{
			if (int.TryParse(s2, out result))
			{
				list.Add(result);
			}
			if (float.TryParse(s2, out result2))
			{
				list2.Add(result2);
			}
		}
		theIntList = list.ToArray();
		theFloatList = list2.ToArray();
		intCount = theIntList.Length;
		floatCount = theFloatList.Length;
	}
}
