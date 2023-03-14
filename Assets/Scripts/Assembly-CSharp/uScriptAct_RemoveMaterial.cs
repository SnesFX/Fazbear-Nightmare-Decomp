using System;
using System.Collections.Generic;
using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Remove Material", "Remove an existing Material at the specified index on the target GameObject.")]
[NodePath("Actions/GameObjects")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Removes an existing Material at the specified index on the target GameObject.")]
public class uScriptAct_RemoveMaterial : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The GameObject(s) to remove the material from.")] GameObject[] Target, [FriendlyName("Material Index", "The index of the material you wish to remove on the Target.")] int materialIndex)
	{
		foreach (GameObject gameObject in Target)
		{
			if (!(null != gameObject))
			{
				continue;
			}
			try
			{
				List<Material> list = new List<Material>();
				Material[] materials = gameObject.GetComponent<Renderer>().materials;
				if (materialIndex < materials.Length && materialIndex > -1)
				{
					Material[] array = materials;
					foreach (Material item in array)
					{
						list.Add(item);
					}
					list.RemoveAt(materialIndex);
					gameObject.GetComponent<Renderer>().materials = list.ToArray();
				}
				else
				{
					uScriptDebug.Log("(Node = Remove Material) The index supplied is outside the material index range on the specified Target GameObject (" + gameObject.name + ").");
				}
			}
			catch (Exception ex)
			{
				uScriptDebug.Log("(Node = Remove Material) Could not remove the material on '" + gameObject.name + "'. " + ex.ToString(), uScriptDebug.Type.Error);
			}
		}
	}
}
