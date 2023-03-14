using UnityEngine;

[NodeToolTip("Shuffles a list.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Shuffle_List")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Variables/Lists")]
[FriendlyName("Shuffle List", "Takes an input list, shuffles it and puts the result into Shuffled List.")]
public class uScriptAct_ShuffleList : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("List", "The list to shuffle.")] object[] list, [FriendlyName("Shuffled List", "The shuffled list.")] out object[] shuffled)
	{
		shuffled = new object[list.Length];
		int num = list.Length;
		for (int i = 0; i < shuffled.Length; i++)
		{
			int num2 = Random.Range(0, num);
			shuffled[i] = list[num2];
			num--;
			list[num2] = list[num];
		}
	}
}
