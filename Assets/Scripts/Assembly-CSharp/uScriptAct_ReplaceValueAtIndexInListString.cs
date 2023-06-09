[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Variables/Lists/String")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Replaces a value in the list with the new value at the specified index.")]
[FriendlyName("Replace Value At Index In List (String)", "Replaces a value in the list with the new value at the specified index.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_ReplaceValueAtIndexInListString : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] string[] TargetList, [FriendlyName("Index", "The index of the item to replace.")] int Index, [FriendlyName("New Value", "The new value to replace at the index.")] string NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out string[] ModifiedList)
	{
		if (TargetList.Length > Index)
		{
			TargetList[Index] = NewValue;
		}
		ModifiedList = TargetList;
	}
}
