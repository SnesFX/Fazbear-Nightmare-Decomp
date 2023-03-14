[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_Object_From_List")]
[FriendlyName("Get Object From List", "Gets the object at the specified index from a list of objcts.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Utilities")]
[NodeToolTip("Gets an object from a list of objects.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_GetObjectFromList : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("List", "The object list.")] object[] list, [FriendlyName("Index", "The target object index.")] int index, [FriendlyName("Object", "The object.")] out object item)
	{
		item = list[index];
	}
}
