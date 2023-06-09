using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Replace Value At Index In List (GameObject)", "Replaces a value in the list with the new value at the specified index.")]
[NodePath("Actions/Variables/Lists/GameObject")]
[NodeToolTip("Replaces a value in the list with the new value at the specified index.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_ReplaceValueAtIndexInListGameObject : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] GameObject[] TargetList, [FriendlyName("Index", "The index of the item to replace.")] int Index, [FriendlyName("New Value", "The new value to replace at the index.")] GameObject NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out GameObject[] ModifiedList)
	{
		if (TargetList.Length > Index)
		{
			TargetList[Index] = NewValue;
		}
		ModifiedList = TargetList;
	}
}
