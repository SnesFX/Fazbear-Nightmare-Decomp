[NodeToolTip("Sets a boolean to the defined value.")]
[FriendlyName("Set Bool", "Sets a boolean to the defined value.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Bool")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Variables/Bool")]
public class uScriptAct_SetBool : uScriptLogic
{
	private bool _setTrue;

	private bool _setFalse;

	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Set True", "Fired only if the variable was set to true.")]
	public bool SetTrue
	{
		get
		{
			return _setTrue;
		}
	}

	[FriendlyName("Set False", "Fired only if the variable was set to false.")]
	public bool SetFalse
	{
		get
		{
			return _setFalse;
		}
	}

	public void True(out bool Target)
	{
		_setFalse = false;
		_setTrue = false;
		Target = true;
		_setTrue = true;
	}

	public void False([FriendlyName("Target", "The value that has been set for this variable.")] out bool Target)
	{
		_setFalse = false;
		_setTrue = false;
		Target = false;
		_setFalse = true;
	}
}
