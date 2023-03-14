using System;
using System.Collections.Generic;
using System.Reflection;
using RAIN.BehaviorTrees;
using RAIN.Serialization;
using RAIN.Utility;
using UnityEngine;

namespace RAIN.Action
{
	[RAINSerializableClass]
	public class RAINDecision : BTNode
	{
		public const string NODETYPE = "decision";

		public static RAINDecision LoadDecisionInstance(string aAssemblyName, string aDecisionName)
		{
			List<Type> allClassSubclassTypes = ReflectionUtils.GetAllClassSubclassTypes(typeof(RAINDecision));
			for (int i = 0; i < allClassSubclassTypes.Count; i++)
			{
				if (!Attribute.IsDefined(allClassSubclassTypes[i], typeof(RAINDecisionAttribute)))
				{
					continue;
				}
				RAINDecisionAttribute rAINDecisionAttribute = (RAINDecisionAttribute)allClassSubclassTypes[i].GetCustomAttributes(typeof(RAINDecisionAttribute), true)[0];
				if (!(allClassSubclassTypes[i].Name != aDecisionName) || !(rAINDecisionAttribute.decisionName != aDecisionName))
				{
					string text = allClassSubclassTypes[i].Assembly.FullName.Split(',')[0];
					if ((!(aAssemblyName == "(global)") || text.StartsWith("Assembly")) && (!(aAssemblyName != "(global)") || !(text != aAssemblyName)))
					{
						return Activator.CreateInstance(allClassSubclassTypes[i]) as RAINDecision;
					}
				}
			}
			Debug.LogError("Unable to load custom decision: " + aDecisionName);
			return null;
		}

		public static RAINDecision LoadDecisionInstance(Assembly aAssembly, string aDecisionName)
		{
			List<Type> allClassSubclassTypes = ReflectionUtils.GetAllClassSubclassTypes(typeof(RAINDecision));
			for (int i = 0; i < allClassSubclassTypes.Count; i++)
			{
				if (allClassSubclassTypes[i].Assembly == aAssembly && Attribute.IsDefined(allClassSubclassTypes[i], typeof(RAINDecisionAttribute)))
				{
					RAINDecisionAttribute rAINDecisionAttribute = (RAINDecisionAttribute)allClassSubclassTypes[i].GetCustomAttributes(typeof(RAINDecisionAttribute), true)[0];
					if (!(allClassSubclassTypes[i].Name != aDecisionName) || !(rAINDecisionAttribute.decisionName != aDecisionName))
					{
						return Activator.CreateInstance(allClassSubclassTypes[i]) as RAINDecision;
					}
				}
			}
			Debug.LogError("Unable to load custom decision: " + aDecisionName);
			return null;
		}
	}
}
