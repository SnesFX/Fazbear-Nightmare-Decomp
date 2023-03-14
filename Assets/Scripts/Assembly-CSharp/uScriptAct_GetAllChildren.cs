using System.Collections.Generic;
using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Returns all the children GameObjects of a parent GameObject.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/GameObjects")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_All_Children")]
[FriendlyName("Get All Children", "Returns all the child GameObjects of a parent GameObject.\n\n\"Children Found\" will fire if one (or more) child GameObject is found, otherwise \"Children Not Found\" will fire.")]
public class uScriptAct_GetAllChildren : uScriptLogic
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

	[FriendlyName("Children Found")]
	public bool ChildrenFound
	{
		get
		{
			return m_True;
		}
	}

	[FriendlyName("Children Not Found")]
	public bool ChildrenNotFound
	{
		get
		{
			return !m_True;
		}
	}

	public void In([FriendlyName("Target", "The parent GameObject you wish to search for children GameObjects on.")] GameObject Target, [SocketState(false, false)][DefaultValue(false)][FriendlyName("Search In Children", "Whether or not to return children of children.")] bool recursive, [FriendlyName("First Child", "The first child in the list of Children.")] out GameObject FirstChild, [FriendlyName("Children", "Assigns found child GameObjects to the attached variable.")] out GameObject[] Children, [SocketState(false, false)][FriendlyName("Children Count", "Sets the total number of child GameObjects found to the attached variable.")] out int ChildrenCount)
	{
		m_Out = false;
		m_True = false;
		List<GameObject> list = new List<GameObject>();
		if (null != Target)
		{
			list.AddRange(GetChildren(recursive, Target));
			ChildrenCount = list.Count;
			Children = list.ToArray();
			m_True = ChildrenCount > 0;
			if (m_True)
			{
				FirstChild = Children[0];
			}
			else
			{
				FirstChild = null;
			}
		}
		else
		{
			uScriptDebug.Log("(Node - Get All Children): The specified Target GameObject could not be found (was null). Did you specify a valid GameObject?", uScriptDebug.Type.Warning);
			FirstChild = null;
			Children = null;
			ChildrenCount = 0;
		}
		m_Out = true;
	}

	private GameObject[] GetChildren(bool recursive, GameObject Target)
	{
		List<GameObject> list = new List<GameObject>();
		foreach (Transform item in Target.transform)
		{
			if (recursive)
			{
				list.AddRange(GetChildren(recursive, item.gameObject));
			}
			list.Add(item.gameObject);
		}
		return list.ToArray();
	}
}
