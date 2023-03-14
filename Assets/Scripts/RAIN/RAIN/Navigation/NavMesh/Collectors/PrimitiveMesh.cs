using System;
using System.Collections.Generic;
using RAIN.Navigation.NavMesh.RecastNodes;
using UnityEngine;

namespace RAIN.Navigation.NavMesh.Collectors
{
	public class PrimitiveMesh : SpanMesh
	{
		private float _cellSize;

		private Point3 _origin;

		private int _width;

		private int _length;

		private Vector3[] _vertices;

		private List<int> _triangles = new List<int>();

		private List<bool> _walkables = new List<bool>();

		private List<ushort> _volumeIDs = new List<ushort>();

		private Vector3[] _polyListXTemp = new Vector3[16];

		private Vector3[] _polyListXFinal = new Vector3[16];

		private Vector3[] _polyListZTemp = new Vector3[16];

		private Vector3[] _polyListZFinal = new Vector3[16];

		private float[] _polyDistances = new float[16];

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

		public PrimitiveMesh(ColliderMesh aMesh, float aCellSize, List<string> aUnwalkableTags, float aMaxSlope)
		{
			_cellSize = aCellSize;
			_origin = Point3.RoundToPoint(aMesh.UnityMesh.bounds.min / aCellSize);
			_width = Mathf.RoundToInt(aMesh.UnityMesh.bounds.size.x / aCellSize) + 1;
			_length = Mathf.RoundToInt(aMesh.UnityMesh.bounds.size.z / aCellSize) + 1;
			_vertices = aMesh.UnityMesh.vertices;
			float aMaxSlopeCos = Mathf.Cos(aMaxSlope * (float)Math.PI / 180f);
			for (int i = 0; i < aMesh.UnityMesh.subMeshCount; i++)
			{
				int count = _triangles.Count;
				_triangles.AddRange(aMesh.UnityMesh.GetTriangles(i));
				if (aUnwalkableTags.Contains(aMesh.Tag[i]))
				{
					_walkables.AddRange(new bool[(_triangles.Count - count) / 3]);
				}
				else
				{
					for (int j = count; j < _triangles.Count; j += 3)
					{
						_walkables.Add(CalculateWalk(j, aMaxSlopeCos));
					}
				}
				ushort item = aMesh.VolumeID[i];
				for (int k = count; k < _triangles.Count; k += 3)
				{
					_volumeIDs.Add(item);
				}
			}
		}

		public override List<Span>[] GetAllSpans(int aStartX, int aStartZ, int aEndX, int aEndZ)
		{
			List<Span>[] array = new List<Span>[(aEndX - aStartX) * (aEndZ - aStartZ)];
			Vector3[] array2 = new Vector3[3];
			for (int i = 0; i < _triangles.Count; i += 3)
			{
				array2[0] = _vertices[_triangles[i]];
				array2[1] = _vertices[_triangles[i + 1]];
				array2[2] = _vertices[_triangles[i + 2]];
				RasterizeTriangle(array, aStartX, aStartZ, aEndX, aEndZ, array2, _walkables[i / 3], _volumeIDs[i / 3]);
			}
			return array;
		}

		private bool CalculateWalk(int aTriangleIndex, float aMaxSlopeCos)
		{
			Vector3 lhs = _vertices[_triangles[aTriangleIndex + 1]] - _vertices[_triangles[aTriangleIndex]];
			Vector3 rhs = _vertices[_triangles[aTriangleIndex + 2]] - _vertices[_triangles[aTriangleIndex]];
			return Vector3.Cross(lhs, rhs).normalized.y >= aMaxSlopeCos;
		}

