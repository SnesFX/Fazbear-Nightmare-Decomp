using System;

namespace RAIN.Navigation.NavMesh.RecastNodes
{
	public class Span
	{
		private int _x;

		private int _z;

		private int _minHeight;

		private int _maxHeight;

		private bool _walkable;

		private ushort _volumeID;

		public int X
		{
			get
			{
				return _x;
			}
			set
			{
				_x = value;
			}
		}

		public int Z
		{
			get
			{
				return _z;
			}
			set
			{
				_z = value;
			}
		}

		public int MinHeight
		{
			get
			{
				return _minHeight;
			}
			set
			{
				_minHeight = value;
			}
		}

		public int MaxHeight
		{
			get
			{
				return _maxHeight;
			}
			set
			{
				_maxHeight = value;
			}
		}

		public bool Walkable
		{
			get
			{
				return _walkable;
			}
			set
			{
				_walkable = value;
			}
		}

		public ushort VolumeID
		{
			get
			{
				return _volumeID;
			}
			set
			{
				_volumeID = value;
			}
		}

		public Span()
		{
		}

		public Span(int aX, int aZ, int aMinHeight, int aMaxHeight, bool aWalkable, ushort aVolumeID)
		{
			_x = aX;
			_z = aZ;
			_minHeight = aMinHeight;
			_maxHeight = aMaxHeight;
			_walkable = aWalkable;
			_volumeID = aVolumeID;
		}

		public void Absorb(Span aMergeSpan)
		{
			if (_maxHeight < aMergeSpan._maxHeight)
			{
				_walkable = aMergeSpan._walkable;
			}
			else if (_maxHeight == aMergeSpan._maxHeight)
			{
				_walkable |= aMergeSpan._walkable;
			}
			if (_volumeID == 0)
			{
				_volumeID = aMergeSpan._volumeID;
			}
			_minHeight = Math.Min(_minHeight, aMergeSpan._minHeight);
			_maxHeight = Math.Max(_maxHeight, aMergeSpan._maxHeight);
		}
	}
}
