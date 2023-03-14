using System;
using System.Collections.Generic;
using System.Text;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Memory
{
	[RAINSerializableClass]
	public class BasicMemory : RAINMemory
	{
		[RAINSerializableClass]
		public class MemoryItem
		{
			[RAINSerializableField]
			public string Key;

			[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced)]
			public Type ValueType;

			[RAINSerializableField]
			public object Value;
		}

		[RAINSerializableField(Visibility = FieldVisibility.Hide)]
		private List<MemoryItem> _memoryItems = new List<MemoryItem>();

		private Dictionary<string, MemoryItem> _memoryDictionary;

		private RAINMemory _nestedMemory;

		public virtual RAINMemory NestedMemory
		{
			get
			{
				return _nestedMemory;
			}
		}

		public BasicMemory()
		{
		}

		public BasicMemory(RAINMemory aNestedMemory)
			: base(aNestedMemory)
		{
			_nestedMemory = aNestedMemory;
		}

		public override void AIInit()
		{
			base.AIInit();
			_memoryDictionary = new Dictionary<string, MemoryItem>();
			for (int i = 0; i < _memoryItems.Count; i++)
			{
				_memoryDictionary[_memoryItems[i].Key] = _memoryItems[i];
			}
		}

		public override List<string> GetItems()
		{
			if (Initialized)
			{
				List<string> list = new List<string>(_memoryDictionary.Keys);
				if (_nestedMemory == null)
				{
					return list;
				}
				List<string> items = _nestedMemory.GetItems();
				for (int i = 0; i < items.Count; i++)
				{
					if (!_memoryDictionary.ContainsKey(items[i]))
					{
						list.Add(items[i]);
					}
				}
				return list;
			}
			List<string> list2 = new List<string>();
			for (int j = 0; j < _memoryItems.Count; j++)
			{
				list2.Add(_memoryItems[j].Key);
			}
			if (_nestedMemory == null)
			{
				return list2;
			}
			List<string> items2 = _nestedMemory.GetItems();
			for (int k = 0; k < items2.Count; k++)
			{
				if (!list2.Contains(items2[k]))
				{
					list2.Add(items2[k]);
				}
			}
			return list2;
		}

		public override object GetItem(string aItemName)
		{
			if (Initialized)
			{
				MemoryItem value;
				if (_memoryDictionary.TryGetValue(aItemName, out value))
				{
					return value.Value;
				}
				if (_nestedMemory != null)
				{
					return _nestedMemory.GetItem(aItemName);
				}
				return null;
			}
			for (int i = 0; i < _memoryItems.Count; i++)
			{
				if (_memoryItems[i].Key == aItemName)
				{
					return _memoryItems[i].Value;
				}
			}
			if (_nestedMemory != null)
			{
				return _nestedMemory.GetItem(aItemName);
			}
			return null;
		}

		public override Type GetItemType(string aItemName)
		{
			if (Initialized)
			{
				MemoryItem value;
				if (_memoryDictionary.TryGetValue(aItemName, out value))
				{
					return value.ValueType;
				}
				if (_nestedMemory != null)
				{
					return _nestedMemory.GetItemType(aItemName);
				}
				return null;
			}
			for (int i = 0; i < _memoryItems.Count; i++)
			{
				if (_memoryItems[i].Key == aItemName)
				{
					return _memoryItems[i].ValueType;
				}
			}
			if (_nestedMemory != null)
			{
				return _nestedMemory.GetItemType(aItemName);
			}
			return null;
		}

		public override void SetItem(string aItemName, object aValue, Type aType)
		{
			if (Initialized)
			{
				MemoryItem value;
				if (_memoryDictionary.TryGetValue(aItemName, out value))
				{
					value.Value = aValue;
					value.ValueType = aType;
					return;
				}
				MemoryItem memoryItem = new MemoryItem();
				memoryItem.Key = aItemName;
				memoryItem.Value = aValue;
				memoryItem.ValueType = aType;
				value = memoryItem;
				_memoryDictionary[aItemName] = value;
				_memoryItems.Add(value);
				return;
			}
			for (int i = 0; i < _memoryItems.Count; i++)
			{
				if (_memoryItems[i].Key == aItemName)
				{
					_memoryItems[i].Value = aValue;
					_memoryItems[i].ValueType = aType;
					return;
				}
			}
			_memoryItems.Add(new MemoryItem
			{
				Key = aItemName,
				Value = aValue,
				ValueType = aType
			});
		}

		public override void RemoveItem(string aItemName)
		{
			if (Initialized && !_memoryDictionary.Remove(aItemName))
			{
				return;
			}
			for (int i = 0; i < _memoryItems.Count; i++)
			{
				if (_memoryItems[i].Key == aItemName)
				{
					_memoryItems.RemoveAt(i);
					break;
				}
			}
		}

		public override bool ItemExists(string aItemName)
		{
			if (Initialized)
			{
				return _memoryDictionary.ContainsKey(aItemName);
			}
			for (int i = 0; i < _memoryItems.Count; i++)
			{
				if (_memoryItems[i].Key == aItemName)
				{
					return true;
				}
			}
			if (_nestedMemory != null)
			{
				return _nestedMemory.ItemExists(aItemName);
			}
			return false;
		}

		public override bool RenameItem(string aFromItemName, string aToItemName)
		{
			if (Initialized)
			{
				MemoryItem value;
				if (!_memoryDictionary.TryGetValue(aFromItemName, out value) || _memoryDictionary.ContainsKey(aToItemName))
				{
					return false;
				}
				_memoryDictionary[aToItemName] = value;
				_memoryDictionary.Remove(aFromItemName);
				value.Key = aToItemName;
				return true;
			}
			int num = -1;
			int num2 = -1;
			for (int i = 0; i < _memoryItems.Count; i++)
			{
				if (_memoryItems[i].Key == aFromItemName)
				{
					num = i;
				}
				if (_memoryItems[i].Key == aToItemName)
				{
					num2 = i;
				}
			}
			if (num < 0 || num2 >= 0)
			{
				return false;
			}
			_memoryItems[num].Key = aToItemName;
			return true;
		}

		public override void Clear()
		{
			if (Initialized)
			{
				_memoryDictionary.Clear();
			}
			_memoryItems.Clear();
		}

		public override void Commit()
		{
			if (_nestedMemory != null)
			{
				for (int i = 0; i < _memoryItems.Count; i++)
				{
					_nestedMemory.SetItem(_memoryItems[i].Key, _memoryItems[i].Value, _memoryItems[i].ValueType);
				}
				Clear();
			}
		}

		public override byte[] Save()
		{
			try
			{
				FieldSerializer fieldSerializer = new FieldSerializer();
				fieldSerializer.SerializeRAINObject(this);
				return Encoding.UTF8.GetBytes(fieldSerializer.SerializedData);
			}
			catch
			{
			}
			return null;
		}

		public override void Load(byte[] aSavedMemory, bool aClearMemoryFirst = true)
		{
			if (aClearMemoryFirst)
			{
				Clear();
			}
			try
			{
				if (aSavedMemory != null && aSavedMemory.Length != 0)
				{
					BasicMemory basicMemory = new BasicMemory();
					FieldSerializer fieldSerializer = new FieldSerializer(Encoding.UTF8.GetString(aSavedMemory, 0, aSavedMemory.Length), new List<UnityEngine.Object>(), new List<FieldSerializer.CustomSerializedData>());
					fieldSerializer.DeserializeRAINObject(basicMemory);
					for (int i = 0; i < basicMemory._memoryItems.Count; i++)
					{
						SetItem(basicMemory._memoryItems[i].Key, basicMemory._memoryItems[i].Value, basicMemory._memoryItems[i].ValueType);
					}
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}
	}
}
