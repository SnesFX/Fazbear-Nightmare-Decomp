using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Set Layer", "Sets the layer for the target GameObjects.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Sets the layer for the target GameObjects.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_GameObject_By_Name")]
[NodePath("Actions/GameObjects")]
public class uScriptAct_SetLayer : uScriptLogic
{
	private bool m_ApplyToChildren;

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The GameObject(s) you wish to set the layer for.")] GameObject[] Target, [FriendlyName("Layer", "The Layer you wish to set the Target(s) to.")] LayerMask Layer, [SocketState(false, false)][DefaultValue(true)][FriendlyName("Apply To Children", "Specify if the Layer should also be assigned to any children GameObjects of the Target if found.")] bool ApplyToChildren)
	{
		m_ApplyToChildren = ApplyToChildren;
		int i = 0;
		if (Layer.value > 0)
		{
			for (i = 0; i < 32 && ((Layer.value >> i) & 1) == 0; i++)
			{
			}
		}
		foreach (GameObject gameObject in Target)
		{
			Transform transform = gameObject.transform;
			SetGameObjectLayer(transform, i);
		}
	}

	private void SetGameObjectLayer(Transform obj, int newLayer)
	{
		obj.gameObject.layer = newLayer;
		if (!m_ApplyToChildren)
		{
			return;
		}
		foreach (Transform item in obj)
		{
			SetGameObjectLayer(item, newLayer);
		}
	}
}
