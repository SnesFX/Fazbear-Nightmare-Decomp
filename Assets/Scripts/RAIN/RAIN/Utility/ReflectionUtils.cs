using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RAIN.Utility
{
	public static class ReflectionUtils
	{
		private static Dictionary<Type, List<Type>> _classTypes = new Dictionary<Type, List<Type>>();

		public static bool TypeExists(string aType)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < assemblies.Length; i++)
			{
				Type[] array = null;
				try
				{
					array = assemblies[i].GetTypes();
				}
				catch (ReflectionTypeLoadException ex)
				{
					string text = "Could not load types from assembly: " + assemblies[i].FullName + "\n";
					for (int j = 0; j < ex.LoaderExceptions.Length; j++)
					{
						text = text + ex.LoaderExceptions[j].Message + "\n";
					}
					Debug.LogWarning(text);
					array = ex.Types;
				}
				for (int k = 0; k < array.Length; k++)
				{
					if (array[k].FullName == aType)
					{
						return true;
					}
				}
			}
			return false;
		}

		public static List<Type> GetAllClassSubclassTypes(Type aBaseType)
		{
			List<Type> value;
			if (_classTypes.TryGetValue(aBaseType, out value))
			{
				return value;
			}
			value = new List<Type>();
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < assemblies.Length; i++)
			{
				string text2 = assemblies[i].FullName.Split(',')[0];
				Type[] array = null;
				try
				{
					array = assemblies[i].GetTypes();
				}
				catch (ReflectionTypeLoadException ex)
				{
					string text = "ReflectionUtils: Could not load types from assembly: " + assemblies[i].FullName + "\n";
					for (int j = 0; j < ex.LoaderExceptions.Length; j++)
					{
						text = text + ex.LoaderExceptions[j].Message + "\n";
					}
					Debug.LogWarning(text);
					array = ex.Types;
				}
				for (int k = 0; k < array.Length; k++)
				{
					if (array[k] != null && !array[k].IsAbstract && (array[k] == aBaseType || array[k].IsSubclassOf(aBaseType)))
					{
						value.Add(array[k]);
					}
				}
			}
			_classTypes[aBaseType] = value;
			return value;
		}
	}
}
