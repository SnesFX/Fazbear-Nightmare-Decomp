[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Replace Value At Index In List (Int)", "Replaces a value in the list with the new value at the specified index.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Replaces a value in the list with the new value at the specified index.")]
[NodePath("Actions/Variables/Lists/Int")]
public class uScriptAct_ReplaceValueAtIndexInListInt : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] int[] TargetList, [FriendlyName("Index", "The index of the item to replace.")] int Index, [FriendlyName("New Value", "The new value to replace at the index.")] int NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out int[] ModifiedList)
	{
		if (TargetList.Length > Index)
		{
			TargetList[Index] = NewValue;
		}
		ModifiedList = TargetList;
	}
}
