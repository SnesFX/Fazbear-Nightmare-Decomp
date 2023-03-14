using RAIN.Core;
using RAIN.Serialization;

namespace RAIN.Minds
{
	[RAINSerializableClass]
	public abstract class RAINMind : RAINAIElement
	{
		public abstract void Think();
	}
}
