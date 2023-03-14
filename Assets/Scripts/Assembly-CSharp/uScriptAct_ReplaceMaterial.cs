using System;
using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Replace Material", "Replaces an existing Material at the specified index on the target GameObject with another Material.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/GameObjects")]
[NodeToolTip("Replaces an existing Material at the specified index on the target GameObject.")]
public class uScriptAct_ReplaceMaterial : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The GameObject(s) to replace the material on.")] GameObject[] Target, [FriendlyName("Material Index", "The index of the material you wish to replace on the Target.")] int materialIndex, [FriendlyName("New Material", "The new material you wish to use.")] Material newMaterial)
	{
		if (null != newMaterial)
		{
			foreach (GameObject gameObject in Target)
			{
				if (!(null != gameObject))
				{
					continue;
				}
				try
				{
					Material[] materials = gameObject.GetComponent<Renderer>().materials;
					if (materialIndex < materials.Length && materialIndex > -1)
					{
						materials[materialIndex] = newMaterial;
						gameObject.GetComponent<Renderer>().materials = materials;
					}
					else
					{
						uScriptDebug.Log("(Node = Replace Material) The index supplied is outside the material index range on the specified Target GameObject (" + gameObject.name + ").");
					}
				}
				catch (Exception ex)
				{
					uScriptDebug.Log("(Node = Replace Material) Could not replace the material on '" + gameObject.name + "'. " + ex.ToString(), uScriptDebug.Type.Error);
				}
			}
		}
		else
		{
			uScriptDebug.Log("(Node = Replace Material) The new material is empty (null), not replacing.");
		}
	}
}
