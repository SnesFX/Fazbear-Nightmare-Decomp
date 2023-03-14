using System;
using UnityEngine;

[NodePath("Actions/GameObjects")]
[FriendlyName("Spawn Child Prefab At Location", "Create (instantiate) an instance of a Prefab as a child of the parent GameObject at the specified Vector3 location at runtime (must be in the Resources folder structure).")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2012 by Detox Studios LLC")]
[NodeToolTip("Create an instance of a Prefab as a child of the parent GameObject at the specified spawn point.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
public class uScriptAct_SpawnChildPrefabAtLocation : uScriptLogic
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

	public void In([FriendlyName("Prefab Name", "The name of the prefab to spawn.")] string PrefabName, [FriendlyName("Resource Path", "The resource path to look in for the prefab.")][SocketState(false, false)] string ResourcePath, [FriendlyName("Spawn Position", "The position to spawn prefab(s) from.")] Vector3 SpawnPosition, [SocketState(false, false)][FriendlyName("Spawn Rotation", "The rotation to spawn prefab(s) from.")] Quaternion SpawnRotation, [FriendlyName("Spawned Name", "The name given to newly spawned prefab(s).")][SocketState(false, false)] string SpawnedName, [FriendlyName("Parent", "The parent GameObject you wish the newly spawned GameObject to be a child of. If left blank, the spawned GameObject will have no parent.")] GameObject Parent, [FriendlyName("Spawned GameObject", "The GameObject that gets spawned.")] out GameObject SpawnedGameObject, [FriendlyName("Spawned InstancedID", "The instance ID of the spawned GameObject.")][SocketState(false, false)] out int SpawnedInstancedID)
	{
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
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load(empty), SpawnPosition, SpawnRotation) as GameObject;
			if (null != Parent)
			{
				gameObject.transform.parent = Parent.transform;
			}
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
