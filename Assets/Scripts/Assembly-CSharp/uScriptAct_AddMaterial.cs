using System;
using System.Collections.Generic;
using UnityEngine;

[NodePath("Actions/GameObjects")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Add Material", "Adds a new Material to the existing set of materials on the target GameObject. This new material will be appended to the GameObject's existing material channel array and be at the last index position.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Adds a new Material to the existing set of materials on the target GameObject.")]
public class uScriptAct_AddMaterial : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The GameObject(s) to assign add the new material to.")] GameObject[] Target, [FriendlyName("Material", "The material(s) to add to the Target. If material channel index order is important and you are adding more than one material at once, use a Material List or add one material at a time.")] Material[] materialName, [FriendlyName("Index", "Returns the index to which the new material was assigned in the Materials array. If adding the material to more than one GameObject at a time, this will return the index from the last GameObject to have the material added.")][SocketState(false, false)] out int materialIndex)
	{
		int num = -1;
		foreach (GameObject gameObject in Target)
		{
			if (!(null != gameObject))
			{
				continue;
			}
			try
			{
				foreach (Material material in materialName)
				{
					if (null != material)
					{
						List<Material> list = new List<Material>();
						Material[] materials = gameObject.GetComponent<Renderer>().materials;
						list.AddRange(materials);
						Material item = new Material(material);
						list.Add(item);
						gameObject.GetComponent<Renderer>().materials = list.ToArray();
						num = gameObject.GetComponent<Renderer>().materials.Length - 1;
					}
				}
			}
			catch (Exception ex)
			{
				uScriptDebug.Log("(Node = Add Material) Could not add the material to '" + gameObject.name + "'. Returning -1 for the Index if this was the last GameObject to have the material added." + ex.ToString(), uScriptDebug.Type.Error);
				num = -1;
			}
		}
		materialIndex = num;
	}
}
