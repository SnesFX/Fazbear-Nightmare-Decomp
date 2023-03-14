using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Create_Primitive_GameObject")]
[NodeToolTip("Creates a GameObject with a primitive mesh renderer and appropriate collider.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Create Primitive", "Creates a GameObject with a primitive mesh renderer and appropriate collider.")]
[NodePath("Actions/GameObjects")]
public class uScriptAct_CreatePrimitive : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Name", "The name of the new GameObject.")][DefaultValue("Primitive")] string Name, [DefaultValue(PrimitiveType.Cube)][FriendlyName("Primitive", "The type of primitive mesh for the GameObject.")][SocketState(false, false)] PrimitiveType Primitive, [FriendlyName("Location", "The location to place the new GameObject.")] Vector3 Location, [FriendlyName("GameObject", "The newly created GameObject")] out GameObject NewGameObject)
	{
		NewGameObject = GameObject.CreatePrimitive(Primitive);
		if (string.Empty != Name)
		{
			NewGameObject.name = Name;
		}
		NewGameObject.transform.position = Location;
	}
}
