using System.Collections.Generic;
using RAIN.Navigation.NavMesh.RecastNodes;
using UnityEngine;

namespace RAIN.Navigation.NavMesh.Collectors
{
	public class HeightMesh : SpanMesh
	{
		private Point3 _origin;

		private int _width;

		private int _length;

		private int[] _heightField;

		private bool[] _walkable;

		private ushort _volumeID;

		public override Point3 Origin
		{
			get
			{
				return _origin;
			}
		}

		public override int Width
		{
			get
			{
				return _width;
			}
		}

		public override int Length
		{
			get
			{
				return _length;
			}
		}

		public HeightMesh(Point3 aOrigin, Texture2D aDepthInfo, ushort aVolumeID)
		{
			_origin = aOrigin;
			_width = aDepthInfo.width;
			_length = aDepthInfo.height;
			_heightField = new int[aDepthInfo.width * aDepthInfo.height];
			_walkable = new bool[aDepthInfo.width * aDepthInfo.height];
			_volumeID = aVolumeID;
			Color32[] pixels = aDepthInfo.GetPixels32();
			for (int i = 0; i < pixels.Length; i++)
			{
				_heightField[i] = (pixels[i].r << 23) + (pixels[i].g << 15) + (pixels[i].b << 7) + (pixels[i].a >> 1);
				_walkable[i] = (pixels[i].a & 1) > 0;
			}
		}

		public HeightMesh(Point3 aOrigin, int aWidth, int aLength, int[] aHeightField, bool[] aWalkability, string aTag, ushort aVolumeID)
		{
			_origin = aOrigin;
			_width = aWidth;
			_length = aLength;
			_heightField = aHeightField;
			_walkable = aWalkability;
			_volumeID = aVolumeID;
		}

		public override List<Span>[] GetAllSpans(int aStartX, int aStartZ, int aEndX, int aEndZ)
		{
			int num = aEndX - aStartX;
			int num2 = aEndZ - aStartZ;
			List<Span>[] array = new List<Span>[num * num2];
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					int num3 = j * num + i;
					int num4 = (aStartZ + j) * Width + (aStartX + i);
					array[num3] = new List<Span>();
					array[num3].Add(new Span(Origin.X + aStartX + i, Origin.Z + aStartZ + j, Origin.Y, Origin.Y + _heightField[num4], _walkable[num4], _volumeID));
				}
			}
			return array;
		}
	}
}
