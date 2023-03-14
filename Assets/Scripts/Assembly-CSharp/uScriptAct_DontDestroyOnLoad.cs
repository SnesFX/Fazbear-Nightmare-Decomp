using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Don't Destroy On Load", "Sets DontDestroyOnLoad on an object so it will not be destroyed automatically when loading a new scene. If the object is a component or GameObject then its entire transform hierarchy will not be destroyed either.")]
[NodePath("Actions/Level")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeToolTip("Sets DontDestroyOnLoad on an object so it will not be destroyed automatically when loading a new scene.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScriptAct_DontDestroyOnLoad : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The GameObject(s) or other object types you wish to set DontDestroyOnLoad on.")] Object[] Target)
	{
		foreach (Object @object in Target)
		{
			if (null != @object)
			{
				Object.DontDestroyOnLoad(@object);
			}
		}
	}
}
