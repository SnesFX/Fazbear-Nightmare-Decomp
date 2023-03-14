using System.Collections.Generic;
using RAIN.Core;
using RAIN.Entities.Aspects;
using RAIN.Perception.Sensors;
using RAIN.Serialization;

namespace RAIN.Perception
{
	[RAINSerializableClass]
	public abstract class RAINSenses : RAINAIElement
	{
		public abstract IList<RAINSensor> Sensors { get; }

		public override void AIInit()
		{
			base.AIInit();
			IList<RAINSensor> sensors = Sensors;
			for (int i = 0; i < sensors.Count; i++)
			{
				sensors[i].AIInit(AI);
			}
		}

		public override void BodyInit()
		{
			base.BodyInit();
			IList<RAINSensor> sensors = Sensors;
			for (int i = 0; i < sensors.Count; i++)
			{
				sensors[i].BodyInit();
			}
		}

		public abstract void AddSensor(RAINSensor aSensor);

		public abstract void RemoveSensor(RAINSensor aSensor);

		public abstract void RemoveSensor(string aSensorName);

		public abstract void RemoveSensor(int aIndex);

		public abstract void EnableSensor(RAINSensor aSensor);

		public abstract void EnableSensor(string aSensorName);

		public abstract void DisableSensor(RAINSensor aSensor);

		public abstract void DisableSensor(string aSensorName);

		public abstract void UpdateSenses();

		public abstract RAINSensor GetSensor(string aSensorName);

		public abstract List<RAINSensor> GetSensors(string aSensorName, bool aEmptyMatchesAll = true);

		public abstract IList<RAINAspect> Match(string aSensorName, string aAspectName);

		public abstract IList<RAINAspect> Match(List<RAINSensor> aSensors, string aAspectName);

		public abstract IList<RAINAspect> Match(RAINSensor aSensor, string aAspectName);

		public abstract IList<RAINAspect> Match(string aSensorName, RAINAspect aAspect);

		public abstract IList<RAINAspect> Match(List<RAINSensor> aSensors, RAINAspect aAspect);

		public abstract IList<RAINAspect> Match(RAINSensor aSensor, RAINAspect aAspect);

		public abstract IList<RAINAspect> MatchAll();
	}
}
