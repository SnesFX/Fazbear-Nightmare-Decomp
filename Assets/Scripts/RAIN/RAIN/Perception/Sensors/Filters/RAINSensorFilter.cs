using System.Collections.Generic;
using RAIN.Core;
using RAIN.Entities.Aspects;
using RAIN.Serialization;

namespace RAIN.Perception.Sensors.Filters
{
	[RAINSerializableClass]
	public abstract class RAINSensorFilter : RAINElement
	{
		public RAINSensorFilter()
		{
		}

		public abstract void Filter(RAINSensor aSensor, List<RAINAspect> aValues);
	}
}
