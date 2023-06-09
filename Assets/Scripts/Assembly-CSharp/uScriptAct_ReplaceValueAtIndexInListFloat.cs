[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Replaces a value in the list with the new value at the specified index.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Replace Value At Index In List (Float)", "Replaces a value in the list with the new value at the specified index.")]
[NodePath("Actions/Variables/Lists/Float")]
public class uScriptAct_ReplaceValueAtIndexInListFloat : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target List", "The list to check.")] float[] TargetList, [FriendlyName("Index", "The index of the item to replace.")] int Index, [FriendlyName("New Value", "The new value to replace at the index.")] float NewValue, [FriendlyName("Modified list", "The List after the values have been changed.")] out float[] ModifiedList)
	{
		if (TargetList.Length > Index)
		{
			TargetList[Index] = NewValue;
		}
		ModifiedList = TargetList;
	}
}
