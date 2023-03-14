using System;
using System.Collections.Generic;
using UnityEngine;

namespace RAIN.Utility
{
	[ExecuteInEditMode]
	public class MeshHolder : MonoBehaviour
	{
		[Serializable]
		private class ObjectMeshList
		{
			public UnityEngine.Object Object;

			public string ListName = "";

			public List<Mesh> Meshes = new List<Mesh>();
		}

		[SerializeField]
		private List<ObjectMeshList> _objectMeshes = new List<ObjectMeshList>();

		private ObjectMeshList _lastObjectMesh;

		private static MeshHolder _instance;

		public static MeshHolder Instance
		{
			get
			{
				if (_instance == null)
				{
					MeshHolder[] array = Resources.FindObjectsOfTypeAll<MeshHolder>();
					if (array.Length > 0)
					{
						_instance = array[0];
					}
					if (_instance == null)
					{
						GameObject gameObject = new GameObject("MeshHolder");
						gameObject.hideFlags = HideFlags.HideAndDontSave;
						_instance = gameObject.AddComponent<MeshHolder>();
					}
				}
				return _instance;
			}
		}

		public IList<Mesh> GetMeshList(UnityEngine.Object aObject, string aListName)
		{
			GarbageCollect();
			return GetInstanceMesh(aObject, aListName).Meshes.AsReadOnly();
		}

		public void Add(UnityEngine.Object aObject, string aListName, Mesh aMesh)
		{
			GetInstanceMesh(aObject, aListName).Meshes.Add(aMesh);
		}

		public void AddRange(UnityEngine.Object aObject, string aListName, List<Mesh> aMeshes)
		{
			GetInstanceMesh(aObject, aListName).Meshes.AddRange(aMeshes);
		}

		public void Clear(UnityEngine.Object aObject, string aListName)
		{
			CleanupList(GetInstanceMesh(aObject, aListName));
		}

		private void OnDestroy()
		{
			for (int i = 0; i < _objectMeshes.Count; i++)
			{
				CleanupList(_objectMeshes[i]);
			}
		}

		private void GarbageCollect()
		{
			for (int i = 0; i < _objectMeshes.Count; i++)
			{
				if (!_objectMeshes[i].Object)
				{
					CleanupList(_objectMeshes[i]);
					_objectMeshes.RemoveAt(i--);
				}
			}
		}

		private void CleanupList(ObjectMeshList aMeshList)
		{
			for (int i = 0; i < aMeshList.Meshes.Count; i++)
			{
				if (Application.isEditor)
				{
					UnityEngine.Object.DestroyImmediate(aMeshList.Meshes[i]);
				}
				else
				{
					UnityEngine.Object.Destroy(aMeshList.Meshes[i]);
				}
			}
			aMeshList.Meshes.Clear();
		}

		private ObjectMeshList GetInstanceMesh(UnityEngine.Object aObject, string aListName)
		{
			if (_lastObjectMesh != null && (bool)_lastObjectMesh.Object && _lastObjectMesh.Object == aObject)
			{
				return _lastObjectMesh;
			}
			for (int i = 0; i < _objectMeshes.Count; i++)
			{
				if ((bool)_objectMeshes[i].Object && _objectMeshes[i].Object == aObject && _objectMeshes[i].ListName == aListName)
				{
					return _objectMeshes[i];
				}
			}
			_lastObjectMesh = new ObjectMeshList
			{
				Object = aObject,
				ListName = aListName
			};
			_objectMeshes.Add(_lastObjectMesh);
			return _lastObjectMesh;
		}
	}
}
