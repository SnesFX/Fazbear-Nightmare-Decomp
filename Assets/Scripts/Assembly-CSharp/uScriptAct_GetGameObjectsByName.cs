using System.Collections.Generic;
using UnityEngine;

[NodePath("Actions/GameObjects")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_GameObjects_By_Name")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2013 by Detox Studios LLC")]
[FriendlyName("Get GameObjects By Name", "Returns the GameObjects in the scene with the specified name.\n\nThe \"Found\" output socket will be triggered if a GameObject matching the name is found, otherwise the \"Not Found\" output socket will be triggered.\n\nWARNING: For performance reasons, this should not be executed every frame. Also, if you know there will only be one result, you should use Get GameObject By Name.")]
[NodeToolTip("Returns the GameObjects in the scene with the specified name.")]
public class uScriptAct_GetGameObjectsByName : uScriptLogic
{
	private bool m_Out;

	private bool m_True;

	public bool Out
	{
		get
		{
			return m_Out;
		}
	}

	[FriendlyName("Found")]
	public bool GameObjectsFound
	{
		get
		{
			return m_True;
		}
	}

	[FriendlyName("Not Found")]
	public bool GameObjectsNotFound
	{
		get
		{
			return !m_True;
		}
	}

	public void In([FriendlyName("Name", "The name of the GameObject(s) you are looking for.")] string Name, [FriendlyName("GameObjects", "Assigns found GameObjects to the attached variable.")] out GameObject[] gameObjects)
	{
		m_Out = false;
		List<GameObject> list = new List<GameObject>();
		GameObject[] array = (GameObject[])Object.FindObjectsOfType(typeof(GameObject));
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if ((bool)gameObject && gameObject.name == Name)
			{
				list.Add(gameObject);
			}
		}
		gameObjects = list.ToArray();
		m_True = gameObjects != null && gameObjects.Length > 0;
		m_Out = true;
	}
}
