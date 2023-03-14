[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Concatenate")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Concatenates two objects as a string for output.")]
[NodePath("Actions/Variables/String")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Concatenate", "Concatenates two objects as a string for output.\n\nNote: This node will strip leading and trailing whitespace from the input strings. use the Seperator to add a space between strings.")]
public class uScriptAct_Concatenate : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("A", "Objects to be concatenated with B as a string. If there is more than 1 object, they will all be concatenated together as strings before being concatenated with B.")] object[] A, [FriendlyName("B", "Objects to be concatenated with A as a string. If there is more than 1 object, they will all be concatenated together as strings before being concatenated with A.")] object[] B, [FriendlyName("Separator", "String to use as a seaparator between each concatenated string. If there are multiple objects attached to either A or B, this separator will also be inserted between each of those as they are concatenated.")][SocketState(false, false)] string Separator, [FriendlyName("Result", "Resulting concatenated string.")] out string Result)
	{
		string text = A[0].ToString();
		string text2 = B[0].ToString();
		for (int i = 1; i < A.Length; i++)
		{
			text = text + Separator + A[i].ToString();
		}
		for (int i = 1; i < B.Length; i++)
		{
			text2 = text2 + Separator + B[i].ToString();
		}
		Result = text + Separator + text2;
	}
}
