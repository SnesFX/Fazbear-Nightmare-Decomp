using System.Collections.Generic;
using RAIN.Entities.Aspects;
using RAIN.Perception.Sensors;
using RAIN.Serialization;

namespace RAIN.Perception
{
	[RAINSerializableClass]
	public class BasicSenses : RAINSenses
	{
		[RAINSerializableField(Visibility = FieldVisibility.Hide)]
		private List<RAINSensor> _sensors = new List<RAINSensor>();

		public override IList<RAINSensor> Sensors
		{
			get
			{
				return _sensors.AsReadOnly();
			}
		}

		public override void AddSensor(RAINSensor aSensor)
		{
			if (aSensor != null && !_sensors.Contains(aSensor))
			{
				_sensors.Add(aSensor);
				if (Initialized)
				{
					aSensor.AIInit(AI);
					aSensor.BodyInit();
				}
			}
		}

		public override void RemoveSensor(RAINSensor aSensor)
		{
			if (aSensor != null)
			{
				_sensors.Remove(aSensor);
			}
		}

		public override void RemoveSensor(string aSensorName)
		{
			for (int num = _sensors.Count - 1; num >= 0; num--)
			{
				if (_sensors[num].SensorName == aSensorName)
				{
					RemoveSensor(_sensors[num]);
				}
			}
		}

		public override void RemoveSensor(int aIndex)
		{
			_sensors.RemoveAt(aIndex);
		}

		public override void EnableSensor(RAINSensor aSensor)
		{
			if (aSensor != null)
			{
				aSensor.IsActive = true;
			}
		}

		public override void EnableSensor(string aSensorName)
		{
			for (int i = 0; i < _sensors.Count; i++)
			{
				if (_sensors[i].SensorName == aSensorName)
				{
					EnableSensor(_sensors[i]);
				}
			}
		}

		public override void DisableSensor(RAINSensor aSensor)
		{
			if (aSensor != null)
			{
				aSensor.IsActive = false;
			}
		}

		public override void DisableSensor(string aSensorName)
		{
			for (int num = _sensors.Count - 1; num >= 0; num--)
			{
				if (_sensors[num].SensorName == aSensorName)
				{
					DisableSensor(_sensors[num]);
				}
			}
		}

		public override void UpdateSenses()
		{
		}

		public override RAINSensor GetSensor(string aSensorName)
		{
			foreach (RAINSensor sensor in _sensors)
			{
				if (sensor.SensorName == aSensorName)
				{
					return sensor;
				}
			}
			return null;
		}

		public override List<RAINSensor> GetSensors(string aSensorName, bool aEmptyMatchesAll = true)
		{
			bool flag = aEmptyMatchesAll && string.IsNullOrEmpty(aSensorName);
			List<RAINSensor> list = new List<RAINSensor>();
			foreach (RAINSensor sensor in _sensors)
			{
				if (flag || sensor.SensorName == aSensorName)
				{
					list.Add(sensor);
				}
			}
			return list;
		}

		public override IList<RAINAspect> Match(string aSensorName, string aAspectName)
		{
			return Match(GetSensors(aSensorName), aAspectName);
		}

		public override IList<RAINAspect> Match(List<RAINSensor> aSensors, string aAspectName)
		{
			if (aSensors == null)
			{
				return new List<RAINAspect>().AsReadOnly();
			}
			if (aSensors.Count == 1)
			{
				return Match(aSensors[0], aAspectName);
			}
			List<RAINAspect> list = new List<RAINAspect>();
			foreach (RAINSensor aSensor in aSensors)
			{
				IList<RAINAspect> list2 = Match(aSensor, aAspectName);
				if (list2.Count <= 0)
				{
					continue;
				}
				foreach (RAINAspect item in list2)
				{
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
			}
			return list.AsReadOnly();
		}

		public override IList<RAINAspect> Match(RAINSensor aSensor, string aAspectName)
		{
			IList<RAINAspect> list = null;
			if (aSensor != null && aSensor.IsActive)
			{
				aSensor.MatchAspectName(aAspectName);
				return aSensor.Matches;
			}
			return new List<RAINAspect>().AsReadOnly();
		}

		public override IList<RAINAspect> Match(string aSensorName, RAINAspect aAspect)
		{
			return Match(GetSensors(aSensorName), aAspect);
		}

		public override IList<RAINAspect> Match(List<RAINSensor> aSensors, RAINAspect aAspect)
		{
			if (aSensors == null)
			{
				return new List<RAINAspect>().AsReadOnly();
			}
			if (aSensors.Count == 1)
			{
				return Match(aSensors[0], aAspect);
			}
			List<RAINAspect> list = new List<RAINAspect>();
			foreach (RAINSensor aSensor in aSensors)
			{
				IList<RAINAspect> list2 = Match(aSensor, aAspect);
				if (list2.Count <= 0)
				{
					continue;
				}
				foreach (RAINAspect item in list2)
				{
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
			}
			return list.AsReadOnly();
		}

		public override IList<RAINAspect> Match(RAINSensor aSensor, RAINAspect aAspect)
		{
			IList<RAINAspect> list = null;
			if (aSensor != null && aSensor.IsActive)
			{
				aSensor.MatchAspect(aAspect);
				return aSensor.Matches;
			}
			return new List<RAINAspect>().AsReadOnly();
		}

		public override IList<RAINAspect> MatchAll()
		{
			List<RAINSensor> sensors = GetSensors(null);
			List<RAINAspect> list = new List<RAINAspect>();
			foreach (RAINSensor item in sensors)
			{
				if (item == null || !item.IsActive)
				{
					continue;
				}
				item.MatchAll();
				IList<RAINAspect> matches = item.Matches;
				if (matches.Count <= 0)
				{
					continue;
				}
				foreach (RAINAspect item2 in matches)
				{
					if (!list.Contains(item2))
					{
						list.Add(item2);
					}
				}
			}
			return list.AsReadOnly();
		}
	}
}