		private void RasterizeTriangle(List<Span>[] aSpans, int aStartX, int aStartZ, int aEndX, int aEndZ, Vector3[] aTriangle, bool aWalkable, ushort aVolumeID)
		{
			int value = Mathf.RoundToInt(Mathf.Min(aTriangle[0].x, aTriangle[1].x, aTriangle[2].x) / _cellSize) - 1;
			int value2 = Mathf.RoundToInt(Mathf.Max(aTriangle[0].x, aTriangle[1].x, aTriangle[2].x) / _cellSize) + 1;
			int value3 = Mathf.RoundToInt(Mathf.Min(aTriangle[0].z, aTriangle[1].z, aTriangle[2].z) / _cellSize) - 1;
			int value4 = Mathf.RoundToInt(Mathf.Max(aTriangle[0].z, aTriangle[1].z, aTriangle[2].z) / _cellSize) + 1;
			value = Mathf.Clamp(value, Origin.X + aStartX, Origin.X + aEndX);
			value2 = Mathf.Clamp(value2, Origin.X + aStartX, Origin.X + aEndX);
			value3 = Mathf.Clamp(value3, Origin.Z + aStartZ, Origin.Z + aEndZ);
			value4 = Mathf.Clamp(value4, Origin.Z + aStartZ, Origin.Z + aEndZ);
			if (value == value2 || value3 == value4)
			{
				return;
			}
			for (int i = value; i < value2; i++)
			{
				int num = ClipPolyToPlane(1f, 0f, (float)(-i) * _cellSize, aTriangle, 3, ref _polyListXTemp);
				if (num < 3)
				{
					continue;
				}
				num = ClipPolyToPlane(-1f, 0f, (float)(i + 1) * _cellSize, _polyListXTemp, num, ref _polyListXFinal);
				if (num < 3)
				{
					continue;
				}
				for (int j = value3; j < value4; j++)
				{
					int num2 = ClipPolyToPlane(0f, 1f, (float)(-j) * _cellSize, _polyListXFinal, num, ref _polyListZTemp);
					if (num2 < 3)
					{
						continue;
					}
					num2 = ClipPolyToPlane(0f, -1f, (float)(j + 1) * _cellSize, _polyListZTemp, num2, ref _polyListZFinal);
					if (num2 >= 3)
					{
						float num3 = _polyListZFinal[0].y;
						float num4 = _polyListZFinal[0].y;
						for (int k = 1; k < num2; k++)
						{
							num3 = Math.Min(num3, _polyListZFinal[k].y);
							num4 = Math.Max(num4, _polyListZFinal[k].y);
						}
						int num5 = (j - (Origin.Z + aStartZ)) * (aEndX - aStartX) + (i - (Origin.X + aStartX));
						if (aSpans[num5] == null)
						{
							aSpans[num5] = new List<Span>();
						}
						aSpans[num5].Add(new Span(i, j, Mathf.RoundToInt(num3 / _cellSize), Mathf.RoundToInt(num4 / _cellSize), aWalkable, aVolumeID));
					}
				}
			}
		}

		private int ClipPolyToPlane(float aPlaneNormalX, float aPlaneNormalZ, float aPlaneNormalDistance, Vector3[] aVerticesIn, int aVerticesInLength, ref Vector3[] aVerticesOut)
		{
			if (_polyDistances.Length < aVerticesInLength)
			{
				Array.Resize(ref _polyDistances, aVerticesInLength * 2);
			}
			for (int i = 0; i < aVerticesInLength; i++)
			{
				_polyDistances[i] = aPlaneNormalX * aVerticesIn[i].x + aPlaneNormalZ * aVerticesIn[i].z + aPlaneNormalDistance;
			}
			if (aVerticesOut.Length < aVerticesInLength * 2)
			{
				Array.Resize(ref aVerticesOut, aVerticesInLength * 2);
			}
			int result = 0;
			int j = 0;
			int num = aVerticesInLength - 1;
			for (; j < aVerticesInLength; j++)
			{
				bool flag = _polyDistances[num] >= 0f;
				bool flag2 = _polyDistances[j] >= 0f;
				if (flag != flag2)
				{
					aVerticesOut[result++] = aVerticesIn[num] + (aVerticesIn[j] - aVerticesIn[num]) * (_polyDistances[num] / (_polyDistances[num] - _polyDistances[j]));
				}
				if (flag2)
				{
					aVerticesOut[result++] = aVerticesIn[j];
				}
				num = j;
			}
			return result;
		}
	}
}
