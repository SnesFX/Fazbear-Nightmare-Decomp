using System;
using System.Collections.Generic;
using System.Reflection;
using RAIN.Core;
using RAIN.Serialization;
using RAIN.Utility;
using UnityEngine;

namespace RAIN.Action
{
	[RAINSerializableClass]
	public abstract class RAINAction
	{
		public enum ActionResult
		{
			SUCCESS = 0,
			RUNNING = 1,
			FAILURE = 2,
			NONE = 3
		}

		protected string actionName = "undefined";

		public virtual string ActionName
		{
			get
			{
				return actionName;
			}
			set
			{
				actionName = value;
			}
		}

		public virtual void Start(AI ai)
		{
		}

		public virtual ActionResult Execute(AI ai)
		{
			return ActionResult.SUCCESS;
		}

		public virtual void Stop(AI ai)
		{
		}

		public static RAINAction LoadActionInstance(string aAssemblyName, string aClassname, Assembly aLoadFromAssembly = null)
		{
			if (aLoadFromAssembly != null)
			{
				return LoadActionInstance(aLoadFromAssembly, aClassname);
			}
			List<Type> allClassSubclassTypes = ReflectionUtils.GetAllClassSubclassTypes(typeof(RAINAction));
			foreach (Type item in allClassSubclassTypes)
			{
				if (item != null && !(item.Name != aClassname))
				{
					string text = item.Assembly.FullName.Split(',')[0];
					if ((!(aAssemblyName == "(global)") || text.StartsWith("Assembly")) && (!(aAssemblyName != "(global)") || !(text != aAssemblyName)))
					{
						return item.Assembly.CreateInstance(item.FullName) as RAINAction;
					}
				}
			}
			Debug.LogError("Unable to load custom action: " + aClassname);
			return null;
		}

		public static RAINAction LoadActionInstance(Assembly aLoadFromAssembly, string aCustomActionName)
		{
			if (aLoadFromAssembly == null)
			{
				return null;
			}
			Type[] array = null;
			try
			{
				array = aLoadFromAssembly.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				string text = "Could not load types from assembly: " + aLoadFromAssembly.FullName + "\n";
				for (int i = 0; i < ex.LoaderExceptions.Length; i++)
				{
					text = text + ex.LoaderExceptions[i].Message + "\n";
				}
				Debug.LogWarning(text);
				array = ex.Types;
			}
			if (array != null)
			{
				Type[] array2 = array;
				foreach (Type type in array2)
				{
					if (type == null || !Attribute.IsDefined(type, typeof(RAINActionAttribute)))
					{
						continue;
					}
					RAINActionAttribute[] array3 = type.GetCustomAttributes(typeof(RAINActionAttribute), true) as RAINActionAttribute[];
					for (int k = 0; k < array3.Length; k++)
					{
						if (array3[k].actionName.Trim() == aCustomActionName)
						{
							return aLoadFromAssembly.CreateInstance(type.FullName) as RAINAction;
						}
					}
				}
				Type[] array4 = array;
				foreach (Type type2 in array4)
				{
					if (type2 != null && (type2.FullName == aCustomActionName || type2.Name == aCustomActionName))
					{
						return aLoadFromAssembly.CreateInstance(type2.FullName) as RAINAction;
					}
				}
			}
			Debug.LogError("Unable to load custom action: " + aCustomActionName);
			return null;
		}

		public static List<Type> ListCustomActions()
		{
			List<Type> list = new List<Type>();
			List<Type> allClassSubclassTypes = ReflectionUtils.GetAllClassSubclassTypes(typeof(RAINAction));
			foreach (Type item in allClassSubclassTypes)
			{
				if (item != null && Attribute.IsDefined(item, typeof(RAINActionAttribute)))
				{
					list.Add(item);
				}
			}
			return list;
		}
	}
}
