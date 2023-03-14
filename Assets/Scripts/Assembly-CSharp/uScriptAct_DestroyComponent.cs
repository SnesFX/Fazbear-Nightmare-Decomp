using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Destroy_Component")]
[FriendlyName("Destroy Component", "Removes the specified Component from the target GameObject.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/GameObjects")]
[NodeToolTip("Removes the specified Component from the target GameObject.")]
public class uScriptAct_DestroyComponent : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The target GameObject(s) that will be affected.")] GameObject[] Target, [FriendlyName("Component Name", "The name of the component(s) to destroy from all target GameObject(s).")] string[] ComponentName, [FriendlyName("Delay", "The time to wait before destroying the target component(s).")][SocketState(false, false)] float DelayTime)
	{
		foreach (GameObject gameObject in Target)
		{
			if (!(gameObject != null))
			{
				continue;
			}
			foreach (string text in ComponentName)
			{
				Component component = gameObject.GetComponent(text);
				if (component != null)
				{
					if (DelayTime > 0f)
					{
						Object.Destroy(component, DelayTime);
					}
					else
					{
						Object.Destroy(component);
					}
					continue;
				}
				uScriptDebug.Log("Component '" + text + "' not found on GameObject '" + gameObject.name + "'.", uScriptDebug.Type.Warning);
			}
		}
	}
}
