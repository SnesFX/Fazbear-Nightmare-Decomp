using System;
using System.Collections.Generic;
using RAIN.Navigation.Graph;

namespace RAIN.Navigation.Pathfinding
{
	public class PathGraphPriorityHeap
	{
		private const int cnstBaseHeapLevels = 10;

		private const int cnstBaseHeapSize = 1024;

		private const int cnstMaxHeapLevels = 10;

		private PathNodeHelper[] _baseHeap = new PathNodeHelper[1024];

		private PathNodeHelper[][] _extendedHeap;

		private int[] _heapLevelSize = new int[10];

		private int _heapcount;

		public int Count
		{
			get
			{
				return _heapcount;
			}
		}

		public PathGraphPriorityHeap()
		{
			_heapcount = 0;
			InitHeap();
		}

		public void Reset()
		{
			_heapcount = 0;
			InitHeap();
		}

		private void InitHeap()
		{
			_extendedHeap = new PathNodeHelper[10][];
			for (int i = 0; i < 10; i++)
			{
				_heapLevelSize[i] = 1 << 10 + i + 1;
				_extendedHeap[i] = null;
			}
		}

		public void HeapCheck()
		{
			Dictionary<NavigationGraphNode, NavigationGraphNode> dictionary = new Dictionary<NavigationGraphNode, NavigationGraphNode>();
			for (int i = 0; i < _heapcount; i++)
			{
				PathNodeHelper item = GetItem(i);
				if (item == null)
				{
					throw new Exception("HeapCheck: Null PathNodeHelper at index " + i);
				}
				if (dictionary.ContainsKey(item.Node))
				{
					throw new Exception("HeapCheck: Duplicate nodes in heap at index " + i);
				}
				if (item.HeapPosition != i)
				{
					throw new Exception("HeapCheck: Helper has wrong heap position (" + item.HeapPosition + ") at index " + i);
				}
				if (i > 0)
				{
					int index = (i - 1) / 2;
					PathNodeHelper item2 = GetItem(index);
					if (item2.CompareTo(item) > 0)
					{
						item2.CompareTo(item);
						throw new Exception("HeapCheck: Parent > child at child index " + i);
					}
				}
			}
		}

		public void Add(PathNodeHelper helper)
		{
			SetItem(_heapcount, helper);
			Raise(helper.HeapPosition);
		}

		public PathNodeHelper Remove(int index)
		{
			PathNodeHelper result = null;
			if (index >= 0 && index < _heapcount)
			{
				result = GetItem(index);
				SetItem(index, GetItem(_heapcount - 1));
				_heapcount--;
				Lower(index);
			}
			return result;
		}

		public void Fix(int index)
		{
			if (index >= 0 && index < _heapcount)
			{
				PathNodeHelper item = GetItem(index);
				Raise(index);
				if (item == GetItem(index))
				{
					Lower(index);
				}
			}
		}

		public int SetItem(int index, PathNodeHelper item)
		{
			if (index >= _heapcount)
			{
				index = _heapcount;
				_heapcount++;
			}
			if (index < 1024)
			{
				_baseHeap[index] = item;
			}
			else
			{
				int num = index - 1024;
				for (int i = 0; i < 10; i++)
				{
					if (_extendedHeap[i] == null)
					{
						_extendedHeap[i] = new PathNodeHelper[_heapLevelSize[i]];
					}
					if (num >= _heapLevelSize[i])
					{
						num -= _heapLevelSize[i];
					}
					else
					{
						_extendedHeap[i][num] = item;
					}
				}
			}
			item.HeapPosition = index;
			return index;
		}

		public PathNodeHelper GetItem(int index)
		{
			if (index < 0 || index >= _heapcount)
			{
				return null;
			}
			if (index < 1024)
			{
				return _baseHeap[index];
			}
			int num = index - 1024;
			for (int i = 0; i < 10; i++)
			{
				if (num >= _heapLevelSize[i])
				{
					num -= _heapLevelSize[i];
					continue;
				}
				if (_extendedHeap[i] == null)
				{
					return null;
				}
				return _extendedHeap[i][num];
			}
			return null;
		}

		private void Raise(int index)
		{
			int num = index;
			PathNodeHelper item = GetItem(num);
			while (num > 0)
			{
				int index2 = (num - 1) / 2;
				PathNodeHelper item2 = GetItem(index2);
				if (item2.CompareTo(item) > 0)
				{
					SetItem(index2, item);
					SetItem(num, item2);
					num = item.HeapPosition;
					continue;
				}
				break;
			}
		}

		private void Lower(int index)
		{
			int num = index;
			PathNodeHelper item = GetItem(num);
			while (num < _heapcount)
			{
				int num2 = num * 2 + 1;
				PathNodeHelper pathNodeHelper = GetItem(num2);
				if (pathNodeHelper == null)
				{
					break;
				}
				PathNodeHelper item2 = GetItem(num2 + 1);
				if (item2 != null && item2.CompareTo(pathNodeHelper) < 0)
				{
					num2++;
					pathNodeHelper = item2;
				}
				if (pathNodeHelper.CompareTo(item) < 0)
				{
					SetItem(num2, item);
					SetItem(num, pathNodeHelper);
					num = item.HeapPosition;
					continue;
				}
				break;
			}
		}
	}
}
