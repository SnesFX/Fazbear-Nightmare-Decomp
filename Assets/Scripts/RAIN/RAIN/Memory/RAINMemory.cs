using System;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Serialization;
using RAIN.Utility;

namespace RAIN.Memory
{
	[RAINSerializableClass]
	public abstract class RAINMemory : RAINAIElement
	{
		public RAINMemory()
		{
		}

		public RAINMemory(RAINMemory aNestedMemory)
		{
		}

		public abstract List<string> GetItems();

		public T GetItem<T>(string aItemName)
		{
			return TypeConvert.ConvertValue<T>(GetItem(aItemName));
		}

		public abstract object GetItem(string aItemName);

		public abstract Type GetItemType(string aItemName);

		public void SetItem<T>(string aItemName, T aValue)
		{
			SetItem(aItemName, aValue, typeof(T));
		}

		public abstract void SetItem(string aItemName, object aValue, Type aType);

		public abstract void RemoveItem(string aItemName);

		public abstract bool ItemExists(string aItemName);

		public abstract bool RenameItem(string aFromItem, string aToItem);

		public abstract void Clear();

		public abstract void Commit();

		public abstract byte[] Save();

		public abstract void Load(byte[] aSavedMemory, bool aClearMemoryFirst = true);
	}
}
