using RAIN.Serialization;

namespace RAIN.Core
{
	[RAINSerializableClass]
	public abstract class RAINElement
	{
		public virtual void Reset(RAINComponent aComponent)
		{
		}
	}
}
