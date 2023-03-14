using System;
using System.Collections.Generic;
using UnityEngine;

namespace RAIN.Navigation.NavMesh.Collectors
{
	public class ColliderMesh : IDisposable
	{
		private static Mesh _cubeMesh = null;

		private static Mesh _sphereMesh = null;

		private static Mesh _capsuleMesh = null;

		private Mesh _unityMesh;

		private ushort[] _volumeID;

		private string[] _tag;

		private static ushort _globalVolumeID = 1;

		public Mesh UnityMesh
		{
			get
			{
				return _unityMesh;
			}
		}

		public ushort[] VolumeID
		{
			get
			{
				return _volumeID;
			}
		}

		public string[] Tag
		{
			get
			{
				return _tag;
			}
		}

		public static ushort GetVolumeID()
		{
			return _globalVolumeID++;
		}

		public static ColliderMesh ConvertCollider(Collider aCollider, Matrix4x4 aTransform)
		{
			if (aCollider is BoxCollider)
			{
				return ConvertBoxCollider((BoxCollider)aCollider, aTransform);
			}
			if (aCollider is SphereCollider)
			{
				return ConvertSphereCollider((SphereCollider)aCollider, aTransform);
			}
			if (aCollider is CapsuleCollider)
			{
				return ConvertCapsuleCollider((CapsuleCollider)aCollider, aTransform);
			}
			if (aCollider is CharacterController)
			{
				return ConvertCharacterController((CharacterController)aCollider, aTransform);
			}
			if (aCollider is MeshCollider)
			{
				return ConvertMeshCollider((MeshCollider)aCollider, aTransform);
			}
			Debug.LogWarning("ColliderCollector: unsupported collider '" + aCollider.ToString() + "'");
			return null;
		}

		public static ColliderMesh ConvertBoxCollider(BoxCollider aCollider, Matrix4x4 aTransform)
		{
			aTransform *= Matrix4x4.TRS(aCollider.center, Quaternion.identity, Vector3.one);
			ColliderMesh colliderMesh = CreateCube(GetVolumeID(), aCollider.gameObject.tag);
			colliderMesh.MultVertices(Matrix4x4.Scale(aCollider.size));
			colliderMesh.MultVertices(aTransform);
			return colliderMesh;
		}

		public static ColliderMesh ConvertSphereCollider(SphereCollider aCollider, Matrix4x4 aTransform)
		{
			float magnitude = aTransform.GetColumn(0).magnitude;
			float magnitude2 = aTransform.GetColumn(1).magnitude;
			float magnitude3 = aTransform.GetColumn(2).magnitude;
			aTransform *= Matrix4x4.Inverse(Matrix4x4.Scale(new Vector3(magnitude, magnitude2, magnitude3)));
			aTransform *= Matrix4x4.TRS(aCollider.center, Quaternion.identity, Vector3.one);
			float num = aCollider.radius * Mathf.Max(magnitude, magnitude2, magnitude3);
			ColliderMesh colliderMesh = CreateSphere(GetVolumeID(), aCollider.gameObject.tag);
			colliderMesh.MultVertices(Matrix4x4.Scale(Vector3.one * num * 2f));
			colliderMesh.MultVertices(aTransform);
			return colliderMesh;
		}

		public static ColliderMesh ConvertCapsuleCollider(CapsuleCollider aCollider, Matrix4x4 aTransform)
		{
			float magnitude = aTransform.GetColumn(0).magnitude;
			float magnitude2 = aTransform.GetColumn(1).magnitude;
			float magnitude3 = aTransform.GetColumn(2).magnitude;
			aTransform *= Matrix4x4.Inverse(Matrix4x4.Scale(new Vector3(magnitude, magnitude2, magnitude3)));
			float x = ((aCollider.direction == 2) ? 90 : 0);
			float z = ((aCollider.direction == 0) ? 90 : 0);
			aTransform *= Matrix4x4.TRS(aCollider.center, Quaternion.Euler(x, 0f, z), Vector3.one);
			float aRadius = aCollider.radius * Mathf.Max(magnitude, magnitude3);
			float aHeight = aCollider.height * magnitude2;
			return CreateCapsule(aCollider, aTransform, aRadius, aHeight);
		}

		public static ColliderMesh ConvertCharacterController(CharacterController aCollider, Matrix4x4 aTransform)
		{
			float magnitude = aTransform.GetColumn(0).magnitude;
			float magnitude2 = aTransform.GetColumn(1).magnitude;
			float magnitude3 = aTransform.GetColumn(2).magnitude;
			aTransform *= Matrix4x4.Inverse(Matrix4x4.Scale(new Vector3(magnitude, magnitude2, magnitude3)));
			aTransform *= Matrix4x4.TRS(aCollider.center, Quaternion.identity, Vector3.one);
			float aRadius = aCollider.radius * Mathf.Max(magnitude, magnitude3);
			float aHeight = aCollider.height * magnitude2;
			return CreateCapsule(aCollider, aTransform, aRadius, aHeight);
		}

