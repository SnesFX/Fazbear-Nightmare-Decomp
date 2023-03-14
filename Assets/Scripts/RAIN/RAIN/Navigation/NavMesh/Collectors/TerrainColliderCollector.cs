using System.Collections.Generic;
using RAIN.Navigation.NavMesh.RecastNodes;
using UnityEngine;

namespace RAIN.Navigation.NavMesh.Collectors
{
	public class TerrainColliderCollector : ColliderCollector
	{
		public class TreeData
		{
			private TreeInstance _instance;

			private TreePrototype _prototype;

			private Matrix4x4 _transform;

			private Bounds _bounds;

			public TreeInstance Instance
			{
				get
				{
					return _instance;
				}
				set
				{
					_instance = value;
				}
			}

			public TreePrototype Prototype
			{
				get
				{
					return _prototype;
				}
				set
				{
					_prototype = value;
				}
			}

			public Matrix4x4 Transform
			{
				get
				{
					return _transform;
				}
				set
				{
					_transform = value;
				}
			}

			public Bounds TreeBounds
			{
				get
				{
					return _bounds;
				}
				set
				{
					_bounds = value;
				}
			}
		}

		private List<Terrain> _terrainColliders = new List<Terrain>();

		private List<TreeData> _treeData = new List<TreeData>();

		private int _currentCollider;

		public List<Terrain> TerrainColliders
		{
			get
			{
				return _terrainColliders;
			}
		}

		public List<TreeData> TreeColliders
		{
			get
			{
				return _treeData;
			}
		}

		public override float Progress
		{
			get
			{
				return (float)_currentCollider / (float)ColliderCount;
			}
		}

		public override int ColliderCount
		{
			get
			{
				return _terrainColliders.Count + _treeData.Count;
			}
		}

		public override void InitCollector(float aCellSize, float aMaxSlope, List<string> aUnwalkableTags, List<string> aIgnoredTags, LayerMask aIncludedLayers)
		{
			base.InitCollector(aCellSize, aMaxSlope, aUnwalkableTags, aIgnoredTags, aIncludedLayers);
			Terrain[] array = Object.FindObjectsOfType<Terrain>();
			_terrainColliders.Clear();
			for (int i = 0; i < array.Length; i++)
			{
				if (IsValidTerrainCollider(array[i]))
				{
					_terrainColliders.Add(array[i]);
				}
			}
			_treeData.Clear();
			for (int j = 0; j < _terrainColliders.Count; j++)
			{
				for (int k = 0; k < _terrainColliders[j].terrainData.treeInstances.Length; k++)
				{
					if (IsValidTreeCollider(_terrainColliders[j], k))
					{
						_treeData.Add(CollectTreeData(_terrainColliders[j], k));
					}
				}
			}
			_currentCollider = 0;
		}

		public override bool CollectColliders(Bounds aBounds, int aStart, int aCount, out List<SpanMesh> aSpanMeshes)
		{
			_currentCollider = aStart;
			aSpanMeshes = new List<SpanMesh>();
			while (_currentCollider < _terrainColliders.Count && aCount > 0)
			{
				HeightMesh heightMesh = CollectTerrainCollider(aBounds, _terrainColliders[_currentCollider]);
				if (heightMesh != null)
				{
					aSpanMeshes.Add(heightMesh);
				}
				_currentCollider++;
				aCount--;
			}
			if (_currentCollider < _terrainColliders.Count)
			{
				return true;
			}
			int num = _currentCollider - _terrainColliders.Count;
			while (num < _treeData.Count && aCount > 0)
			{
				if (aBounds.Intersects(_treeData[num].TreeBounds))
				{
					using (ColliderMesh colliderMesh = ConvertTreeData(_treeData[num]))
					{
						if (colliderMesh != null && colliderMesh.ClipToBounds(aBounds))
						{
							aSpanMeshes.Add(new PrimitiveMesh(colliderMesh, base.CellSize, base.UnwalkableTags, base.MaxSlope));
						}
					}
				}
				num++;
				_currentCollider++;
				aCount--;
			}
			return _currentCollider < ColliderCount;
		}

		private bool IsValidTerrainCollider(Terrain aTerrain)
		{
			if (!aTerrain.enabled)
			{
				return false;
			}
			if ((base.IncludedLayers.value & (1 << aTerrain.gameObject.layer)) == 0)
			{
				return false;
			}
			if (base.IgnoredTags.Contains(aTerrain.gameObject.tag))
			{
				return false;
			}
			return true;
		}

