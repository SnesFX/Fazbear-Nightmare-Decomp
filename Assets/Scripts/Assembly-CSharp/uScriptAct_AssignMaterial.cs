using System;
using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Assigns the specified Material to the GameObject.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Assign_Material")]
[FriendlyName("Assign Material", "Assigns the specified Material to the GameObject on the specifed material channel.")]
[NodePath("Actions/GameObjects")]
public class uScriptAct_AssignMaterial : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The GameObject(s) to assign the material to.")] GameObject[] Target, [FriendlyName("Material", "The material to assign.")] Material materialName, [FriendlyName("Material Channel", "The material channel of the object to assign the material to.")][SocketState(false, false)] int MatChannel)
	{
		foreach (GameObject gameObject in Target)
		{
			try
			{
				Material[] materials = gameObject.GetComponent<Renderer>().materials;
				materials[MatChannel] = materialName;
				gameObject.GetComponent<Renderer>().materials = materials;
			}
			catch (Exception ex)
			{
				uScriptDebug.Log("(Node = Assign Material) Could not assign the material to '" + gameObject.name + "'. Please verify you are assigning to a valid material channel.\nError output: " + ex.ToString(), uScriptDebug.Type.Error);
			}
		}
	}
}