		public static ColliderMesh ConvertMeshCollider(MeshCollider aCollider, Matrix4x4 aTransform)
		{
			if (aCollider.sharedMesh == null)
			{
				return null;
			}
			ColliderMesh colliderMesh = new ColliderMesh(aCollider.sharedMesh, 0, aCollider.gameObject.tag);
			colliderMesh.MultVertices(aTransform);
			return colliderMesh;
		}

		public static ColliderMesh CreateCube(ushort aVolumeID, string aTag)
		{
			if (_cubeMesh == null)
			{
				GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
				_cubeMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
				if (Application.isEditor)
				{
					UnityEngine.Object.DestroyImmediate(gameObject);
				}
				else
				{
					UnityEngine.Object.Destroy(gameObject);
				}
			}
			return new ColliderMesh(_cubeMesh, aVolumeID, aTag);
		}

		public static ColliderMesh CreateSphere(ushort aVolumeID, string aTag)
		{
			if (_sphereMesh == null)
			{
				GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				_sphereMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
				if (Application.isEditor)
				{
					UnityEngine.Object.DestroyImmediate(gameObject);
				}
				else
				{
					UnityEngine.Object.Destroy(gameObject);
				}
			}
			return new ColliderMesh(_sphereMesh, aVolumeID, aTag);
		}

		public static ColliderMesh CreateCapsule(ushort aVolumeID, string aTag)
		{
			if (_capsuleMesh == null)
			{
				GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
				_capsuleMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
				if (Application.isEditor)
				{
					UnityEngine.Object.DestroyImmediate(gameObject);
				}
				else
				{
					UnityEngine.Object.Destroy(gameObject);
				}
			}
			return new ColliderMesh(_capsuleMesh, aVolumeID, aTag);
		}

		public static ColliderMesh CreateCapsule(Collider aCollider, Matrix4x4 aTransform, float aRadius, float aHeight)
		{
			ColliderMesh colliderMesh = CreateCapsule(GetVolumeID(), aCollider.gameObject.tag);
			float num = Mathf.Max(1f, aHeight / (aRadius * 2f)) - 2f;
			Vector3[] vertices = colliderMesh.UnityMesh.vertices;
			for (int i = 0; i < vertices.Length; i++)
			{
				if (vertices[i].y > 0f)
				{
					vertices[i].y = Mathf.Max(vertices[i].y + num * 0.5f, 0f);
				}
				else if (vertices[i].y < 0f)
				{
					vertices[i].y = Mathf.Min(vertices[i].y - num * 0.5f, 0f);
				}
			}
			colliderMesh.UnityMesh.vertices = vertices;
			Vector3 v = new Vector3(aRadius * 2f, aRadius * 2f, aRadius * 2f);
			colliderMesh.MultVertices(Matrix4x4.Scale(v));
			colliderMesh.MultVertices(aTransform);
			return colliderMesh;
		}

		public ColliderMesh(Mesh aSharedMesh, ushort aVolumeID, string aTag)
		{
			_unityMesh = new Mesh();
			_unityMesh.vertices = aSharedMesh.vertices;
			_unityMesh.uv = aSharedMesh.uv;
			_unityMesh.uv2 = aSharedMesh.uv2;
			_unityMesh.subMeshCount = aSharedMesh.subMeshCount;
			for (int i = 0; i < aSharedMesh.subMeshCount; i++)
			{
				_unityMesh.SetTriangles(aSharedMesh.GetTriangles(i), i);
			}
			_unityMesh.RecalculateNormals();
			_unityMesh.RecalculateBounds();
			_volumeID = new ushort[_unityMesh.subMeshCount];
			for (int j = 0; j < _volumeID.Length; j++)
			{
				_volumeID[j] = aVolumeID;
			}
			_tag = new string[_unityMesh.subMeshCount];
			for (int k = 0; k < _tag.Length; k++)
			{
				_tag[k] = aTag;
			}
		}

		public ColliderMesh(Vector3[] aVertices, Vector3[] aNormals, int[] aTriangles, ushort aVolumeID, string aTag)
		{
			_unityMesh = new Mesh();
			_unityMesh.vertices = aVertices;
			_unityMesh.normals = aNormals;
			_unityMesh.uv = new Vector2[aVertices.Length];
			_unityMesh.uv2 = _unityMesh.uv;
			_unityMesh.SetTriangles(aTriangles, 0);
			_unityMesh.RecalculateBounds();
			_volumeID = new ushort[1] { aVolumeID };
			_tag = new string[1] { aTag };
		}

