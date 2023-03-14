using UnityEngine;

[NodeToolTip("Replaces a value in the list with the new value at the specified index.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Replace Value At Index In List (Camera)", "Replaces a value in the list with the new value at the specified index.")]
[NodePath("Actions/Variables/Lists/Camera")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_ReplaceValueAtIndexInListCamera : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] Camera[] TargetList, [FriendlyName("Index", "The index of the item to replace.")] int Index, [FriendlyName("New Value", "The new value to replace at the index.")] Camera NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out Camera[] ModifiedList)
	{
		if (TargetList.Length > Index)
		{
			TargetList[Index] = NewValue;
		}
		ModifiedList = TargetList;
	}
}
