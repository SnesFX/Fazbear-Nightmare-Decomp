using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Get Material", "Returns a GameObject's material, material color, and material name assigned to the specified material index.")]
[NodePath("Actions/GameObjects")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Returns a GameObject's material, material color, and material name assigned to the specified material index.")]
public class uScriptAct_GetGameObjectMaterial : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The Target GameObject you wish to get the material information from.")] GameObject Target, [DefaultValue(0)][FriendlyName("Material Channel", "The index number of the material you wish to get from the Target. Zero (0) is the default and most common material index.")] int MaterialIndex, [FriendlyName("Material", "Returns the material.")] out Material targetMaterial, [SocketState(false, false)][FriendlyName("Color", "Returns the color of the material.")] out Color materialColor, [SocketState(false, false)][FriendlyName("Name", "Returns the name of the material.")] out string materialName)
	{
		if (Target != null)
		{
			if (MaterialIndex <= Target.GetComponent<Renderer>().materials.Length - 1)
			{
				targetMaterial = Target.GetComponent<Renderer>().materials[MaterialIndex];
				materialColor = Target.GetComponent<Renderer>().materials[MaterialIndex].color;
				materialName = Target.GetComponent<Renderer>().materials[MaterialIndex].name;
			}
			else
			{
				uScriptDebug.Log("The specified Material Channel does not exist on " + Target.name, uScriptDebug.Type.Warning);
				targetMaterial = null;
				materialColor = Color.magenta;
				materialName = string.Empty;
			}
		}
		else
		{
			uScriptDebug.Log("The specified Target GameObject does not exist.", uScriptDebug.Type.Warning);
			targetMaterial = null;
			materialColor = Color.magenta;
			materialName = string.Empty;
		}
	}
}
