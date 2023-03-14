using System.Collections.Generic;
using RAIN.Entities.Aspects;

namespace RAIN.Perception.Sensors
{
	public class SensorManager
	{
		private static SensorManager _instance = new SensorManager();

		private Dictionary<string, List<RAINAspect>> _aspects = new Dictionary<string, List<RAINAspect>>();

		public static SensorManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new SensorManager();
				}
				return _instance;
			}
		}

		private SensorManager()
		{
		}

		public void Register(RAINAspect aAspect, string aAspectType)
		{
			if (aAspect != null)
			{
				if (!_aspects.ContainsKey(aAspectType))
				{
					_aspects.Add(aAspectType, new List<RAINAspect>());
				}
				List<RAINAspect> list = _aspects[aAspectType];
				if (!list.Contains(aAspect))
				{
					list.Add(aAspect);
				}
			}
		}

		public void Unregister(RAINAspect aAspect, string aAspectType)
		{
			if (_aspects.ContainsKey(aAspectType))
			{
				_aspects[aAspectType].Remove(aAspect);
			}
		}

		public IList<RAINAspect> GetAspects(string aAspectType)
		{
			if (!_aspects.ContainsKey(aAspectType))
			{
				return new List<RAINAspect>().AsReadOnly();
			}
			return _aspects[aAspectType].AsReadOnly();
		}
	}
}
