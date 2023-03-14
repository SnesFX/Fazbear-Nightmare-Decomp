using RAIN.Core;
using RAIN.Serialization;

namespace RAIN.Entities.Aspects
{
	[RAINElement("Visual Aspect")]
	[RAINSerializableClass]
	public class VisualAspect : RAINAspect
	{
		public const string cnstAspectType = "visual";

		public override string AspectType
		{
			get
			{
				return "visual";
			}
		}

		public VisualAspect()
		{
		}

		public VisualAspect(string aAspectName)
			: base(aAspectName)
		{
		}
	}
}
