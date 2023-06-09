using UnityEngine;

[NodePath("Actions/Variables/Lists/Vector3")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeToolTip("Replaces a value in the list with the new value at the specified index.")]
[FriendlyName("Replace Value At Index In List (Vector3)", "Replaces a value in the list with the new value at the specified index.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_ReplaceValueAtIndexInListVector3 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] Vector3[] TargetList, [FriendlyName("Index", "The index of the item to replace.")] int Index, [FriendlyName("New Value", "The new value to replace at the index.")] Vector3 NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out Vector3[] ModifiedList)
	{
		if (TargetList.Length > Index)
		{
			TargetList[Index] = NewValue;
		}
		ModifiedList = TargetList;
	}
}
