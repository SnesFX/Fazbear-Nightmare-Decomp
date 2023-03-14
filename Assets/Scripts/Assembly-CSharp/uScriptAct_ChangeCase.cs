[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Changes the case of the chracters in the specified string.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/String")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Change Case", "Changes the case of the chracters in the specified string based on the case type (Upper, Lower, or Inverted).")]
public class uScriptAct_ChangeCase : uScriptLogic
{
	public enum CaseType
	{
		Upper = 0,
		Lower = 1,
		Invert = 2
	}

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The target string.")] string Target, [SocketState(false, false)][FriendlyName("Case", "Specifies what case to change the characters to.")] CaseType caseType, [FriendlyName("Result", "Resulting string with replaced characters.")] out string Result)
	{
		if (string.Empty != Target)
		{
			switch (caseType)
			{
			case CaseType.Upper:
				Result = Target.ToUpper();
				return;
			case CaseType.Lower:
				Result = Target.ToLower();
				return;
			}
			string text = string.Empty;
			char[] array = Target.ToCharArray();
			int num = 0;
			char[] array2 = array;
			foreach (char c in array2)
			{
				if (char.IsLetter(c))
				{
					if (char.IsUpper(c))
					{
						array[num] = char.ToLower(c);
					}
					else
					{
						array[num] = char.ToUpper(c);
					}
				}
				text += array[num];
				num++;
			}
			Result = text;
		}
		else
		{
			Result = Target;
		}
	}
}
