using UnityEngine;

[NodePath("Actions/GameObjects")]
[NodeToolTip("Returns the GameObject in the scene with the specified name.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_GameObject_By_Name")]
[FriendlyName("Get GameObject By Name", "Returns the GameObject in the scene with the specified name.\n\nThe \"Found\" output socket will be triggered if a GameObject matching the name is found, otherwise the \"Not Found\" output socket will be triggered.\n\nWARNING: For performance reasons, this should not be executed every frame.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_GetGameObjectByName : uScriptLogic
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
	public bool GameObjectFound
	{
		get
		{
			return m_True;
		}
	}

	[FriendlyName("Not Found")]
	public bool GameObjectNotFound
	{
		get
		{
			return !m_True;
		}
	}

	public void In([FriendlyName("Name", "The name of the GameObject you are looking for.")] string Name, [FriendlyName("GameObject", "Assigns found GameObject to the attached variable.")] out GameObject gameObject)
	{
		m_Out = false;
		gameObject = GameObject.Find(Name);
		m_True = gameObject != null;
		m_Out = true;
	}
}
