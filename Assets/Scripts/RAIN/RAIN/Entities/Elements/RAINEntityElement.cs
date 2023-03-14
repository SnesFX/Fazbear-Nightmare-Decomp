using RAIN.Core;
using RAIN.Serialization;

namespace RAIN.Entities.Elements
{
	[RAINSerializableClass]
	public abstract class RAINEntityElement : RAINElement
	{
		protected Entity _entity;

		public Entity Entity
		{
			get
			{
				return _entity;
			}
		}

		public void EntityInit(Entity aEntity)
		{
			_entity = aEntity;
			EntityInit();
		}

		public virtual void EntityInit()
		{
		}

		public virtual void FormInit()
		{
		}
	}
}
