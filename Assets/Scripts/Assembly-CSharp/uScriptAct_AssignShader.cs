using UnityEngine;

[NodePath("Actions/GameObjects")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Assigns the specified Shader to the GameObject.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Assign_Shader")]
[FriendlyName("Assign Shader", "Assigns the specified Shader (by name) to the GameObject on the specifed material.")]
public class uScriptAct_AssignShader : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([RequiresLink][FriendlyName("Material", "The material of the object to assign the shader to.")][SocketState(false, false)] Material material, [RequiresLink][FriendlyName("Shader", "The shader to assign.")] Shader shader)
	{
		material.shader = shader;
	}
}
