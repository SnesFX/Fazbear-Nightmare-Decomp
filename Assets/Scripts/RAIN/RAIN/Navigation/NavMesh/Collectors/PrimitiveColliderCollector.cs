using System.Collections.Generic;
using UnityEngine;

namespace RAIN.Navigation.NavMesh.Collectors
{
	public class PrimitiveColliderCollector : ColliderCollector
	{
		private List<Collider> _primitiveColliders = new List<Collider>();

		private int _currentCollider;

		public List<Collider> PrimitiveColliders
		{
			get
			{
				return _primitiveColliders;
			}
		}

		public override float Progress
		{
			get
			{
				return (float)_currentCollider / (float)_primitiveColliders.Count;
			}
		}

		public override int ColliderCount
		{
			get
			{
				return _primitiveColliders.Count;
			}
		}

		public override void InitCollector(float aCellSize, float aMaxSlope, List<string> aUnwalkableTags, List<string> aIgnoredTags, LayerMask aIncludedLayers)
		{
			base.InitCollector(aCellSize, aMaxSlope, aUnwalkableTags, aIgnoredTags, aIncludedLayers);
			Collider[] array = Object.FindObjectsOfType<Collider>();
			_primitiveColliders.Clear();
			for (int i = 0; i < array.Length; i++)
			{
				if (IsValidNormalCollider(array[i]))
				{
					_primitiveColliders.Add(array[i]);
				}
			}
			_currentCollider = 0;
		}

		public override bool CollectColliders(Bounds aBounds, int aStart, int aCount, out List<SpanMesh> aSpanMeshes)
		{
			_currentCollider = aStart;
			aSpanMeshes = new List<SpanMesh>();
			while (_currentCollider < _primitiveColliders.Count && aCount > 0)
			{
				if (aBounds.Intersects(_primitiveColliders[_currentCollider].bounds))
				{
					using (ColliderMesh colliderMesh = ColliderMesh.ConvertCollider(_primitiveColliders[_currentCollider], _primitiveColliders[_currentCollider].gameObject.transform.localToWorldMatrix))
					{
						if (colliderMesh != null && colliderMesh.ClipToBounds(aBounds))
						{
							aSpanMeshes.Add(new PrimitiveMesh(colliderMesh, base.CellSize, base.UnwalkableTags, base.MaxSlope));
						}
					}
				}
				_currentCollider++;
				aCount--;
			}
			return _currentCollider < ColliderCount;
		}

		private bool IsValidNormalCollider(Collider aCollider)
		{
			if (aCollider == null || aCollider is TerrainCollider || aCollider is WheelCollider)
			{
				return false;
			}
			if (!aCollider.enabled || aCollider.isTrigger)
			{
				return false;
			}
			if ((base.IncludedLayers.value & (1 << aCollider.gameObject.layer)) == 0)
			{
				return false;
			}
			if (base.IgnoredTags.Contains(aCollider.gameObject.tag))
			{
				return false;
			}
			return true;
		}
	}
}
