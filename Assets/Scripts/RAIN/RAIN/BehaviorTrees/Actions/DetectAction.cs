using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities.Aspects;
using RAIN.Perception.Sensors;
using RAIN.Representation;
using UnityEngine;

namespace RAIN.BehaviorTrees.Actions
{
	public class DetectAction : RAINAction
	{
		public const string ACTIONNAME = "detect";

		public Expression sensor;

		public Expression aspect;

		public string entityObjectVariable;

		public string aspectVariable;

		public string aspectObjectVariable;

		private List<RAINSensor> sensorsToUse = new List<RAINSensor>();

		public override ActionResult Execute(AI ai)
		{
			sensorsToUse.Clear();
			List<RAINSensor> list = sensorsToUse;
			if (sensor != null && sensor.IsValid)
			{
				object obj = sensor.Evaluate<object>(ai.DeltaTime, ai.WorkingMemory);
				if (obj is List<RAINSensor>)
				{
					list = (List<RAINSensor>)obj;
				}
				else if (obj is RAINSensor)
				{
					list.Add((RAINSensor)obj);
				}
				else
				{
					string text = obj as string;
					if (text != null)
					{
						list = ai.Senses.GetSensors(text);
					}
				}
			}
			else
			{
				list = ai.Senses.GetSensors(null);
			}
			string text2 = null;
			RAINAspect rAINAspect = null;
			if (aspect != null && aspect.IsValid)
			{
				object obj2 = aspect.Evaluate<object>(ai.DeltaTime, ai.WorkingMemory);
				if (obj2 is RAINAspect)
				{
					rAINAspect = (RAINAspect)obj2;
				}
				else
				{
					text2 = obj2 as string;
				}
			}
			IList<RAINAspect> list2 = null;
			if (rAINAspect != null)
			{
				list2 = ai.Senses.Match(list, rAINAspect);
			}
			else
			{
				if (text2 == null)
				{
					return ActionResult.FAILURE;
				}
				list2 = ai.Senses.Match(list, text2);
			}
			RAINAspect rAINAspect2 = null;
			foreach (RAINAspect item in list2)
			{
				if (item != null)
				{
					rAINAspect2 = item;
					break;
				}
			}
			if (rAINAspect2 == null)
			{
				if (!string.IsNullOrEmpty(entityObjectVariable))
				{
					ai.WorkingMemory.SetItem<GameObject>(entityObjectVariable, null);
				}
				if (!string.IsNullOrEmpty(aspectVariable))
				{
					ai.WorkingMemory.SetItem<RAINAspect>(aspectVariable, null);
				}
				if (!string.IsNullOrEmpty(aspectObjectVariable))
				{
					ai.WorkingMemory.SetItem<GameObject>(aspectObjectVariable, null);
				}
				return ActionResult.FAILURE;
			}
			if (!string.IsNullOrEmpty(entityObjectVariable))
			{
				ai.WorkingMemory.SetItem(entityObjectVariable, rAINAspect2.Entity.Form);
			}
			if (!string.IsNullOrEmpty(aspectVariable))
			{
				ai.WorkingMemory.SetItem(aspectVariable, rAINAspect2);
			}
			if (!string.IsNullOrEmpty(aspectObjectVariable))
			{
				if (rAINAspect2.MountPoint != null)
				{
					ai.WorkingMemory.SetItem(aspectObjectVariable, rAINAspect2.MountPoint.gameObject);
				}
				else
				{
					ai.WorkingMemory.SetItem(aspectObjectVariable, rAINAspect2.Entity.Form);
				}
			}
			return ActionResult.SUCCESS;
		}
	}
}