		private HeightMesh CollectTerrainCollider(Bounds aBounds, Terrain aTerrain)
		{
			Vector3 vector = (Vector3)Point3.RoundToPoint(aTerrain.transform.position / base.CellSize) * base.CellSize;
			Vector3 vector2 = new Vector3((float)(aTerrain.terrainData.heightmapWidth - 1) * aTerrain.terrainData.heightmapScale.x, aBounds.size.y, (float)(aTerrain.terrainData.heightmapHeight - 1) * aTerrain.terrainData.heightmapScale.z);
			vector2 = (Vector3)Point3.RoundToPoint(vector2 / base.CellSize) * base.CellSize;
			Vector3 vector3 = Vector3.Max(aBounds.min, vector);
			Vector3 max = Vector3.Min(aBounds.max, vector + vector2);
			Bounds bounds = default(Bounds);
			bounds.max = max;
			bounds.min = vector3;
			if (!aBounds.Intersects(bounds))
			{
				return null;
			}
			int num = Mathf.RoundToInt((max.x - vector3.x) / base.CellSize);
			int num2 = Mathf.RoundToInt((max.z - vector3.z) / base.CellSize);
			int[] array = new int[num * num2];
			bool[] array2 = new bool[num * num2];
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					Vector3 worldPosition = new Vector3(vector3.x + (float)i * base.CellSize, vector3.y, vector3.z + (float)j * base.CellSize);
					float x = (worldPosition.x - vector.x) / vector2.x;
					float y = (worldPosition.z - vector.z) / vector2.z;
					array[j * num + i] = Mathf.RoundToInt(aTerrain.SampleHeight(worldPosition) / base.CellSize);
					array2[j * num + i] = aTerrain.terrainData.GetSteepness(x, y) < base.MaxSlope;
				}
			}
			return new HeightMesh(Point3.RoundToPoint(vector3 / base.CellSize), num, num2, array, array2, aTerrain.tag, 0);
		}

		private bool IsValidTreeCollider(Terrain aTerrain, int aTreeIndex)
		{
			TreeInstance treeInstance = aTerrain.terrainData.treeInstances[aTreeIndex];
			TreePrototype treePrototype = aTerrain.terrainData.treePrototypes[treeInstance.prototypeIndex];
			CapsuleCollider capsuleCollider = treePrototype.prefab.GetComponent<Collider>() as CapsuleCollider;
			if (capsuleCollider == null || !capsuleCollider.enabled || capsuleCollider.isTrigger)
			{
				return false;
			}
			if ((base.IncludedLayers.value & (1 << capsuleCollider.gameObject.layer)) == 0)
			{
				return false;
			}
			if (base.IgnoredTags.Contains(capsuleCollider.gameObject.tag))
			{
				return false;
			}
			return true;
		}

		private TreeData CollectTreeData(Terrain aTerrain, int aTreeIndex)
		{
			TreeInstance instance = aTerrain.terrainData.treeInstances[aTreeIndex];
			TreePrototype treePrototype = aTerrain.terrainData.treePrototypes[instance.prototypeIndex];
			CapsuleCollider capsuleCollider = treePrototype.prefab.GetComponent<Collider>() as CapsuleCollider;
			Vector3 position = new Vector4(instance.position.x * (float)(aTerrain.terrainData.heightmapWidth - 1) * aTerrain.terrainData.heightmapScale.x, instance.position.y * aTerrain.terrainData.heightmapScale.y, instance.position.z * (float)(aTerrain.terrainData.heightmapHeight - 1) * aTerrain.terrainData.heightmapScale.z);
			position = aTerrain.transform.TransformPoint(position);
			Matrix4x4 identity = Matrix4x4.identity;
			identity.SetColumn(3, new Vector4(position.x, position.y, position.z, 1f));
			Bounds treeBounds = new Bounds(position + new Vector3(0f, capsuleCollider.height * instance.heightScale / 2f, 0f), new Vector3(capsuleCollider.radius * 2f * instance.widthScale, capsuleCollider.height * instance.heightScale, capsuleCollider.radius * 2f * instance.widthScale));
			TreeData treeData = new TreeData();
			treeData.Instance = instance;
			treeData.Prototype = treePrototype;
			treeData.Transform = identity;
			treeData.TreeBounds = treeBounds;
			return treeData;
		}

		private ColliderMesh ConvertTreeData(TreeData aData)
		{
			CapsuleCollider capsuleCollider = aData.Prototype.prefab.GetComponent<Collider>() as CapsuleCollider;
			float x = ((capsuleCollider.direction == 2) ? 90 : 0);
			float z = ((capsuleCollider.direction == 0) ? 90 : 0);
			float num = capsuleCollider.height * aData.Instance.heightScale;
			Vector3 pos = capsuleCollider.center + new Vector3(0f, (num - capsuleCollider.height) / 2f, 0f);
			Matrix4x4 transform = aData.Transform;
			transform *= Matrix4x4.TRS(pos, Quaternion.Euler(x, 0f, z), Vector3.one);
			float aRadius = capsuleCollider.radius * aData.Instance.widthScale;
			return ColliderMesh.CreateCapsule(capsuleCollider, transform, aRadius, num);
		}
	}
}
