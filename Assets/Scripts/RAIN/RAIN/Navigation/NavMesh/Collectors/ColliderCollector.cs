using System.Collections.Generic;
using UnityEngine;

namespace RAIN.Navigation.NavMesh.Collectors
{
	public abstract class ColliderCollector
	{
		private float _cellSize;

		private float _maxSlope;

		private List<string> _unwalkableTags;

		private List<string> _ignoredTags;

		private LayerMask _includedLayers;

		public float CellSize
		{
			get
			{
				return _cellSize;
			}
		}

		public float MaxSlope
		{
			get
			{
				return _maxSlope;
			}
		}

		public List<string> UnwalkableTags
		{
			get
			{
				return _unwalkableTags;
			}
		}

		public List<string> IgnoredTags
		{
			get
			{
				return _ignoredTags;
			}
		}

		public LayerMask IncludedLayers
		{
			get
			{
				return _includedLayers;
			}
		}

		public abstract float Progress { get; }

		public abstract int ColliderCount { get; }

		public ColliderCollector()
		{
		}

		public virtual void InitCollector(float aCellSize, float aMaxSlope, List<string> aUnwalkableTags, List<string> aIgnoredTags, LayerMask aIncludedLayers)
		{
			_cellSize = aCellSize;
			_maxSlope = aMaxSlope;
			_unwalkableTags = aUnwalkableTags;
			_ignoredTags = aIgnoredTags;
			_includedLayers = aIncludedLayers;
		}

		public abstract bool CollectColliders(Bounds aBounds, int aStart, int aCount, out List<SpanMesh> aSpanMeshes);
	}
}
