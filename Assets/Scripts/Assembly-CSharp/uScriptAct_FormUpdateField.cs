using UnityEngine;

[NodeToolTip("Add a simple field to the form.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#WebFormUpdateField")]
[FriendlyName("Form Update Field", "Add a simple field to the form.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Web/Form")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_FormUpdateField : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Form", "The Form to modify.")] ref WWWForm Form, [FriendlyName("Field Name", "The field name.")] string Field, [FriendlyName("Value", "The field value. Non-string objects will be convertd to a string using ToString().")] object Value)
	{
		if (Form == null)
		{
			Form = new WWWForm();
		}
		Form.AddField(Field, Value.ToString());
	}
}
