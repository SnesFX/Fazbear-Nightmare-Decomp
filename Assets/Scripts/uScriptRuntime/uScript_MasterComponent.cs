using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

[ExecuteInEditMode]
public class uScript_MasterComponent : MonoBehaviour
{
	private Hashtable m_BreakpointCache;

	private static GameObject m_LatestMaster;

	private static uScript_MasterComponent m_LatestMasterComponent;

	public static string Version = "1.CMR";

	public Hashtable m_Values = new Hashtable();

	[HideInInspector]
	public string[] m_Breakpoints = new string[0];

	[HideInInspector]
	public string CurrentBreakpoint = "";

	[HideInInspector]
	public GameObject[] GameObjects = new GameObject[0];

	[HideInInspector]
	public string[] GameObjectGuids = new string[0];

	[HideInInspector]
	public string[] m_uScriptsToAttach = new string[0];

	private Hashtable m_Types = new Hashtable();

	public static GameObject LatestMaster
	{
		get
		{
			if (null == m_LatestMaster)
			{
				m_LatestMaster = GameObject.Find(uScriptRuntimeConfig.MasterObjectName);
			}
			return m_LatestMaster;
		}
	}

	public static uScript_MasterComponent LatestMasterComponent
	{
		get
		{
			if (null == m_LatestMasterComponent && null != LatestMaster)
			{
				m_LatestMasterComponent = LatestMaster.GetComponent<uScript_MasterComponent>();
			}
			return m_LatestMasterComponent;
		}
	}

	public string[] uScriptsToAttach
	{
		get
		{
			return m_uScriptsToAttach;
		}
	}

	public void Awake()
	{
		m_LatestMaster = base.gameObject;
		m_LatestMasterComponent = this;
		GameObject[] array = (GameObject[])UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
		PruneGameObjects();
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			CacheGameObject(gameObject);
		}
	}

	public void UpdateNodeValue(string guid, object value)
	{
		m_Values[guid] = value;
	}

	public object GetNodeValue(string guid)
	{
		return m_Values[guid];
	}

	public bool HasBreakpoint(string guid)
	{
		BuildBreakpointCache(false);
		return m_BreakpointCache.Contains(guid);
	}

	public void AddBreakpoint(string guid)
	{
		BuildBreakpointCache(false);
		if (!m_BreakpointCache.Contains(guid))
		{
			Array.Resize(ref m_Breakpoints, m_Breakpoints.Length + 1);
			m_Breakpoints[m_Breakpoints.Length - 1] = guid;
			BuildBreakpointCache(true);
		}
	}

	public void RemoveBreakpoint(string guid)
	{
		BuildBreakpointCache(false);
		if (!m_BreakpointCache.Contains(guid))
		{
			return;
		}
		m_BreakpointCache.Remove(guid);
		m_Breakpoints = new string[m_BreakpointCache.Keys.Count];
		int num = 0;
		foreach (string key in m_BreakpointCache.Keys)
		{
			m_Breakpoints[num++] = key;
		}
	}

	private void BuildBreakpointCache(bool force)
	{
		if (m_BreakpointCache == null || force)
		{
			m_BreakpointCache = new Hashtable();
			string[] breakpoints = m_Breakpoints;
			foreach (string key in breakpoints)
			{
				m_BreakpointCache[key] = true;
			}
		}
	}

	private string CacheGameObject(GameObject gameObject)
	{
		int i;
		for (i = 0; i < GameObjects.Length; i++)
		{
			if (GameObjects[i] == gameObject)
			{
				return GameObjectGuids[i];
			}
		}
		Array.Resize(ref GameObjects, GameObjects.Length + 1);
		Array.Resize(ref GameObjectGuids, GameObjectGuids.Length + 1);
		GameObjects[i] = gameObject;
		GameObjectGuids[i] = Guid.NewGuid().ToString();
		return GameObjectGuids[i];
	}

	private void PruneGameObjects()
	{
		for (int i = 0; i < GameObjects.Length; i++)
		{
			if (null == GameObjects[i])
			{
				GameObjects[i] = GameObjects[GameObjects.Length - 1];
				GameObjectGuids[i] = GameObjectGuids[GameObjects.Length - 1];
				Array.Resize(ref GameObjects, GameObjects.Length - 1);
				Array.Resize(ref GameObjectGuids, GameObjectGuids.Length - 1);
			}
		}
	}

	public string GetGameObject(string oldName, string referenceGuid)
	{
		for (int i = 0; i < GameObjects.Length; i++)
		{
			if (!(null == GameObjects[i]) && GameObjectGuids[i] == referenceGuid)
			{
				return GameObjects[i].name;
			}
		}
		return oldName;
	}

	public string GetGameObjectGuid(string name)
	{
		for (int i = 0; i < GameObjects.Length; i++)
		{
			if (!(null == GameObjects[i]) && GameObjects[i].name == name)
			{
				return GameObjectGuids[i];
			}
		}
		GameObject gameObject = GameObject.Find(name);
		if (null != gameObject)
		{
			return CacheGameObject(gameObject);
		}
		return "";
	}

	public void ClearAttachList()
	{
		m_uScriptsToAttach = new string[0];
	}

	public void AttachScriptToMaster(string fullPath)
	{
		Array.Resize(ref m_uScriptsToAttach, m_uScriptsToAttach.Length + 1);
		m_uScriptsToAttach[m_uScriptsToAttach.Length - 1] = fullPath;
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawIcon(base.gameObject.transform.position, "uscript_gizmo_master.png");
	}

	public Type GetType(string typeName)
	{
		Type type = m_Types[typeName] as Type;
		if (type == null)
		{
			type = GetAssemblyQualifiedType(typeName);
		}
		return type;
	}

	public Type GetAssemblyQualifiedType(string typeName)
	{
		if (typeName == null)
		{
			return null;
		}
		if (Type.GetType(typeName) != null)
		{
			return Type.GetType(typeName);
		}
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		foreach (Assembly assembly in assemblies)
		{
			if (Type.GetType(typeName + ", " + assembly.ToString()) != null)
			{
				return Type.GetType(typeName + ", " + assembly.ToString());
			}
		}
		return null;
	}

	public void AddType(Type type)
	{
		m_Types[type.ToString()] = type;
	}
}