		public bool ClipToBounds(Bounds aBounds)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			List<Dictionary<int, int>> list = new List<Dictionary<int, int>>();
			bool flag = true;
			bool flag2 = true;
			Vector3[] vertices = _unityMesh.vertices;
			List<int[]> list2 = new List<int[]>();
			for (int i = 0; i < _unityMesh.subMeshCount; i++)
			{
				list.Add(new Dictionary<int, int>());
				list2.Add(_unityMesh.GetTriangles(i));
				if (_volumeID[i] == 0)
				{
					for (int j = 0; j < list2[i].Length; j += 3)
					{
						Vector3 vector = Vector3.Max(vertices[list2[i][j]], Vector3.Max(vertices[list2[i][j + 1]], vertices[list2[i][j + 2]]));
						Vector3 vector2 = Vector3.Min(vertices[list2[i][j]], Vector3.Min(vertices[list2[i][j + 1]], vertices[list2[i][j + 2]]));
						if (aBounds.Intersects(new Bounds((vector + vector2) / 2f, vector - vector2)))
						{
							list[i].Add(j, list[i].Count);
							for (int k = 0; k < 3; k++)
							{
								if (!dictionary.ContainsKey(list2[i][j + k]))
								{
									dictionary.Add(list2[i][j + k], dictionary.Count);
								}
							}
							flag2 = false;
						}
						else
						{
							flag = false;
						}
					}
					continue;
				}
				bool flag3 = false;
				for (int l = 0; l < list2[i].Length; l += 3)
				{
					Vector3 vector3 = Vector3.Max(vertices[list2[i][l]], Vector3.Max(vertices[list2[i][l + 1]], vertices[list2[i][l + 2]]));
					Vector3 vector4 = Vector3.Min(vertices[list2[i][l]], Vector3.Min(vertices[list2[i][l + 1]], vertices[list2[i][l + 2]]));
					if (aBounds.Intersects(new Bounds((vector3 + vector4) / 2f, vector3 - vector4)))
					{
						flag3 = true;
						break;
					}
				}
				if (flag3)
				{
					for (int m = 0; m < list2[i].Length; m += 3)
					{
						list[i].Add(m, list[i].Count);
						for (int n = 0; n < 3; n++)
						{
							if (!dictionary.ContainsKey(list2[i][m + n]))
							{
								dictionary.Add(list2[i][m + n], dictionary.Count);
							}
						}
					}
					flag2 = false;
				}
				else
				{
					flag = false;
				}
			}
			if (flag2)
			{
				return false;
			}
			if (flag)
			{
				return true;
			}
			Vector2[] uv = _unityMesh.uv;
			Vector2[] uv2 = _unityMesh.uv2;
			Vector3[] array = new Vector3[dictionary.Count];
			Vector2[] array2 = new Vector2[0];
			if (uv.Length > 0)
			{
				array2 = new Vector2[dictionary.Count];
			}
			Vector2[] array3 = new Vector2[0];
			if (uv2.Length > 0)
			{
				array3 = new Vector2[dictionary.Count];
			}
			foreach (int key in dictionary.Keys)
			{
				array[dictionary[key]] = vertices[key];
				if (uv.Length > 0)
				{
					array2[dictionary[key]] = uv[key];
				}
				if (uv2.Length > 0)
				{
					array3[dictionary[key]] = uv2[key];
				}
			}
			_unityMesh.Clear();
			_unityMesh.vertices = array;
			_unityMesh.uv = array2;
			_unityMesh.uv2 = array3;
			List<ushort> list3 = new List<ushort>();
			List<string> list4 = new List<string>();
			int num = 0;
			for (int num2 = 0; num2 < list.Count; num2++)
			{
				if (list[num2].Count != 0)
				{
					num++;
				}
			}
			_unityMesh.subMeshCount = num;
			int num3 = 0;
			for (int num4 = 0; num4 < list.Count; num4++)
			{
				if (list[num4].Count == 0)
				{
					continue;
				}
				int[] array4 = new int[list[num4].Count * 3];
				foreach (int key2 in list[num4].Keys)
				{
					array4[list[num4][key2] * 3] = dictionary[list2[num4][key2]];
					array4[list[num4][key2] * 3 + 1] = dictionary[list2[num4][key2 + 1]];
					array4[list[num4][key2] * 3 + 2] = dictionary[list2[num4][key2 + 2]];
				}
				_unityMesh.SetTriangles(array4, num3);
				list3.Add(_volumeID[num4]);
				list4.Add(_tag[num4]);
				num3++;
			}
			_unityMesh.RecalculateNormals();
			_unityMesh.RecalculateBounds();
			_volumeID = list3.ToArray();
			_tag = list4.ToArray();
			return true;
		}

		public void MultVertices(Matrix4x4 aTransform)
		{
			Vector3[] vertices = _unityMesh.vertices;
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i] = aTransform.MultiplyPoint(vertices[i]);
			}
			_unityMesh.vertices = vertices;
			_unityMesh.RecalculateNormals();
			_unityMesh.RecalculateBounds();
		}

		public void Dispose()
		{
			if (Application.isEditor)
			{
				UnityEngine.Object.DestroyImmediate(_unityMesh);
			}
			else
			{
				UnityEngine.Object.Destroy(_unityMesh);
			}
		}

		private ColliderMesh()
		{
		}

		private ColliderMesh(Mesh aInternalMesh, ushort[] aVolumeID, string[] aTag)
		{
			_unityMesh = aInternalMesh;
			_volumeID = aVolumeID;
			_tag = aTag;
		}
	}
}
