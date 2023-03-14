using System;
using UnityEngine;

[NodeToolTip("Assigns the Material color of the target GameObject on the specifed material channel.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Assign_Material_Color")]
[FriendlyName("Assign Material Color", "Assigns the Material color of the target GameObject on the specifed material channel.")]
[NodePath("Actions/GameObjects")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_AssignMaterialColor : uScriptLogic
{
	private Material m_NewMaterial;

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The GameObject(s) to assign the material color to.")] GameObject[] Target, [FriendlyName("Color", "The material color to assign to the Target object(s).")] Color MatColor, [SocketState(false, false)][FriendlyName("Material Channel", "The material channel of the object to assign the material color to.")] int MatChannel)
	{
		try
		{
			foreach (GameObject gameObject in Target)
			{
				gameObject.GetComponent<Renderer>().materials[MatChannel].color = MatColor;
			}
		}
		catch (Exception ex)
		{
			uScriptDebug.Log("(Node = Assign Material Color) Error output: " + ex.ToString(), uScriptDebug.Type.Error);
		}
	}
}
