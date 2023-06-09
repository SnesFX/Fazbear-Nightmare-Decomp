using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Replace Value At Index In List (Vector2)", "Replaces a value in the list with the new value at the specified index.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Variables/Lists/Vector2")]
[NodeToolTip("Replaces a value in the list with the new value at the specified index.")]
public class uScriptAct_ReplaceValueAtIndexInListVector2 : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] Vector2[] TargetList, [FriendlyName("Index", "The index of the item to replace.")] int Index, [FriendlyName("New Value", "The new value to replace at the index.")] Vector2 NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out Vector2[] ModifiedList)
	{
		if (TargetList.Length > Index)
		{
			TargetList[Index] = NewValue;
		}
		ModifiedList = TargetList;
	}
}
