using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Log")]
[NodePath("Actions/Editor Only")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Outputs a string to the debug log.")]
[FriendlyName("Log", "Outputs a string to the debug log.")]
public class uScriptAct_Log : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Prefix", "String to print ahead of each attached Target object.")][SocketState(false, false)] object Prefix, [FriendlyName("Target", "Objects to be printed to the console. If multiple are attached, they will all be printed 1 per line as Prefix + Target + Postfix.")] object[] Target, [SocketState(false, false)][FriendlyName("Postfix", "String to print after each attached Target object.")] object Postfix)
	{
		if (Target.Length > 0)
		{
			foreach (object obj in Target)
			{
				Debug.Log(((Prefix != null) ? Prefix.ToString() : string.Empty) + obj.ToString() + ((Postfix != null) ? Postfix.ToString() : string.Empty) + "\n");
			}
		}
		else
		{
			Debug.Log(((Prefix != null) ? Prefix.ToString() : string.Empty) + ((Postfix != null) ? Postfix.ToString() : string.Empty) + "\n");
		}
	}
}
