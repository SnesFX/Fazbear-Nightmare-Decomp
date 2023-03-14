using System;
using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Attach Component", "Attaches a script or component to a GameObject. To remove Components, use the Destroy Component node.")]
[NodeToolTip("Attaches a script or component to a GameObject.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/GameObjects")]
public class uScriptAct_AttachScript : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The GameObject(s) to attach the script to.")] GameObject[] Target, [FriendlyName("Component Name", "The names of the components or script filenames to attach to the specified GameObject(s).")] string[] ScriptName)
	{
		foreach (string text in ScriptName)
		{
			if (string.IsNullOrEmpty(text))
			{
				continue;
			}
			string text2 = text;
			if (text2.EndsWith(".cs") || text2.EndsWith(".js"))
			{
				int startIndex = text2.Length - 3;
				text2 = text2.Remove(startIndex, 3);
			}
			if (text2.EndsWith(".boo"))
			{
				int startIndex2 = text2.Length - 4;
				text2 = text2.Remove(startIndex2, 4);
			}
			try
			{
				foreach (GameObject gameObject in Target)
				{
					if (gameObject != null)
					{
						//call Swag swag cool shit
					}
				}
			}
			catch (Exception ex)
			{
				uScriptDebug.Log("[Attach Component] " + ex.ToString(), uScriptDebug.Type.Error);
			}
		}
	}
}
