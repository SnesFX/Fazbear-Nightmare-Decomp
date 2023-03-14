using System;
using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Spawn_Prefab")]
[NodeToolTip("Create an instance of a Prefab at the specified spawn point.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Spawn Prefab", "Create (instantiate) an instance of a Prefab at the specified spawn point GameObject at runtime (must be in the Resources folder structure).")]
[NodePath("Actions/GameObjects")]
public class uScriptAct_SpawnPrefab : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private bool m_FinishedSpawning;

	public bool Immediate
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Finished Spawning")]
	public event uScriptEventHandler FinishedSpawning;

	public void In([FriendlyName("Prefab Name", "The name of the prefab to spawn.")] string PrefabName, [SocketState(false, false)][FriendlyName("Resource Path", "The resource path to look in for the prefab.")] string ResourcePath, [FriendlyName("Spawn Point", "The GameObject to spawn prefab(s) from.")] GameObject SpawnPoint, [FriendlyName("Spawned Name", "The name given to newly spawned prefab(s).")] string SpawnedName, [FriendlyName("Location Offset", "The offset from the Spawn Point to spawn prefab(s) from.")][SocketState(false, false)] Vector3 LocationOffset, [FriendlyName("Spawned GameObject", "The GameObject that gets spawned.")] out GameObject SpawnedGameObject, [FriendlyName("Spawned InstancedID", "The instance ID of the spawned GameObject.")][SocketState(false, false)] out int SpawnedInstancedID)
	{
		Vector3 position = SpawnPoint.transform.position + LocationOffset;
		Quaternion rotation = SpawnPoint.transform.rotation;
		if (!string.IsNullOrEmpty(ResourcePath))
		{
			if (ResourcePath.Contains("\\"))
			{
				ResourcePath = ResourcePath.Replace("\\", "/");
			}
			if (ResourcePath.StartsWith("/") || ResourcePath.StartsWith("\\"))
			{
				ResourcePath = ResourcePath.Remove(0, 1);
			}
			if (ResourcePath.EndsWith("/") || ResourcePath.EndsWith("\\"))
			{
				int startIndex = ResourcePath.Length - 1;
				ResourcePath = ResourcePath.Remove(startIndex, 1);
			}
			if (ResourcePath.StartsWith("Assets") || ResourcePath.StartsWith("assets"))
			{
				ResourcePath = ResourcePath.Remove(0, "Assets".Length);
			}
			if (ResourcePath.StartsWith("Resources") || ResourcePath.StartsWith("resources"))
			{
				ResourcePath = ResourcePath.Remove(0, "Resources".Length);
			}
		}
		if (!string.IsNullOrEmpty(PrefabName))
		{
			if (PrefabName.Contains("\\"))
			{
				PrefabName = PrefabName.Replace("\\", "/");
			}
			if (PrefabName.StartsWith("/") || PrefabName.StartsWith("\\"))
			{
				PrefabName = PrefabName.Remove(0, 1);
			}
			if (PrefabName.EndsWith("/") || PrefabName.EndsWith("\\"))
			{
				int startIndex2 = PrefabName.Length - 1;
				PrefabName = PrefabName.Remove(startIndex2, 1);
			}
		}
		string empty = string.Empty;
		empty = (string.IsNullOrEmpty(ResourcePath) ? PrefabName : (ResourcePath + "/" + PrefabName));
		try
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load(empty), position, rotation) as GameObject;
			if (!string.IsNullOrEmpty(SpawnedName))
			{
				gameObject.name = SpawnedName;
			}
			SpawnedGameObject = gameObject;
			SpawnedInstancedID = gameObject.GetInstanceID();
			m_FinishedSpawning = true;
		}
		catch (Exception ex)
		{
			uScriptDebug.Log(ex.ToString(), uScriptDebug.Type.Error);
			SpawnedGameObject = null;
			SpawnedInstancedID = 0;
		}
	}

	public void Update()
	{
		if (m_FinishedSpawning)
		{
			m_FinishedSpawning = false;
			if (this.FinishedSpawning != null)
			{
				this.FinishedSpawning(this, new EventArgs());
			}
		}
	}
}
